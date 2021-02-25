import Handle from './modules/handle.js'// 数据解析
 export function webMsg(code,data) {
	 console.log(code)
	 console.log(data)
	 var p1 = new Handle(1)
	 console.log(Handle.do110(p1))
	 // Handle.do110()
 }