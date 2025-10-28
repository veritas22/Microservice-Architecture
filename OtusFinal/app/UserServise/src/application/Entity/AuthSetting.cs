using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AuthSetting
    {
        public TimeSpan TimeExpires { get; set; }
        public string SecretKey { get; set; }
    }
}
