using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.Services;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ClasicoConcreto.Entity;
using ClasicoConcreto.Bussines;

public partial class Pages_Reportes_Firma : System.Web.UI.Page
{
    private static Entity_Clientes CurrentUser;
    private const string path = @"\\192.168.100.10\wwwroot\Firmas\";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

  

    [WebMethod(EnableSession = true)]
    public static string[] SaveFile(string fileBase64)
    {
        string[] rtnData = new string[3];
        try
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            string name = currentTime.ToString().Replace(".", "_").Replace(":", "_");
            //   Session["strFirma"] = CurrentUser.strEmail;
            NameSignature.strNameSignature = name;
            string imgPath = Path.Combine(path, name + ".png");

            string dataUrl = fileBase64.Substring(fileBase64.LastIndexOf(',') + 1);
            byte[] imageBytes = Convert.FromBase64String(dataUrl);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            Bitmap rsImage = ResizeImage(image, 200, 100);
            rsImage.SetResolution(50,50);

            //qawithexperts.com


            rsImage = Transparent2Color(rsImage, Color.White);



            rsImage.Save(imgPath);


            rtnData[0] = "ok";
        }

        catch (Exception ex)
        {
            rtnData[0] = "Error image save";
            System.Diagnostics.Debug.Print(ex.Message);
        }
        return rtnData;
    }


    private static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }

    private static Bitmap Transparent2Color(Bitmap bmp1, Color target)
    {
        Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
        Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
        using (Graphics G = Graphics.FromImage(bmp2))
        {
            G.Clear(target);
            G.DrawImageUnscaledAndClipped(bmp1, rect);
        }
        return bmp2;
    }

}

