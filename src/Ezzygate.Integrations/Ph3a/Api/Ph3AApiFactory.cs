namespace Ezzygate.Integrations.Ph3a.Api;

public static class Ph3AApiFactory
{
    public static LoginRequest CreateLoginRequest(string username)
    {
        return new LoginRequest(username);
    }

    public static AnalysisRequest CreateAnalysisRequest(Ph3ARequest requestDto)
    {
        var (directCode, phoneNumber) = ParsePhone(requestDto.Phone);

        return new AnalysisRequest
        {
            Parameters = new AnalysisRequestParameters()
            {
                CampaignId = 152,
                Parameters =
                [
                    new { name = "CPF / CNPJ", value = requestDto.CustomerIdNumber },
                    new { name = "CEP", value = requestDto.ZipCode },
                    new { name = "DDD", value = directCode },
                    new { name = "Telefone", value = phoneNumber }
                ]
            }
        };
    }

    private static (string? directCode, string? phoneNumber) ParsePhone(string? phone)
    {
        if (string.IsNullOrEmpty(phone) || phone.Length < 3)
            return (null, phone);

        var directCode = phone[..3];
        var phoneNumber = phone[3..];

        return (directCode, phoneNumber);
    }
}