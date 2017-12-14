using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deep_Thunder
{
    public partial class AccountSettingsForm : MetroFramework.Forms.MetroForm
    {
        public AccountSettingsForm()
        {
            InitializeComponent();
        }

        string fileName;
        DataRow[] myData;

        private void AccountSettingsForm_Load(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT * FROM tbl_login WHERE id_login = '" + Properties.Settings.Default.idLogin + "'";
            myData = dataBase.getData(sqlQuery);

            tbName.Text = myData[0].ItemArray[3].ToString();
            tbSurname.Text = myData[0].ItemArray[4].ToString();
            tbBirthday.Text = myData[0].ItemArray[6].ToString();
            tbGender.Text = myData[0].ItemArray[5].ToString();
            this.Text = "Персональная учетная запись: " + myData[0].ItemArray[1].ToString();

            if (myData[0].ItemArray[7].ToString() == "")
                pictureBox1.Image = Properties.Resources.imageAccount;
            else
                pictureBox1.Image = dataBase.getImage("SELECT image FROM tbl_login WHERE tbl_login.id_login = " + myData[0].ItemArray[0] + "");

            pictureBox1.Focus();
        }

        //Слайдер полей, когда пользователь нажимает на поле чтобы его редактировать, второй раз чтобы сохранить 
        private void sliderField(MetroFramework.Controls.MetroTextBox field, Label txtEdit, PictureBox btnEdit, bool foo, int speed)
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                if (foo)
                {
                    field.Left -= speed;
                    if (field.Left <= 90)
                    {
                        timer.Stop();
                        txtEdit.Visible = true;
                        btnEdit.Image = Properties.Resources.okay;
                        field.ReadOnly = false;
                    }
                }
                else
                {
                    field.Left += speed;
                    if (field.Left >= 160)
                    {
                        timer.Stop();
                        txtEdit.Visible = false;
                        btnEdit.Image = Properties.Resources.forward;
                        field.ReadOnly = true;
                    }
                }
            };
        }

        //слайдер, который вызывается при смене пароля 
        private void sliderPanel1(Panel panel, int speed, bool foo)
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                if (foo)
                {
                    panel.Top -= speed;
                    if (panel.Top <= 0)
                        timer.Stop();
                }
                else
                {
                    panel.Top += speed;
                    if (panel.Top >= 290)
                        timer.Stop();
                }
                
            };
        }

        //слайдер, который вызывается при смене пароля, после подтверждения пароля
        private void sliderPanel2(Panel panel, int speed)
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                panel.Left -= speed;
                if (panel.Left <= 0)
                    timer.Stop();
            };
        }

        private void btnEdit1_Click(object sender, EventArgs e)
        {
            if (!txtEdit1.Visible)
                sliderField(tbName, txtEdit1, btnEdit1, true, 20);
            else
                sliderField(tbName, txtEdit1, btnEdit1, false, 20);
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            if (!txtEdit2.Visible)
                sliderField(tbSurname, txtEdit2, btnEdit2, true, 20);
            else
                sliderField(tbSurname, txtEdit2, btnEdit2, false, 20);
        }

        private void btnEdit3_Click(object sender, EventArgs e)
        {
            if(!txtEdit3.Visible)
            {
                txtEdit3.Visible = true;
                btnEdit3.Image = Properties.Resources.okay;
                tbGender.Enabled = true;
            }
            else
            {
                txtEdit3.Visible = false;
                btnEdit3.Image = Properties.Resources.forward;
                tbGender.Enabled = false;
            }
        }

        private void bntEdit4_Click(object sender, EventArgs e)
        {
            if (!txtEdit4.Visible)
            {
                txtEdit4.Visible = true;
                btnEdit4.Image = Properties.Resources.okay;
                tbBirthday.Enabled = true;
            }
            else
            {
                txtEdit4.Visible = false;
                btnEdit4.Image = Properties.Resources.forward;
                tbBirthday.Enabled = false;
            }
        }

        private void btnChangePassword_MouseEnter(object sender, EventArgs e)
        {
            if (panel2.Top != -10) 
                btnChangePassword.ForeColor = Color.White;
        }

        private void btnChangePassword_MouseLeave(object sender, EventArgs e)
        {
            btnChangePassword.ForeColor = Color.Black;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (panel2.Top != -10)
            {
                sliderPanel1(panel2, 20, true);
                btnChangePassword.Cursor = Cursors.Default;
            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sliderPanel1(panel2, 20, false);
            panel3.Left = 380;
            btnNextPassword.Text = "Далее";
            btnChangePassword.Cursor = Cursors.Hand;
            tbOldPassword.Clear();
            metroTextBox2.Clear();
            metroTextBox3.Clear();
            metroLabel10.Visible = false;
            metroLabel11.Visible = false;
            metroLabel12.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbOldPassword.Text))
            {
                if (tbOldPassword.Text == myData[0].ItemArray[2].ToString())
                {
                    if (panel3.Left != 0)
                    {
                        sliderPanel2(panel3, 20);
                        btnNextPassword.Text = "Изменить";
                        metroLabel10.Visible = false;
                    }
                    else
                    {
                        //проверка на пустое поле нового пароля
                        if (string.IsNullOrEmpty(metroTextBox2.Text))
                            metroLabel12.Visible = true;
                        else
                            metroLabel12.Visible = false;

                        //проверка на путое поле подтверждения нового пароля
                        if (string.IsNullOrEmpty(metroTextBox3.Text))
                        {
                            metroLabel11.Text = "Поле должно быть заполнено.";
                            metroLabel11.Visible = true;
                        }
                        else if (metroTextBox2.Text != metroTextBox3.Text) //проверка на соответсвие паролей
                        {
                            metroLabel11.Text = "Пароли не совпадают.";
                            metroLabel11.Visible = true;
                        }
                        else
                        {
                            //запрос на изменение пароля
                            string sqlQueryChangePass = "UPDATE tbl_login SET password = N'" + metroTextBox3.Text.Trim() + "' WHERE tbl_login.id_login = " + myData[0].ItemArray[0] + "; ";
                            dataBase.changePassword(sqlQueryChangePass);

                            //запрос на получение нового пароля
                            string sqlQueryReturnNewData = "SELECT * FROM tbl_login WHERE id_login = '" + Properties.Settings.Default.idLogin + "'";
                            myData = dataBase.getData(sqlQueryReturnNewData);

                            //возвращаем положение компонентов
                            sliderPanel1(panel2, 20, false);
                            panel3.Left = 380;
                            btnNextPassword.Text = "Далее";
                            btnChangePassword.Cursor = Cursors.Hand;
                            tbOldPassword.Clear();
                            metroTextBox2.Clear();
                            metroTextBox3.Clear();
                            metroLabel10.Visible = false;
                            metroLabel11.Visible = false;
                            metroLabel12.Visible = false;
                        }
                    }
                }
                else
                {
                    metroLabel10.Visible = true;
                    metroLabel10.Text = "Неверный пароль. Повторите попытку.";
                }
            }
            else
            {
                metroLabel10.Visible = true;
                metroLabel10.Text = "Поле должно быть заполнено.";
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEdit1.Visible || txtEdit2.Visible || txtEdit3.Visible || txtEdit4.Visible)
                label8.Visible = true;
            else
            {
                label8.Visible = false;

                //запрос на изменение данных
                DateTime myDateTime = Convert.ToDateTime(tbBirthday.Text);
                var sqlFormattedDate = myDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss");
                string sqlQueryUpdateDate = "UPDATE tbl_login SET first_name = N'" + tbName.Text.Trim() + "', last_name = N'" + tbSurname.Text.Trim() + "', gender = N'" + tbGender.Text.Trim() + "', birthday = N'" + sqlFormattedDate + "' WHERE tbl_login.id_login = " + myData[0].ItemArray[0] + ";";
                dataBase.updateData(sqlQueryUpdateDate);

                //запись картинки пользователя
                if (!string.IsNullOrEmpty(fileName))
                {
                    string sqlQuery = "UPDATE tbl_login SET image = @img WHERE tbl_login.id_login = " + myData[0].ItemArray[0] + "";
                    dataBase.loadImage(sqlQuery, fileName);
                }
                    
                this.DialogResult = DialogResult.OK;
            }
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {
            //открытие диалогового окна для выбора аватара
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "корабль(*.JPG;*.PNG)|*.JPG;*.PNG";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            //если выбрали нужный аватар
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
