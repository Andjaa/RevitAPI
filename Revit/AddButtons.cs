using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace FinalniZadatak
{
    public class AddButtons : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                RibbonPanel ribbonPanel = application.CreateRibbonPanel("Tools");
                string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

                //Pulldownbutton
                PulldownButtonData group1Data = new PulldownButtonData("pullDownButton1", "Room number");
                PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;

                //Button (Change selected rooms number)
                PushButtonData pushButtonData1 = new PushButtonData("pushButtonData1", "Changes room number", thisAssemblyPath, "FinalniZadatak._1.zadatak.ChangeRoomNumber");
                string uri1 = "pack://application:,,,/FinalniZadatak;component/Resources/changeRoomNum.png";
                string tooltip1 = "Changes number of selected rooms from list of all rooms in model";
                SetPushDownButton(group1, pushButtonData1, tooltip1, uri1);

                // Button ( Change all rooms numbers in model)
                PushButtonData pushButtonData2 = new PushButtonData("pushButtonData2", "Changes rooms numbers", thisAssemblyPath, "FinalniZadatak._1.zadatak.AutoChangeRoomsNumbers");
                string uri2 = "pack://application:,,,/FinalniZadatak;component/Resources/changeRoomsNums.png";
                string tooltip2 = "Changes numbers of all rooms in model automatically";
                SetPushDownButton(group1, pushButtonData2, tooltip2, uri2);

                group1.LargeImage = new BitmapImage(new Uri(uri1));


                //Button (List of furniture)
                PushButtonData pushButtonData3 = new PushButtonData("pushButtonData3", "Furniture", thisAssemblyPath, "FinalniZadatak._2.zadatak.Furniture");
                string uri3 = "pack://application:,,,/FinalniZadatak;component/Resources/furnitureList.png";
                string tooltip3 = "Show all parameters of furniture in active view sorted by type";
                SetPushButton(ribbonPanel, pushButtonData3, tooltip3, uri3);


                //Button (Change element parameter)
                PushButtonData pushButtonData4 = new PushButtonData("pushButtonData4", "Parameters", thisAssemblyPath, "FinalniZadatak._3.zadatak.ChangeElParam");
                string uri4 = "pack://application:,,,/FinalniZadatak;component/Resources/changeElemParam.png";
                string tooltip4 = "Allows user to select one element from model and change element parameter with string storage type and if parameter is not read-only.";
                tooltip4 += "This command will change same parameter to all elements in model with same type as selected element";
                SetPushButton(ribbonPanel, pushButtonData4, tooltip4, uri4);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Excepton", ex.Message);
            }

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }
        public static void SetPushButton(RibbonPanel panel, PushButtonData pushButtonData, string toolTip, string uri)
        {
            PushButton pushButton = panel.AddItem(pushButtonData) as PushButton;
            pushButton.ToolTip = toolTip;
            BitmapImage bitmapImage = new BitmapImage(new Uri(uri));
            pushButton.LargeImage = bitmapImage;
        }
        public static void SetPushDownButton(PulldownButton group, PushButtonData pushButtonData, string toolTip, string uri)
        {
            PushButton pushButton = group.AddPushButton(pushButtonData) as PushButton;
            pushButton.ToolTip = toolTip;
            BitmapImage bitmapImage = new BitmapImage(new Uri(uri));
            pushButton.LargeImage = bitmapImage;
        }

    }
}
