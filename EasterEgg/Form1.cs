using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;

namespace EasterEgg
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public void TakeScreenshot()
        {
            Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
                               SystemInformation.VirtualScreen.Height,
                               PixelFormat.Format32bppArgb);
            Graphics screenGraph = Graphics.FromImage(screenshot);
            screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X,
                                       SystemInformation.VirtualScreen.Y,
                                       0,
                                       0,
                                       SystemInformation.VirtualScreen.Size,
                                       CopyPixelOperation.SourceCopy);

            screenshot.Save("Screenshot.png", ImageFormat.Png);
            Clipboard.Clear();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool end = false;
            WindowState = FormWindowState.Minimized;
            while(true)
            {
                TakeScreenshot();
                Bitmap img = new Bitmap("Screenshot.png");
                /*if(img.GetPixel(467, 168).GetBrightness() < 0.4 && img.GetPixel(496, 193).GetBrightness() > 0.8)
                {

                }*/
                for(int x = 250; x > 190; x--)
                {
                    for(int y = 230; y < 240; y++)
                    {
                        if(img.GetPixel(x,y).GetBrightness() < 0.4)
                        {
                            SendKeys.Send(" ");
                            //Thread.Sleep(250);
                            //SendKeys.Send("{DOWN}");
                            end = true;
                            break;
                        }
                    }
                    if(end)
                    {
                        end = false;
                        break;
                    }
                }
                img.Dispose();
                if (File.Exists(@"Screenshot.png"))
                {
                    File.Delete(@"Screenshot.png");
                }
                Thread.Sleep(40);
            }
            
        }
    }
}
