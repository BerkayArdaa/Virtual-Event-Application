using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualEvent_SE410
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Goes to Creating page
        private void addEventButton_Click(object sender, EventArgs e)
        {
            AddEventForm addEventForm = new AddEventForm();
            addEventForm.Show();
            this.Hide();
        }

        //Goes to Manage Page
        private void manageButton_Click(object sender, EventArgs e)
        {
            ManageScreen m1 = new ManageScreen();
            m1.Show();
            this.Hide();
        }
    }
}
