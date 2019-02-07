using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//
using System.IO;
using System.Drawing;
//

namespace Tests
{
    /// <summary>
    /// Verify that Windows File Logic works correctly
    /// </summary>
    [TestClass]
    public class FileTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CanCompressSingleJpegFile()
        {
            const string SOURCE_FILE_NAME = @"d:\P4150319.JPG";
            const string TARGET_FILE_NAME = @"c:\TEMP\P4150319.JPG";
            const int COMPRESSION_QUALITY = 40;

            Image result = ImageUtilities.ImageHelper.SaveCompressedJpegImage(SOURCE_FILE_NAME, TARGET_FILE_NAME, COMPRESSION_QUALITY);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanCompressMultipleJpegFiles()
        {
            const string INPUT_PATH = @"C:\Users\fpmorrison\Pictures\2012-07-22 Deer\originals";
            const string OUTPUT_PATH = @"C:\Users\fpmorrison\Pictures\2012-07-22 Deer\small";
            const int COMPRESSION_QUALITY = 67;

            var di = new DirectoryInfo(INPUT_PATH);
            Assert.IsTrue(di.Exists, string.Format("Path {0} does not exist", INPUT_PATH));
            foreach (var result in di.GetFiles("*.jpg")
                                     .Select(f => new { f, outputFilename = Path.Combine(OUTPUT_PATH, f.Name) })
                                     .Where(@t => !File.Exists(@t.outputFilename))
                                     .Select(@t => ImageUtilities.ImageHelper.SaveCompressedJpegImage(@t.f.FullName, @t.outputFilename, COMPRESSION_QUALITY)))
            {
                Assert.IsNotNull(result);
                result.Dispose();
            }
        }
    }
}
