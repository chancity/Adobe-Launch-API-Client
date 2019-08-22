﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeLaunch.ConsoleApp
{
    internal class Defaults
    {
        public static readonly Dictionary<string, string> Configuration = new Dictionary<string, string>
        {
            {"CERTIFICATE_PATH", "path to pfx file"},
            {"ORGANIZATION_ID", "Organization ID"},
            {"TECHNICAL_ACCOUNT_ID", "Technical account ID"},
            {"CLIENT_ID", "API Key (Client ID)"},
            {"CLIENT_SECRET", "Client secret"}
        };
    }
}