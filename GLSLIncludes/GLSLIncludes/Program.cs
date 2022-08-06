using CommandLine;
using System;
using System.IO;
using System.Linq;

namespace GLSLIncludes
{
    public class Program
    {
        public static readonly string FILENAME_MACRO = "{fileNameNoExt}";
        public static readonly string FILENAME_EXT_MACRO = "{ext}";
        public static int DoIncludes(string[] args, bool outputFiles = true)
        {
            int returnCode = 0;
            try
            {
                returnCode = CommandLine.Parser.Default
                    .ParseArguments<Options>(args)
                    .MapResult((o) =>
                    {
                        if (o.Files.Count() == 0)
                        {
                            throw new Exception("You have to provide at least one file.");
                        }
                        if (o.Files.Count() > 1)
                        {
                            if (o.Output.Count() == 0 || !o.Output.First().Contains(FILENAME_MACRO))
                            {
                                throw new Exception($"You have to specify {FILENAME_MACRO} with -o or --output when using multiple files.");
                            }
                        }
                        foreach (var file in o.Files)
                        {
                            try // We do not want a failed include to fail other files
                            {
                                var inputFileDirectory = Path.GetDirectoryName(file);
                                var inputFileNameNoExt = Path.GetFileNameWithoutExtension(file);
                                var inputFileExt = Path.GetExtension(file);
                                string outputFileName = inputFileNameNoExt + inputFileExt + ".generated";
                                if (o.Output.Count() > 0)
                                {
                                    outputFileName = o.Output.First()
                                        .Replace(FILENAME_MACRO, inputFileNameNoExt)
                                        .Replace(FILENAME_EXT_MACRO, inputFileExt);
                                }
                                string outputFilePath = Path.Combine(inputFileDirectory, outputFileName);
                                string appliedIncludeCodeFile = GLSLIncludes.ApplyIncludes(file);
                                if (outputFiles)
                                    File.WriteAllText(outputFilePath, appliedIncludeCodeFile);
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine(e.Message);
                            }
                        }
                        return 0;
                    }, (err) =>
                    {
                        Console.Error.WriteLine(err);
                        return -1;
                    }
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                returnCode = -1;
            }
            return returnCode;
        }
        static int Main(string[] args)
        {
            int returnCode = DoIncludes(args);

#if DEBUG
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif // !DEBUG
            return returnCode;
        }
    }
}
