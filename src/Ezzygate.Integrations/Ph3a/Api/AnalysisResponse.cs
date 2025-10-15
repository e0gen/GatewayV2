namespace Ezzygate.Integrations.Ph3a.Api;

public class AnalysisResponse
{
    public AnalysisData Data { get; set; } = null!;

    public class AnalysisData
    {
        public string ExecutionMessage { get; set; } = null!;

        public AnalysisScore? ParsedExecutionMessage
        {
            get
            {
                if (string.IsNullOrEmpty(ExecutionMessage))
                    return null;

                var executionMessage = ExecutionMessage.Split('-')[1].Trim();
                var executionMessageSplit = executionMessage.Split(';');
                return new AnalysisScore
                {
                    Score = int.Parse(executionMessageSplit[0]),
                    Message = executionMessageSplit[1].Trim()
                };
            }
        }
    }

    public class AnalysisScore
    {
        public int Score { get; set; }
        public string Message { get; set; } = null!;
    }
}