using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class XMLEntity
    {
        public int XmlId { get; set; }
        public int Id { get; set; }

        public XMLEntity(int id)
        {
            Id = id;
        }

        public XMLEntity()
        {
        }
    }
}
