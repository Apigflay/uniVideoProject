using System;
using Zmop.Api;
using Zmop.Api.Request;
using Zmop.Api.Response;
using Jayrock.Json.Conversion;
using Common;

namespace ThirdAPI
{
    public class ZhimaAuthLib
    {
        //正式 芝麻 RSA 公钥
        public const String publicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCtrEHnmXJyEy+nLKkJZFTfQQyvDf9Io2CnyL5Gl/1InFGEH9lv2yYfpqMjTZz0yCoylgCT8nhqbeMJ/W8kpjO5K0FI9+0KcxOXwYwFCQPXdK+gFKocfpy30UwlBGoZDGkdsPrq0ZMhUpVru+Z042YeK9qUURDMnehU3Uu4Z5YiXwIDAQAB";
        //正式 商户 RSA 私钥
        public const String privateKey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBANCvA+jeEkE2+Vn0pg28i73pg5tZBs8IL/dKYg9MLAEmw2ba8pguogTxzqMsAuHhJazQ2WCX/E3+en8P3OtNM6ug7pFsdkDqXvV+bvCGMnM7pRKrUp9tqt1rAOR99cYQWDTSmKN0Qzgf8SaAaKNQGqmyeTlzsZrDA4/4/vbaknHxAgMBAAECgYBB0iH6jijV1wAZJnhFtuWgtgmjsxXZsSxn5Fc/mff7OP3C8GY6J+NEifxyLQyPsFMQyiL2O5oCA7UhZKB6uzQDaXyeP7aShB2cqe/HGWaOUKo/Hju/8Ts5NT1GcyxbSRJKzkkBQ//yYEB9IsV12Xv7VtaA/8ZtkvUOrdjkeZKhHQJBAOxUprmI8kna9nbs3s2snTSp7ZMWA/57+aokEbaRov8WCrybZSwy47i3IVoYe1ubbQDernu0SIo4eEBP+lt6Ui8CQQDiDU5ztNGPT9w1rzXU7+aCICPc16JYWBZjSq1++BBsZrZrfoGBJ1kLgjq1m1Wt6E8yzPynq7cjbBh9jYgjaRXfAkARKjTR1PiEFLtB+AJWverNIGp4/Ghd23NOwD/pGrrT/C3bdQ3sH+YaTHLHsG+FP8yy+3mA0p9SKrfRM/3jFwdRAkBtWrKRFvApvCBzeAc6s7N3T8UJmOdYhYSPFBlKSwbm6ellpxVPPZG+F4n/QN35+2AU20V+d3tpD2npkwZA2x1RAkAI+Rnl6sXp682kcTEjaOEq4rFwunAUCl/Lx/SWT5T7ZW6o24vGMeLZEvFbY+A8vY+J3zZltv4uBQAtvg4Tdmp+";

        //测试时使用
        //public const string publicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFQADEdSeIeKw+ezu3+HXrN/MKtQENRdCb/mp6VxJUy/E9lKJG10wyTbHFWjsqiGTK301lQWSPr+wS5vo82DRk/auwJ3goSdkoExRIGxEgFGOwO5fnp7Mb2BQjShaPsl4EVt3g5NUJD465X1I2lfbNd8KpktL83liO0cSwc9bb8wIDAQAB";
        //public const string privateKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAIjfNWUXqCNtlaBFpv4KrBTVQb4btX9Gi+UPuz7AMvwbSXmtIxV2awkz4Rg0Q3PtTkuAUpuW958u3uNxypcsCJD3o4qphgqSMVpe33bJ1bZaeL/h8WT9KNBiIxGsYQco44JbJkjrHTAI3Wt+RNhHyn+nKI9wCaeYPCY7ZipVpmL3AgMBAAECgYBn/BxWx1hIQjMQ5pnuGzGNSk9+HRMQtQoHZqI9FEwn2JtDw9QJtEOxZCa4+svcQQfguIcKCfHqj/NqHMNrglqmmJhKrut+ilUt8rqJCmvRSh8AdG7ZnqzxQYdTrrEEKgO932WqPOI98X2H+2cPzLfIOgxN7FhCDg+lVi4CBdfJ8QJBANosNf2b4f50AgAkstAKX1Hqqsb+k5L5reoF9e6eQufPXIpSrbME3cETyRMMLKX+YfhHpv66fpfg0X4JiHZIpdsCQQCgmmPNWs2nbvHY3Eo3xuYs/KJRIltgYaAd9+CCjpCTbU21afqyGkK7zibHfLADh+m5GOkOZ5IFn5Alhg1NQNgVAkBTX/PeEDVEPWcKUPv4nw4gSvKqi10wHLSGq3J5lwdweQEfZ0s0D5cDEyGTYuKpKNadwBwkWnbIacUFSnVY5phjAkAt4Ix73+F5X77kROFKl52u4if350mU+a5EgUd35AO2qXWWSgTcFZZUkaoQODULfSqtvkjs3Xcf9hm2LlnkZI6VAkEAl8+0+a6Rjz119mxJXy9JWKTbWirhja+ijyndLVEAmxTYjWTuNKbRdTYe59ekRYhmMWkiKzggP+PKjXHCHV6eRQ==";


