namespace BrizonForum.Application.Models.Settings;
public class FileSettings
{
    public required string S3AccessKey { get; set; }
    public required string S3SecretKey { get; set; }
    public required string S3BucketName { get; set; }
    public required string S3Region { get; set; }
    public required string S3DefaultServerRoot { get; set; }
    public required string S3UploadFolderPath { get; set; }
    public required string S3UploadFolderPrivateMessage { get; set; }
}
