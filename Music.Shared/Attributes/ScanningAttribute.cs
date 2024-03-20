using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScanningAttribute : Attribute
    {
        public string RegisterType { get; set; }
       
    }
}
