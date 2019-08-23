using System.Collections.Generic;

namespace Backend
{
    internal class Defaults
    {
        public const string CERTIFICATE_PATH = "CERTIFICATE_PATH";
        public const string ORGANIZATION_ID = "ORGANIZATION_ID";
        public const string TECHNICAL_ACCOUNT_ID = "TECHNICAL_ACCOUNT_ID";
        public const string CLIENT_ID = "CLIENT_ID";
        public const string CLIENT_SECRET = "CLIENT_SECRET";
        public const string ALL_CORS_POLICY = "ALL_CORS_POLICY";

        public static readonly Dictionary<string, string> Configuration = new Dictionary<string, string>
        {
            {CERTIFICATE_PATH, "path to pfx file"},
            {ORGANIZATION_ID, "Organization ID"},
            {TECHNICAL_ACCOUNT_ID, "Technical account ID"},
            {CLIENT_ID, "API Key (Client ID)"},
            {CLIENT_SECRET, "Client secret"}
        };
    }
}