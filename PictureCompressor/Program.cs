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
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            const string INPUT_PATH = @"S:\Cats\ValleyVet stuff";
            const string OUTPUT_PATH = @"S:\Cats\ValleyVet stuff\small50";
            const int COMPRESSION_QUALITY = 50;

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
                Console.WriteLine("Either the input directory or output directory or both doesn't exist.");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
