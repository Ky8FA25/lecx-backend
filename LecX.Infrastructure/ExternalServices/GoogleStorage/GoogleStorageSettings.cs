namespace LecX.Infrastructure.ExternalServices.GoogleStorage
{
    public class GoogleStorageSettings
    {
        public string Bucket { get; set; } = string.Empty;
        public string DefaultAvatarPath { get; set; } = string.Empty;
        public string? KeyPath { get; set; }
    }
}
