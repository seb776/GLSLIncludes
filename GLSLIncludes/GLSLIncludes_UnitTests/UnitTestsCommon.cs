using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSLIncludes_UnitTests
{
    static class UnitTestsCommon
    {
        public static readonly string TEST_FOLDER = "TMP_TEST_FOLDER";
        public static readonly string TEST_SHADERS_FOLDER = "Shaders";

        public static void CopyToTestFolder(string filePath)
        {
            var fullPath = Path.GetDirectoryName(filePath);
            var folders = fullPath.Replace("\\", "/").Split('/');
            var curFolder = TEST_FOLDER;
            foreach (var folder in folders)
            {
                curFolder = Path.Combine(curFolder, folder);
                if (!Directory.Exists(curFolder))
                    Directory.CreateDirectory(curFolder);
            }
            File.Copy(Path.Combine(TEST_SHADERS_FOLDER, filePath),
                Path.Combine(TEST_FOLDER, filePath));
        }
        public static void CreateEmptyTestFolder()
        {
            CleanTestFolder();
            Directory.CreateDirectory(TEST_FOLDER);
        }
        public static void CleanTestFolder()
        {
            if (Directory.Exists(TEST_FOLDER))
                Directory.Delete(TEST_FOLDER, true);
        }
    }
}
