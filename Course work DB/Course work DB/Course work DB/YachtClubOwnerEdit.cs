using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class YachtClubOwnerEdit : Form
    {
        private readonly int id;
        readonly bool edit;

        public YachtClubOwnerEdit()
        {
            InitializeComponent();
            edit = false;
        }

        public YachtClubOwnerEdit(int id, string name, int age, string adress, int bill) : this()
        {
            edit = true;
            this.id = id;
            textBox1.Text = name;
            textBox2.Text = Convert.ToString(age);
            textBox3.Text = adress;
            textBox4.Text = Convert.ToString(bill);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            label13.Visible = false;
            label14.Visible = false;
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                    c.BackColor = Color.White;
            }
            //проверка на заполненость
            foreach (Control c in Controls)
            {
                if (c is TextBox && c.Text == "")
                {
                    c.BackColor = Color.LightSalmon;
                    label13.Visible = true;
                }
            }
            //вернуть если ошибки
            if (label13.Visible == true)
            {
                return;
            }
            //проверка на коректность
            int k;
            if (!(Int32.TryParse(textBox2.Text, out k)))
            {
                textBox2.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox4.Text, out k)))
            {
                textBox4.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if ((Int32.TryParse(textBox1.Text, out k)))
            {
                textBox1.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            //вернуть если ошибки
            if (label14.Visible == true)
            {
                return;
            }
            if (edit)
            {
                yacht_club_ownerTableAdapter1.UpdateQueryYacht_club_owner(textBox1.Text, Convert.ToInt32(textBox2.Text), textBox3.Text,
                    Convert.ToInt32(textBox4.Text), id);
                yacht_club_ownerTableAdapter1.Fill(_Yacht_clubDataSet1._Yacht_club_owner);
            }
            else
            {
                yacht_club_ownerTableAdapter1.Insert(yacht_club_ownerTableAdapter1.GetData().Last().Id + 1, textBox1.Text,
                    Convert.ToInt32(textBox2.Text), textBox3.Text, Convert.ToInt32(textBox4.Text));
                yacht_club_ownerTableAdapter1.Fill(_Yacht_clubDataSet1._Yacht_club_owner);
            }
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
