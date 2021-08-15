using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace WebSide.Models
{
    public class ConvertedBook
    {

        public ConvertedBook()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            string filename = "onebook.pdf";
            PdfDocument outputDocument = new PdfDocument();
            outputDocument.PageLayout = PdfPageLayout.SinglePage;
            XGraphics gfx;
            XRect box;

            XPdfForm form = XPdfForm.FromFile(filename);

            for (int idx = 0; idx < form.PageCount; idx += 2)
            {
                PdfPage page = outputDocument.AddPage();
                page.Orientation = PageOrientation.Landscape;
                double width = page.Width;
                double height = page.Height;

                int rotate = page.Elements.GetInteger("/Rotate");
                gfx = XGraphics.FromPdfPage(page);
                
                form.PageNumber = idx + 1;
                box = new XRect(0, 0, width / 2, height);
                gfx.DrawImage(form, box);


                if (idx + 1 < form.PageCount)
                {
                    form.PageNumber = idx + 2;
                    box = new XRect(width / 2, 0, width / 2, height);
                    gfx.DrawImage(form, box);

                }
            }
            filename = "TwoPagesOnOne_tempfile.pdf";
            outputDocument.Save(filename);

        }
    }
}
