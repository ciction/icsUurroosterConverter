using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class TeacherGroup: XMLEntity
    {
        public string Code { get; set; }
        public string SubCode { get; set; }

        public TeacherGroup(string code, string subCode)
        {
            Code = code;
            SubCode = subCode;
        }
    }
}
