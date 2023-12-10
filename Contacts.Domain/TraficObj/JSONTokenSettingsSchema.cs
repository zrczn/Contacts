using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Security.Models
{
    public class JSONTokenSettingsSchema
    {
        //wzór dla tokenu JWT, później wdłg tego schematu będzie fetchowany z appsettings
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpireDuration { get; set; }
    }
}
