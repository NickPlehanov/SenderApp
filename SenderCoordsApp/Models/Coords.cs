using System;
using System.Collections.Generic;
using System.Text;

namespace SenderCoordsApp.Models {
    public class Coords {
        public Guid coo_RecordID { get; set; }
        public string coo_imei { get; set; }
        public string coo_longitude { get; set; }
        public string coo_latitude { get; set; }
    }
}
