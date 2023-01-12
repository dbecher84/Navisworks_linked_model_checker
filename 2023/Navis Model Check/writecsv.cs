using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navis_Model_Check
{
    public partial class writecsv
    {
        ////classe to write model names to csv file
        ////inputs list of file names as string and filepath as string
        public static void writeToCsv(List<string> inputList, string filepath)
        {
            StringBuilder csvInfo = new StringBuilder();
            foreach (string mName in inputList)
            {
                string[] info = { mName };
                csvInfo.AppendLine(string.Join(",", info));
                File.WriteAllText(filepath, csvInfo.ToString());
            }
        }
    }
}
