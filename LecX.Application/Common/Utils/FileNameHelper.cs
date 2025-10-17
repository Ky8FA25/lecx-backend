using System.Text.RegularExpressions;

namespace LecX.Application.Common.Utils
{
    public static class FileNameHelper
    {
        /// <summary>
        /// Chuẩn hóa tên file: bỏ ký tự đặc biệt, khoảng trắng → '-', lowercase.
        /// </summary>
        /// <param name="originalName">Tên file gốc (vd: 'Ảnh đại diện mới (1).PNG')</param>
        /// <returns>
        /// Tuple gồm:
        ///  - SafeName: tên file đã chuẩn hóa (không có extension)
        ///  - Extension: phần mở rộng (vd: '.png')
        /// </returns>
        public static (string SafeName, string Extension) Normalize(string originalName)
        {
            if (string.IsNullOrWhiteSpace(originalName))
                return ("unnamed-file", string.Empty);

            var ext = Path.GetExtension(originalName).ToLowerInvariant();
            var name = Path.GetFileNameWithoutExtension(originalName).Trim().ToLowerInvariant();

            // Thay khoảng trắng bằng '-'
            name = Regex.Replace(name, @"\s+", "-");

            // Xóa ký tự đặc biệt / unicode lạ (chỉ giữ chữ, số, '-', '_')
            name = Regex.Replace(name, @"[^a-z0-9\-_]", "");

            // Giới hạn độ dài để tránh vượt giới hạn GCS (tùy chọn)
            if (name.Length > 100)
                name = name[..100];

            if (string.IsNullOrWhiteSpace(name))
                name = "unnamed-file";

            return (name, ext);
        }  
    }
}
