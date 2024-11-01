using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Helpers
{
    public class JWTSettings
    {
        public string Secret { get; set; } = "[Guid(AD2BC9EC-13EA-4D8F-8436-B67E9D51B98A)]";
        public TimeSpan TokenLifeTime { get; set; } = TimeSpan.FromDays(30);
        public string Issuer { get; set; } = "SecureApi";
        public string Audience { get; set; } = "SecureApiUser";


    }
}
