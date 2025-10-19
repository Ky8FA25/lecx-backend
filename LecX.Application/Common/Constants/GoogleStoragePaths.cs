namespace LecX.Application.Commons.Constants;

public static class GoogleStoragePaths
{
    public const string PublicRoot = "public";
    public const string PrivateRoot = "private";
    public const string SystemRoot = "system";

    public static class Public
    {
        public const string AssignmentFiles = $"{PublicRoot}/assignment-files";
        public const string AssignmentSubmissions = $"{PublicRoot}/assignment-submissions"; // chú ý đúng chính tả folder
        public const string CommentFiles = $"{PublicRoot}/comment-files";
        public const string CourseCoverImages = $"{PublicRoot}/course-cover-images";
        public const string CourseMaterials = $"{PublicRoot}/course-materials";
        public const string LectureFiles = $"{PublicRoot}/lecture-files";
        public const string LectureVideos = $"{PublicRoot}/lecture-videos";
        public const string QuestionImages = $"{PublicRoot}/question-images";
        public const string UserAvatars = $"{PublicRoot}/user-avatars";
        public const string DefaultAvatars = $"{PublicRoot}/user-avatars/default-avatar.png";
    }

    public static class Private
    {
        public const string Certificates = $"{PrivateRoot}/certificates";
        public const string InstructorCvs = $"{PrivateRoot}/instructor-cvs";
    }

    public static class System
    {
        public const string Root = SystemRoot;
        // thêm nếu cần: logs, backups, v.v.
    }

    /// <summary>Prefix mặc định khi không truyền gì (có thể để "uploads" hoặc trống "")</summary>
    public const string Default = "uploads";
}
