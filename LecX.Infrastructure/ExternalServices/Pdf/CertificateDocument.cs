//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LecX.Infrastructure.ExternalServices.Pdf
//{
//    public class CertificateDocument : IDocument
//    {
//        public string FullName { get; }
//        public string CourseName { get; }
//        public DateTime IssueDate { get; }
//        public byte[]? SignatureImage { get; } // optional
//        public byte[]? QrImage { get; }

//        public CertificateDocument(string fullName, string courseName, DateTime issueDate, byte[]? signature = null, byte[]? qr = null)
//        {
//            FullName = fullName;
//            CourseName = courseName;
//            IssueDate = issueDate;
//            SignatureImage = signature;
//            QrImage = qr;
//        }

//        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

//        public void Compose(IDocumentContainer container)
//        {
//            container.Page(page =>
//            {
//                page.PageColor(Colors.White);
//                page.Size(PageSizes.A4.Landscape());
//                page.Margin(40);
//                page.DefaultTextStyle(x => x.FontSize(18));
//                page.Content().Padding(10).Column(col =>
//                {
//                    col.Spacing(10);

//                    col.Item().AlignCenter().Text("CERTIFICATE OF COMPLETION")
//                        .FontSize(34).SemiBold().FontColor(Colors.Black);

//                    col.Item().AlignCenter().Text($"This is to certify that").FontSize(16);

//                    col.Item().AlignCenter().Text(FullName)
//                        .FontSize(30).Bold();

//                    col.Item().AlignCenter().Text($"has successfully completed the course").FontSize(16);

//                    col.Item().AlignCenter().Text(CourseName)
//                        .FontSize(22).Italic();

//                    col.Item().PaddingTop(20).Row(row =>
//                    {
//                        row.RelativeColumn().Stack(stack =>
//                        {
//                            stack.Item().Text($"Date: {IssueDate:dd MMMM yyyy}");
//                        });

//                        row.ConstantColumn(150).AlignRight().Stack(stack =>
//                        {
//                            if (SignatureImage != null)
//                                stack.Item().Image(SignatureImage, ImageScaling.FitArea);
//                            else
//                                stack.Item().Text("Signature").AlignRight();
//                        });
//                    });

//                    if (QrImage != null)
//                    {
//                        col.Item().AlignRight().PaddingTop(10).Width(100).Image(QrImage, ImageScaling.FitArea);
//                    }
//                });
//            });
//        }
//}

//// Generate:
//var qr = GenerateQrPng("https://your-site/verify?id=123&name=..."); // optional
//        var signature = System.IO.File.ReadAllBytes("sign.png"); // optional

//        var doc = new CertificateDocument("Nguyen Van A", "Lập trình .NET nâng cao", DateTime.Now, signature, qr);
//doc.GeneratePdf("certificate.pdf");

//// Helper: QR using QRCoder
//byte[] GenerateQrPng(string text)
//    {
//        using var qrGen = new QRCodeGenerator();
//        using var data = qrGen.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
//        using var qr = new PngByteQRCode(data);
//        return qr.GetGraphic(20);
//    }
//}
