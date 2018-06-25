using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace FinalniZadatak._2.zadatak
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class Furniture : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                View view = commandData.Application.ActiveUIDocument.ActiveView;
                ElementId viewId = view.Id;

                List<Element> furniture = new FilteredElementCollector(document, viewId)
                    .WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Furniture)).ToElements().ToList();
                if (furniture.Count == 0)
                {
                    TaskDialog.Show("Message", "This view does not contain furniture.");
                }
                else
                {
                    try
                    {
                        using (FormShowAllFurniture formShowFurniture = new FormShowAllFurniture(document, furniture))
                        {
                            formShowFurniture.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("Exception", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }

            return Result.Succeeded;
        }
    }
}
