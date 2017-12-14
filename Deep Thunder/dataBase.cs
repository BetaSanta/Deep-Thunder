using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Drawing;
using System.Data.Common;

namespace Deep_Thunder
{
    public static class dataBase
    {
        public static int id_user;
        public static string path = Environment.CurrentDirectory + "\\DeepThunderDB.mdf";
        public static string sqlconnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30";

        public static bool enterAccount(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlQuery, sqlConn);
                DataTable dtTable = new DataTable();
                sqlDA.Fill(dtTable);
                var myData = dtTable.Select();
                if (dtTable.Rows.Count == 1)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                MetroFramework.MetroMessageBox.Show(AuthorizationForm.ActiveForm, "Произошла ошибка проверки логина и пароля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void addRecord(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                sqlConn.Open();
                SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
                try
                {
                    sqlcom.ExecuteNonQuery();
                    MetroFramework.MetroMessageBox.Show(CreateAccountForm.ActiveForm, "Аккаунт успешно создан, пройдите авторизацию", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information, 120);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(CreateAccountForm.ActiveForm, "Добавить запись не удалось", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void addReport(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                sqlConn.Open();
                SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
                try
                {
                    sqlcom.ExecuteNonQuery();
                    MetroFramework.MetroMessageBox.Show(MainForm.ActiveForm, "Отчет успешно сохранен.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information, 120);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
                MetroFramework.MetroMessageBox.Show(MainForm.ActiveForm, "Сохранить отчет не удалось.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void changePassword(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                sqlConn.Open();
                SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
                try
                {
                    sqlcom.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void updateData(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                sqlConn.Open();
                SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
                try
                {
                    sqlcom.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
            }
        }

        public static DataRow[] getData(string sqlQuery)
        {
            SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
            sqlConn.Open();
            SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
            sqlcom.ExecuteNonQuery();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            var myData = dt.Select();
            return myData;
        }


        public static void loadImage(string sqlQuery,string fileName)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlconnectionString);
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            SqlParameter sqlParameter = new SqlParameter("@img", SqlDbType.VarBinary);
            Image image = Image.FromFile(fileName);                                                                               //Изображение из файла.
            MemoryStream memoryStream = new MemoryStream();                                                                       //Поток в который запишем изображение
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            sqlParameter.Value = memoryStream.ToArray();
            sqlCommand.Parameters.Add(sqlParameter);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            memoryStream.Dispose();
        }

        public static Image getImage(string sqlQuery)
        {
            Image img = new Bitmap(1, 1);
            SqlConnection connection = new SqlConnection(sqlconnectionString);
            SqlCommand sqlCom = new SqlCommand(sqlQuery, connection);
            connection.Open();
            sqlCom.ExecuteNonQuery();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            foreach (DataRow rows in dt.Rows)
            {
                img = new Bitmap(new MemoryStream((byte[])rows["image"]));
            }

            return img;
        }

        public static void showDB(DataRow[] myData, DataGridView myTable)
        {

            if (myData.Length != 0)
            {
                myTable.RowCount = myData.Length;
                for (int i = 0; i < myData.Length; i++)
                {
                    for (int j = 0; j < myData[i].ItemArray.Length; j++)
                    {
                        if (j == 3)
                            myTable.Rows[i].Cells[j].Value = myData[i].ItemArray[j].ToString().Remove(10);
                        else
                            myTable.Rows[i].Cells[j].Value = myData[i].ItemArray[j];
                    }   
                }
            }
        }

        public static int checkUniqueLogin(string name)
        {
            SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
            string sqlQuery = "SELECT login FROM tbl_login WHERE login = '" + name + "' ";
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlQuery, sqlConn);
            DataTable dtTable = new DataTable();
            sqlDA.Fill(dtTable);
            if (dtTable.Rows.Count == 1)
                return 1;
            else
                return 0;
        }

        public static void deleteReports(string sqlQuery)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(sqlconnectionString);
                sqlConn.Open();
                SqlCommand sqlcom = new SqlCommand(sqlQuery, sqlConn);
                try
                {
                    sqlcom.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}