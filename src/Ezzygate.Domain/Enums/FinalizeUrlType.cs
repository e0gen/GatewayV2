namespace Ezzygate.Domain.Enums;

public enum FinalizeUrlType
{
    Unknown,
    GeneralRedirect,
    SuccessRedirect,
    FailureRedirect,
    ChallengeRedirect,
    FingerprintRedirect,
    AsyncPendingRedirect,
    Notification
}