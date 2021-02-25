import {Get,Post,Get1,Post1,Delete1} from "./GlobalFunction.js"  //post1为header带参数 aid
// 通过密码获取流地址
export const getPlayBackLiveUrl = (p) => Post(p,'UserInfo/getPlayBackLiveUrl')


// 新增收获地址  编辑收获地址 aid : 111
export const addAdress = (p,aid) => Post1(p,aid,'/member/api/memberInternal/editAddress')
// 删除收获地址  112
export const deleteAdress = (p,aid,url) => Delete1(p,aid,url)





