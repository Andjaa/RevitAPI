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
    class AutoChangeRoomsNumbers : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
           
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                FilteredElementCollector filtereElementCollector = new FilteredElementCollector(document);
                RoomFilter roomFilter = new RoomFilter();

                List<Room> rooms = filtereElementCollector.WherePasses(roomFilter).OfType<Room>().ToList();
                rooms = rooms.OrderBy(r => r.Level.Name).ToList();
                if (rooms.Count == 0)
                {
                    TaskDialog.Show("Message", "This model does not contain rooms.");
                }
                else
                {
                    try
                    {
                        using (Form_AutoChangeRoomNumber formAutoChangeRoomNum = new Form_AutoChangeRoomNumber(document, rooms))
                        {
                            formAutoChangeRoomNum.ShowDialog();
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
