using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.io;
using iTextSharp.text;


namespace Deep_Thunder
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        DataRow[] myData;
        DataRow[] myReports;
        int MaxThreads;
        int CurrentThreads;
        int head;
        int tail;
        static ArrayList al = new ArrayList();
        static ArrayList VulnerableUrls = new ArrayList();
        static string siteName;
        static string strRegex = @"(http|https|ftp):(\/\/|\\\\)([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        Regex regex = new Regex("href=\"[^\"]+\"", RegexOptions.IgnoreCase);
        Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
        static string temp = null;


        private bool foo = true;

        private void MainForm_Load(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT * FROM tbl_login WHERE id_login = '" + Properties.Settings.Default.idLogin + "'";
            myData = dataBase.getData(sqlQuery);

            string userName = myData[0].ItemArray[3].ToString();
            string userSurname = myData[0].ItemArray[4].ToString();

            int lenghtTextField = (userName + " " + userSurname).Length;

            labelAccount.Location = new Point(885 - (lenghtTextField * 7) - 4, 5);
            labelAccount.Text = userName + " " + userSurname;
            labelAccount2.Text = userName + " " + userSurname;
            labelAccountLogin.Text = "Login: " + myData[0].ItemArray[1].ToString();
            labelAccountPic.Text = (userName[0].ToString() + userSurname[0].ToString()).ToUpper();
            labelAccountPic2.Text = (userName[0].ToString() + userSurname[0].ToString()).ToUpper();

            switchPanel(1);

            // загрузка отчетов
            string sqlQueryReports = "SELECT id_reports, site, description, date FROM tbl_reports WHERE id_user = '" + Properties.Settings.Default.idLogin + "' ";
            myReports = dataBase.getData(sqlQueryReports);
            dataBase.showDB(myReports, tableReports);
        }

        private void slider()
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                if (foo)
                {
                    metroPanel2.Width -= 20;
                    if (metroPanel2.Width <= 60)
                    {
                        timer.Stop();
                        foo = false;
                    }
                }
                else
                {
                    metroPanel2.Width += 20;
                    if (metroPanel2.Width >= 160)
                    {
                        timer.Stop();
                        foo = true;
                    }
                }
            };
        }

        private void sliderValnurable(bool foo)
        {
            Timer timer = new Timer(); // создаём таймер
            timer.Interval = 1; // задаём интервал
            timer.Start(); // стартуем
            timer.Tick += (s, e) => // это обработчик того, что произойдёт после интервала
            {
                if (foo)
                {
                    panelVulnerable.Top += 20;
                    if (panelVulnerable.Top >= 330)
                        timer.Stop();
                }
                else
                {
                    if (panelVulnerable.Top != 10)
                    {
                        panelVulnerable.Top -= 20;
                        if (panelVulnerable.Top <= 10)
                            timer.Stop();
                    }
                }
            };
        }

        private void GetWholeSite()
        {
            for (;;)
            {
                if (stopAnalysis)
                    break;

                if (head < tail && CurrentThreads < MaxThreads)
                {
                    CurrentThreads += 1;
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Refreshal));
                    t.Start(al[head]);
                    head += 1;
                }
                else if (head == tail && CurrentThreads == 0)
                    break;
                Invoke(new Action(() => { PB1.Maximum = tail; LB1.Text = "Прогресс: " + PB1.Value.ToString() + " / " + PB1.Maximum.ToString(); }));
                System.Threading.Thread.Sleep(50);
            }

            MetroFramework.MetroMessageBox.Show(this, "Сканирование завершено", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Invoke(new Action(() =>
            {
                btnStartAnalysis.Enabled = true;
                lbStartAnalysis.Enabled = true;
                btnStartSearchValnurable.Enabled = true;
                btnStartAnalysis.Image = Properties.Resources.play;
            }));

            if(!string.IsNullOrEmpty(tbScanUrl.Text))
                Invoke(new Action(() => { sliderValnurable(true); }));

            return;
        }

        private void Refreshal(object uri)
        {
            string url = (string)uri;
            string htmlCode = GetPageResource(url);
            string CurUrl;
            MatchCollection m = regex.Matches(htmlCode);
            foreach (Match mc in m)
            {
                if (stopAnalysis)
                    break;
                lock (this)
                {
                    CurUrl = mc.Value.Substring(6, mc.Value.Length - 7);
                    if (!CurUrl.StartsWith("http"))
                    {
                        CurUrl = BuildUrl(url, CurUrl);
                    }
                    if (CurUrl != null && !al.Contains(CurUrl) && CurUrl.StartsWith(temp))
                    {
                        Invoke(new Action(() => { al.Add(CurUrl); }));
                        tail += 1;
                        Invoke(new Action(() => { tbScanUrl.AppendText(CurUrl + "\r\n"); }));
                    }
                }
            }
            CurrentThreads -= 1;
            Invoke( new Action(() => { PB1.Value += 1; }));
            return;
        }

        private string GetPageResource(string url)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("GB18030"));
                string strHTML = sr.ReadToEnd();
                myStream.Close();
                return strHTML;
            }
            catch
            {
                return "-";
            }
        }

        string BuildUrl(string Root, string Cur)
        {
            try
            {
                Uri uri = new Uri(Root);
                Uri ur = new Uri(uri, Cur);
                return ur.AbsoluteUri;
            }
            catch { return null; }
        }

        public bool Infected(string url)
        {
            string html = "";
            try
            {
                html = new WebClient().DownloadString(url + " '");
            }
            catch
            {
                // ignored
            }
            return html.Contains("You have an error in your SQL syntax;");
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            slider();
        }

        /// <summary>
        /// switching Panel
        /// </summary>
        /// <param name="number"></param>
        private void switchPanel(int number)
        {
            panelMain.Visible = false;
            panelSQLinj.Visible = false;
            panelBrowser.Visible = false;
            panelReports.Visible = false;
            panelSettings.Visible = false;
            panelSaveReports.Visible = false;
            btnMain.Image = Properties.Resources.imgHouse;
            btnAnalysis.Image = Properties.Resources.imgAnalysis;
            btnBrowser.Image = Properties.Resources.imgBrowser;
            btnReports.Image = Properties.Resources.imgReports;
            btnSettings.Image = Properties.Resources.imgSettings;

            switch (number)
            {
                case 1:
                    panelMain.Visible = true;
                    btnMain.Image = Properties.Resources.imgHouseHide;
                    break;
                case 2:
                    panelSQLinj.Visible = true;
                    btnAnalysis.Image = Properties.Resources.imgAnalysisHide;
                    break;
                case 3:
                    panelBrowser.Visible = true;
                    btnBrowser.Image = Properties.Resources.imgBrowserHide;
                    break;
                case 4:
                    panelReports.Visible = true;
                    btnReports.Image = Properties.Resources.imgReportsHide;
                    break;
                case 5:
                    panelSettings.Visible = true;
                    btnSettings.Image = Properties.Resources.imgSettingsHide;
                    break;
                case 6:
                    panelSaveReports.Visible = true;
                    btnReports.Image = Properties.Resources.imgReportsHide;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Enter mouse 
        /// </summary>
        #region

        private void btnMain_MouseEnter(object sender, EventArgs e)
        {
            btnMain.Image = Properties.Resources.imgHouseHide;
        }

        private void btnMain_MouseLeave(object sender, EventArgs e)
        {
            if (!panelMain.Visible)
                btnMain.Image = Properties.Resources.imgHouse;
        }

        private void btnAnalysis_MouseEnter(object sender, EventArgs e)
        {
            btnAnalysis.Image = Properties.Resources.imgAnalysisHide;
        }

        private void btnAnalysis_MouseLeave(object sender, EventArgs e)
        {
            if(!panelSQLinj.Visible)
                btnAnalysis.Image = Properties.Resources.imgAnalysis;
        }

        private void btnBrowser_MouseEnter(object sender, EventArgs e)
        {
            btnBrowser.Image = Properties.Resources.imgBrowserHide;
        }

        private void btnBrowser_MouseLeave(object sender, EventArgs e)
        {
            if(!panelBrowser.Visible)
                btnBrowser.Image = Properties.Resources.imgBrowser;
        }

        private void btnReports_MouseEnter(object sender, EventArgs e)
        {
            btnReports.Image = Properties.Resources.imgReportsHide;
        }

        private void btnReports_MouseLeave(object sender, EventArgs e)
        {
            if (!panelReports.Visible && !panelSaveReports.Visible) 
                btnReports.Image = Properties.Resources.imgReports;
        }

        private void btnSettings_MouseEnter(object sender, EventArgs e)
        {
            btnSettings.Image = Properties.Resources.imgSettingsHide;
        }

        private void btnSettings_MouseLeave(object sender, EventArgs e)
        {
            if(!panelSettings.Visible)
                btnSettings.Image = Properties.Resources.imgSettings;
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.Image = Properties.Resources.imgExitHide;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.Image = Properties.Resources.imgExit;
        }
        
        private void btnMenu_MouseEnter(object sender, EventArgs e)
        {
            btnMenu.Image = Properties.Resources.imgMenuHide;
        }

        private void btnMenu_MouseLeave(object sender, EventArgs e)
        {
            btnMenu.Image = Properties.Resources.imgMenu;
        }

        #endregion

        private void btnMain_Click(object sender, EventArgs e)
        {
            switchPanel(1);
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            switchPanel(2);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            switchPanel(3);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            switchPanel(4);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            switchPanel(5);
        }

        //BROWSER
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            if(toolStripTextBox1.Text != null)
                webBrowser.Navigate(toolStripTextBox1.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            if (e.KeyCode == Keys.Enter)
                webBrowser.Navigate(toolStripTextBox1.Text);
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripTextBox1.Text = webBrowser.Document.Url.OriginalString;
        }

        /// <summary>
        /// visibling Panel Account Settings
        /// </summary>
        private void visiblePanelAccountSettings()
        {
            if (!panelAccountSettings.Visible)
            {
                panelAccountSettings.Visible = true;
                pictureBox1.BackColor = Color.FromArgb(157, 157, 157);
                pictureBox1.Image = Properties.Resources.arrowDownAccountWhite;
            }
            else
            {
                panelAccountSettings.Visible = false;
                pictureBox1.BackColor = Color.FromArgb(255, 255, 255);
                pictureBox1.Image = Properties.Resources.arrowDownAccount;
            }
        }

        private void labelAccount_Click(object sender, EventArgs e)
        {
            visiblePanelAccountSettings();
        }

        private void linkSettingAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccountSettingsForm accountSettingForm = new AccountSettingsForm();
            accountSettingForm.ShowDialog();
            if (accountSettingForm.DialogResult == DialogResult.OK)
            {
                string sqlQuery = "SELECT * FROM tbl_login WHERE id_login = '" + Properties.Settings.Default.idLogin + "'";
                myData = dataBase.getData(sqlQuery);

                string userName = myData[0].ItemArray[3].ToString();
                string userSurname = myData[0].ItemArray[4].ToString();

                int lenghtTextField = (userName + " " + userSurname).Length;

                labelAccount.Location = new Point(885 - (lenghtTextField * 7), 5);
                labelAccount.Text = userName + " " + userSurname;
                labelAccount2.Text = userName + " " + userSurname;
                labelAccountLogin.Text = "Login: " + myData[0].ItemArray[1].ToString();
                labelAccountPic.Text = (userName[0].ToString() + userSurname[0].ToString()).ToUpper();
                labelAccountPic2.Text = (userName[0].ToString() + userSurname[0].ToString()).ToUpper();
            }
        }

        private void tbInfectedUrl_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            switchPanel(3);

            toolStripTextBox1.Text = e.LinkText;
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            if (toolStripTextBox1.Text != null)
                webBrowser.Navigate(toolStripTextBox1.Text);
        }

        private bool stopAnalysis = false;
        private bool stopSearchVulnerable = false;

        private void btnStopAnalysis_Click(object sender, EventArgs e)
        {
            stopAnalysis = true;
            btnStartAnalysis.Enabled = true;
            metroButton2.Enabled = true;
        }

        private void btnStartAnalysis_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as PictureBox).Top += 1;
            (sender as PictureBox).Left += 1;
        }

        private void btnStartAnalysis_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as PictureBox).Top -= 1;
            (sender as PictureBox).Left -= 1;
        }

        private void lbStartAnalysis_MouseEnter(object sender, EventArgs e)
        {
            lbStartAnalysis.ForeColor = Color.FromArgb(202, 202, 202);
            btnStartAnalysis.Image = Properties.Resources.playHide;
        }

        private void lbStartAnalysis_MouseLeave(object sender, EventArgs e)
        {
            lbStartAnalysis.ForeColor = Color.Black;
            btnStartAnalysis.Image = Properties.Resources.play;
        }

        private void lbStopAnalysis_MouseEnter(object sender, EventArgs e)
        {
            lbStopAnalysis.ForeColor = Color.FromArgb(202, 202, 202);
            btnStopAnalysis.Image = Properties.Resources.stopHide;
        }

        private void lbStopAnalysis_MouseLeave(object sender, EventArgs e)
        {
            lbStopAnalysis.ForeColor = Color.Black;
            btnStopAnalysis.Image = Properties.Resources.stop;
        }

        private void btnStartAnalysis_MouseEnter(object sender, EventArgs e)
        {
            lbStartAnalysis.ForeColor = Color.FromArgb(202, 202, 202);
            btnStartAnalysis.Image = Properties.Resources.playHide;
        }

        private void btnStartAnalysis_MouseLeave(object sender, EventArgs e)
        {
            lbStartAnalysis.ForeColor = Color.Black;
            if(btnStartAnalysis.Enabled)
                btnStartAnalysis.Image = Properties.Resources.play;
        }

        private void btnStopAnalysis_MouseEnter(object sender, EventArgs e)
        {
            lbStopAnalysis.ForeColor = Color.FromArgb(202, 202, 202);
            btnStopAnalysis.Image = Properties.Resources.stopHide;
        }

        private void btnStopAnalysis_MouseLeave(object sender, EventArgs e)
        {
            lbStopAnalysis.ForeColor = Color.Black;
            btnStopAnalysis.Image = Properties.Resources.stop;
        }

        private void btnStartAnalysis_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUrlAnalysis.Text) || !string.IsNullOrEmpty(tbUrlMain.Text)) 
            {
                switchPanel(2);
                sliderValnurable(false);
                stopAnalysis = false;
                tbScanUrl.Clear();
                tbInfectedUrl.Clear();

                btnStartSearchValnurable.Enabled = false;
                stopSearchVulnerable = false;
                PB2.Value = 0;
                LB2.Text = "";
                btnSaveReports.Visible = false;
                lbWarning.Visible = false;
                warningVulnerable = false;
                VulnerableUrls.Clear();
                tbDescription.Clear();
                tableVulnerableLinks.ClearSelection();

                siteName = tbUrlAnalysis.Text;

                MaxThreads = 30;
                CurrentThreads = 0;
                PB1.Value = 0;
                head = 0;
                tail = 1;
                al.Clear();

                string analUrl = !string.IsNullOrEmpty(tbUrlAnalysis.Text) ? tbUrlAnalysis.Text : tbUrlMain.Text;

                al.Add(analUrl);
                temp = analUrl;
                tbUrlMain.Text = analUrl;
                tbUrlAnalysis.Text = analUrl;
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(GetWholeSite));
                t.Start();
                btnStartAnalysis.Enabled = false;
                lbStartAnalysis.Enabled = false;
                metroButton2.Enabled = false;
                btnStartAnalysis.Image = Properties.Resources.playHide;

                return;
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Сначала введите URL адрес", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lbStartSearchValnurable_MouseEnter(object sender, EventArgs e)
        {
            lbStartSearchValnurable.ForeColor = Color.FromArgb(202, 202, 202);
            btnStartSearchValnurable.Image = Properties.Resources.playHide;
        }

        private void lbStartSearchValnurable_MouseLeave(object sender, EventArgs e)
        {
            lbStartSearchValnurable.ForeColor = Color.Black;
            if (btnStartSearchValnurable.Enabled)
                btnStartSearchValnurable.Image = Properties.Resources.play;
        }

        private void lbStopSearchValnurable_MouseEnter(object sender, EventArgs e)
        {
            lbStopSearchValnurable.ForeColor = Color.FromArgb(202, 202, 202);
            btnStopSearchValnurable.Image = Properties.Resources.stopHide;
        }

        private void lbStopSearchValnurable_MouseLeave(object sender, EventArgs e)
        {
            lbStopSearchValnurable.ForeColor = Color.Black;
            btnStopSearchValnurable.Image = Properties.Resources.stop;
        }

        private void btnStopSearchValnurable_Click(object sender, EventArgs e)
        {
            stopSearchVulnerable = true;
            btnStartSearchValnurable.Enabled = true;
        }

        private string saveFile(ArrayList array)
        {
            string links = "";
            foreach (var vulnerableUrl in array)
                links += vulnerableUrl + Environment.NewLine;

            return links;
        }

        private DateTime myDateTime;

        private void btnSaveReports_Click(object sender, EventArgs e)
        {
            myDateTime = DateTime.Now;
            metroLabel4.Text = siteName;
            metroLabel6.Text = "Дата сканирования: " + myDateTime.ToString().Remove(10);

            if (warningVulnerable)
            {
                panel2.Visible = true;
                tableVulnerableLinks.RowCount = VulnerableUrls.Count;
                for (int i = 0; i < VulnerableUrls.Count; i++)
                {
                    tableVulnerableLinks.Rows[i].Cells[0].Value = i + 1;
                    tableVulnerableLinks.Rows[i].Cells[1].Value = VulnerableUrls[i];
                }
            }
            else
                panel2.Visible = false;

            switchPanel(6);
        }

        private bool warningVulnerable = false;

        private void btnStartSearchValnurable_Click(object sender, EventArgs e)
        {
            //поиск уязвимостей
            stopSearchVulnerable = false;
            PB2.Value = 0;
            LB2.Text = "";
            tbInfectedUrl.Clear();
            btnSaveReports.Visible = false;
            lbWarning.Visible = false;
            warningVulnerable = false;

            btnStartSearchValnurable.Enabled = false;
            lbStartSearchValnurable.Enabled = false;
            btnStartSearchValnurable.Image = Properties.Resources.playHide;

            IList<string> vulnerableResults = new List<string>();
            IList<string> urlsToCheck = new List<string>();
            string[] separators = new string[] { Environment.NewLine };
            string urlBatch = tbScanUrl.Text;


            var th = new System.Threading.Thread(() =>
            {
                if (!string.IsNullOrEmpty(urlBatch))
                    urlsToCheck = urlBatch.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var url in urlsToCheck)
                {
                    if (stopSearchVulnerable)
                        break;

                    if (Infected(url))
                    {
                        Invoke(new Action(() =>
                        {
                            tbInfectedUrl.AppendText(Environment.NewLine + url + "  - данная ссылка уязвима" + Environment.NewLine);
                            if (!lbWarning.Visible)
                            {
                                lbWarning.Visible = true;
                                warningVulnerable = true;
                            }   
                        }));
                        VulnerableUrls.Add(url);
                    }
                    else
                    {
                        Invoke(new Action(() => { tbInfectedUrl.AppendText(Environment.NewLine + url + " - надежная ссылка" + Environment.NewLine); }));
                    }

                    Invoke(new Action(() => { PB2.Maximum = urlsToCheck.Count; LB2.Text = "Прогресс: " + PB2.Value.ToString() + " / " + PB2.Maximum.ToString(); }));
                    Invoke(new Action(() => { PB2.Value += 1; }));
                }
                MetroFramework.MetroMessageBox.Show(this, "Поиск уязвимости завершен", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Invoke(new Action(() =>
                {
                    btnStartSearchValnurable.Enabled = true;
                    lbStartSearchValnurable.Enabled = true;
                    btnStartSearchValnurable.Image = Properties.Resources.play;
                    btnSaveReports.Visible = true;
                }));
            });
            th.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //если сохранение отчета в первый раз
            if (!editReports)
            {
                string guidNumber = "";
                //можно записать уязвимые ссылки
                if (warningVulnerable)
                {
                    //Сохранение уязвимых ссылок в файл
                    string SaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Deep Thunder\\Deep Thunder\\"; //путь к папке Мои Документы + название папки сохранений

                    if (!Directory.Exists(SaveFolder)) //если папки не существует
                        Directory.CreateDirectory(SaveFolder); //создаем ее

                    guidNumber = Guid.NewGuid().ToString();

                    File.WriteAllText(SaveFolder + "userID_" + myData[0].ItemArray[0] + "_vulnrlb_" + guidNumber + ".frmdt", saveFile(VulnerableUrls));
                }

                var sqlFormattedDate = myDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss");

                string description = "";
                if (string.IsNullOrEmpty(tbDescription.Text))
                    description = "Описание отсутствует";
                else
                    description = tbDescription.Text;

                string sqlQuery = "INSERT INTO tbl_reports (site, description, date, id_user, guidNumber) VALUES (N'" + siteName + "', N'" + description + "', '" + sqlFormattedDate + "', N'" + myData[0].ItemArray[0].ToString() + "', N'" + guidNumber + "' )";
                dataBase.addReport(sqlQuery);
            }
            else
            {
                string description = "";
                if (string.IsNullOrEmpty(tbDescription.Text))
                    description = "Описание отсутствует";
                else
                    description = tbDescription.Text;

                string sqlQuery = "UPDATE tbl_reports SET description = N'" + description + "' WHERE id_reports = '" + idReports + "' ";
                dataBase.updateData(sqlQuery);
            }

            //показываем данные в таблице
            string sqlQueryReports = "SELECT id_reports, site, description, date FROM tbl_reports WHERE id_user = '" + Properties.Settings.Default.idLogin + "' ";
            myReports = dataBase.getData(sqlQueryReports);
            dataBase.showDB(myReports, tableReports);
            editReports = false;

            switchPanel(4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbDescription.Clear();
            tableVulnerableLinks.ClearSelection();
            switchPanel(4);
        }

        private void StartLoadedVulnerableLinks(int id_user, string guidNumber)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Deep Thunder\\Deep Thunder\\userID_" + id_user + "_vulnrlb_" + guidNumber + "";
            if (File.Exists(path + ".frmdt")) 
            {
                StreamReader reader = new StreamReader(path + ".frmdt");
                string[] file = (reader.ReadToEnd()).Split('\n');
                reader.Close();
                string[] row = file;

                showReports(row, tableVulnerableLinks);
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
                return;
            }
        }

        private void showReports(string[] row, DataGridView myTable)
        {
            if (row.Length != 0)
            {
                myTable.RowCount = row.Length;
                for (int i = 0; i < row.Length; i++)
                {
                    myTable.Rows[i].Cells[0].Value = i + 1;
                    myTable.Rows[i].Cells[1].Value = row[i];
                }
            }
        }

        private bool editReports = false;
        private int idReports = 0;

        private void tableReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                idReports = Convert.ToInt32(tableReports.Rows[e.RowIndex].Cells[0].Value);
                string sqlQuery = "SELECT id_reports, site, description, date, id_user, guidNumber FROM tbl_reports WHERE id_reports = '" + idReports + "' ;";
                DataRow[] reportsData = dataBase.getData(sqlQuery);
                if (reportsData.Length != 0)  
                {
                    metroLabel4.Text = reportsData[0].ItemArray[1].ToString();
                    tbDescription.Text = reportsData[0].ItemArray[2].ToString();
                    metroLabel6.Text = "Дата сканирования: " + reportsData[0].ItemArray[3].ToString().Remove(10);
                    StartLoadedVulnerableLinks(Convert.ToInt32(reportsData[0].ItemArray[4]), reportsData[0].ItemArray[5].ToString());

                    switchPanel(6);
                    editReports = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            DialogResult deleteMess =  MetroFramework.MetroMessageBox.Show(this, "Вы уверены, что хотите удалить данный отчет?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if(deleteMess == DialogResult.Yes)
            {
                string delQuery = "DELETE FROM tbl_reports WHERE id_reports = '" + Convert.ToInt32(tableReports[0, tableReports.CurrentRow.Index].Value) + "'";
                dataBase.deleteReports(delQuery);

                //показываем данные в таблице
                string sqlQueryReports = "SELECT id_reports, site, description, date FROM tbl_reports WHERE id_user = '" + Properties.Settings.Default.idLogin + "' ";
                myReports = dataBase.getData(sqlQueryReports);
                dataBase.showDB(myReports, tableReports);
            }           
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
            DataRow[] reportsData = dataBase.getData("SELECT site, description, date, guidNumber, id_user FROM tbl_reports WHERE id_reports = '" + Convert.ToInt32(tableReports[0, tableReports.CurrentRow.Index].Value) + "'");
            Document document = new Document();
            FileStream stream = new FileStream(DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-report.pdf", FileMode.Create);
            document.SetPageSize(PageSize.LETTER.Rotate()); // landscape 
            PdfWriter.GetInstance(document, stream);
            document.Open();
            PdfPTable table = new PdfPTable(1);
            PdfPCell header = new PdfPCell(new Phrase("Отчет анализа сайта: " + reportsData[0].ItemArray[0].ToString() + ",\n Дата проведения: " + reportsData[0].ItemArray[2].ToString().Remove(10) + " ", font));
            PdfPCell Descrip = new PdfPCell(new Phrase("Описание", fontHeader));
            PdfPCell VulUrl = new PdfPCell(new Phrase("Уязвимые ссылки", fontHeader));

            header.Colspan = 1;
            header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 

            table.AddCell(header);
            table.AddCell(Descrip);
            table.AddCell(new Phrase(reportsData[0].ItemArray[1].ToString(), font));
            table.AddCell(VulUrl);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Deep Thunder\\Deep Thunder\\userID_" + reportsData[0].ItemArray[4] + "_vulnrlb_" + reportsData[0].ItemArray[3] + "";
            if (File.Exists(path + ".frmdt"))
            {
                StreamReader reader = new StreamReader(path + ".frmdt");
                string[] file = (reader.ReadToEnd()).Split('\n');
                reader.Close();
                string[] row = file;

                for (int i = 0; i < row.Length; i++)
                {
                    table.AddCell(new Phrase(row[i], font));
                }
            }
            else
            {
                table.AddCell(new Phrase("Не содержит уязвимых ссылок", font));
            }

            document.Add(table);
            document.Close();
            System.Diagnostics.Process.Start(DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-report.pdf");
        }
    }
}