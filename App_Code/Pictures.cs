using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

/// <summary>
/// Summary description for Pictures
/// </summary>
public class Pictures
{
    public Pictures()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Image(string NewImageName)
    {
        try
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(NewImageName + ".jpg");

            int height = int.Parse(image.Height.ToString());

            int width = int.Parse(image.Width.ToString());

            #region ===667_500===
            if (height > 500 && width > 667)
            {
                if (height > width)
                {
                    float temp = 0, TempHight = height, tempWidth = width;
                    temp = TempHight / 500;
                    tempWidth = tempWidth * (1 / temp);
                    TempHight = TempHight * (1 / temp);
                    height = (int)TempHight;
                    width = (int)tempWidth;
                }
                else
                {
                    float temp = 0, TempHight = height, tempWidth = width;
                    temp = tempWidth / 667;
                    tempWidth = tempWidth * (1 / temp);
                    TempHight = TempHight * (1 / temp);
                    height = (int)TempHight;
                    width = (int)tempWidth;
                }
            }
            else
            {
                if (height > width && height > 500)
                {
                    float temp = 0, TempHight = height, tempWidth = width;
                    temp = TempHight / 500;
                    tempWidth = tempWidth * (1 / temp);
                    TempHight = TempHight * (1 / temp);
                    height = (int)TempHight;
                    width = (int)tempWidth;
                }

                else if (width > height && width > 667)
                {
                    float temp = 0, TempHight = height, tempWidth = width;
                    temp = tempWidth / 667;
                    tempWidth = tempWidth * (1 / temp);
                    TempHight = TempHight * (1 / temp);
                    height = (int)TempHight;
                    width = (int)tempWidth;
                }
                else
                {
                    width = width;
                    height = height;
                }
            }

            Convert_667_500(NewImageName, ".jpg", width, height);
            #endregion
            image.Dispose();
            return "Success";
        }
        catch (Exception ex)
        {
            return Convert.ToString(ex.Message);
        }
    }
    public void Convert_667_500(string MapPath, string fileext, int width, int height)
    {
        try
        {
            string originalFilePath = MapPath + fileext; //Replace with your image path
            string thumbnailFilePath = string.Empty;
            Size newSize = new Size(width, height); // Thumbnail size (width = 120) (height = 90)
            using (Bitmap bmp = new Bitmap(originalFilePath))
            {
                thumbnailFilePath = MapPath + "1.jpg"; //Change the thumbnail path if you want  

                using (Bitmap thumb = new Bitmap((System.Drawing.Image)bmp, newSize))
                {
                    using (Graphics g = Graphics.FromImage(thumb)) // Create Graphics object from original Image    
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        //Set Image codec of JPEG type, the index of JPEG codec is "1"      
                        System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];
                        //Set the parameters for defining the quality of the thumbnail... here it is set to 100%
                        System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(1);
                        eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                        //Now draw the image on the instance of thumbnail Bitmap object
                        g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, thumb.Width, thumb.Height));
                        thumb.Save(thumbnailFilePath, codec, eParams);
                    }
                }
                // System.Drawing.Image image = System.Drawing.Image.FromFile(MapPath + fileext);
                //System.Drawing.Image thumbnailImage = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
                // System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
                // bm.Save(MapPath + "1.jpg", System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool ThumbnailCallback()
    {
        return true;
    }
}