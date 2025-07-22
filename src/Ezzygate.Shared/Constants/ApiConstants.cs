namespace Ezzygate.Shared.Constants;

public static class ApiConstants
{
    public static class Versions
    {
        public const string V3 = "3.0";
        public const string V4 = "4.0";
    }

    public static class Headers
    {
        public const string ApiVersion = "api-version";
        public const string XVersion = "X-Version";
        public const string ApiKey = "api-key";
        public const string Signature = "signature";
        public const string RequestId = "request-id";
        public const string MerchantNumber = "merchant-number";
    }

    public static class Policies
    {
        public const string MerchantPolicy = "MerchantPolicy";
        public const string AdminPolicy = "AdminPolicy";
        public const string IntegrationPolicy = "IntegrationPolicy";
    }
} 