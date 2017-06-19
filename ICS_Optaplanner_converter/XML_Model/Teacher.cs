using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Teacher: XMLEntity
    {
        public string Code { get; set; }

        public Teacher(int xmlId, int id, string code) : base(id)
        {
            XmlId = xmlId;
            Id = id;
            Code = code;
        }

        public Teacher(int id, string code) : base(id)
        {
            Id = id;
            Code = code;
        }

        public Teacher(int id) : base(id)
        {
        }

    }
}
