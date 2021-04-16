using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gmod_easyTool.WinForm
{
    /// <summary>
    /// Логика взаимодействия для WindEntityInf.xaml
    /// </summary>
    public partial class WindEntityInf : Window
    {
    
        TimerTick timerTick = new TimerTick();
        Random R = new Random();
        List<Label> labels = new List<Label>();
        IntPtr WindowGmod;
        string s = "";
        public string Text
        {
            get 
            {
                return s;
            }
            set
            {
                s = value;
                stack.Children.Clear();
                if (this.Visibility == Visibility.Hidden)
                    return;
                int w = 0;
                string[] ss = s.Split(new char[] { '\n' });
                foreach (var item in ss)
                {
                    if (string.IsNullOrEmpty(item))
                        return;
                    Label L = new Label()
                    {
                        FontFamily = new System.Windows.Media.FontFamily("Calibri Light"),
                        FontSize = 12,
                        Content = item,
                        Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(200, 44, 44, 44)),
                        Foreground = System.Windows.Media.Brushes.White,
                        Padding = new Thickness(2,0,2,0),
                        Margin = new Thickness(0,3,0,0)
                        
                    };
                    stack.Children.Add(L);
                }
                //this.Width = w * 16;
                //SetText(RichTextBox_b, s);
            }
        }
        public  void SetText(RichTextBox richTextBox, string text)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }



        public WindEntityInf()
        {
            InitializeComponent();

            this.Top = 545;
            this.Left = 1;





            timerTick.Tick += (o, e) => 
            {
                List<Process> lis = Proc.find("gmod");
                if (lis.Count == 0)
                    return;
                

                WindowGmod = lis[0].MainWindowHandle;
                if (string.IsNullOrEmpty(s))
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (Proc.GetMainWindowTitle() == lis[0].MainWindowTitle)
                    {
                        this.Visibility = Visibility.Visible;
                        Win32Api.HideFromAltTabAndTopMost(this);

                        


                    }
                    else
                    {
                        this.Visibility = Visibility.Hidden;
                    }
                }

                //using (var gr = Graphics.FromHwnd(IntPtr.Zero))
                //    gr.DrawRectangle(Pens.Red, new System.Drawing.Rectangle(1920 / 2 - 5, 1080 / 2 - 5, 10, 10));


                #region MyRegion
                //foreach (UIElement item in StackPanel_list.Children)
                //{
                //    if(item.GetType().ToString() == "System.Windows.Controls.Label")
                //    {
                //        var label = (Label)item;
                //        label.Content = $"{R.Next(10, 111)}";
                //    }

                //}
                //foreach (var item in Proc.find("gmod"))
                //{


                //}


                //for (int i = 0; i < lis.Count; i++)
                //{
                //    labels[i].Content = $"{lis[i].ProcessName} {lis[i].Id} {lis[i].MainWindowTitle}";
                //}

                //labels[0].Content = Proc.GetMainWindowTitle();
                #endregion

            };
            timerTick.Start();
            
        }
    }
}
