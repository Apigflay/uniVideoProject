using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CacheKeys
    {

        /// <summary>
        /// �ǵø����˼�ֵ
        /// </summary>
        private const string BASE_KEY = "LoWo_";

        /// <summary>
        /// ���ĵ�½�û�token        
        /// </summary>
        public const string MiaoPai_USER_TOKEN = BASE_KEY+"Web_Login_UserToken_";

        /// <summary>
        /// ��ȡ�û���Ϣtoken
        /// </summary>
        public const string LIVE_USER_TOKEN = BASE_KEY + "Token_";

        /// <summary>
        /// ��̬��Կ
        /// </summary>
        public const string LIVE_SECRET_KEY = BASE_KEY + "Live_web_token";

        /// <summary>
        /// ��������½��Ϣ�·����� �����useridx
        /// </summary>
        public const string LIVE_Third_TOKEN = BASE_KEY + "Live_Third_";

        /// <summary>
        /// �ҵķ�˿�б�
        /// </summary>
        public const string LIVE_GET_MyFans_List_Key = BASE_KEY + "Live_GetMyFans_List_";

        // <summary>
        // �û���Ϣ
        // </summary>
        public const string LIVE_PHONE_LIVE_USER_INFO_KEY = BASE_KEY + "live_get_LiveUser_info_";
        public const string LIVE_PHONE_USER_INFO_KEY = BASE_KEY + "live_Get_User_info_";
        /// <summary>
        /// �ֻ��󶨲���
        /// </summary>
        //public const string LIVE_PHONE_BIND_STEP = "live_mobile_phone_bind_step";
        /// <summary>
        /// �ֻ���¼ ip ������
        /// </summary>
        //public const string LIVE_PHONE_LOGIN_IP_ERROR_NUM = "live_login_ip_error_num_";

        /// <summary>
        /// �ֻ���֤�� 
        /// </summary>
        public const string LIVE_PHONE_VALI_CODE = BASE_KEY + "live_phone_code_";

        /// <summary>
        /// �ֻ�������֤�����
        /// </summary>
        public const string LIVE_PHONE_SEND_CODE_NUM = BASE_KEY + "live_phone_send_code_num_";

        /// <summary>
        /// ͬһIP������֤�����
        /// </summary>
        public const string LIVE_PHONE_SEND_NUM_SAMEIP = BASE_KEY + "live_phone_send_code_num_sameIp_";
        /// <summary>
        /// ͬһIPע������
        /// </summary>
        public const string LIVE_PHONE_Regiseter_NUM_SAMEIP = BASE_KEY + "live_phone_Register_SameIP_";
        /// <summary>
        /// ͬ�豸����������
        /// </summary>
        public const string LIVE_SEARCH_NUM_SAME_DeviceId = BASE_KEY + "live_Phone_Search_DeviceId_";
        /// <summary>
        /// �����޸�ͼ���������
        /// </summary>
        public const string LIVE_UPLOAD_Photo = BASE_KEY + "live_Phone_Upload_Photo_Useridx_";
        public const string LIVE_Follow = BASE_KEY + "Live_folow_Useridx_";
        public const string LIVE_FollowByIP = BASE_KEY + "Live_folow_Useridx_IP_";
        public const string LIVE_UpPwdToken = BASE_KEY + "Live_UpPwdToken_Useridx_";
        //spublic const string LIVE_UserExchangeCion = BASE_KEY + "Live_UserExchangeCion_Useridx_";

        public const string LIVE_RANK_V2 = "LIVE_RANK_V2_{0}_{1}";


        #region ������ֵ
        /// <summary>
        /// 70��IP��ַ���ѯ
        /// </summary>
        public const string COMMON_IPADDRESSDATA = BASE_KEY + "Common_IPAddressData";
        //public const string COMMON_PROVINCE = "Common_Province";
        //public const string COMMON_CITY = "Common_City";
        //public const string COMMON_LOCATION_ENTITY = "Common_Location_Entity";
        #endregion
    }
}
