namespace Ezzygate.Domain.Enums
{
    public enum TransactionStage
    {
        Unknown = -1,
        Created = 0,
        DetectMethod = 10,
        LoadConfig = 20,
        Validate = 30,
        LoadPrevTrans = 40,
        Risk = 50,
        Process = 60,
        Save = 70,
        PostProcess = 80,
        Complete = 100,
    }
}