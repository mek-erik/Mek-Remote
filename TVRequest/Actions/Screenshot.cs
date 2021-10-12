using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TVRequest.Actions
{
    class Screenshot
    {
        
        public void SaveScreenshot(string filename, int width = 1920, int height = 1080)
        {
            if (OS.GetOS() == "windows")
            {
                using var bitmap = new Bitmap(width, height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0,
                    bitmap.Size, CopyPixelOperation.SourceCopy);
                }
              
                bitmap.Save(filename, ImageFormat.Png);
            }    
            else if (OS.GetOS() == "mac")
            {
                Process.Start(new ProcessStartInfo("screencapture", filename));
            }
        }
    }
}
