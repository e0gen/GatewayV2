namespace Ezzygate.WebApi.Dtos;

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