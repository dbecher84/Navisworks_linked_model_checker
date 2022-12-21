using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Navis_Model_Check
{
    public partial class getmodels
    {
        public static List<string> getnavismodels(Document navisDoc)
        {
            navisDoc.CurrentSelection.SelectAll();
            List<string> navisModellist = new List<string>();

            foreach (ModelItem item in navisDoc.CurrentSelection.SelectedItems)
            {
                string name = item.DisplayName;
                navisModellist.Add(name);
            }
            return navisModellist;
        }
    }
}
