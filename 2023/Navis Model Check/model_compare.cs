using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navis_Model_Check
{
    public partial class model_compare : Form
    {
        public model_compare(List<string> FromFile, List<string> FoundList)
        {
            InitializeComponent();

            //string[] FromFile = File.ReadAllLines(Models.path);

            //List<string> cleanDisplay = new List<string>();

            foreach (string item in FromFile)
            {
            //    string fmodel = item.Substring(0, item.LastIndexOf(','));
            //    string fdate = item.Substring(item.LastIndexOf(',') + 1);
            //    //MessageBox.Show(fmodel + "----" + fdate);
            //    //cleanDisplay.Add(fmodel);
                listBox1.Items.Add(item);
            }
            foreach (string item2 in FoundList)
            {
                listBox2.Items.Add(item2);
            }

            foreach (string item3 in FoundList)
            {
                if (FromFile.Contains(item3) == false)
                {
                    listBox3.Items.Add(item3);
                }
                else
                {

                }
            }
            foreach (string item4 in FromFile)
            {
                if (FoundList.Contains(item4) == false)
                {
                    listBox4.Items.Add(item4);
                }
                else
                {

                }
            }
        }

        private void model_compare_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var regMatchNum = @"([0-9]{5})";
            Match regMatchnum = Regex.Match(Models.nFilePath, regMatchNum);
            string projectNum = regMatchnum.Value;

            string buttonPath = Models.path1 + @"\" + projectNum + Models.path2;

            File.Delete(buttonPath);

            List<string> newList = getmodels.getnavismodels(Models.document);
            writecsv.writeToCsv(newList, buttonPath);
            MessageBox.Show("The list of models has been updated", "File Updated");
        }
    }
}
