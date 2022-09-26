namespace Global.Shared.Helpers
{
    public enum PasswordVerificationResult : byte
    {
        Failed,
        Success,
        SuccessRehashNeeded
    }
}
