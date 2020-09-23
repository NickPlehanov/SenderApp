using System;
using System.Collections.Generic;
using System.Text;

namespace SenderCoordsApp.Models {
    public class Coords {
        public Guid CooRecordId { get; set; }
        public string CooImei { get; set; }
        public string CooLongitude { get; set; }
        public string CooLatitude { get; set; }
    }
}
