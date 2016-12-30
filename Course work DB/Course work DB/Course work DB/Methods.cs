using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_work_DB
{
    class Methods
    {
        //метод розрахунку висоти хвилі
        public static double GetWave(string wind,string height, string deck, out bool warning)
        {
            double wave = 0;
            double i = 0;
            warning = false;

            if (Convert.ToInt32(wind) > 0 && Convert.ToInt32(wind) < 6)
            {
                i = 1.1;
            }
            if (Convert.ToInt32(wind) > 5 && Convert.ToInt32(wind) < 11)
            {
                i = 1.2;
            }
            if (Convert.ToInt32(wind) > 10 && Convert.ToInt32(wind) < 16)
            {
                i = 1.4;
            }
            if (Convert.ToInt32(wind) > 15 && Convert.ToInt32(wind) < 21)
            {
                i = 1.75;
            }
            if (Convert.ToInt32(wind) > 20)
            {
                i = 2.2;
            }
            wave = (Convert.ToInt32(wind) * 0.2) * i;

            if (Convert.ToInt32(height) < 30)
            {
                if (wave > (3 / 4) * Convert.ToInt32(deck))
                {
                    if (((Convert.ToInt32(height) - Convert.ToInt32(deck)) > (Convert.ToInt32(deck) * 2)) || wave > Convert.ToInt32(height) + 0.5)
                    {
                        warning = true;
                    }
                }
            }
            return wave;
        }

        public static void CleanSearch(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
                for (int j = 0; j < dgv.Rows.Count; j++)
                    dgv.Rows[j].Cells[i].Style.BackColor = Color.White;
        }

        public static void Search(DataGridView dgv, string search)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
                for (int j = 0; j < dgv.Rows.Count; j++)
                    if (dgv.Rows[j].Cells[i].Value.ToString().ToLower().Contains(search.ToLower()))
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.Khaki;
        }

        public static void GetHTMLReport(Label firstName, Label secondName, Label thirdName, Label FourthName, Label FirstValue, Label SecondValue, Label ThirdValue, Label FourthValue)
        {
            StreamWriter w = new StreamWriter("E:\\Course work DB\\htmlReport.html");

            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine("<meta charset='UTF-8'>");
            w.WriteLine("</head>");
            w.WriteLine("<style type='text/css'>");
            w.WriteLine("body{background-color:#FFF8DC;}");
            w.WriteLine("</style>");
            w.WriteLine("<body>");

            w.WriteLine("<table style='width:100%;border:0px;cellpadding='100''>");
            w.WriteLine("<tr>");
            w.WriteLine("<td colspan='2' style='background-color:#85C2FF;'>");
            w.WriteLine("<h1 style='color:black; text-align:center;'>ОТЧЕТ О КОЛИЧЕСТВЕ ЛЮДЕЙ В ЯХТ-КЛУБАХ</h1>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom:1px dashed black;padding-top:90px;'>");
            w.WriteLine("<center><b><h2>" + firstName.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:200px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + FirstValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + secondName.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:200px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + SecondValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + thirdName.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:200px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + ThirdValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#EEDD82;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + FourthName.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#FFFFE0;height:200px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 90px;'>");
            w.WriteLine("<center><b><h2>" + FourthValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("</body>");
            w.WriteLine("</html>");

            w.Close();
        }

        public static void GetPDFReport(Label firstValue, Label secondValue, Label trirdValue, Label fourthValue)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Report.pdf", FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("Report about people who consists in the following clubs:", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 16f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            table.AddCell("Poseidon");
            table.AddCell(firstValue.Text);

            table.AddCell("Chernomorec");
            table.AddCell(secondValue.Text);

            table.AddCell("Veter");
            table.AddCell(trirdValue.Text);

            table.AddCell("All:");
            table.AddCell(fourthValue.Text);

            doc.Add(table);

            doc.Close();
        }

        public static void GetPDFReportAboutOneClub(string clubName, Label firstValue, Label secondValue, Label trirdValue, Label fourthValue, Label fifthValue, Label sixthValue)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("ReportAboutOneClub.pdf", FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(2);

            string title = "Report about club: "+ clubName;
            PdfPCell cell = new PdfPCell(new Phrase(title, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 16f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            table.AddCell("Owner:");
            table.AddCell(firstValue.Text);

            table.AddCell("Club bill:");
            table.AddCell(secondValue.Text);

            table.AddCell("Location:");
            table.AddCell(trirdValue.Text);

            table.AddCell("Participants:");
            table.AddCell(fourthValue.Text);

            table.AddCell("Avg age of participants:");
            table.AddCell(fifthValue.Text);

            table.AddCell("Sum bill of participants:");
            table.AddCell(sixthValue.Text);

            doc.Add(table);

            doc.Close();
        }

        public static void GetHTMLReportAboutOneClub(string clubName, Label firstValue, Label secondValue, Label trirdValue, Label fourthValue, Label fifthValue, Label sixthValue)
        {
            StreamWriter w = new StreamWriter("E:\\Course work DB\\htmlReportAboutOneClub.html");

            w.WriteLine("<html>");
            w.WriteLine("<head>");
            w.WriteLine("<meta charset='UTF-8'>");
            w.WriteLine("</head>");
            w.WriteLine("<style type='text/css'>");
            w.WriteLine("body{background-color:#FFF8DC;}");
            w.WriteLine("</style>");
            w.WriteLine("<body>");

            w.WriteLine("<table style='width:100%;border:0px;cellpadding='0''>");
            w.WriteLine("<tr>");
            w.WriteLine("<td colspan='2' style='background-color:#85C2FF;'>");
            w.WriteLine("<h1 style='color:black; text-align:center;'>ОТЧЕТ О ЯХТ-КЛУБЕ '" + clubName + "'</h1>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom:1px dashed black;padding-top:35px;'>");
            w.WriteLine("<center><b><h2>Владелец</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + firstValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>Счет клуба($)</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + secondValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>Местонахождение</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + trirdValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>Количество участников</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + fourthValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>Средний возраст участников</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + fifthValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("<tr style='vertical-align:top;'>");
            w.WriteLine("<td style='background-color:#C2EBFF;width:100px;text-align:top;padding-left:10px;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>Общая сумма денег участников</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("<td style='background-color:#F0FFFF;height:70px;width:200px;text-align:top;border-bottom: 1px dashed black;padding-top: 35px;'>");
            w.WriteLine("<center><b><h2>" + sixthValue.Text + "</h2></b></center>");
            w.WriteLine("</td>");
            w.WriteLine("</tr>");

            w.WriteLine("</body>");
            w.WriteLine("</html>");

            w.Close();
        }

        public static void AdvanvedYachtSearch(DataGridView dataGridView2, ComboBox comboBox1, TextBox searchTextBox)
        {
            string search = searchTextBox.Text;
            if (comboBox1.SelectedIndex == 0)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView2.Rows[j].Cells[1].Value.ToString().ToLower().Contains(search.ToLower()))
                        dataGridView2.Rows[j].Cells[1].Style.BackColor = Color.Khaki;
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView2.Rows[j].Cells[3].Value.ToString().ToLower().Contains(search.ToLower()))
                        dataGridView2.Rows[j].Cells[3].Style.BackColor = Color.Khaki;
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView2.Rows[j].Cells[9].Value.ToString().ToLower().Contains(search.ToLower()))
                        dataGridView2.Rows[j].Cells[9].Style.BackColor = Color.Khaki;
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    if (dataGridView2.Rows[j].Cells[10].Value.ToString().ToLower().Contains(search.ToLower()))
                        dataGridView2.Rows[j].Cells[10].Style.BackColor = Color.Khaki;
                }
            }
        }
    }
}
