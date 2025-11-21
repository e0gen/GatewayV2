using System.Net;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;

namespace Ezzygate.Infrastructure.Notifications;

public sealed class NotificationClient : INotificationClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<NotificationClient> _logger;

    public NotificationClient(IHttpClientFactory httpClientFactory, ILogger<NotificationClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<NotificationResult> SendNotificationAsync(NotificationData data)
    {
        using var logger = _logger.GetScoped(LogTag.WebApi, "Notification sent");
        try
        {
            var hashData = $"{data.ReplyCode}{data.TrxNum}{data.Order}{data.Amount:0.00}{data.Currency}{data.MerchantHashCode}";
            var notificationSignature = WebUtility.UrlEncode(hashData.ToSha256());

            var postData = new Dictionary<string, string?>
            {
                { "TransType", data.TrxType.ToString() },
                { "Reply", data.ReplyCode },
                { "TransID", data.TrxNum.ToString() },
                { "Date", DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") },
                { "Order", data.Order },
                { "Amount", data.Amount.ToString("0.00") },
                { "Payments", data.Payments.ToString() },
                { "Currency", data.Currency.ToString() },
                { "ConfirmationNum", data.ApprovalNumber },
                { "Comment", data.Comment },
                { "ReplyDesc", data.ReplyDesc },
                { "Descriptor", data.Descriptor },
                { "CCType", data.CcType },
                { "signType", data.SignType },
                { "signature", notificationSignature },
                { "movedPendingId", data.MovedPendingId.ToString() }
            };

            if (!string.IsNullOrEmpty(data.Last4))
                postData.Add("Last4", data.Last4);
            if (!string.IsNullOrEmpty(data.CcStorageId))
                postData.Add("ccStorageID", data.CcStorageId);
            if (!string.IsNullOrEmpty(data.Source))
                postData.Add("Source", data.Source);
            if (!string.IsNullOrEmpty(data.WalletId))
                postData.Add("WalletID", data.WalletId);
            if (data.LogChargeId.HasValue)
                postData.Add("logChargeID", data.LogChargeId.ToString());
            if (!string.IsNullOrEmpty(data.TimeString))
                postData.Add("timeString", data.TimeString);
            if (!string.IsNullOrEmpty(data.D3Redirect))
                postData.Add("D3Redirect", data.D3Redirect);
            if (!string.IsNullOrEmpty(data.D3RedirectMethod))
                postData.Add("D3RedirectMethod", data.D3RedirectMethod);
            if (!string.IsNullOrEmpty(data.RecurringSeries))
                postData.Add("RecurringSeries", data.RecurringSeries);

            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            var request = new HttpRequestMessage(HttpMethod.Post, data.NotificationUrl)
            {
                Content = new FormUrlEncodedContent(postData)
            };

            var response = await client.SendAsync(request);

            var statusCode = (int)response.StatusCode;
            var requestUrl = data.NotificationUrl + "?" +
                             string.Join("&", postData.Select(kvp => $"{kvp.Key}={kvp.Value?.ToEncodedUrl()}"));

            logger.Info($"Merchant: {data.MerchantId} Order: {data.Order} Pending Id: {data.MovedPendingId}");
            logger.Info($"Notify Url: {requestUrl}");
            logger.Info($"Reply Code: {statusCode}");

            var logXml = new XElement("XML",
                new XElement("ActionType", "Process Notify"),
                new XElement("ReplyCode", statusCode),
                new XElement("Request", requestUrl),
                new XElement("Response"),
                new XElement("SystemComment", "Webhook Notification")
            );

            var logXmlString = logXml.ToString(SaveOptions.DisableFormatting);
            logger.Info($"DB Format: {logXmlString}");

            return new NotificationResult
            {
                StatusCode = statusCode,
                MerchantId = data.MerchantId,
                TransactionId = data.TrxNum,
                LogXml = logXmlString,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            logger.Error($"Couldn't send notification. {ex.Message}\n\n{ex}");

            return new NotificationResult { IsSuccess = false };
        }
    }
}