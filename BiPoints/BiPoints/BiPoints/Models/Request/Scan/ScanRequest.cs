using System;

namespace BiPoints.Models.Request.Scan
{
    class ScanRequest
    {
        public Guid UserId { get; set; }
        public string Result { get; set; }
    }
}
