using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video.FFMPEG;

namespace ConsoleApplication11
{
    class Program
    {
        static void Main(string[] args)
        {
 

           Console.WriteLine("done");
           Console.Read();
        }

        private createVideo()
        {
            int width = 320;
            int height = 240;

            //// create instance of video writer
            VideoFileWriter writer = new VideoFileWriter();
            //// create new video file
            writer.Open("test.avi", width, height, 25, VideoCodec.MPEG4);
            // create a bitmap to save into the video file
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            // write 1000 video frames
            for (int i = 0; i < 1000; i++)
            {
                image.SetPixel(i % width, i % height, Color.Red);
                writer.WriteVideoFrame(image);
            }
            writer.Close();
        }
    }


}
