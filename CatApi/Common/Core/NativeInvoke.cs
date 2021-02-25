using Common;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

/// <summary>
/// 动态调用C++ 非托管DLL 方法类
/// </summary>
public class NativeInvoke
{
    /// <summary>
    /// 原型是 :HMODULE LoadLibrary(LPCTSTR lpFileName);
    /// </summary>
    /// <param name="lpFileName">DLL文件名</param>
    /// <returns>函数库模块句柄</returns>
    [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
    public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

    /// <summary>
    /// 原型是 : FARPROC GetProcAddress(HMODULE hModule, LPCWSTR lpProcName);
    /// </summary>
    /// <param name="hModule">包含需调用函数的函数库模块的句柄</param>
    /// <param name="funcName">调用函数名称</param>
    /// <returns>函数指针</returns>
    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string funcName);

    /// <summary>
    /// 原型是 : BOOL FreeLibrary(HMODULE hModule);
    /// </summary>
    /// <param name="hModule">需释放函数库的句柄</param>
    /// <returns>是否已释放指定的DLL</returns>
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

    // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
    private static AssemblyName MyAssemblyName = null;
    //private static AssemblyBuilder MyAssemblyBuilder = null;
    //private static ModuleBuilder MyModuleBuilder = null;

    /// <summary>
    /// 装载Dll, 获得函数指针
    /// </summary>
    /// <param name="lpFileName">DLL 文件名</param>
    public NativeInvoke(string dllPath)
    {
        hModule = LoadLibrary(dllPath);//加载非托管dll的加载

        // 若函数库模块的句柄为空，则抛出异常
        if (hModule == IntPtr.Zero)
            //throw (new Exception("没有找到 :" + lpFileName + ", 函数库模块的句柄为空"));
            LogHelper.WriteLog(LogFile.Warning, "没有找到:" + dllPath + ",函数库模块的句柄为空");
    }

    /// <summary>
    /// 调用所设定的函数
    /// </summary>
    /// <param name="ObjArray_Parameter"> 实参 </param>
    /// <param name="Type_Return"> 返回类型 </param>
    /// <returns> 返回所调用函数的 object</returns>
    public object Invoke(string funcName, object[] objParams, Type typeReturn)
    {

        // 取得函数指针

        farProc = GetProcAddress(hModule, funcName);

        // 下面3个if 是进行安全检查 , 若不能通过 , 则抛出异常

        // 若函数指针，则抛出异常
        if (farProc == IntPtr.Zero)
            //throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
            LogHelper.WriteLog(LogFile.Warning, "没有找到:" + funcName + ",这个函数的入口点");

        if (hModule == IntPtr.Zero)
            throw (new Exception(" 函数库模块的句柄为空!"));

        //获取参数类型
        Type[] typeParams = new Type[objParams.Length];
        for (int i = 0; i < objParams.Length; ++i)
        {
            typeParams[i] = objParams[i].GetType();
        }
        //farProc()
        if (MyAssemblyName == null)
        {
            MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "DllInvoke_Assembly";

        }
        // 生成单模块配件
        //if (MyAssemblyBuilder == null)
        //{
        AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
        //}
        //if (MyModuleBuilder == null)
        //{
        ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");
        //}

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

    /// <summary>
    /// 释放资源 按位取反运算符(~)
    /// </summary>
    ~NativeInvoke()
    {
        //if (hModule != IntPtr.Zero)
        //{
        FreeLibrary(hModule);
        hModule = IntPtr.Zero;
        farProc = IntPtr.Zero;
        //}
    }
}
