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
        ////initials form to display models
        ////inputs model list from csv as string and model list from navis as string
        public model_compare(List<string> FromFile, List<string> FoundList)
        {
            InitializeComponent();

            foreach (string item in FromFile)
            {
                listBox1.Items.Add(item);
            }
            foreach (string item2 in FoundList)
            {
                listBox2.Items.Add(item2);
            }

            ////checks if file found in navis is in saved csv list
            ////if not model is added to box 3 (list of models added since check)
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

            ////checks if file found in saved csv list is in navis
            ////if not model is added to box 4 (list of models removed since check)
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

        //// button will replace an existing csv file wit a new one containing the updated model list
        private void button1_Click(object sender, EventArgs e)
        {
            ////searches model file path for project number for use in csv file name
            var regMatchNum = @"([0-9]{5})";
            Match regMatchnum = Regex.Match(Models.nFilePath, regMatchNum);
            string projectNum = regMatchnum.Value;

            ////path to save csv file
            string buttonPath = Models.path1 + @"\" + projectNum + Models.path2;

            ////delete existing csv file
            File.Delete(buttonPath);

            ////create new updated csv file
            List<string> newList = getmodels.getnavismodels(Models.document);
            writecsv.writeToCsv(newList, buttonPath);

            MessageBox.Show("The list of models has been updated", "File Updated");
        }
    }
}
