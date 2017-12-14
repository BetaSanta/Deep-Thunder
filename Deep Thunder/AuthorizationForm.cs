using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Deep_Thunder
{
    public partial class AuthorizationForm : MetroFramework.Forms.MetroForm
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

       

        //слайдер, смены панелей при вводе логина/пароля
        private void sliderPanel(Panel panel1, Panel panel2, int speed)
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                //двигаем панели
                panel1.Left -= speed;
                panel2.Left -= speed;

                //останавливаем панели
                if (panel2.Left <= 86)
                    timer.Stop();  
                else
                {
                    if (panel1.Left >= 86)
                    {
                        timer.Stop();
                    }
                       
                }
            };
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Check user login
            if (!string.IsNullOrEmpty(tbLogin.Text))
            {
                string query = "SELECT login FROM tbl_login WHERE login = '" + tbLogin.Text.Trim() + "'";
                if (dataBase.enterAccount(query))
                {
                    sliderPanel(panelLogin, panelPass, 40);
                    btnBack.Visible = true;
                    cbRememberMe.Visible = true;

                    //загружает аватарку если есть
                    DataRow[] getImage = dataBase.getData("SELECT image FROM tbl_login WHERE login = '" + tbLogin.Text.Trim() + "'");
                    if (getImage[0].ItemArray[0].ToString() == "")
                        pictureBox1.Image = Properties.Resources.imageAccount;
                    else
                        pictureBox1.Image = dataBase.getImage("SELECT image FROM tbl_login WHERE login = '" + tbLogin.Text.Trim() + "'");
                }
                else
                    MetroFramework.MetroMessageBox.Show(this, "Неправильный логин", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Пожалуйста, введите данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEnterAccount_Click(object sender, EventArgs e)
        {
            //Check user password
            if (!string.IsNullOrEmpty(tbPass.Text))
            {
                string query = "SELECT id_login, login, password FROM tbl_login WHERE login = '" + tbLogin.Text.Trim() + "' AND password = '" + tbPass.Text.Trim() + "' ";
                if (dataBase.enterAccount(query))
                {
                    Properties.Settings.Default.Save();
                    DataRow[] myData = dataBase.getData(query);
                    Properties.Settings.Default.idLogin = Convert.ToInt32(myData[0].ItemArray[0]);

                    MainForm mainF = new MainForm();
                    this.Hide();
                    mainF.ShowDialog();
                    if (mainF.DialogResult == DialogResult.OK)
                    {
                        sliderPanel(panelLogin, panelPass, -40);
                        btnBack.Visible = false;
                        cbRememberMe.Visible = false;
                        tbPass.Clear();

                        this.Show();
                    }
                    else
                        Application.Exit();
                }
                else
                    MetroFramework.MetroMessageBox.Show(this, "Неправильный пароль", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Пожалуйста, введите данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            sliderPanel(panelLogin, panelPass, -40);
            btnBack.Visible = false;
            cbRememberMe.Visible = false;
            tbPass.Clear();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            CreateAccountForm createForm = new CreateAccountForm();
            this.Hide();
            createForm.ShowDialog();
            if(createForm.DialogResult == DialogResult.OK)
            {
                tbLogin.Text = "";
                tbPass.Text = "";
                cbRememberMe.Checked = false;
                this.Show();
            }     
        }

        private void btnCreateAccount_MouseEnter(object sender, EventArgs e)
        {
            btnCreateAccount.Font = new Font(btnCreateAccount.Font, FontStyle.Underline);
        }

        private void btnCreateAccount_MouseLeave(object sender, EventArgs e)
        {
            btnCreateAccount.Font = new Font(btnCreateAccount.Font, FontStyle.Regular);
        }

        private void AuthorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                MainForm mainF = new MainForm();
                this.Hide();
                mainF.ShowDialog();
                if (mainF.DialogResult == DialogResult.OK)
                    this.Show();
                else
                    Application.Exit();
            } 
        }
    }
}