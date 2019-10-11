using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//

//
namespace PictureCompressor
{
    class Program
    {
        private const int COMPRESSION_QUALITY = 25;
        static void Main(string[] args)
        {
            const int COMPRESSION_QUALITY = 33;
            // "C:\Users\fpmor\OneDrive\travel\2019-10-Elkins-WV\pictures\mls pics"
            const string INPUT_PATH = @"C:\Users\fpmor\OneDrive\travel\2019-10-Elkins-WV\pictures\originals";
            Console.WriteLine("Start");
            
            // C:\Users\fpmor\OneDrive\2019 Driveway Pictures
            
            string OUTPUT_PATH = Path.Combine(INPUT_PATH, COMPRESSION_QUALITY.ToString());
            if (!Directory.Exists(OUTPUT_PATH)) 
            {
                Directory.CreateDirectory(OUTPUT_PATH);
            }
            

            if (Directory.Exists(INPUT_PATH) && Directory.Exists(OUTPUT_PATH))
            {
                var di = new DirectoryInfo(INPUT_PATH);
                foreach (Image file in di.GetFiles("*.jpg")
                                         .Select(f => new { f, outputFilename = Path.Combine(OUTPUT_PATH, f.Name) })
                                         .Where(@t => !File.Exists(@t.outputFilename))
                                         .Select(@t => ImageUtilities.ImageHelper.SaveCompressedJpegImage(@t.f.FullName, @t.outputFilename, COMPRESSION_QUALITY)))
                {
                    file.Dispose();
                } 
            }
            else
            {
                Console.WriteLine($"Either the input directory {INPUT_PATH} does not exist or output directory {OUTPUT_PATH} does not exits or both don't exist.");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
