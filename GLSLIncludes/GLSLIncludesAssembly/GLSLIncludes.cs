using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GLSLIncludes
{
    public class IncludeNode
    {
        public IncludeNode(string filePath, IncludeNode parent = null)
        {
            //Includes = new Dictionary<string, IncludeNode>();
            FilePath = filePath;
            Parent = parent;
        }
        public string FilePath;
        public IncludeNode Parent;
        //public Dictionary<string, IncludeNode> Includes;

        public bool IsARootNode(string path)
        {
            IncludeNode current = this;
            while (true)
            {
                if (current == null)
                    break;
                if (current.FilePath == path)
                    return true;
                current = current.Parent;
            }
            return false;
        }
    }
    public static class GLSLIncludes
    {
        private static readonly Regex INCLUDE_REGEX = new Regex(@"^\s*\#include\s+["" <]([^"">]+)*["" >]");

        private static string _recurApplyIncludes(string srcFolder, string relativeFilePath, HashSet<string> absoluteFilesPath, IncludeNode currentNode)
        {
            var absoluteFilePath = Path.Combine(srcFolder, relativeFilePath);

            FileInfo f = new FileInfo(absoluteFilePath);
            string fullname = f.FullName;
            if (currentNode.Parent != null && currentNode.IsARootNode(fullname))// currentNode.Parent absoluteFilesPath.Contains(fullname)) // This avoids circular inclusion
                throw new Exception("File already included " + fullname); // TODO this will react to diamond inclusion and we don't want to
            absoluteFilesPath.Add(fullname);
            var currentCodeFile = File.ReadAllText(absoluteFilePath);
            StringBuilder outputFile = new StringBuilder();
            var lines = currentCodeFile.Split(new string[] { "\n", "\n\r" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var matchRes = INCLUDE_REGEX.Match(line);
                if (matchRes.Success)
                {
                    var includeFile = matchRes.Groups[1].Value;
                    var currentDirectory = Path.GetDirectoryName(absoluteFilePath);
                    var newIncludeNode = new IncludeNode(fullname, currentNode);
                    //currentNode.Includes.Add(fullname, newIncludeNode);
                    string includeCode = _recurApplyIncludes(currentDirectory, includeFile, absoluteFilesPath, newIncludeNode);
                    outputFile.AppendLine(includeCode);
                }
                else
                    outputFile.Append(line);
            }
            var outputCode = outputFile.ToString();
            outputCode = outputCode.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\r", "\n").Replace("\n", "\r\n");

            return outputCode;

        }

        /// <summary>
        /// Looks in the provided file for #include "file" and then applied it recursively.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Returns provided shader code with included code.</returns>
        public static string ApplyIncludes(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            string fullname = f.FullName;
            IncludeNode rootNode = new IncludeNode(fullname);
            return _recurApplyIncludes(Path.GetDirectoryName(filePath), Path.GetFileName(filePath), new HashSet<string>(), rootNode);
        }
    }
}
