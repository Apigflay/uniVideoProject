using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAuth.Facebook
{
    public class FaceBookConfig
    {
        public string AppKey = "";//台湾版
        public string AppSecrect = "";
        public string BaseURL = "https://graph.facebook.com";
        public string RedirectURL = "http://tw.livemiao.com/Passport/FacebookCallback";

        public string AuthCodeURL = "https://www.facebook.com/dialog/oauth";
        public string AcessTokenURL = "https://graph.facebook.com/oauth/access_token";
        public string UserInfoURL = "https://graph.facebook.com/me";
    }
}