        //芝麻信用网关地址
        public const string gatewayUrl = "https://zmopenapi.zmxy.com.cn/openapi.do";//test https://zmopenapi.zmxy.com.cn/sandbox.do
        //数据编码格式
        public const string charset = "UTF-8";
        ////芝麻分配给商户的 appId
        public const string appId = "1000710";// "1000699";

        #region 芝麻认证V1版本

        /// <summary>
        /// Step1 客户端身份认证
        /// </summary>
        /// <returns></returns>
        public static string ZhimaCustomerCertifyInitial(int useridx, ZhiMaInfo zm)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertifyInitialRequest request = new ZhimaCustomerCertifyInitialRequest();

            string param = JsonConvert.ExportToString(zm);

            //request.SetChannel("app");
            //request.SetPlatform("zmop");
            request.TransactionId = DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Ticks.ToString();// "201512100936588040000000465158";  //必要参数         
            request.ContractFlag = "si201608260005265004";// 测试"si201604200003674009";  //必要参数         
            request.ProductCode = "w1010100400000000001";  //必要参数         
            request.IdentityType = "BY_CERTNO_AND_NAME";// "BY_CERTNO_AND_NAME或BY_MOBILE_NO或BY_CERT_IMAGE";  //必要参数         
            request.IdentityParam = param;// "{\"certNo\":\"xxx\",\"name\":\"张三\",\"certType\":\"IDENTITY_CARD\",\"mobileNo\":\"13901234567\"} 或 {\"mobileNo\":\"13901234567\"}或{\"frontCertImage\":\"oioiweroeworewoiho2323\",\"backCertImage\":\"dsrrwerewew\"}";  //必要参数         
            request.State = "{\"xxx_id\":\"" + useridx + "\"}";  //必要参数
            request.SourceType = "sdk";// "h5，pc，app，sdk，window";  //必要参数         
            //request.BizParams = "{\"verifyScene\":\"WSECURITY\"}";  //必要参数         
            //request.PageUrl = "http://www.xxx.com";  //必要参数         
            //request.SchemaUrl = "myapplink://xxx";
            ZhimaCustomerCertifyInitialResponse response = client.Execute(request);
            ZhimaCustomerCertifyInitialResponse resp = response;
            //LogHelper.WriteLog(LogFile.Data, "【实名认证获取token】" + useridx + "," + resp.Body);
            return resp.Token;
        }

