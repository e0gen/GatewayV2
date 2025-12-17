namespace Ezzygate.Infrastructure.Repositories;

public class ChargeAttemptUpdate
{
    internal int? TransactionId { get; private set; }
    internal string? ReplyCode { get; private set; }
    internal string? ReplyDescription { get; private set; }
    internal bool? RedirectFlag { get; private set; }
    internal string? InnerRequest { get; private set; }
    internal string? InnerResponse { get; private set; }
    internal bool ThrowIfNotFound { get; private set; }
    internal string? NotFoundMessage { get; private set; }

    public ChargeAttemptUpdate SetTransaction(int transactionId, string replyCode, string replyDescription)
    {
        TransactionId = transactionId;
        ReplyCode = replyCode;
        ReplyDescription = replyDescription;
        return this;
    }

    public ChargeAttemptUpdate SetRedirectFlag(bool flag)
    {
        RedirectFlag = flag;
        return this;
    }

    public ChargeAttemptUpdate SetInnerRequest(string? request)
    {
        InnerRequest = request ?? InnerRequest;
        return this;
    }

    public ChargeAttemptUpdate SetInnerResponse(string? response)
    {
        InnerResponse = response ?? InnerResponse;
        return this;
    }

    public ChargeAttemptUpdate RequireExists(string? message = null)
    {
        ThrowIfNotFound = true;
        NotFoundMessage = message;
        return this;
    }
}