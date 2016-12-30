using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class YachtClubEdit : Form
    {
        private readonly int id;
        readonly bool edit;

        public YachtClubEdit()
        {
            InitializeComponent();
            yacht_club_ownerTableAdapter1.Fill(_Yacht_clubDataSet1._Yacht_club_owner);
            edit = false;
        }
        public YachtClubEdit(int id, string name, string owner, string port, int bill) : this()
        {
            yacht_club_ownerTableAdapter1.Fill(_Yacht_clubDataSet1._Yacht_club_owner);
            edit = true;
            this.id = id;
            textBox1.Text = name;
            comboBox1.SelectedValue = owner;
            comboBox2.SelectedValue = port;
            textBox2.Text = Convert.ToString(bill);         
        }

        private void YachtClubEdit_Load(object sender, EventArgs e)
        {
            yacht_club_ownerTableAdapter1.Fill(_Yacht_clubDataSet1._Yacht_club_owner);
            portTableAdapter1.Fill(_Yacht_clubDataSet1.Port);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            label13.Visible = false;
            label14.Visible = false;
            foreach (Control c in Controls)
            {
                if (c is TextBox || c is ComboBox)
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
            //вернуть если ошибки
            if (label14.Visible == true)
            {
                return;
            }

            if (edit)
            {
                yacht_clubTableAdapter1.UpdateQueryYacht_club(textBox1.Text, Convert.ToInt32(comboBox1.SelectedValue),
                    Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(textBox2.Text), id);
            }
            else
            {
                yacht_clubTableAdapter1.Insert(yacht_clubTableAdapter1.GetData().Last().Id + 1, textBox1.Text,
               Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue), Convert.ToInt32(textBox2.Text));
            }
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
