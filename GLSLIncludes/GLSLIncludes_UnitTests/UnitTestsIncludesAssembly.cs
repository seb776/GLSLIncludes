using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLSLIncludes_UnitTests
{
    [TestClass]
    public class UnitTestsIncludesAssembly
    {


        [TestMethod]
        public void TestInclude_Circular()
        {
            var testFile = "Shaders/Circular.glsl";

            bool failedTest = false;
            try
            {
                var result = GLSLIncludes.GLSLIncludes.ApplyIncludes(testFile);
                failedTest = true;
            }
            catch (Exception e)
            {

            }
            if (failedTest)
                throw new Exception();
       }

        [TestMethod]
        public void TestInclude_Diamond()
        {
            var testFile = "Shaders/Diamond.glsl";

            var result = GLSLIncludes.GLSLIncludes.ApplyIncludes(testFile);

        }
    }
}
