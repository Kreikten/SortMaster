using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using Ionic.Zip;
using Ionic.Zlib;
using System.Windows.Forms;

namespace SortMaster
{
    class Main_workwithfiles
    {
        public FileInfo[] GetFiles(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] dirs = info.GetDirectories();
            FileInfo[] Files = info.GetFiles();
            return Files;
        }
        public string[] GetExtensions(FileInfo[] Files)
        {
            int i,k = 0;
            bool Flag = false;
            string ext;
            List <string> Extensions = new List<string>();
            for (i = 0; i < Files.Length; i++)
            {
                if (Files[i].Exists)
                {
                    Flag = false;
                    ext = Path.GetExtension(Files[i].ToString());
                    foreach (string e in Extensions)
                    {
                        if (e == ext)
                        {
                            Flag = true;
                        }
                    }
                    if (Flag != true)
                    {
                        Extensions.Add(ext);
                        k++;

                    }
                }
            }
            return Extensions.ToArray();
        }


        public void QuickSort(string username, FileInfo[] Files,string DesktopPath)
        {
            string ext,path,p;
            string[] PictExt = new string[8] {".tiff", ".jpeg", ".bmp", ".jpe", ".jpg", ".png", ".gif", ".psd"};
            string[] DocExt = new string[8] { ".txt", ".rtf", ".odt", ".doc", ".docx",".djvu", ".fb2", ".pdf" };
            string[] VidExt = new string[7] { ".mpeg", ".flv", ".mov", ".3gp", ".avi", ".ogg", ".vob" };
            string[] MusicExt = new string[6] { ".WAV", ".AIFF",".MP3", ".Ogg", ".APE", ".FLAC" };
            foreach (FileInfo file in Files)
            {
                if (file.Exists)
                {
                    ext = Path.GetExtension(file.Name);
                    if (Array.IndexOf(PictExt, ext) != -1)
                    {
                        p = Path.Combine(DesktopPath.Substring(0,DesktopPath.Length-7), "Pictures");
                        path = Path.Combine(p, file.Name);
                        if (!File.Exists(path))
                        {
                            file.MoveTo(path);
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Файл " + file.Name + " уже существует. Заменить его?", "Проблема!", MessageBoxButtons.YesNo);
                            switch (dr)
                            {
                                case DialogResult.Yes:
                                    File.Delete(path);
                                    file.MoveTo(path);
                                    break;
                                case DialogResult.No:
                                    break;
                            }
                        }

                    }
                    if (Array.IndexOf(DocExt, ext) != -1)
                    {
                        path = Path.Combine(DesktopPath.Substring(0, DesktopPath.Length - 7), "Documents", file.Name);
                        if (!File.Exists(path))
                        {
                            file.MoveTo(path);
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Файл " + file.Name + " уже существует", "Заменить?", MessageBoxButtons.YesNo);
                            switch (dr)
                            {
                                case DialogResult.Yes:
                                    File.Delete(path);
                                    file.MoveTo(path);
                                    break;
                                case DialogResult.No:
                                    break;
                            }
                        }

                    }
                    if (Array.IndexOf(MusicExt, ext) != -1)
                    {
                        path = Path.Combine(DesktopPath.Substring(0, DesktopPath.Length - 7), "Music", file.Name);
                        if (!File.Exists(path))
                        {
                            file.MoveTo(path);
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Файл " + file.Name + " уже существует", "Заменить?", MessageBoxButtons.YesNo);
                            switch (dr)
                            {
                                case DialogResult.Yes:
                                    File.Delete(path);
                                    file.MoveTo(path);
                                    break;
                                case DialogResult.No:
                                    break;
                            }
                        }
                    }
                    if (Array.IndexOf(VidExt, ext) != -1)
                    {
                        path = Path.Combine(DesktopPath.Substring(0, DesktopPath.Length - 7), "Videos", file.Name);
                        if (!File.Exists(path))
                        {
                            file.MoveTo(path);
                        }
                        else
                        {
                            DialogResult dr = MessageBox.Show("Файл " + file.Name + " уже существует", "Заменить?", MessageBoxButtons.YesNo);
                            switch (dr)
                            {
                                case DialogResult.Yes:
                                    File.Delete(path);
                                    file.MoveTo(path);
                                    break;
                                case DialogResult.No:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void UserSettingsSort(FileInfo[] Files, string[] Folders,string[] Extensions)
        {
            string ext,path;
            int i;
            foreach (FileInfo file in Files)
            {
                if (file.Exists)
                {
                    ext = Path.GetExtension(file.Name);
                    for (i = 0; i < Extensions.Length; i++)
                    {
                        if (ext == Extensions[i])
                        {
                            if (Folders[i]!="null")
                            {
                                path = Path.Combine(Folders[i], file.Name);
                                if (!File.Exists(path))
                                {
                                    file.MoveTo(path);
                                }
                                else
                                {
                                    DialogResult dr = MessageBox.Show("Файл " + file.Name + " уже существует", "Заменить?", MessageBoxButtons.YesNo);
                                    switch (dr)
                                    {
                                        case DialogResult.Yes:
                                            File.Delete(path);
                                            file.MoveTo(path);
                                            break;
                                        case DialogResult.No:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        }


        public void BackUp(string directory, string DesktopPath)
        {
            string p, path,time,name;
            time = System.DateTime.Now.ToString();
            time = time.Replace(":","-");
            path = new DirectoryInfo(directory).Name;
            p = Path.Combine(DesktopPath.Substring(0, DesktopPath.Length - 7), "Documents","SortMaster",time );
          //  p1= Path.Combine(p,path);
            ZipFile zip = new ZipFile();
            if (!Directory.Exists(p))
            {
                  Directory.CreateDirectory(p);
          //        Directory.CreateDirectory(p1);
            }
            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
            zip.AddDirectory(directory);
            name = @p + @"\"+"backup.rar";
            zip.Save(name);
        }


        
    }
}
