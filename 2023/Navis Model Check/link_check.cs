using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Autodesk.Navisworks.Api.Interop;
using System.Text.RegularExpressions;

namespace Navis_Model_Check
{
    //plugin attributes require Name, DeveloperID and optional parameters
    [PluginAttribute("Linked Models Check", "Derek B", DisplayName = "Linked Models Check", ToolTip = "Checks model links in file to saved list of models.", ExtendedToolTip = "Version 2023.1.0.0")]
    [AddInPluginAttribute(AddInLocation.AddIn, Icon = "C:\\Program Files\\Autodesk\\Navisworks Manage 2023\\Plugins\\Navis Model Check\\resources\\16x16_scale.bmp", 
        LargeIcon = "C:\\Program Files\\Autodesk\\Navisworks Manage 2023\\Plugins\\Navis Model Check\\resources\\32x32_scale.bmp")]

    public class Models : AddInPlugin
    {

        //// public static make this available in all cs files using Models.path or Models.document
        public static string path1 = @"C:\ProgramData\Autodesk\IPS Navis Model Check\Model List\";
        public static string path2 = "model_list.csv";
        public static Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
        public static string nFilePath = Autodesk.Navisworks.Api.Application.MainDocument.FileName.ToString();

        public override int Execute(params string[] parameters)
        {
            var regMatchNum = @"([0-9]{5})";
            Match regMatchnum = Regex.Match(nFilePath, regMatchNum);
            string projectNum = regMatchnum.Value;

            string path = path1 + @"\" + projectNum + path2;

            //string date = DateTime.Now.ToString("yyyy.MM.dd");

            //Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;

            //document.CurrentSelection.SelectAll();

            ////ModelItemCollection workingCollection2 = new ModelItemCollection();

            //List<string> navisModels = new List<string>();

            //foreach (ModelItem item in document.CurrentSelection.SelectedItems)
            //{
            //    string name = item.DisplayName;
            //    navisModels.Add(name);
            //    //workingCollection2.AddRange(item.Children);
            //}

            List<string> navisModels = getmodels.getnavismodels(document);
            //MessageBox.Show(workingCollection2.Count.ToString(), "item count collection 2");
            //MessageBox.Show(string.Join(", ", navisModels), "files found in model");


            if (File.Exists(path) != true)
            {
                //create file
                //write model list to file
                MessageBox.Show("No file with a model list is present. One will be created a populated with the current list of models.", "File not Found");
                //StringBuilder csvInfo = new StringBuilder();
                ////string[] headers = { "fileName", "date" };
                ////csvInfo.AppendLine(string.Join(",", headers));
                ////File.WriteAllText(path, csvheaders.ToString());

                ////var sb = new StringBuilder();

                //foreach (string mName in navisModels)
                //{
                //    string[] info = { mName };
                //    csvInfo.AppendLine(string.Join(",", info));
                //}
                //MessMessageBox.Show(string.Join(", ", sb));
                try
                {
                    writecsv.writeToCsv(navisModels, path);
                    //File.WriteAllText(path, csvInfo.ToString());
                    //MessageBox.Show(string.Join(", ", csvInfo), "Info saved to CSV.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "error");
                }

                string[] modelsFromFile = File.ReadAllLines(path);

                List<string> csvModels = new List<string>();
                foreach (string item in modelsFromFile)
                {
                    //MessageBox.Show(item, "model name to list");
                    csvModels.Add(item);
                }
                var displayForm = new model_compare(csvModels, navisModels);
                displayForm.ShowDialog();
            }
            else
            {
                //check current list to existing file
                //display changes
                //write new list of files if ok
                //MessageBox.Show("Files found", "file ?");
                string[] modelsFromFile = File.ReadAllLines(path);

                List<string> csvModels = new List<string>();
                foreach (string item in modelsFromFile)
                {
                    //MessageBox.Show(item, "model name to list");
                    csvModels.Add(item);
                }

                var displayForm = new model_compare(csvModels, navisModels);
                displayForm.ShowDialog();
            }

            return 0;
        }
    }
}