        /// <summary>
        /// Step2 获取认证参数
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ZhimaCustomerCertifyApply(string token)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertifyApplyRequest request = new ZhimaCustomerCertifyApplyRequest();
            request.Token = token; // 必要参数，初始化取得的token         
            return client.generatePageRedirectInvokeUrl(request);
        }

        /// <summary>
        /// 返回的结果进行解密解签
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static string decryptAndVerifySign(string param, string sign)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);

            try
            {
                //解密验签，返回结果
                String decryptedParam = client.decryptAndVerifySign(param, sign);

                //通过浏览器返回时，不需要decode
                return System.Web.HttpUtility.UrlDecode(decryptedParam);
            }
            catch (Exception ex)
            {
                return "";
            }
            //示例结果如下
            //certify_result=true&token=cdc394da0ee7c48bfb3867dec4b1a564e&open_id=268812407828113441734640502&real_name_flag=true
        }

        /// <summary>
        /// step3 芝麻认证结果检查(非必要判断)
        /// </summary>
        public static string ZhimaCustomerCertifyCheck(string token)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertifyCheckRequest request = new ZhimaCustomerCertifyCheckRequest();
            request.SetChannel("app");
            request.SetPlatform("zmop");
            request.Token = token;  //必要参数         
            ZhimaCustomerCertifyCheckResponse response = client.Execute(request);
            ZhimaCustomerCertifyCheckResponse resp = response;
            return resp.Body;
        }

        #endregion

        #region 芝麻认证V2版本

        /// <summary>
        /// step1 客户端身份认证V2
        /// 芝麻认证初始化
        /// </summary>
        /// <param name="zm"></param>
        /// <returns>返回bizNo</returns>
        public static string ZhimaCustomerCertificationInitialize(ZhiMaInfo zm)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertificationInitializeRequest request = new ZhimaCustomerCertificationInitializeRequest();
            request.SetChannel("apppc");
            request.SetPlatform("zmop");
            request.TransactionId = DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Ticks.ToString();  //必要参数             
            request.ProductCode = "w1010100000000002978";  //必要参数             
            request.BizCode = zm.certScene.ToString();//TODO "FACE_SDK";  //必要参数             
            request.IdentityParam = "{\"identity_type\":\"CERT_INFO\",\"cert_type\":\"IDENTITY_CARD\",\"cert_name\":\"" + zm.name + "\",\"cert_no\":\"" + zm.certNo + "\"}";  //必要参数             
            request.MerchantConfig = "{\"need_user_authorization\":\"false\"}";
            request.ExtBizParam = "{}";  //必要参数
            ZhimaCustomerCertificationInitializeResponse response = client.Execute(request);
            ZhimaCustomerCertificationInitializeResponse resp = response;

            return resp.BizNo;
        }

        public static string ZhimaCustomerCertificationQuery(string bizNo)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertificationQueryRequest request = new ZhimaCustomerCertificationQueryRequest();
            request.SetChannel("apppc");
            request.SetPlatform("zmop");
            request.BizNo = bizNo;  //必要参数
            ZhimaCustomerCertificationQueryResponse response = client.Execute(request);
            ZhimaCustomerCertificationQueryResponse resp = response;

            return resp.Passed;
        }

        #endregion

        #region 多因子人脸认证

        /// <summary>
        /// 接入时间：2017-11-22
        /// </summary>
        /// <param name="returnURL"></param>
        /// <param name="bizNo"></param>
        /// <returns></returns>
        public static string ZhimaCustomerCertificationCertify(string returnURL, string bizNo)
        {
            DefaultZmopClient client = new DefaultZmopClient(gatewayUrl, appId, privateKey, publicKey, charset);
            ZhimaCustomerCertificationCertifyRequest request = new ZhimaCustomerCertificationCertifyRequest();
            request.SetChannel("apppc");
            request.SetPlatform("zmop");
            request.BizNo = bizNo;// "ZM201705173000000323200000189778";  //必要参数             
            request.ReturnUrl = returnURL;  //必要参数             
            string url = client.generatePageRedirectInvokeUrl(request);
            return url;
        }

        #endregion

        public class ZhiMaInfo
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string certNo { get; set; }
            //public string certType { get; set; }
            /// <summary>
            /// 芝麻认证环境
            /// </summary>
            public CertScene certScene { get; set; }
        }

        /// <summary>
        /// 芝麻认证场景类别
        /// </summary>
        public enum CertScene
        {
            FACE, //多因子人脸认证
            SMART_FACE, //多因子快捷人脸认证
            CERT_PHOTO, //多因子证件照片认证
            CERT_PHOTO_FACE, //多因子证件照片和人脸认证
            FACE_SDK//人脸认证SDK
        }
    }
}
