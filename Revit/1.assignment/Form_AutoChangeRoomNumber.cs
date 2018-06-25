using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FinalniZadatak._1.zadatak
{
    public partial class Form_AutoChangeRoomNumber : System.Windows.Forms.Form
    {
        private Document document;
        private List<Room> rooms;
        private int number;

        public Form_AutoChangeRoomNumber(Document doc, List<Room>rooms)
        {
            InitializeComponent();
            document = doc;
            this.rooms = rooms;
        }

        private void Form_ChangeRoomNumber_Load(object sender, EventArgs e)
        {
            ActiveControl = txtNumber;
        }
        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            bool tryParse = int.TryParse(txtNumber.Text, out number);
            if ((tryParse == true) && number > 0)
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
                    transaction.Start("Change rooms numbers");
                    foreach (Room room in rooms)
                    {
                        room.Number = number.ToString();
                        number++;
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
        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
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
