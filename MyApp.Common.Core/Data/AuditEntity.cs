using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Core.Data
{
    public abstract class AuditEntity
    {
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string IpAddress { get; set; } = null!;
    }
}
