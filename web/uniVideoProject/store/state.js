const state ={//所有状态以all开头
	alltoast:false,//quanju 提示框 模拟  uni.showtoast
	allLoginInfo:null,//登录成功信息
	allHomeVideoData:null,//首页推荐视频
	allisRelogin:false,//是否断线重连 默认false
	allTryLoginData:{msg:null,num:null},//断线重连前的操作  保存其 数据及其协议 断线重连之后 重复调用该方法
	allChatList:null,//初始聊天列表
	allChatPrice:{anchoridx:null,price:null},//聊天价格
	allChatObjToObjList:null,//1v1聊天列表
	allchatObjInfo:null,//点击聊天  对方的头像 id 等信息
	allNoGiftList:null,//未分类礼物列表
	allFollowStatus:null,//个人页面关注状态 false 未关注 true 关注   视频页面公共
	allBlackStatus:null,//	1-已拉黑 0-未拉黑
	allSexStatus:null,//喜好暂存状态  修改成功 更新 本地存储
	allRoomid:null,//直播间页面 房间id
	allLiveidx:null,//直播间的主播id
	allLiveRoomNum:0,//直播间的人数
	allLiveChatList:[],//直播间聊天消息列表
	allLiveGiftList:[],//直播间聊天
	allLiveRevideoUrl:null,//直播视频回放接口
	allChatPageIsNew:null,//在chat页面使用 判断是否有新消息
	allIsChatpop:false,//是否在私人聊天页
	allIsStartUp:false,//是否在启动页 在 不进行断线重连
	allIsTravalUser:false,//是否为游客  默认不是游客
	allSystemMsg:'',//系统消息


	isload:false,
	alllogin:false,
	id:33333,
	name:'游客'
}
export default state