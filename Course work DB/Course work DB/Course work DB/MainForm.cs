using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Course_work_DB
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string Constring = Constr.GetConnectionString();
        
        //заполнение таблиц и статистик при запуске
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_Yacht_clubDataSet.Port' table. You can move, or remove it, as needed.
            this.portTableAdapter.Fill(this._Yacht_clubDataSet.Port);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet.Yacht_owner' table. You can move, or remove it, as needed.
            this.yacht_ownerTableAdapter.Fill(this._Yacht_clubDataSet.Yacht_owner);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet._Yacht_club_owner' table. You can move, or remove it, as needed.
            this.yacht_club_ownerTableAdapter.Fill(this._Yacht_clubDataSet._Yacht_club_owner);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet.Yacht' table. You can move, or remove it, as needed.
            this.yachtTableAdapter.Fill(this._Yacht_clubDataSet.Yacht);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet._Yacht_club' table. You can move, or remove it, as needed.
            this.yacht_clubTableAdapter.Fill(this._Yacht_clubDataSet._Yacht_club);
            // TODO: This line of code loads data into the '_Yacht_clubDataSet._Yacht_club' table. You can move, or remove it, as needed.
            this.yacht_clubTableAdapter.Fill(this._Yacht_clubDataSet._Yacht_club);

            //добавление удаление редактирование
            dataGridView1.DataSource = yachtBindingSource2;
            label1.Text = "Яхты";
            buttonPreviousTable.Visible = false;

            //фильтрация поиск сортировка
            dataGridView2.DataSource = yachtBindingSource2;

            //покупка яхт
            string strFirstCase = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner IS NULL";
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView5.DataSource = dt;        

            //cтатистики
            SqlCommand sql = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Type = 'motor'", sqlconn);
            label105.Text = sql.ExecuteScalar().ToString();

            SqlCommand sql2 = new SqlCommand("SELECT AVG(Price) FROM Yacht WHERE Type = 'motor'", sqlconn);
            label100.Text = sql2.ExecuteScalar().ToString();

            SqlCommand sql3 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Type = 'sailing'", sqlconn);
            label104.Text = sql3.ExecuteScalar().ToString();

            SqlCommand sql4 = new SqlCommand("SELECT AVG(Price) FROM Yacht WHERE Type = 'sailing'", sqlconn);
            label99.Text = sql4.ExecuteScalar().ToString();

            SqlCommand sql5 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Type = 'race'", sqlconn);
            label103.Text = sql5.ExecuteScalar().ToString();

            SqlCommand sql6 = new SqlCommand("SELECT AVG(Price) FROM Yacht WHERE Type = 'race'", sqlconn);
            label97.Text = sql6.ExecuteScalar().ToString();

            label133.Text = ((Convert.ToInt32(label105.Text) + Convert.ToInt32(label104.Text) + Convert.ToInt32(label103.Text))).ToString();
            label134.Text = Convert.ToInt32(((Convert.ToDouble(label100.Text) + Convert.ToDouble(label99.Text) + Convert.ToDouble(label97.Text))/3)).ToString() + ",0000";

            SqlCommand sql7 = new SqlCommand("SELECT AVG(Age) FROM [Yacht owner]", sqlconn);
            label95.Text = sql7.ExecuteScalar().ToString();

            SqlCommand sql22 = new SqlCommand("SELECT AVG(Bill) FROM [Yacht owner]", sqlconn);
            label144.Text = sql22.ExecuteScalar().ToString();

            SqlCommand sql23 = new SqlCommand("SELECT Name FROM [Yacht-club] WHERE Id = (SELECT [Yacht-Club] FROM [Yacht owner] GROUP BY [Yacht-Club] HAVING COUNT(Id) >= ALL(SELECT COUNT(Id) FROM [Yacht owner] GROUP BY [Yacht-club]));", sqlconn);
            label145.Text = sql23.ExecuteScalar().ToString();

            SqlCommand sql8 = new SqlCommand("SELECT SUM(Bill) FROM [Yacht owner]", sqlconn);
            label93.Text = sql8.ExecuteScalar().ToString();

            SqlCommand sql9 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Owner IS NOT NULL", sqlconn);
            label120.Text = sql9.ExecuteScalar().ToString();

            SqlCommand sql12 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Owner IS NULL", sqlconn);
            label116.Text = sql12.ExecuteScalar().ToString();

            SqlCommand sql13 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Location IS NOT NULL", sqlconn);
            label115.Text = sql13.ExecuteScalar().ToString();

            SqlCommand sql14 = new SqlCommand("SELECT COUNT(*) FROM Yacht WHERE Location IS NULL", sqlconn);
            label114.Text = sql14.ExecuteScalar().ToString();

            SqlCommand sql18 = new SqlCommand("SELECT AVG(Price) FROM Yacht WHERE Owner IS NOT NULL", sqlconn);
            label138.Text = sql18.ExecuteScalar().ToString();

            SqlCommand sql19 = new SqlCommand("SELECT AVG(Price) FROM Yacht WHERE Owner IS NULL", sqlconn);
            label139.Text = sql19.ExecuteScalar().ToString();

            SqlCommand sql20 = new SqlCommand("SELECT SUM(Price) FROM Yacht WHERE Location IS NOT NULL", sqlconn);
            label140.Text = sql20.ExecuteScalar().ToString();

            SqlCommand sql21 = new SqlCommand("SELECT SUM(Price) FROM Yacht WHERE Location IS NULL", sqlconn);
            label141.Text = sql21.ExecuteScalar().ToString();

            if(label141.Text == "")
                  label141.Text = "0,0000";

            //отчет
            SqlCommand sql15 = new SqlCommand("SELECT COUNT(*) FROM [Yacht owner] WHERE [Yacht-Club] = '1001'", sqlconn);
            label126.Text = sql15.ExecuteScalar().ToString();

            SqlCommand sql16 = new SqlCommand("SELECT COUNT(*) FROM [Yacht owner] WHERE [Yacht-Club] = '1002'", sqlconn);
            label125.Text = sql16.ExecuteScalar().ToString();

            SqlCommand sql17 = new SqlCommand("SELECT COUNT(*) FROM [Yacht owner] WHERE [Yacht-Club] = '1003'", sqlconn);
            label124.Text = sql17.ExecuteScalar().ToString();

            int sum = Convert.ToInt32(label126.Text) + Convert.ToInt32(label125.Text) + Convert.ToInt32(label124.Text);
            label122.Text = sum.ToString();

            sqlconn.Close();
        }

        //сохранение при выходе
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            yachtTableAdapter.Update(_Yacht_clubDataSet);
            yacht_ownerTableAdapter.Update(_Yacht_clubDataSet);
            yacht_clubTableAdapter.Update(_Yacht_clubDataSet);
            yacht_club_ownerTableAdapter.Update(_Yacht_clubDataSet);
            portTableAdapter.Update(_Yacht_clubDataSet);
        }

        //РАБОТА С ДАННЫМИ
        //редактирование данных
        private void updateSelected_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Яхты")
            {
                var st = new _Yacht_clubDataSet.YachtDataTable();
                yachtTableAdapter.FillBy1(st, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                object[] row = st.Rows[0].ItemArray;
                bool already = false;

                if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "" && dataGridView1.SelectedRows[0].Cells[9].Value.ToString() != "")
                {
                    var edit1 = new EditYacht(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), 0,
                    Convert.ToInt32(row[4]), Convert.ToInt32(row[5]), Convert.ToInt32(row[6]), Convert.ToInt32(row[7]),
                    Convert.ToInt32(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]));

                    edit1.ShowDialog();
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                    already = true;
                }

                if (!already && dataGridView1.SelectedRows[0].Cells[9].Value.ToString() == "" && dataGridView1.SelectedRows[0].Cells[3].Value.ToString() != "")
                {
                    var edit1 = new EditYacht(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToInt32(row[3]),
                    Convert.ToInt32(row[4]), Convert.ToInt32(row[5]), Convert.ToInt32(row[6]), Convert.ToInt32(row[7]),
                    Convert.ToInt32(row[8]), 0, Convert.ToInt32(row[10]));

                    edit1.ShowDialog();
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                    already = true;
                }

                if (!already && dataGridView1.SelectedRows[0].Cells[9].Value.ToString() == "" && dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "")
                {
                    var edit1 = new EditYacht(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), 0,
                    Convert.ToInt32(row[4]), Convert.ToInt32(row[5]), Convert.ToInt32(row[6]), Convert.ToInt32(row[7]),
                    Convert.ToInt32(row[8]), 0, Convert.ToInt32(row[10]));

                    edit1.ShowDialog();
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                    already = true;
                }
                if (!already && dataGridView1.SelectedRows[0].Cells[8].Value.ToString() != "" && dataGridView1.SelectedRows[0].Cells[3].Value.ToString() != "")
                {
                    var edit = new EditYacht(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToInt32(row[3]),
                    Convert.ToInt32(row[4]), Convert.ToInt32(row[5]), Convert.ToInt32(row[6]), Convert.ToInt32(row[7]),
                    Convert.ToInt32(row[8]), Convert.ToInt32(row[9]), Convert.ToInt32(row[10]));
                    edit.ShowDialog();
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                }
            }
            if (label1.Text == "Владельцы яхт")
            {
                var st1 = new _Yacht_clubDataSet.Yacht_ownerDataTable();
                yacht_ownerTableAdapter.FillByYachtOwner(st1, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                object[] row1 = st1.Rows[0].ItemArray;

                var edit1 = new YachtOwnerEdit(Convert.ToInt32(row1[0]), row1[1].ToString(), Convert.ToInt32(row1[2]), row1[3].ToString(),
                    Convert.ToInt32(row1[4]), Convert.ToInt32(row1[5]));
                edit1.ShowDialog();
                yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Яхт-клубы")
            {
                var st2 = new _Yacht_clubDataSet._Yacht_clubDataTable();
                yacht_clubTableAdapter.FillByYacht_club(st2, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                object[] row2 = st2.Rows[0].ItemArray;

                var edit2 = new YachtClubEdit(Convert.ToInt32(row2[0]), row2[1].ToString(), row2[2].ToString(), row2[3].ToString(), Convert.ToInt32(row2[4]));
                edit2.ShowDialog();
                yacht_clubTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Владельцы яхт-клубов")
            {
                var st3 = new _Yacht_clubDataSet._Yacht_club_ownerDataTable();
                yacht_club_ownerTableAdapter.FillByYacht_club_owner(st3, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                object[] row3 = st3.Rows[0].ItemArray;

                var edit3 = new YachtClubOwnerEdit(Convert.ToInt32(row3[0]), row3[1].ToString(), Convert.ToInt32(row3[2]),
                    row3[3].ToString(), Convert.ToInt32(row3[4]));
                edit3.ShowDialog();
                yacht_club_ownerTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club_owner);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Порты")
            {
                var str4 = new _Yacht_clubDataSet.PortDataTable();
                portTableAdapter.FillByPort(str4, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                object[] row4 = str4.Rows[0].ItemArray;

                var edit4 = new Port(Convert.ToInt32(row4[0]), row4[1].ToString(), row4[2].ToString());
                edit4.ShowDialog();
                portTableAdapter.Fill(_Yacht_clubDataSet.Port);
                _Yacht_clubDataSet.AcceptChanges();
            }
        }
        //удаление выделенных данных
        private void deleteSelected_Click(object sender, EventArgs e)
        {
            const string message = "Вы уверенны, что хотите удалить запись?";
            const string caption = "Удаление";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                if (label1.Text == "Яхты")
                {
                    yachtTableAdapter.DeleteQuery(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Владельцы яхт")
                {
                    yacht_ownerTableAdapter.DeleteQueryYachtOwner(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Яхт-клубы")
                {
                    yacht_clubTableAdapter.DeleteQueryYachtClub(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    yacht_clubTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Владельцы яхт-клубов")
                {
                    yacht_club_ownerTableAdapter.DeleteQueryYacht_club_owner(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    yacht_club_ownerTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club_owner);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Порты")
                {
                    portTableAdapter.DeleteQueryPort(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    portTableAdapter.Fill(_Yacht_clubDataSet.Port);
                    _Yacht_clubDataSet.AcceptChanges();
                }
            }
        }
        //удаление данных по id
        private void button4_Click(object sender, EventArgs e)
        {
            const string message = "Вы уверенны, что хотите удалить запись?";
            const string caption = "Удаление";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                textBoxIdToDelete.Text = "";
                return;

            }
            else
            {
                if (label1.Text == "Яхты")
                {
                    yachtTableAdapter.DeleteQuery(Convert.ToInt32(textBoxIdToDelete.Text));
                    yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Владельцы яхт")
                {
                    yacht_ownerTableAdapter.DeleteQueryYachtOwner(Convert.ToInt32(textBoxIdToDelete.Text));
                    yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Яхт-клубы")
                {
                    yacht_clubTableAdapter.DeleteQueryYachtClub(Convert.ToInt32(textBoxIdToDelete.Text));
                    yacht_clubTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Владельцы яхт-клубов")
                {
                    yacht_club_ownerTableAdapter.DeleteQueryYacht_club_owner(Convert.ToInt32(textBoxIdToDelete.Text));
                    yacht_club_ownerTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club_owner);
                    _Yacht_clubDataSet.AcceptChanges();
                }
                if (label1.Text == "Порты")
                {
                    portTableAdapter.DeleteQueryPort(Convert.ToInt32(textBoxIdToDelete.Text));
                    portTableAdapter.Fill(_Yacht_clubDataSet.Port);
                    _Yacht_clubDataSet.AcceptChanges();
                }
            }
            textBoxIdToDelete.Text = "";
        }
        //добавление данных
        private void AddNew_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Яхты")
            {
                var edit = new EditYacht();
                edit.ShowDialog();
                yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Владельцы яхт")
            {
                var editYachtOwner = new YachtOwnerEdit();
                editYachtOwner.ShowDialog();
                yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Яхт-клубы")
            {
                var editYachtClub = new YachtClubEdit();
                editYachtClub.ShowDialog();
                yacht_clubTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Владельцы яхт-клубов")
            {
                var editYachtClubOwner = new YachtClubOwnerEdit();
                editYachtClubOwner.ShowDialog();
                yacht_club_ownerTableAdapter.Fill(_Yacht_clubDataSet._Yacht_club_owner);
                _Yacht_clubDataSet.AcceptChanges();
            }
            if (label1.Text == "Порты")
            {
                var editPort = new Port();
                editPort.ShowDialog();
                portTableAdapter.Fill(_Yacht_clubDataSet.Port);
                _Yacht_clubDataSet.AcceptChanges();
            }


        }             
        
        //переключение вперед при просмотре таблиц
        private void buttonNextTable_Click(object sender, EventArgs e)
        {
            if(label1.Text == "Яхты")
            {
                dataGridView1.DataSource = yachtOwnerBindingSource;             
                label1.Text = "Владельцы яхт";
                buttonPreviousTable.Visible = true;
                return;
            }
            if (label1.Text == "Владельцы яхт")
            {
                dataGridView1.DataSource = yachtclubBindingSource;
                label1.Text = "Яхт-клубы";
                buttonPreviousTable.Visible = true;
                return;
            }
            if (label1.Text == "Яхт-клубы")
            {
                dataGridView1.DataSource = yachtclubOwnerBindingSource;
                label1.Text = "Владельцы яхт-клубов";
                buttonPreviousTable.Visible = true;
                return;
            }
            if (label1.Text == "Владельцы яхт-клубов")
            {
                dataGridView1.DataSource = portBindingSource;
                label1.Text = "Порты";
                buttonNextTable.Visible = false;
                buttonPreviousTable.Visible = true;
                return;              
            }
        }
        //переключение назад при просмотре таблиц
        private void buttonPreviousTable_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Владельцы яхт")
            {              
                dataGridView1.DataSource = yachtBindingSource;
                label1.Text = "Яхты";
                buttonPreviousTable.Visible = false;
                buttonNextTable.Visible = true;
                return;
            }
            if (label1.Text == "Яхт-клубы")
            {
                buttonPreviousTable.Visible = true;
                dataGridView1.DataSource = yachtOwnerBindingSource;
                label1.Text = "Владельцы яхт";
                buttonNextTable.Visible = true;
                return;
            }
            if (label1.Text == "Владельцы яхт-клубов")
            {
                buttonPreviousTable.Visible = true;
                dataGridView1.DataSource = yachtclubBindingSource;
                label1.Text = "Яхт-клубы";
                buttonNextTable.Visible = true;
                return;
            }
            if (label1.Text == "Порты")
            {
                buttonPreviousTable.Visible = true;
                buttonNextTable.Visible = true;
                dataGridView1.DataSource = yachtclubOwnerBindingSource;
                label1.Text = "Владельцы яхт-клубов";
                return;
            }
        }

        //ЗАДАЧА АВТОМАТИЗАЦИИ
        public string height;
        public string deck;
        public string width;
        public string length;
        public string longitude;
        public string latitude;
        public string name;
        //выбор пользователя при автоматизации
        private void buttonAcceptId_Click(object sender, EventArgs e)
        {
            textBoxOwnersId.BackColor = Color.White;
            if (textBoxOwnersId.Text == "")
            {
                textBoxOwnersId.BackColor = Color.LightSalmon;
                return;
            }

            string strFirstCase = "SELECT Id, Name, Height, Deck, Width, Length FROM Yacht WHERE Owner = ";
            strFirstCase += textBoxOwnersId.Text;

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView3.DataSource = dt;
            sqlconn.Close();
        }
        //выбор яхты при автоматизации
        private void button6_Click(object sender, EventArgs e)
        {           
            textBoxOwnersId.BackColor = Color.White;
            if (textBoxOwnersId.Text == "")
            {
                textBoxOwnersId.BackColor = Color.LightSalmon;
                return;
            }
            height = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            deck = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            width = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();
            length = dataGridView3.SelectedRows[0].Cells[5].Value.ToString();


            dataGridView3.Enabled = false;
            dataGridView3.BackgroundColor = Color.Gainsboro;
            button6.Visible = false;
            buttonChangeYacht.Visible = true;         
        }
        //изменение яхты при автоматизации
        private void buttonChangeYacht_Click(object sender, EventArgs e)
        {
            button6.Visible = true;
            buttonChangeYacht.Visible = false;
            dataGridView3.Enabled = true;
            dataGridView3.BackgroundColor = Color.WhiteSmoke;
        }
        //автоматизация реализация
        private void button5_Click(object sender, EventArgs e)
        {
            textBoxOwnersId.BackColor = Color.White;
            if (textBoxOwnersId.Text == "")
            {
                textBoxOwnersId.BackColor = Color.LightSalmon;
                return;
            }
            textBoxLatitude.BackColor = Color.White;
            textBoxLongitude.BackColor = Color.White;
            textBoxNamePlace.BackColor = Color.White;
            if((textBoxLatitude.Text == "" || textBoxLongitude.Text == "") && textBoxNamePlace.Text == "")
            {
                textBoxLatitude.BackColor = Color.LightSalmon;
                textBoxLongitude.BackColor = Color.LightSalmon;
                textBoxNamePlace.BackColor = Color.LightSalmon;
                return;
            }

            groupBox1.Visible = true;
            button5.Visible = false;
            button7.Visible = true;
            textBoxLatitude.Enabled = false;
            textBoxLongitude.Enabled = false;
            textBoxNamePlace.Enabled = false;
            buttonChangeYacht.Visible = false;

            latitude = textBoxLatitude.Text;
            longitude = textBoxLongitude.Text;
            name = textBoxNamePlace.Text;

            label26.Text = height;
            label27.Text = width;
            label28.Text = length;
            label50.Text = deck;

            if(textBoxLatitude.Text != "" || textBoxLongitude.Text != "")
            {
                label44.Text = latitude;
                label46.Text = longitude;
                label46.Visible = true;
                label44.Visible = true;
                label47.Visible = true;
                label48.Visible = true;
            }

            string nameTmp = "Место: " + name; 

            if(textBoxNamePlace.Text != "")
            {
                labelName.Text = nameTmp;
                labelName.Visible = true;
            }     
            if (latitude == "" && longitude == "")
            {
                labelName.Visible = true;
            }
            label40.Text = "";

            //попытка достать место из БД
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            string windDataBase = "";
            if ((textBoxLatitude.Text == "" || textBoxLongitude.Text == "") && textBoxNamePlace.Text != "")
            {
                windDataBase = meteostationTableAdapter1.ScalarQueryWind(textBoxNamePlace.Text).ToString();
            }
            if((textBoxLatitude.Text != "" && textBoxLongitude.Text != "") && textBoxNamePlace.Text == "")
            {
                try
                {
                    windDataBase = meteostationTableAdapter1.ScalarQueryWindXY(Convert.ToInt32(textBoxLatitude.Text), Convert.ToInt32(textBoxLongitude.Text)).ToString();
                }
                catch { }
            }

            label40.Text = windDataBase;

            string wind = label40.Text;
            if (label40.Text == "")
            {
                Random rand = new Random();
                wind = rand.Next(30).ToString();
                label40.Text = wind;
               
            }

            bool warning;
            double wave = Methods.GetWave(wind, height, deck, out warning);

            label39.Text = wave.ToString();

            if (warning)
            {
                label42.Text = "Осторожно, погодные условия в \n  данном регионе опасны для \n вашего судна. \n Рекомендуем воздержаться \n от этого путешествия!!";
                label42.ForeColor = Color.Red;
            }
            else
            {
                label42.Text = "Место имеет удовлетворительные \n условия для вашей яхты! \n Приятного путешествия!!";
                label42.ForeColor = Color.Green;
            }
        }
        //измениние данных о путешествии
        private void button7_Click(object sender, EventArgs e)
        {
            textBoxOwnersId.BackColor = Color.White;
            if (textBoxOwnersId.Text == "")
            {
                textBoxOwnersId.BackColor = Color.LightSalmon;
                return;
            }
            label46.Visible = false;
            label44.Visible = false;
            label47.Visible = false;
            label48.Visible = false;
            labelName.Visible = false;
            groupBox1.Visible = false;
            button5.Visible = true;
            button7.Visible = false;
            textBoxLatitude.Enabled = true;
            textBoxLongitude.Enabled = true;
            textBoxNamePlace.Enabled = true;
            buttonChangeYacht.Visible = true;
        }

        //ПОКУПКА И ПРОДАЖА
        private void button10_Click(object sender, EventArgs e)
        {
            textBox9.BackColor = Color.White;
            if (textBox9.Text == "")
            {
                textBox9.BackColor = Color.LightSalmon;
                return;
            }

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            label65.Visible = false;
            button9.Enabled = true;
            button9.BackColor = Color.SeaGreen;
            dataGridView5.Enabled = true;
            dataGridView5.BackgroundColor = Color.WhiteSmoke;

            string strFirstCase = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner = ";
            strFirstCase += textBox9.Text;
          
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView4.DataSource = dt;

            string performSql1 = "SELECT Bill FROM [Yacht owner] WHERE Id = " + textBox9.Text;
            SqlCommand sql = new SqlCommand(performSql1, sqlconn);
            label63.Text = sql.ExecuteScalar().ToString();
            
            double finalSum = Convert.ToDouble(label63.Text) - Convert.ToDouble(label69.Text);
            label64.Text = finalSum.ToString() + ",0000";
            if (finalSum < 0)
            {
                button9.Enabled = false;
                button9.BackColor = Color.Silver;
                label71.Visible = true;
            }
            else
            {
                button9.Enabled = true;
                button9.BackColor = Color.SeaGreen;
                label71.Visible = false;
            }
            sqlconn.Close();
        }
        //продажа
        private void button8_Click(object sender, EventArgs e)
        {
            textBox9.BackColor = Color.White;
            if (textBox9.Text == "")
            {
                textBox9.BackColor = Color.LightSalmon;
                return;
            }
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            int idSell = Convert.ToInt32(dataGridView4.CurrentRow.Cells[0].Value);
            int idOwner = Convert.ToInt32(textBox9.Text);
            string bill = label62.Text;
            char separator = ',';
            string[] intBill = bill.Split(separator);           

            yachtTableAdapter.UpdateQuerySellAndBuy(null, idSell);
            yacht_ownerTableAdapter.UpdateQueryOwnersBill(Convert.ToInt32(intBill[0]), idOwner);

            yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
            yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);

            _Yacht_clubDataSet.AcceptChanges();

            string strFirstCase2 = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner = ";
            strFirstCase2 += textBox9.Text;

            SqlDataAdapter oda2 = new SqlDataAdapter(strFirstCase2, sqlconn);
            DataTable dt2 = new DataTable();
            oda2.Fill(dt2);
            dataGridView4.DataSource = dt2;

            string strFirstCase = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner IS NULL";
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView5.DataSource = dt;

            sqlconn.Close();
        }
        //покупка
        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            int idBuy = Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value);
            int idOwner = Convert.ToInt32(textBox9.Text);

            string bill = label64.Text;
            char separator = ',';
            string[] intBill = bill.Split(separator);

            yachtTableAdapter.UpdateQuerySellAndBuy(idOwner, idBuy);
            yacht_ownerTableAdapter.UpdateQueryOwnersBill(Convert.ToInt32(intBill[0]), idOwner);

            yachtTableAdapter.Fill(_Yacht_clubDataSet.Yacht);
            yacht_ownerTableAdapter.Fill(_Yacht_clubDataSet.Yacht_owner);

            _Yacht_clubDataSet.AcceptChanges();

            string strFirstCase = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner IS NULL";
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView5.DataSource = dt;

            string strFirstCase2 = "SELECT Id, Name, Year, Price FROM Yacht WHERE Owner = ";
            strFirstCase2 += textBox9.Text;

            SqlDataAdapter oda2 = new SqlDataAdapter(strFirstCase2, sqlconn);
            DataTable dt2 = new DataTable();
            oda2.Fill(dt2);
            dataGridView4.DataSource = dt2;

            sqlconn.Close();
        }
        //выбор яхт для продажи
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn2 = new SqlConnection(Constring);
                sqlconn2.Open();

                string performSql1 = "SELECT Bill FROM [Yacht owner] WHERE Id = " + textBox9.Text;
                SqlCommand sql = new SqlCommand(performSql1, sqlconn2);
                label61.Text = sql.ExecuteScalar().ToString();
                sqlconn2.Close();
                string performSql2 = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                label70.Text = performSql2;
                double finalSum = Convert.ToDouble(label70.Text) + Convert.ToDouble(label61.Text);
                label62.Text = finalSum.ToString() + ",0000";
            }
            catch { }
        }
        //выбор яхт для покупки
        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn2 = new SqlConnection(Constring);
                sqlconn2.Open();

                string performSql1 = "SELECT Bill FROM [Yacht owner] WHERE Id = " + textBox9.Text;
                SqlCommand sql = new SqlCommand(performSql1, sqlconn2);
                label63.Text = sql.ExecuteScalar().ToString();

                string price = dataGridView5.CurrentRow.Cells[3].Value.ToString();
                label69.Text = price;
                double finalSum = Convert.ToDouble(label63.Text) - Convert.ToDouble(label69.Text);
                label64.Text = finalSum.ToString() + ",0000";
                if (finalSum < 0)
                {
                    button9.Enabled = false;
                    button9.BackColor = Color.Silver;
                    label71.Visible = true;
                }
                else
                {
                    button9.Enabled = true;
                    button9.BackColor = Color.SeaGreen;
                    label71.Visible = false;
                }
                sqlconn2.Close();
            }
            catch { }
        }
      
        //ПОИСК
        //активация расширенный поиск
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {         
           comboBox1.Enabled = true;

            if (!checkBox10.Checked)
                comboBox1.Enabled = false;           
        }
        //переключение между таблицами при поиске и фильтрации
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                dataGridView2.DataSource = yachtBindingSource2;
                label72.Text = "Яхты";
            }
            if (tabControl1.SelectedIndex == 1)
            {
                dataGridView2.DataSource = yachtclubBindingSource;
                label72.Text = "Яхт-клубы";
            }
            if (tabControl1.SelectedIndex == 2)
            {
                dataGridView2.DataSource = portBindingSource;
                label72.Text = "Порты";
            }
            if (tabControl1.SelectedIndex == 3)
            {
                dataGridView2.DataSource = yachtOwnerBindingSource;
                label72.Text = "Собственники яхт";
            }
            if (tabControl1.SelectedIndex == 4)
            {
                dataGridView2.DataSource = yachtclubOwnerBindingSource;
                label72.Text = "Собственники яхт-клубов";
            }
        }
        //поиск по яхтам
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Methods.CleanSearch(dataGridView2);          
            string search = searchTextBox.Text;
            //расширенный поиск
            if (checkBox10.Checked)
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.AdvanvedYachtSearch(dataGridView2, comboBox1, searchTextBox);
            }//обычный поиск
            else
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.Search(dataGridView2, searchTextBox.Text);
            }
        }
        //Очистка поиска по яхтам
        private void ClearSearching_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            Methods.CleanSearch(dataGridView2);
        }
        //поиск по яхт-клубам
        private void button11_Click(object sender, EventArgs e)
        {
            Methods.CleanSearch(dataGridView2);
            if (textBox10.Text == "")
                return;
            Methods.Search(dataGridView2, textBox10.Text);
        }
        //очистка поиска по яхт-клубам
        private void button2_Click_1(object sender, EventArgs e)
        {         
            textBox10.Text = "";
            Methods.CleanSearch(dataGridView2);
        }   
        //поиск по портам
        private void button13_Click(object sender, EventArgs e)
        {
            Methods.CleanSearch(dataGridView2);
            if (textBox11.Text == "")
                return;
            Methods.Search(dataGridView2, textBox11.Text);
        }
        //очистка поиска по портам
        private void button12_Click(object sender, EventArgs e)
        {
            textBox11.Text = "";
            Methods.CleanSearch(dataGridView2);
        }
        //поиск по владельцам яхт
        private void button15_Click(object sender, EventArgs e)
        {
            Methods.CleanSearch(dataGridView2);
            if (textBox12.Text == "")
                return;
            Methods.Search(dataGridView2, textBox12.Text);
        }
        //очистка поиска по владельцам яхт
        private void button14_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
            Methods.CleanSearch(dataGridView2);
        }
        //поиск по владельцам яхт-клубов
        private void button17_Click(object sender, EventArgs e)
        {
            Methods.CleanSearch(dataGridView2);
            if (textBox13.Text == "")
                return;
            Methods.Search(dataGridView2, textBox13.Text);
        }
        //очистка поиска по владельцам яхт-клубов
        private void button16_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            Methods.CleanSearch(dataGridView2);
        }
        //cохранение поиска при сортировке столбцов
        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                string search = searchTextBox.Text;
                //расширенный поиск
                if (checkBox10.Checked)
                {
                    if (searchTextBox.Text == "")
                        return;
                    Methods.AdvanvedYachtSearch(dataGridView2, comboBox1, searchTextBox);
                }//обычный поиск
                else
                {
                    if (searchTextBox.Text == "")
                        return;
                    Methods.Search(dataGridView2, searchTextBox.Text);
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (textBox10.Text == "")
                    return;
                Methods.Search(dataGridView2, textBox10.Text);
            }

            if (tabControl1.SelectedIndex == 2)
            {
                if (textBox11.Text == "")
                    return;
                Methods.Search(dataGridView2, textBox11.Text);
            }

            if (tabControl1.SelectedIndex == 3)
            {
                if (textBox12.Text == "")
                    return;
                Methods.Search(dataGridView2, textBox12.Text);
            }
            if (tabControl1.SelectedIndex == 4)
            {
                if (textBox13.Text == "")
                    return;
                Methods.Search(dataGridView2, textBox13.Text);
            }
        }

        //ФИЛЬТРАЦИЯ
        //фильтрация яхт
        private void button1_Click(object sender, EventArgs e)
        {
            //фильтр по типу
            string par = " Type = 'Sailing' ";
            string mot = " Type = 'Motor' ";
            string race = " Type = 'Race' ";

            //фильтр по годам
            string yearOne = " Year BETWEEN 0000 AND 2000 ";
            string yearTwo = " Year BETWEEN 2000 AND 2004 ";
            string yearThree = " Year BETWEEN 2005 AND 2009 ";
            string yearFour = " Year BETWEEN 2010 AND 3000 ";

            string strFirstCase = "SELECT * FROM Yacht ";
            //переменные для избежания повторяющихся проверок в запросе
            bool already2 = false;
            bool already3 = false;
            bool already5 = false;
            bool already6 = false;
            bool already7 = false;

            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked ||
                (textBox3.Text != "") || (textBox4.Text != "") || (textBox1.Text != "") || (textBox2.Text != "") ||
                (textBox5.Text != "") || (textBox6.Text != "") || (textBox7.Text != "") || (textBox8.Text != ""))
            {
                strFirstCase += "WHERE";
            }
            //тип
            if (checkBox1.Checked)
            {
                strFirstCase += par;
                if (checkBox2.Checked)
                {
                    already2 = true;
                    strFirstCase += "OR";
                    strFirstCase += mot;
                    if (checkBox3.Checked)
                    {
                        already3 = true;
                        strFirstCase += "OR";
                        strFirstCase += race;
                    }
                }
                if (checkBox3.Checked && !already3)
                {
                    already3 = true;
                    strFirstCase += "OR";
                    strFirstCase += race;
                }

            }
            if (checkBox2.Checked && !already2)
            {
                strFirstCase += mot;
                if (checkBox3.Checked && !already3)
                {
                    already3 = true;
                    strFirstCase += "OR";
                    strFirstCase += race;
                }
            }
            if (checkBox3.Checked && !already3)
            {
                strFirstCase += race;
            }

            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked)
            {
                if (checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked)
                {
                    strFirstCase += "AND";
                }
            }

            //год
            if (checkBox4.Checked)
            {
                strFirstCase += yearOne;
                if (checkBox5.Checked)
                {
                    already5 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearTwo;
                    if (checkBox6.Checked)
                    {
                        already6 = true;
                        strFirstCase += "OR";
                        strFirstCase += yearThree;
                        if (checkBox7.Checked)
                        {
                            already7 = true;
                            strFirstCase += "OR";
                            strFirstCase += yearFour;
                        }
                    }
                }
                if (checkBox6.Checked && !already6)
                {
                    already6 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearThree;
                    if (checkBox7.Checked)
                    {
                        already7 = true;
                        strFirstCase += "OR";
                        strFirstCase += yearFour;
                    }
                }
                if (checkBox7.Checked && !already7)
                {
                    already7 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearFour;

                }
            }

            if (checkBox5.Checked && !already5)
            {
                strFirstCase += yearTwo;
                if (checkBox6.Checked)
                {
                    already6 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearThree;
                    if (checkBox7.Checked)
                    {
                        already7 = true;
                        strFirstCase += "OR";
                        strFirstCase += yearFour;
                    }
                }
                if (checkBox7.Checked && !already7)
                {
                    already7 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearFour;
                }
            }

            if (checkBox6.Checked && !already6)
            {
                strFirstCase += yearThree;
                if (checkBox7.Checked)
                {
                    already7 = true;
                    strFirstCase += "OR";
                    strFirstCase += yearFour;
                }
            }
            if (checkBox7.Checked && !already7)
            {
                strFirstCase += yearFour;
            }
            //цена
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox5.Text != "") || (textBox6.Text != "") || (textBox7.Text != "") || (textBox8.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Price BETWEEN ";
                strFirstCase += textBox1.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox2.Text;
            }
            if (textBox1.Text != "" && textBox2.Text == "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox5.Text != "") || (textBox6.Text != "") || (textBox7.Text != "") || (textBox8.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Price > ";
                strFirstCase += textBox1.Text;
            }
            if (textBox1.Text == "" && textBox2.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox5.Text != "") || (textBox6.Text != "") || (textBox7.Text != "") || (textBox8.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Price < ";
                strFirstCase += textBox2.Text;
            }
            //размер
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Height BETWEEN ";
                strFirstCase += textBox3.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox4.Text;
            }
            if (textBox3.Text != "" && textBox4.Text == "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Height >= ";
                strFirstCase += textBox3.Text;
            }
            if (textBox3.Text == "" && textBox4.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Height <= ";
                strFirstCase += textBox4.Text;
            }

            if (textBox5.Text != "" && textBox6.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Width BETWEEN ";
                strFirstCase += textBox5.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox6.Text;
            }
            if (textBox5.Text != "" && textBox6.Text == "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Width >= ";
                strFirstCase += textBox5.Text;
            }
            if (textBox5.Text == "" && textBox6.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox1.Text != "") || (textBox2.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Width <= ";
                strFirstCase += textBox6.Text;
            }

            if (textBox7.Text != "" && textBox8.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox5.Text != "") || (textBox1.Text != "") || (textBox2.Text != "") || (textBox6.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Length BETWEEN ";
                strFirstCase += textBox7.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox8.Text;
            }
            if (textBox7.Text != "" && textBox8.Text == "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox1.Text != "") || (textBox2.Text != "") || (textBox5.Text != "") || (textBox6.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Length >= ";
                strFirstCase += textBox7.Text;
            }
            if (textBox7.Text == "" && textBox8.Text != "")
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked ||
                  checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || (textBox3.Text != "") || (textBox4.Text != "") ||
                (textBox1.Text != "") || (textBox2.Text != "") || (textBox5.Text != "") || (textBox6.Text != ""))
                {
                    strFirstCase += " AND ";
                }
                strFirstCase += " Length <= ";
                strFirstCase += textBox8.Text;
            }

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //сохранение поиска при фильтрации
            Methods.CleanSearch(dataGridView2);
            string search = searchTextBox.Text;

            //расширенный поиск
            if (checkBox10.Checked)
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.AdvanvedYachtSearch(dataGridView2, comboBox1, searchTextBox);
            }//обычный поиск
            else
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.Search(dataGridView2, searchTextBox.Text);
            }
        }
        //очистка фильтрации яхт
        private void button3_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM Yacht";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //сохранение поиска при фильтрации
            Methods.CleanSearch(dataGridView2);
            string search = searchTextBox.Text;

            //расширенный поиск
            if (checkBox10.Checked)
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.AdvanvedYachtSearch(dataGridView2, comboBox1, searchTextBox);
            }//обычный поиск
            else
            {
                if (searchTextBox.Text == "")
                    return;
                Methods.Search(dataGridView2, searchTextBox.Text);
            }
        }
        //фильтрация яхт-клуба
        private void button19_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht-club] ";
            if (textBox14.Text!="" && textBox15.Text != "")
            {
                strFirstCase += "WHERE Bill BETWEEN ";
                strFirstCase += textBox14.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox15.Text;
            }
            if (textBox14.Text != "" && textBox15.Text == "")
            {
                strFirstCase += "WHERE Bill > ";
                strFirstCase += textBox14.Text;
            }
            if (textBox14.Text == "" && textBox15.Text != "")
            {
                strFirstCase += "WHERE Bill < ";
                strFirstCase += textBox14.Text;
            }
            strFirstCase += ";";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox10.Text == "")
                return;
            Methods.Search(dataGridView2, textBox10.Text);

        }
        //очистка фильтрации яхт-клуба
        private void button18_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht-club]";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при очистке фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox10.Text == "")
                return;
            Methods.Search(dataGridView2, textBox10.Text);
        }
        //фильтрация порта
        private void button21_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM Port ";
            bool already = false;
            if (checkBox8.Checked)
            {
                strFirstCase += "WHERE Location = N'Украина'";
                if (checkBox9.Checked)
                {
                    strFirstCase += " OR ";
                    strFirstCase += " Location = N'Россия'";
                    already = true;
                }
            }
            if(checkBox9.Checked && !already)
            {
                strFirstCase += " WHERE Location = N'Россия'";
            }
            strFirstCase += ";";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox11.Text == "")
                return;
            Methods.Search(dataGridView2, textBox11.Text);
        }
        //очистка фильтрации порта
        private void button20_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM Port";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при очистке фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox11.Text == "")
                return;
            Methods.Search(dataGridView2, textBox11.Text);
        }
        //фильтрация собственника яхты
        private void button23_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht owner] ";

            if (textBox17.Text != "" && textBox16.Text != "")
            {
                strFirstCase += "WHERE Bill BETWEEN ";
                strFirstCase += textBox17.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox16.Text;
            }
            if (textBox17.Text != "" && textBox16.Text == "")
            {
                strFirstCase += "WHERE Bill > ";
                strFirstCase += textBox17.Text;
            }
            if (textBox17.Text == "" && textBox16.Text != "")
            {
                strFirstCase += "WHERE Bill < ";
                strFirstCase += textBox16.Text;
            }

            if(textBox19.Text != "" && textBox18.Text != "")
            {
                if(textBox17.Text != "" || textBox16.Text != "")
                {
                    strFirstCase += " AND Age BETWEEN ";
                }
                else
                {
                    strFirstCase += " WHERE Age BETWEEN ";
                }
                
                strFirstCase += textBox19.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox18.Text;
            }

            if(textBox19.Text != "" && textBox18.Text == "")
            {
                if (textBox17.Text != "" || textBox16.Text != "")
                {
                    strFirstCase += " AND Age > ";
                }
                else
                {
                    strFirstCase += " WHERE Age > ";
                }
                strFirstCase += textBox19.Text;
            }
            if (textBox19.Text == "" && textBox18.Text != "")
            {
                if (textBox17.Text != "" || textBox16.Text != "")
                {
                    strFirstCase += "  AND Age < ";
                }
                else
                {
                    strFirstCase += " WHERE Age < ";
                }
                strFirstCase += textBox18.Text;
            }

            strFirstCase += ";";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox12.Text == "")
                return;
            Methods.Search(dataGridView2, textBox12.Text);
        }
        //очистка фильтрации собственника яхты
        private void button22_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht owner]";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при очистке фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox12.Text == "")
                return;
            Methods.Search(dataGridView2, textBox12.Text);
        }
        //фильтрация собственника яхт-клуба
        private void button25_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht-club owner] ";

            if (textBox23.Text != "" && textBox22.Text != "")
            {
                strFirstCase += "WHERE Bill BETWEEN ";
                strFirstCase += textBox23.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox22.Text;
            }
            if (textBox23.Text != "" && textBox22.Text == "")
            {
                strFirstCase += "WHERE Bill > ";
                strFirstCase += textBox23.Text;
            }
            if (textBox23.Text == "" && textBox22.Text != "")
            {
                strFirstCase += "WHERE Bill < ";
                strFirstCase += textBox22.Text;
            }

            if (textBox21.Text != "" && textBox20.Text != "")
            {
                if (textBox23.Text != "" || textBox22.Text != "")
                {
                    strFirstCase += " AND Age BETWEEN ";
                }
                else
                {
                    strFirstCase += " WHERE Age BETWEEN ";
                }

                strFirstCase += textBox21.Text;
                strFirstCase += " AND ";
                strFirstCase += textBox20.Text;
            }

            if (textBox21.Text != "" && textBox20.Text == "")
            {
                if (textBox23.Text != "" || textBox22.Text != "")
                {
                    strFirstCase += " AND Age > ";
                }
                else
                {
                    strFirstCase += " WHERE Age > ";
                }
                strFirstCase += textBox21.Text;
            }
            if (textBox21.Text == "" && textBox20.Text != "")
            {
                if (textBox23.Text != "" || textBox22.Text != "")
                {
                    strFirstCase += "  AND Age < ";
                }
                else
                {
                    strFirstCase += " WHERE Age < ";
                }
                strFirstCase += textBox20.Text;
            }

            strFirstCase += ";";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox13.Text == "")
                return;
            Methods.Search(dataGridView2, textBox13.Text);
        }
        //очистка фильтрации собственника яхт-клуба
        private void button24_Click(object sender, EventArgs e)
        {
            string strFirstCase = "SELECT * FROM [Yacht-club owner]";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();
            SqlDataAdapter oda = new SqlDataAdapter(strFirstCase, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlconn.Close();

            //оставить поиск при очистке фильтрации
            Methods.CleanSearch(dataGridView2);
            if (textBox13.Text == "")
                return;
            Methods.Search(dataGridView2, textBox13.Text);
        }

        //отчет pdf
        private void button26_Click(object sender, EventArgs e)
        {
            Methods.GetPDFReport(label126, label125, label124, label122);
            MessageBox.Show("Отчет был создан");
        }

        //отчет html
        private void button27_Click(object sender, EventArgs e)
        {
            Methods.GetHTMLReport(label128, label129, label127, label123, label126, label125, label124, label122);
            MessageBox.Show("Отчет был создан");    
        }

        //отчет по выбранному клубу
        private void button28_Click(object sender, EventArgs e)
        {
            textBox24.BackColor = Color.White;
            if (textBox24.Text == "")
            {
                textBox24.BackColor = Color.LightSalmon;
                return;
            }
            label166.Text = "Яхт-клуб: ";

            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            string owner = textBox24.Text;

            SqlCommand sql1 = new SqlCommand("SELECT [Name] FROM [Yacht-club] WHERE Owner =" + owner + ";", sqlconn);
            string club = sql1.ExecuteScalar().ToString();
            label166.Text += sql1.ExecuteScalar().ToString();

            SqlCommand sql2 = new SqlCommand("SELECT [Full Name] FROM [Yacht-club owner] WHERE Id =" + owner + ";", sqlconn);
            label160.Text = sql2.ExecuteScalar().ToString();

            SqlCommand sql3 = new SqlCommand("SELECT [Bill] FROM [Yacht-club] WHERE Owner =" + owner + ";", sqlconn);
            label161.Text = sql3.ExecuteScalar().ToString();

            SqlCommand sql4 = new SqlCommand("SELECT [Name] FROM [Port] WHERE Id IN(SELECT Port FROM [Yacht-club] WHERE Owner IN(SELECT Id FROM [Yacht-club owner] WHERE Id =" + owner + ")); ", sqlconn);
            label162.Text = sql4.ExecuteScalar().ToString();

            SqlCommand sql5 = new SqlCommand("SELECT COUNT(*) FROM [Yacht owner] WHERE [Yacht-club] IN(SELECT Id FROM [Yacht-club] WHERE  Owner =" + owner + ");", sqlconn);
            label163.Text = sql5.ExecuteScalar().ToString();

            SqlCommand sql6 = new SqlCommand("SELECT AVG(Age) FROM [Yacht owner] WHERE [Yacht-club] IN(SELECT Id FROM [Yacht-club] WHERE  Owner =" + owner + ");", sqlconn);
            label164.Text = sql6.ExecuteScalar().ToString();

            SqlCommand sql7 = new SqlCommand("SELECT SUM(Bill) FROM [Yacht owner] WHERE [Yacht-club] IN(SELECT Id FROM [Yacht-club] WHERE  Owner =" + owner + ");", sqlconn);
            label165.Text = sql7.ExecuteScalar().ToString();

            sqlconn.Close();
        }
        //отчет html about one club
        private void button29_Click(object sender, EventArgs e)
        {
            textBox24.BackColor = Color.White;
            if (textBox24.Text == "")
            {
                textBox24.BackColor = Color.LightSalmon;
                return;
            }
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            SqlCommand sql1 = new SqlCommand("SELECT [Name] FROM [Yacht-club] WHERE Owner =" + textBox24.Text + ";", sqlconn);
            string club = sql1.ExecuteScalar().ToString();

            Methods.GetHTMLReportAboutOneClub(club, label160, label161, label162, label163, label164, label165);
            MessageBox.Show("Отчет был создан");

            sqlconn.Close();
        }
        //отчет pds about one club
        private void button30_Click(object sender, EventArgs e)
        {
            textBox24.BackColor = Color.White;
            if (textBox24.Text == "")
            {
                textBox24.BackColor = Color.LightSalmon;
                return;
            }
            SqlConnection sqlconn = new SqlConnection(Constring);
            sqlconn.Open();

            SqlCommand sql1 = new SqlCommand("SELECT [Name] FROM [Yacht-club] WHERE Owner =" + textBox24.Text + ";", sqlconn);
            string club = sql1.ExecuteScalar().ToString();

            Methods.GetPDFReportAboutOneClub(club, label160, label161, label162, label163, label164, label165);
            MessageBox.Show("Отчет был создан");

            sqlconn.Close();
        }
    }
}
