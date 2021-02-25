﻿
namespace OAuth.Google
{
    public class GoogleUser
    {
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }
        public string updated_datetime { get; set; }
    }
}
