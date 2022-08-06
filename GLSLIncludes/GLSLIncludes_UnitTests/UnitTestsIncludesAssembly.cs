using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLSLIncludes_UnitTests
{
    [TestClass]
    public class UnitTestsIncludesAssembly
    {
        [TestMethod]
        public void TestInclude_Local_NoPath()
        {
            // Include "file.h"
        }
        [TestMethod]
        public void TestInclude_Local_CurrentPath()
        {
            // Include "./file.h"
        }

        [TestMethod]
        public void TestInclude_Local_FullPath()
        {
            // Include "C:/Folder/file.h"
        }
    }
}
