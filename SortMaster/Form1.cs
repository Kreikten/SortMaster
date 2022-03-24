using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SortMaster
{
   
    public partial class Form1 : Form
    {
        Main_workwithfiles main_class;
        FileInfo[] Files_main;
        List <string> Files_Extensions;
        int rb;
        Point LabelCoords,ButtonCoords;
        bool IsModeSelected = false;
        bool QuickSort = false;
        bool UserSort = false;
        bool FileSort = false;
        bool ready = false;
        int count;
        bool OptionsConfirmed = false;
        bool backup;
        public string DesktopPath, username,quicksortfolder,userfilesort, AdditionalUserSort, UserFileSortFolder,chren;
        public List<string> Folders = new List<string>();
        public List<string> Extensions = new List<string>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            main_class = new Main_workwithfiles();
            DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Directory.CreateDirectory(Path.Combine(DesktopPath.Substring(0, DesktopPath.Length - 7), "Documents", "SortMaster"));
            chren = DesktopPath[2].ToString();
            username = Environment.UserName;
            //Files_main = main_class.GetFiles(DesktopPath);
            //Folder_Files_Extensions = main_class.GetExtensions(Files_main);
           // Directory.CreateDirectory(DesktopPath + "\\Pics");
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox1.ReadOnly = true;
            radioButton3.Checked = false;
            button6.Visible = false;
            LabelCoords = label10.Location;
            ButtonCoords = button6.Location;
            List<string> Folders = new List<string>();
            List<string> Exstensions = new List<string>();
            tabPage3.Visible = false;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                switch (rb) {
                    case 1:
                        panel1.Controls.Clear();
                        label6.Visible = true;
                        textBox1.Visible = true;
                        button4.Visible = true;
                        label8.Text = "Режим сортировки:" + " быстрый";
                        label6.Text = "Выберите папку для быстрой сортировки:";
                        panel1.Visible = false;
                        button9.Visible = false;
                        break;
                    case 2:
                        panel1.Controls.Clear();
                        label8.Text = "Режим сортировки:" + " настраиваемый";
                        label6.Text = "Выберите папку для сортировки:";
                        panel1.Visible = true;
                        button9.Visible = true;
                        button9.Enabled = false;
                        break;
                    case 3:
                        panel1.Controls.Clear();
                        label6.Visible = true;
                        textBox1.Visible = true;
                        button4.Visible = true;
                        panel1.Visible = true;
                        button9.Visible = true;
                        label8.Text = "Режим сортировки:" + " пользовательский";
                        label6.Text = "Выберите файл с пользовательскими настройками:";
                        break;
                }
            }



            /*if ((tabControl1.SelectedTab == tabPage3) && IsModeSelected && OptionsConfirmed)
            {
                switch (rb)
                {
                    case 1:
                        main_class.QuickSort(username, Files_main, DesktopPath);
                        ready = true;
                        break;
                    case 2:
                        main_class.UserSettingsSort(Files_main, Folders.ToArray(), Extensions.ToArray());
                        ready = true;
                        label7.Text = "Сортировка завершена!";
                        break;
                    case 3:
                        
                        break;
                }
            }*/


                if ((tabControl1.SelectedTab == tabPage2) && (IsModeSelected == false))
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Сначала необходимо выбрать режим");
            }
            if ((tabControl1.SelectedTab == tabPage3)&& (IsModeSelected == false))
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Сначала необходимо выбрать режим");
            }
            if ((tabControl1.SelectedTab == tabPage3)&& (OptionsConfirmed == false))
            {
                tabControl1.SelectedTab = tabPage2;
                MessageBox.Show("Сначала необходимо корректно ввести настройки");
            }
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Horizontal;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (IsModeSelected)
            {
                panel1.Controls.Clear();
                tabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show("Сначала необходимо выбрать режим");
            }
        }
        public void ReadUserFile(string userfilename)
        {
            int i;
            string s;
            string[] stroke;
            Path.ChangeExtension(userfilename, ".txt");
            StreamReader userfile = new StreamReader(userfilename);
            UserFileSortFolder = userfile.ReadLine();
            count = Convert.ToInt32(userfile.ReadLine());
            for (i = 0;i<count;i++)
            {
                s = userfile.ReadLine();
                stroke = s.Split('|');
                Extensions.Add(stroke[0]);
                Folders.Add(stroke[1]);
            }
        }

        private void Button9_Click_1(object sender, EventArgs e)
        {
            string filename;
            int i;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            { return; }
            filename = saveFileDialog1.FileName;
            Path.ChangeExtension(filename, ".txt");
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(AdditionalUserSort);
            sw.WriteLine(Folders.Count.ToString());
            for (i = 0;i<Folders.Count;i++)
            {
                sw.WriteLine(Extensions[i]+"|"+Folders[i]);
            }
            sw.Close();
            Path.ChangeExtension(filename, ".srtm");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Cursor.Position.X.ToString());
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                backup = true;
            }
            if (!checkBox1.Checked)
            {
                backup = false;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (OptionsConfirmed)
            {
                tabPage3.Visible = true;
                tabControl1.SelectedTab = tabPage3;
                switch (rb)
                {
                    
                    case 1:
                        if (backup)
                        {
                            main_class.BackUp(quicksortfolder, DesktopPath);
                        }
                        main_class.QuickSort(username, Files_main, DesktopPath);
                        ready = true;
                        label7.Text = "Готово!";
                        System.Threading.Thread.Sleep(2000);
                        tabControl1.SelectedTab = tabPage1;
                        OptionsConfirmed = false;
                        break;

                    case 2:
                        if (backup)
                        {
                            main_class.BackUp(AdditionalUserSort, DesktopPath);
                        }
                        OptionsConfirmed = true;
                        main_class.UserSettingsSort(Files_main,Folders.ToArray(), Extensions.ToArray());
                        ready = true;
                        label7.Text = "Готово!";
                        System.Threading.Thread.Sleep(2000);
                        tabControl1.SelectedTab = tabPage1;
                        OptionsConfirmed = false;
                        break;
                    case 3:
                        if (backup)
                        {
                            main_class.BackUp(AdditionalUserSort, DesktopPath);
                        }
                        main_class.UserSettingsSort(Files_main, Folders.ToArray(), Extensions.ToArray());
                        ready = true;
                        label7.Text = "Готово!";
                        System.Threading.Thread.Sleep(2000);
                        tabControl1.SelectedTab = tabPage1;
                        OptionsConfirmed = false;
                        break;
                }
            }
            else
            {
                MessageBox.Show("Сначала необходимо указать настройки");
            }

        }

        private void myButtons_Click(object sender, EventArgs e)
        {
            string i="";
            int k;
            Button btn = (Button)sender;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.Cancel)
            { return; }
            for (k = 8;k<btn.Name.Length;k++)
            {
                i = i + btn.Name[k].ToString();
            }
            if (!Extensions.Contains(Files_Extensions[Convert.ToInt32(i)]))
            {
                Extensions[Convert.ToInt32(i)] = Files_Extensions[Convert.ToInt32(i)];
                Folders[Convert.ToInt32(i)] = folderBrowserDialog2.SelectedPath;
              //  Extensions.Add(Files_Extensions[Convert.ToInt32(i)]);
             //   Folders.Add(folderBrowserDialog2.SelectedPath);
            }
            else
            {
                Folders[Convert.ToInt32(i)] = folderBrowserDialog2.SelectedPath;
            }
                
        }
        private void myButons_Click(object sender, EventArgs e)
        {
            string i = "";
            int k;
            Button btn = (Button)sender;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.Cancel)
            { return; }
            for (k = 7; k < btn.Name.Length; k++)
            {
                i = i + btn.Name[k].ToString();
            }
            if (!Extensions.Contains(Files_Extensions[Convert.ToInt32(i)]))
            {
                Extensions[Convert.ToInt32(i)] = Files_Extensions[Convert.ToInt32(i)];
                Folders[Convert.ToInt32(i)] = folderBrowserDialog2.SelectedPath;
                //  Extensions.Add(Files_Extensions[Convert.ToInt32(i)]);
                //   Folders.Add(folderBrowserDialog2.SelectedPath);
            }
            else
            {
                Folders[Convert.ToInt32(i)] = folderBrowserDialog2.SelectedPath;
            }
        }

            private void Button4_Click(object sender, EventArgs e)
        {
            int i,count = 0;
            if (rb == 1)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                { return; }
                quicksortfolder = folderBrowserDialog1.SelectedPath;
                textBox1.Text = quicksortfolder;
                Files_main = main_class.GetFiles(quicksortfolder);
                OptionsConfirmed = true;
            }
            if (rb ==2)
            {
                int y;
                y = ButtonCoords.Y;
                panel1.Controls.Clear();
                if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                { return; }

                AdditionalUserSort = folderBrowserDialog1.SelectedPath;
                textBox1.Text = AdditionalUserSort;
                Files_main = main_class.GetFiles(AdditionalUserSort);
                Files_Extensions = main_class.GetExtensions(Files_main).ToList();
                
                //   CreateLabelText(Files_Extensions, Files_main.Length);
                
                for (i = 0; i < Files_Extensions.Count; i++)
                {
                    if (Files_Extensions[i] != null)
                    {
                        Button myButton = new Button();
                        myButton.Name = "myButton" + count.ToString();
                        myButton.Text = "Указать папку для файлов с расширением " + Files_Extensions[i];
                        myButton.Enabled = true;
                        Extensions.Add(Files_Extensions[i]);
                        Folders.Add("null");
                        myButton.Left = 10;
                        myButton.Top = y;
                        y = y + 35;
                        myButton.UseVisualStyleBackColor = true;
                        myButton.Click += myButtons_Click;
                        count++;
                        //  myLabel.Size = new Size(100, 35);
                        myButton.Size = new System.Drawing.Size(400, 23);
                        panel1.Controls.Add(myButton);
                    }
                }
                OptionsConfirmed = true;
                button9.Enabled = true;
            }

            


                if (rb==3)
            {
                int y;
                y = ButtonCoords.Y;
                if (openFileDialog1.ShowDialog() ==DialogResult.Cancel)
                { return; }
                userfilesort = openFileDialog1.FileName;
                textBox1.Text = userfilesort;
                ReadUserFile(userfilesort);
                Files_main = main_class.GetFiles(UserFileSortFolder);
                for (i = 0; i < Extensions.Count; i++)
                {
                    if (Extensions[i] != null)
                    {
                        Button myButon = new Button();
                        myButon.Name = "myButon" + count.ToString();
                        myButon.Text = "Указать папку для файлов с расширением " + Extensions[i];
                        myButon.Enabled = true;
                        myButon.Left = 10;
                        myButon.Top = y ;
                        y = y + 35;
                        myButon.UseVisualStyleBackColor = true;
                        myButon.Click += myButons_Click;
                        count++;
                        //  myLabel.Size = new Size(100, 35);
                        myButon.Size = new System.Drawing.Size(400, 23);
                        panel1.Controls.Add(myButon);
                    }
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            QuickSort = false;
            UserSort = false;
            FileSort = false;
            /*foreach (RadioButton rb in (RadioButton)groupBox1.Controls)
            {
                if (rb.Checked == true && rb == radioButton1)
                {
                    QuickSort = true;
                    UserSort = false;
                    FileSort = false;
                }
                if (rb.Checked == true && rb == radioButton2)
                {
                    QuickSort = false;
                    UserSort = true;
                    FileSort = false;
                }
                if (rb.Checked == true && rb == radioButton3)
                {
                    QuickSort = false;
                    UserSort = false;
                    FileSort = true;
                }
            }*/
            
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked== true && radioButton == radioButton1)
            {
                QuickSort = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                UserSort = false;
                FileSort = false;
                rb = 1;
            }
            if (radioButton.Checked== true && radioButton == radioButton2)
            {
                QuickSort = false;
                UserSort = true;
                FileSort = false;
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                rb = 2;
                }
            if (radioButton.Checked == true && radioButton == radioButton3)
            {
                QuickSort = false;
                UserSort = false;
                FileSort = true;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                rb = 3;
                }
            if (radioButton.Checked == false)
            {
                IsModeSelected = false;
            }
            else
            {
                IsModeSelected = true;
            }
         //   label4.Text = QuickSort.ToString() + UserSort.ToString() + FileSort.ToString();
        }

       /*private void RadioButton1_Click(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                var rb = sender as RadioButton;
                rb.Checked = !rb.Checked;
            }
        }*/
    }
}
