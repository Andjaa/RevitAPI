using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FinalniZadatak._1.zadatak
{
    public partial class From_ChangeRoomNumber : System.Windows.Forms.Form
    {
        private Document document;
        private List<Room> rooms;
        private List<Room> selectedRooms = new List<Room>();
        private int number;
        private bool tryParse = false;

        public From_ChangeRoomNumber(Document document, List<Room> rooms)
        {
            InitializeComponent();
            this.document = document;
            this.rooms = rooms;
        }

        private void From_ChangeRoomNumber_Load(object sender, EventArgs e)
        { 
            foreach (Room room in rooms)
            {
                listBoxRooms.Items.Add(room);
                listBoxRooms.DisplayMember = nameof(room.Name);
            }
        }

        private void listBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((listBoxRooms.SelectedIndex == -1) || ((listBoxRooms.SelectedIndex != -1)&&tryParse==false))
            {
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
            }
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            tryParse = int.TryParse(txtNumber.Text, out number);
            if ((tryParse == true) && number > 0)
            {
                btnOK.Enabled = true;
            }
            else
                btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int count = listBoxRooms.Items.Count - 1;
                for (int i = 0; i <= count; i++)
                {
                    if (listBoxRooms.GetSelected(i))
                    {
                        selectedRooms.Add((Room)listBoxRooms.Items[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }

            try
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Change rooms numbers");
                    foreach (Room room in selectedRooms)
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
                btnOK.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
