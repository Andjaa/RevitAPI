using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinalniZadatak._2.zadatak
{
    public partial class FormShowAllFurniture : System.Windows.Forms.Form
    {
        private Document document;
        private List<Element> furniture;
        public FormShowAllFurniture(Document document, List<Element> furniture)
        {
            InitializeComponent();
            this.document = document;
            this.furniture = furniture;
        }

        private void FormShowAllFurniture_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> typesDictionary = new Dictionary<int, string>();
            foreach (Element item in furniture)
            {
                try
                {
                    ElementId typeID = item.GetTypeId();
                    ElementType elementType = document.GetElement(typeID) as ElementType;
                    if (!typesDictionary.ContainsKey(typeID.IntegerValue))
                    {
                        typesDictionary.Add(typeID.IntegerValue, elementType.Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception", ex.Message);
                }
            }
        
            foreach (KeyValuePair<int, string> keyValue in typesDictionary)
            {
                TreeNode typeNode = new TreeNode(keyValue.Value);
                List<Element> tmp = new List<Element>();
                tmp = furniture.Where(x => x.GetTypeId().IntegerValue == keyValue.Key).ToList();
                foreach (Element element in tmp)
                {
                    TreeNode elementNode = new TreeNode(element.Name);
                    foreach (Parameter param in element.Parameters)
                    {
                        switch (param.StorageType)
                        {
                            case StorageType.String:
                                elementNode.Nodes.Add(param.Definition.Name + "=" + param.AsString());
                                break;
                            default:
                                elementNode.Nodes.Add(param.Definition.Name + "=" + param.AsValueString());
                                break;
                        }   
                    }
                    typeNode.Nodes.Add(elementNode);
                }
                treeViewFurniture.Nodes.Add(typeNode);
            }
        }
    }
}
