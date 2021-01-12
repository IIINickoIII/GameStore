using GameStore.Web.Services.Interfaces;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;

namespace GameStore.Web.Services
{
    public class PdfCreator : IPdfCreator
    {
        public MemoryStream CreateStream(string pdfFileContent)
        {
            var document = new PdfDocument();
            var graphics = document.Pages.Add().Graphics;
            var font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            graphics.DrawString(pdfFileContent, font, PdfBrushes.Black, new PointF(0, 0));

            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
