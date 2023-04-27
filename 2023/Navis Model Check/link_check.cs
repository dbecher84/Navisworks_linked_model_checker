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

        //// public static make these available in all cs files. example = Models.path or Models.document
        public static string path1 = @"C:\ProgramData\Autodesk\IPS Navis Model Check\Model List\";
        public static string path2 = "model_list.csv";
        public static Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
        public static string nFilePath = Autodesk.Navisworks.Api.Application.MainDocument.FileName.ToString();

        public override int Execute(params string[] parameters)
        {
            ////search file path forr 5 digit project number for csv file name////////
            //var regMatchNum = @"([0-9]{5})";
            //Match regMatchnum = Regex.Match(nFilePath, regMatchNum);
            //string projectNum = regMatchnum.Value;
            ////////////////////////////////////////////////////////////////////////////

            ////use entire navis file name to account for multiple buildings///////////
            try
            {
                string navisFilePath = Autodesk.Navisworks.Api.Application.MainDocument.FileName.ToString();
                int startPosition = navisFilePath.LastIndexOf("\\") + 1;
                int endPosition = navisFilePath.LastIndexOf(".");
                string modelName = navisFilePath.Substring(startPosition, endPosition - startPosition);

                /////////////////////////////////////////////////////////////////////////////

                ////csv file path//////////////////////////////////////////////////////////
                string path = path1 + @"\" + modelName + path2;
                ///////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////
                try
                {
                    System.IO.Directory.CreateDirectory(path1);
                }
                catch (Exception e_csv)
                {
                    MessageBox.Show(e_csv.ToString(), "error creating folder for csv");
                }
                ///////////////////////////////////////////////////////////////////////////
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

                ////class to get list of models from navis file//////////////////////////////
                List<string> navisModels = getmodels.getnavismodels(document);
                /////////////////////////////////////////////////////////////////////////////

                //MessageBox.Show(workingCollection2.Count.ToString(), "item count collection 2");
                //MessageBox.Show(string.Join(", ", navisModels), "files found in model");


                if (File.Exists(path) != true)
                {
                    //create file
                    //write model list to file
                    MessageBox.Show("No file with a model list is present. One will be created a populated with the current list of models.", "File not Found");
                    //StringBuilder csvInfo = new StringBuilder();
                    //string[] headers = { "fileName", "date" };
                    //csvInfo.AppendLine(string.Join(",", headers));
                    //File.WriteAllText(path, csvheaders.ToString());

                    //var sb = new StringBuilder();

                    //foreach (string mName in navisModels)
                    //{
                    //    string[] info = { mName };
                    //    csvInfo.AppendLine(string.Join(",", info));
                    //}
                    //MessMessageBox.Show(string.Join(", ", sb));
                    try
                    {
                        ////classe to write model names to csv file/////////
                        writecsv.writeToCsv(navisModels, path);
                        ////////////////////////////////////////////////////
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
                    string[] modelsFromFile = File.ReadAllLines(path);

                    List<string> csvModels = new List<string>();
                    foreach (string item in modelsFromFile)
                    {
                        //MessageBox.Show(item, "model name to list");
                        csvModels.Add(item);
                    }

                    ////form to display list of models from saved csv and list of models found in navisworks///
                    var displayForm = new model_compare(csvModels, navisModels);
                    displayForm.ShowDialog();
                    ///////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            catch (Exception e_modelName)
            {
                MessageBox.Show(e_modelName.ToString(), "error fetching model name");
            }
           
            return 0;
        }
    }
}
