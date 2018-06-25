using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace FinalniZadatak._3.zadatak
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class ChangeElParam : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                UIDocument uiDocument = commandData.Application.ActiveUIDocument;

                Selection selection = uiDocument.Selection;
                Reference pickOne = selection.PickObject(ObjectType.Element, "Select one element from the model");
                Element selectedElement = null;

                if ((pickOne != null) && (pickOne.ElementId != ElementId.InvalidElementId))
                {
                    selectedElement = document.GetElement(pickOne);
                }
                using (FormChangeElParam formChangeElParam = new FormChangeElParam(document, selectedElement))
                {
                    formChangeElParam.ShowDialog();
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
