namespace Ezzygate.WebApi.Models;

public enum ResultEnum
{
    Unknown,
    Success,
    Failure,
    Pending,
    LoginRequired,
    SignatureRequired,
    RequestIdRequired,
    MerchantNumberRequired,
    MerchantNotFound,
    InvalidRequestId,
    DuplicateRequestId,
    SslRequired,
    GeneralError,
    InvalidRequest,
    ApiVersionRequired,
    SignatureMismatch
}