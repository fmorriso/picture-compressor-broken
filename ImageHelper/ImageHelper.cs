using System;
using System.IO;
//
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageUtilities
{
    public static class ImageHelper
    {
        /// <summary>
        /// Compresses a JPG image to the specified quality level and save it.
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <param name="CompressionQuality">between 1 and 100</param>
        /// <returns>The compressed image</returns>
        public static Image SaveCompressedJpegImage(string InputFile, string OutputFile, int CompressionQuality)
        {
            if (string.IsNullOrEmpty(InputFile))
            {
                throw new FileNotFoundException("The input file must not be null or an empty string");
            }

            if (string.IsNullOrEmpty(OutputFile))
            {
                throw new FileNotFoundException("The output file must not be null or an empty string");
            }

            if (CompressionQuality < 0 || CompressionQuality > 100)
            {
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");
            }

            Console.WriteLine($"Compressing {InputFile}");
            Image returnValue = Image.FromFile(InputFile, true);

            //
            // Encoder parameter for image quality
            var qualityParam = new EncoderParameter(Encoder.Quality, CompressionQuality);
            // Jpeg image codec 
            var jpegCodec = GetImageCodecInfo("image/jpeg");
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            returnValue.Save(OutputFile, jpegCodec, encoderParams);
            jpegCodec = null;
            encoderParams = null;
            qualityParam = null;
            return returnValue;
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary>         
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            return codecs.FirstOrDefault(i => i.MimeType == mimeType);
        }
    }
}
