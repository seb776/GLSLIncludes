using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLSLIncludes_UnitTests
{
    [TestClass]
    public class UnitTestsIncludesProgram
    {
        [TestMethod]
        public void TestInclude_OneFile()
        {
            var testFile = "Local_NoPath.glsl";

            UnitTestsCommon.CreateEmptyTestFolder();
            UnitTestsCommon.CopyToTestFolder(testFile);
            UnitTestsCommon.CopyToTestFolder("Empty.glsl");
            Directory.SetCurrentDirectory(UnitTestsCommon.TEST_FOLDER);

            var result = GLSLIncludes.Program.DoIncludes(new string[] {
                testFile
            });
            if (result != 0)
                throw new Exception();

            if (!File.Exists(testFile + ".generated"))
                throw new Exception();

            Directory.SetCurrentDirectory("../");
            UnitTestsCommon.CleanTestFolder();
        }


        [TestMethod]
        public void TestInclude_OneFileMissing()
        {
            var testFile = "Local_NoPath.glsl";

            UnitTestsCommon.CreateEmptyTestFolder();
            UnitTestsCommon.CopyToTestFolder(testFile);
            //UnitTestsCommon.CopyToTestFolder("Empty.glsl");
            Directory.SetCurrentDirectory(UnitTestsCommon.TEST_FOLDER);

            var result = GLSLIncludes.Program.DoIncludes(new string[] {
                testFile
            });
            if (result != 0)
                throw new Exception();

            if (File.Exists(testFile + ".generated"))
                throw new Exception();

            Directory.SetCurrentDirectory("../");
            UnitTestsCommon.CleanTestFolder();
        }
        [TestMethod]
        public void TestInclude_NoFile()
        {
            var result = GLSLIncludes.Program.DoIncludes(new string[] {
            });
            if (result == 0)
                throw new Exception();
        }

        [TestMethod]
        public void TestInclude_MultipleFiles_NoOutputFormat()
        {
            var result = GLSLIncludes.Program.DoIncludes(new string[] {
                "Local_NoPath.glsl",
                "Local_NoPath.glsl",
                "Local_NoPath.glsl"
            },
            false);
            if (result == 0)
                throw new Exception();
        }

        [TestMethod]
        public void TestInclude_MultipleFiles_WithOutputFormat_NoFilename()
        {
            var result = GLSLIncludes.Program.DoIncludes(new string[] {
                "Local_NoPath.glsl",
                "Local_NoPath.glsl",
                "Local_NoPath.glsl",
                "-o",
                "test.glsl"
            },
            false);
            if (result == 0)
                throw new Exception();
        }

        [TestMethod]
        public void TestInclude_MultipleFiles_WithOutputFormat_WithFilename()
        {
            var result = GLSLIncludes.Program.DoIncludes(new string[] {
                "Local_NoPath.glsl",
                "Local_NoPath.glsl",
                "Local_NoPath.glsl",
                "-o",
                GLSLIncludes.Program.FILENAME_MACRO + ".glsl"
            },
            false);
            if (result != 0)
                throw new Exception();
        }
    }
}
