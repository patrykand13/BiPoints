using System;

namespace BiPoints.Models.Request.Base
{
    internal class SkipRecordsRequest
    {
        public Guid UserId { get; set; }
        public int skipRecords { get; set; }
    }
}
