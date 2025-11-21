using System;

namespace Ezzygate.Infrastructure.Notifications
{
    public class NotificationData
    {
        public int MerchantId { get; set; }
        public required string MerchantHashCode { get; set; }
        public required string NotificationUrl { get; set; }
        public int? TrxNum { get; set; }
        public int? TrxType { get; set; }
        public decimal Amount { get; set; }
        public int? Currency { get; set; }
        public string? Order { get; set; }
        public int Payments { get; set; }
        public string? Comment { get; set; }
        public string? ReplyCode { get; set; }
        public string? ReplyDesc { get; set; }
        public string? CcType { get; set; }
        public int MovedPendingId { get; set; }
        public string? ApprovalNumber { get; set; }
        public string Descriptor { get; set; } = "";
        public string? Last4 { get; set; }
        public string? CcStorageId { get; set; }
        public string? Source { get; set; }
        public string? WalletId { get; set; }
        public int? LogChargeId { get; set; }
        public string? TimeString { get; set; }
        public string? D3Redirect { get; set; }
        public string? D3RedirectMethod { get; set; }
        public string? RecurringSeries { get; set; }
        public string SignType { get; set; } = "SHA256";
    }
}
