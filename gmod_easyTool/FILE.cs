using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFSpectrum.Config
{
    class FILE
    {
        // ---------------------------------------------------------

        public string file
        {
            get; set;
        } = "file.txt";
        /// <summary>
        /// Кодировака при чтении файла.
        /// </summary>
        public Encoding encoding = System.Text.Encoding.Default;
        private int lastupdate = 0;

        // ---------------------------------------------------------

        public FILE(string file_name) => file = file_name;
        public FILE()
        {}

        // ---methods-------------------------------------------

        public string Read()
        {
            string text = "";
            if (isExists())
                try
                {
                    using (FileStream fstream = File.Open(file, FileMode.Open))
                    {
                        byte[] array = new byte[fstream.Length];
                        fstream.Read(array, 0, array.Length);
                        text = encoding.GetString(array);
                        
                    }
                }
                catch (Exception)
                {}
            
            return text;
        }
        public void Write(string text)
        {
            using (System.IO.StreamWriter f = new System.IO.StreamWriter(file))
            {
                f.Write(text);
            }
        }

//        <RichTextBox Visibility="Hidden" x:Name="RichTextBox_b" Background="#FF464646" BorderBrush="{x:Null}" Foreground="White" FontSize="16" FontFamily="Nirmala UI Semilight"/>

        public bool isExists()
        {
            return File.Exists(file) || Directory.Exists(file);
        }
        // Проверка на изменение файла.
        public bool isUpdateFile()
        {
            bool b = false;
            switch (isExists())
            {
                case true:
                    // получаем время последнего изменения в ms. Бээээ
                    int Millisecond = File.GetLastWriteTime(file).Millisecond;
                    if (lastupdate != Millisecond)
                    {
                        lastupdate = Millisecond;
                        b = true;
                        Thread.Sleep(50);
                    }
                    else
                    {
                        b = false;
                    }
                    break;
                case false:
                    b = false;
                    break;
                default:
                    break;
            }
            return b;
        }


    }
}
