using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class YachtOwnerEdit : Form
    {
        private readonly int id;
        readonly bool edit;

        public YachtOwnerEdit()
        {
            InitializeComponent();
            yacht_clubTableAdapter1.Fill(_Yacht_clubDataSet._Yacht_club);
            edit = false;
        }

        public YachtOwnerEdit(int id, string name, int age, string address, int bill, int yachtClub) : this()
        {
            yacht_clubTableAdapter1.Fill(_Yacht_clubDataSet._Yacht_club);
            edit = true;
            this.id = id;
            textBox1.Text = name;
            textBox2.Text = Convert.ToString(age);
            textBox3.Text = address;
            textBox4.Text = Convert.ToString(bill);
            comboBox1.SelectedValue = yachtClub;
        }

        private void YachtOwnerEdit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_Yacht_clubDataSet._Yacht_club' table. You can move, or remove it, as needed.
            this.yacht_clubTableAdapter1.Fill(this._Yacht_clubDataSet._Yacht_club);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
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
                yacht_ownerTableAdapter1.UpdateQueryYachtOwner(id, textBox1.Text, Convert.ToInt32(textBox2.Text), textBox3.Text,
                    Convert.ToInt32(textBox4.Text), Convert.ToInt32(comboBox1.SelectedValue));               
            }
            else
            {
                yacht_ownerTableAdapter1.Insert(yacht_ownerTableAdapter1.GetData().Last().Id + 1, textBox1.Text, Convert.ToInt32(textBox2.Text), textBox3.Text,
                    Convert.ToInt32(textBox4.Text), Convert.ToInt32(comboBox1.SelectedValue));
            }
            Close();
        }       
    }
}
