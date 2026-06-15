using MyApp.Common.Core.Data;


namespace MyApp.CareerAdvancement.Core.Entities.SessionsManagement
{
    // Always inherit AuditEntity — gives CreatedBy, CreatedOn, IpAddress for free
    public sealed class AssessmentSession : AuditEntity
    {
        public int Id { get; init; }
        public string SessionName { get; set; } = null!;
        public string SessionType { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string AppStatus { get; set; } = null!;
        public DateTime SessionFrom { get; set; }
        public DateTime SessionTo { get; set; }
        public bool IsActive { get; set; }
    }
}
