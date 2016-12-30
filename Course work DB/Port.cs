using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class Port : Form
    {
        private readonly int id;
        readonly bool edit;

        public Port()
        {
            InitializeComponent();
            edit = false;
        }
    
        public Port(int id, string location, string name) : this()
        {
            edit = true;
            this.id = id;
            textBox1.Text = location;
            textBox2.Text = name;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            label13.Visible = false;
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

            if (edit)
            {
                portTableAdapter1.UpdateQueryPort(textBox1.Text, textBox2.Text, id);               
            }
            else
            {
                portTableAdapter1.Insert(portTableAdapter1.GetData().Last().Id + 1, textBox1.Text, textBox2.Text);
            }
            portTableAdapter1.Fill(_Yacht_clubDataSet1.Port);
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
