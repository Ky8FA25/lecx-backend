using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using LecX.Application.Abstractions.ExternalServices.Pdf;
using System.Reflection;
using System.Text;

namespace LecX.Infrastructure.ExternalServices.Pdf
{
    public sealed class PdfService : IPdfService
    {
        private const string TEMPLATE_CERTIFICATE = "CertificateTemplate.html";

        public async Task<Stream> GenerateCertificateAsync(
            string studentName,
            string courseName,
            string completionDate,
            string instructorName,
            string instructorTitle,
            string verifyUrl)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourcePath = $"{typeof(PdfService).Namespace}.Templates.{TEMPLATE_CERTIFICATE}";

            // Dùng 'using' thông thường cho resourceStream để nó được đóng ngay sau khi đọc
            // Khuyến nghị: KHÔNG dùng 'await using' ở đây vì Assembly stream không cần IAsyncDisposable.
            using var resourceStream = asm.GetManifestResourceStream(resourcePath)
                ?? throw new FileNotFoundException($"Certificates template not found: {resourcePath}");

            // Dùng 'using' cho StreamReader. StreamReader sẽ tự động đóng resourceStream.
            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            var html = await reader.ReadToEndAsync();

            // Thay thế placeholders
            html = html
                .Replace("{{ .StudentName }}", studentName)
                .Replace("{{ .CourseName }}", courseName)
                .Replace("{{ .CompletionDate }}", completionDate)
                .Replace("{{ .InstructorName }}", instructorName)
                .Replace("{{ .InstructorTitle }}", instructorTitle)
                .Replace("{{ .VerifyUrl }}", verifyUrl);

            // KHÔNG dùng 'using' cho output Stream vì bạn muốn trả về nó.
            // Caller (người gọi) sẽ chịu trách nhiệm Dispose nó sau này.
            var output = new MemoryStream();
            using var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html));

            var props = new ConverterProperties();

            var writerProps = new WriterProperties()
                .SetCompressionLevel(CompressionConstants.BEST_COMPRESSION)
                .SetFullCompressionMode(true);
            using (var writer = new PdfWriter(output, writerProps))
            {
                writer.SetCloseStream(false);
                using var pdfDoc = new PdfDocument(writer);
                pdfDoc.SetDefaultPageSize(PageSize.A4.Rotate());
                HtmlConverter.ConvertToPdf(htmlStream, pdfDoc, props);
            }

            output.Position = 0;
            return output;
        }
    }
}