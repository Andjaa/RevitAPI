using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;

namespace FinalniZadatak._1.zadatak
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class ChangeRoomNumber : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;

                FilteredElementCollector filteredElementCollector = new FilteredElementCollector(document);
                RoomFilter filter = new RoomFilter();

                List<Room> rooms = filteredElementCollector.WherePasses(filter).OfType<Room>().ToList();
                if (rooms.Count == 0)
                {
                    TaskDialog.Show("Message", "This model does not contain rooms");
                }
                else
                {
                    try
                    {
                        using (From_ChangeRoomNumber formChangeRoomNumber = new From_ChangeRoomNumber(document, rooms))
                        {
                            formChangeRoomNumber.ShowDialog();
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
