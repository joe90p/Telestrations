﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using PictureLinkMVC.Web.Models;
using WebMatrix.WebData;

namespace PictureLinkMVC.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "000000004C0FB73F",
                clientSecret: "TPnfp7o5r5B7mJs5qSIjeBPhQqSdSxaT");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

        }
    }
}
