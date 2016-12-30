using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class EditYacht : Form
    {
        private readonly int id;
        readonly bool edit;

        public EditYacht()
        {
            InitializeComponent();
            yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
            edit = false;
        }

        public EditYacht(int id, string name, string type, int owner, int year, int height, int deck, int width, int lenght, int location, int price) : this()
        {
            yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
            edit = true;

            this.id = id;
            textBox_Name.Text = name;
            comboBox_Type.SelectedValue = type;
            comboBox_Owner.SelectedValue = owner;
            textBox_Year.Text = Convert.ToString(year);
            textBox_Height.Text = Convert.ToString(height);
            textBox_DeckHeight.Text = Convert.ToString(deck);
            textBox_Width.Text = Convert.ToString(width);
            textBox_Length.Text = Convert.ToString(lenght);
            comboBox_Location.SelectedValue = location;
            textBox_Price.Text = Convert.ToString(price);
        }

        private void EditYacht_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_Yacht_clubDataSet.Port' table. You can move, or remove it, as needed.
            this.portTableAdapter.Fill(this._Yacht_clubDataSet.Port);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet.Yacht_owner' table. You can move, or remove it, as needed.
            this.yacht_ownerTableAdapter.Fill(this._Yacht_clubDataSet.Yacht_owner);          
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
                if (c is TextBox || c is ComboBox)
                    c.BackColor = Color.White;
            }
            //проверка на заполненость
            if(comboBox_Type.SelectedIndex == -1)
            {
                comboBox_Type.BackColor = Color.LightSalmon;
                label13.Visible = true;
            }
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
            if(!(Int32.TryParse(textBox_Year.Text, out k)))
            {
                textBox_Year.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox_Height.Text, out k)))
            {
                textBox_Height.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox_DeckHeight.Text, out k)))
            {
                textBox_DeckHeight.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox_Width.Text, out k)))
            {
                textBox_Width.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox_Length.Text, out k)))
            {
                textBox_Length.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            if (!(Int32.TryParse(textBox_Price.Text, out k)))
            {
                textBox_Price.BackColor = Color.LightSalmon;
                label14.Visible = true;
            }
            //вернуть если ошибки
            if(label14.Visible == true)
            {
                return;
            }

            string type = "";
            if(comboBox_Type.SelectedIndex == 0)
            {
                type = "Motor";
            }
            if(comboBox_Type.SelectedIndex == 1)
            {
                type = "Sailing";
            }
            if(comboBox_Type.SelectedIndex == 2)
            {
                type = "Race";
            }

            if (checkBox1.Checked)
            {
                if (edit)
                {
                    
                    yachtTableAdapter.UpdateQuery(textBox_Name.Text, type, null, Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       Convert.ToInt32(comboBox_Location.SelectedValue), Convert.ToInt32(textBox_Price.Text), id);
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);

                }
                else
                {
                    yachtTableAdapter.Insert(yachtTableAdapter.GetData().Last().Id + 1, textBox_Name.Text, type, null, Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       Convert.ToInt32(comboBox_Location.SelectedValue), Convert.ToInt32(textBox_Price.Text));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                Close();
            }
            if (checkBox2.Checked)
            {
                if (edit)
                {

                    yachtTableAdapter.UpdateQuery(textBox_Name.Text, type, Convert.ToInt32(comboBox_Owner.SelectedValue), Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       null, Convert.ToInt32(textBox_Price.Text), id);
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                else
                {
                    yachtTableAdapter.Insert(yachtTableAdapter.GetData().Last().Id + 1, textBox_Name.Text, type, Convert.ToInt32(comboBox_Owner.SelectedValue), Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       null, Convert.ToInt32(textBox_Price.Text));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                Close();
            }
            if (checkBox1.Checked && checkBox2.Checked)
            {
                if (edit)
                {

                    yachtTableAdapter.UpdateQuery(textBox_Name.Text, type, null, Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       null, Convert.ToInt32(textBox_Price.Text), id);
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                else
                {
                    yachtTableAdapter.Insert(yachtTableAdapter.GetData().Last().Id + 1, textBox_Name.Text, type, null, Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       null, Convert.ToInt32(textBox_Price.Text));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                Close();
            }
            if (!(checkBox1.Checked) && !(checkBox2.Checked))
            {
                if (edit)
                {
                    yachtTableAdapter.UpdateQuery(textBox_Name.Text, type, Convert.ToInt32(comboBox_Owner.SelectedValue), Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       Convert.ToInt32(comboBox_Location.SelectedValue), Convert.ToInt32(textBox_Price.Text), id);
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                else
                {
                    yachtTableAdapter.Insert(yachtTableAdapter.GetData().Last().Id + 1, textBox_Name.Text, type, Convert.ToInt32(comboBox_Owner.SelectedValue), Convert.ToInt32(textBox_Year.Text),
                       Convert.ToInt32(textBox_Height.Text), Convert.ToInt32(textBox_DeckHeight.Text), Convert.ToInt32(textBox_Width.Text), Convert.ToInt32(textBox_Length.Text),
                       Convert.ToInt32(comboBox_Location.SelectedValue), Convert.ToInt32(textBox_Price.Text));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                }
                Close();
            }                       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                comboBox_Owner.Enabled = false;
            comboBox_Owner.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                comboBox_Location.Enabled = false;
            comboBox_Location.Enabled = true;
        }
    }
}
