
  const mutations ={
	set_alltoast:(state, tf) => {
		if(tf){
			state.alltoast = tf;
        }else{
			state.alltoast = !state.alltoast
		}
	},
	set_allLoginInfo:(state, data) => {
		state.allLoginInfo = data
	},
	set_allHomeVideoData:(state, data) => {
		state.allHomeVideoData = data
	},
	set_allisRelogin:(state, data) => {
		state.allisRelogin = data
	},
	set_allTryLoginData:(state, data) => {
		state.allTryLoginData.msg = data.str1;
		state.allTryLoginData.num = data.str2;
	},
	set_allChatList:(state, data) => {
		state.allChatList = data;
	},
	set_allChatPrice:(state, data) => {
		state.allChatPrice = data;
	},
	set_allChatObjToObjList:(state, data) => {
		state.allChatObjToObjList = data;
	},
	set_allchatObjInfo:(state, data) => {
		state.allchatObjInfo = data;
	},
	set_allNoGiftList:(state, data) => {
		state.allNoGiftList = data;
	},
	set_allFollowStatus:(state, data) => {
		state.allFollowStatus = data;
	},
	set_allBlackStatus:(state, data) => {
		state.allBlackStatus = data;
	},
	set_allSexStatus:(state, data) => {
		state.allSexStatus = data;
	},
	set_allRoomid:(state, data) => {
		state.allRoomid = data;
	},
	set_allLiveidx:(state, data) => {
		state.allLiveidx = data;
	},
	set_allLiveRoomNum:(state, data) => {
		state.allLiveRoomNum = data;
	},
	set_allLiveChatList:(state, data) => {
		state.allLiveChatList = data;
	},
	set_allLiveGiftList:(state, data) => {
		state.allLiveGiftList = data;
	},
	set_allLiveRevideoUrl:(state, data) => {
		state.allLiveRevideoUrl = data;
	},
	set_allChatPageIsNew:(state, data) => {
		state.allChatPageIsNew = data;
	},
	set_allIsChatpop:(state, data) => {
		state.allIsChatpop = data;
	},
	set_allIsStartUp:(state, data) => {
		state.allIsStartUp = data;
	},
	set_allIsTravalUser:(state, data) => {
		state.allIsTravalUser = data;
	},
	set_allSystemMsg:(state, data) => {
		state.allSystemMsg = data;
	},
	
	
	
  	set_id:(state, data) => {
  	  state.id = data
  	}

  }
  export default mutations