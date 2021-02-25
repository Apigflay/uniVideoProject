using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Reflection.Emit;
using Common;

/// <summary>
/// 动态调用C++ DLL 方法类
/// </summary>
public class DllInvoke
{
    //Kernel32.dll  用于内存管理和资源处理的低级别操作系统函数,是Windows 9x/Me中非常重要的32位动态链接库文件，属于内核级文件。
    //User32.dll    用于消息处理、计时器、菜单和通信的 Windows 管理函数。

    //path 传入dll路径 返回结果为0表示失败。
    [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
    public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string path);

    //lib是LoadLibrary返回的句柄，funcName 是函数名称 返回结果为0标识失败。
    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProceName);

    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    /// <summary>
    /// Loadlibrary 返回的函数库模块的句柄
    /// </summary>
    private IntPtr hModule = IntPtr.Zero;

    /// <summary>
    /// GetProcAddress 返回的函数指针
    /// </summary>
    public IntPtr farProc = IntPtr.Zero;

    /// <summary>
    /// 装载Dll, 获得函数指针
    /// </summary>
    /// <param name="lpFileName">DLL路径</param>
    /// <param name="lpProcName">调用函数的名称</param>
    public DllInvoke(string dllPath, string funcName)
    {
        hModule = LoadLibrary(dllPath);

        // 若函数库模块的句柄为空，则抛出异常
        if (hModule == IntPtr.Zero)
            //throw (new Exception("没有找到 :" + lpFileName + ", 函数库模块的句柄为空"));
            LogHelper.WriteLog(LogFile.Warning, "没有找到:" + dllPath + ",函数库模块的句柄为空");

        // 取得函数指针
        farProc = GetProcAddress(hModule, funcName);

        // 若函数指针，则抛出异常
        if (farProc == IntPtr.Zero)
            throw (new Exception(" 没有找到 :" + funcName + " 这个函数的入口点 "));
        //LogHelper.WriteLog(LogFile.Warning, "没有找到:" + lpProcName + ",这个函数的入口点");
    }
    /// <summary>
    /// 释放资源
    /// </summary>
    ~DllInvoke()
    {
        //if (hModule != IntPtr.Zero)
        //{
        FreeLibrary(hModule);
        hModule = IntPtr.Zero;
        farProc = IntPtr.Zero;
        //}
    }

    /// <summary>
    /// 调用所设定的函数
    /// </summary>
    /// <param name="ObjArray_Parameter"> 实参 </param>
    /// <param name="Type_Return"> 返回类型 </param>
    /// <returns> 返回所调用函数的 object</returns>
    public object Invoke(object[] objParams, Type typeReturn)
    {
        // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
        if (hModule == IntPtr.Zero)
            throw (new Exception(" 函数库模块的句柄为空!"));

        if (farProc == IntPtr.Zero)
            throw (new Exception(" 函数指针为空!"));


        //获取参数类型
        Type[] typeParams = new Type[objParams.Length];
        for (int i = 0; i < objParams.Length; ++i)
        {
            typeParams[i] = objParams[i].GetType();
        }
        //farProc()

        // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
        AssemblyName MyAssemblyName = new AssemblyName();
        MyAssemblyName.Name = "DllInvoke_Assembly";

        // 生成单模块配件
        AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");

        // 定义要调用的方法 , 方法名为“ InvokeFun ”，返回类型是“ typeReturn ”参数类型是“ typeParams ”
        MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod("InvokeFun", MethodAttributes.Public | MethodAttributes.Static, typeReturn, typeParams);

        // 获取一个 ILGenerator ，用于发送所需的 IL
        ILGenerator IL = MyMethodBuilder.GetILGenerator();

        for (int j = 0; j < typeParams.Length; j++)
        {
            // 循环将参数依次压入堆栈
            //if (typeParams[j].IsValueType)
            //{
            IL.Emit(OpCodes.Ldarg, j);
            //}
            //else
            //{
            //    IL.Emit(OpCodes.Ldarga, j);
            //}

        }

        if (IntPtr.Size == 4)
        {// 判断处理器类型
            IL.Emit(OpCodes.Ldc_I4, farProc.ToInt32());
        }
        else if (IntPtr.Size == 8)
        {
            IL.Emit(OpCodes.Ldc_I8, farProc.ToInt64());
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, typeReturn, typeParams);
        IL.Emit(OpCodes.Ret); // 返回值

        MyModuleBuilder.CreateGlobalFunctions();

        // 取得方法信息
        MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod("InvokeFun");

        return MyMethodInfo.Invoke(null, objParams);// 调用方法，并返回其值
    }

}

