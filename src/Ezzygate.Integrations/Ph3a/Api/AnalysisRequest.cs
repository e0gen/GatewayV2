namespace Ezzygate.Integrations.Ph3a.Api;

public class AnalysisRequest
{
    public required AnalysisRequestParameters Parameters { get; set; }
}

public class AnalysisRequestParameters
{
    public int CampaignId { get; set; }
    public object[]? Parameters { get; set; }
}