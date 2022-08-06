using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSLIncludes
{
    public class Options
    {
        [Option('o', "output", Required = false, Min = 1, Max = 1, HelpText = "Provide output format. {fileNameNoExt} and {ext} can be used.")]
        public IEnumerable<string> Output { get; set; }

        [Value(0)]
        public IEnumerable<string> Props
        {
            get;
            set;
        }
    }
}
