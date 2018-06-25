using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinalniZadatak._3.zadatak
{
    public partial class FormChangeElParam : System.Windows.Forms.Form
    {
        private Document document;
        private Element selectedElement;
        private ElementId typeId;
        private List<Element> elements;
        private Parameter parameter;

        public FormChangeElParam(Document document, Element selectedElement)
        {
            InitializeComponent();
            this.document = document;
            this.selectedElement = selectedElement;
        }

        private void FormChangeElParam_Load(object sender, EventArgs e)
        {
            try
            {
                typeId = selectedElement.GetTypeId();
                FilteredElementCollector filter = new FilteredElementCollector(document);
                elements = filter.OfClass(typeof(FamilyInstance)).ToElements().ToList();
                elements = elements.Where(elem => elem.GetTypeId() == typeId).ToList();

                foreach (Parameter param in selectedElement.Parameters)
                {
                    treeViewParameters.Nodes.Add(param.Definition.Name);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void treeViewParameters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblParameter.Text = treeViewParameters.SelectedNode.Text + ": ";
            parameter = selectedElement.LookupParameter(treeViewParameters.SelectedNode.Text);
            switch (parameter.StorageType)
            {
                case StorageType.String:
                    txtParameter.Text = parameter.AsString();
                    break;
                default:
                    txtParameter.Text = parameter.AsValueString();
                    break;
            }
            
            if ((parameter.StorageType == StorageType.String) && !(parameter.IsReadOnly))
            {
                btnApply.Enabled = true;
            }
            else
                btnApply.Enabled = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                using (Transaction transaction = new Transaction(document))
                { 
                     transaction.Start("Change element parameters");
                     foreach (Element element in elements)
                     {
                         parameter = element.LookupParameter(treeViewParameters.SelectedNode.Text);
                         parameter.Set(txtParameter.Text);
                     }
                     transaction.Commit();
                     MessageBox.Show("Done", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }
        private void txtParameter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnApply.PerformClick();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
