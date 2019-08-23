using System;
using System.Collections.Generic;

namespace AdobeJwt
{
    public class JwtPayloadOptions
    {
        public TimeSpan Expires { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public KeyValuePair<string, bool> NotSure { get; set; }
    }
}