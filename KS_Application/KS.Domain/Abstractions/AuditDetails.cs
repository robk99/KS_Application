namespace KS.Domain.Abstractions
{
    public abstract class AuditDetails
    {
        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}
