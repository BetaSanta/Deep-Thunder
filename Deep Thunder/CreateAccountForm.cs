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
using System.Text.RegularExpressions;

namespace Deep_Thunder
{
    public partial class CreateAccountForm : MetroFramework.Forms.MetroForm
    {
        public CreateAccountForm()
        {
            InitializeComponent();
        }

        string[] warningString = new string[] {"Это поле должно быть заполнено.", "Используйте буквы латинского алфавита.", "Пароли не совпадают. Повторите попытку.", "Это имя пользователя уже занято." };
        bool foo = false;

        private void createAccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkingField();

            //всё удовлетворяет условиям, можно создать аккаунт
            if (foo)
            {
                DateTime myDateTime = Convert.ToDateTime(tbBirthday.Text);
                var sqlFormattedDate = myDateTime.Date.ToString("yyyy-MM-dd");

                string sqlQuery = "INSERT INTO tbl_login (login, password, first_name, last_name, gender, birthday) VALUES (N'" + tbLogin.Text.Trim() + "',N'" + tbPass.Text.Trim() + "',N'" + tbName.Text.Trim() + "',N'" + tbSurname.Text.Trim() + "',N'" + tbGender.Text.Trim() + "', '" + sqlFormattedDate + "')";
                dataBase.addRecord(sqlQuery);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void checkingField()
        {
            bool w1 = false, w2 = false, w3 = false, w4 = false, w5 = false;

            //check name and surname
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbSurname.Text)) 
            {
                txtWarning1.Visible = true;
                txtWarning1.Text = warningString[0];
            }
            else
            {
                txtWarning1.Visible = false;
                w1 = true;
            }

            //check login 
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                txtWarning2.Visible = true;
                txtWarning2.Text = warningString[0];
            }
            else if (Regex.Match(tbLogin.Text, @"[^a-zA-Z]").Success)
            {
                txtWarning2.Visible = true;
                txtWarning2.Text = warningString[1];
            }
            else if (dataBase.checkUniqueLogin(tbLogin.Text.Trim()) != 0)  
            {
                txtWarning2.Visible = true;
                txtWarning2.Text = warningString[3];
            }
            else
            {
                txtWarning2.Visible = false;
                w2 = true;
            }

            //check password
            if(string.IsNullOrEmpty(tbPass.Text))
            {
                txtWarning3.Visible = true;
                txtWarning3.Text = warningString[0];
            }
            else
            {
                txtWarning3.Visible = false;
                w3 = true;
            }

            //check confirm password
            if (string.IsNullOrEmpty(tbConfirmPass.Text))
            {
                txtWarning4.Visible = true;
                txtWarning4.Text = warningString[0];
            }
            else if (tbConfirmPass.Text != tbPass.Text)
            {
                txtWarning4.Visible = true;
                txtWarning4.Text = warningString[2];
            }
            else
            {
                txtWarning4.Visible = false;
                w4 = true;
            }

            //check gender 
            if (string.IsNullOrEmpty(tbGender.Text))
            {
                txtWarning5.Visible = true;
                txtWarning5.Text = warningString[0];
            }
            else
            {
                txtWarning5.Visible = false;
                w5 = true;
            }

            if (w1 && w2 && w3 && w4 && w5)
            {
                foo = true;
            }
        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {
            tbName.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!panelinfo.Visible)
                panelinfo.Visible = true;
            else
                panelinfo.Visible = false;
        }
    }
}