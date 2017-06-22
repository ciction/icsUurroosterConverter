using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Optaplanner_converter.XML_Model
{
    public class Room : XMLEntity
    {
        public string Code { get; set; }
        public int Capacity { get; set; }
        public int PcCount { get; set; }

        public Room(int id, string code) : base(id)
        {
            Code = code;
            SetCapacityAndPCCount();
        }

        private void SetCapacityAndPCCount()
        {
            //room info
            //DT/A.0.A1
            //DT/A.0.001
            //DT/C.2.209
            //defaultRoom       (meestal audi 1 --> 100 plaatsen)

            Capacity = 25;
            PcCount = 25;

            switch (Code)
            {
                case "DT/A.0.A1":
                    Capacity = 100;
                    PcCount = 0;
                    break;
                case "DT/A.0.001":
                    Capacity = 200;
                    PcCount = 0;
                    break;
                case "DT/C.2.209":
                    Capacity = 50;
                    PcCount = 50;
                    break;
                case "defaultRoom":
                    Capacity = 100;
                    PcCount = 0;
                    break;
            }
        }
    }
}
