using AutoRun.lib;
using gmod_easyTool.WinForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFSpectrum.Config;

namespace gmod_easyTool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WindEntityInf windEntityInf = new WindEntityInf();
        TimerTick TimerTick_ = new TimerTick();
        TimerTick TimerTick_2 = new TimerTick();
        FILE fILE = new FILE();
        bool IsActiveHudCur = false;
        bool isfilre = false;
        int tim_ = 0;

        System.Drawing.Color[] Colors =
            {
                System.Drawing.Color.FromArgb(251,156,106),
                System.Drawing.Color.FromArgb(0,90,10),
                System.Drawing.Color.FromArgb(248,56,56),

                System.Drawing.Color.FromArgb(93,0,255),
                System.Drawing.Color.FromArgb(38,90,10),
                System.Drawing.Color.FromArgb(168,184,35),
                System.Drawing.Color.FromArgb(231,250,76),
                System.Drawing.Color.FromArgb(93,0,255),
                System.Drawing.Color.FromArgb(177,246,246),
                
               
                
                


            };

        public bool isColor(System.Drawing.Color color)
        {
            bool b = false;
            foreach (System.Drawing.Color item in Colors)
            {
                if(color == item)
                {

                    b = true;
                    break;
                }
            }
            return b;
        }
        public bool isColorFor()
        {
            bool b = false;
            for (int i = 0; i < 12; i++)
            {
                if(isColor(GetColorPixel(942 + i, 600)))
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        public object MosueDll { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            WindResize windResize = new WindResize(this);


            windEntityInf.Show();
            TimerTick_2.Tick += (o, e) =>
            {

                
                if (isfilre )
                {
                    //System.Drawing.Color c = GetColorPixel(5, 533);
                    //if (SmColor(c, System.Drawing.Color.FromArgb(255, 0, 0)) )
                    //{
                    //    tim_ = 5;

                    //}
                    //if (tim_ > 0)
                    //{
                    //    MouseDll.Mouse.LeftClick();
                    //    tim_--;
                    //}
                
                    if (isColorFor () || isColor(GetColorPixel(58, 599)))
                    {

                        MouseDll.Mouse.LeftClick();
                    }
                    //}if (SmColor(c, System.Drawing.Color.FromArgb(255, 0, 0)))
                    //{

                    //    MouseDll.Mouse.LeftClick();
                    //}
                    
             
                }
                   
            };
            TimerTick_2.Start();
            TimerTick_.Tick += (o, e) =>
            {
                //System.Drawing.Color c = GetColorPixel(5, 533);
                //if (isfilre)
                //    if (SmColor(c, System.Drawing.Color.FromArgb(255, 0, 0)))
                //    {
                //        MouseDll.Mouse.LeftClick();
                //    }
                if (fILE.isUpdateFile())
                {
                    windEntityInf.Text = fILE.Read();                    
                }
                if(IsActiveHudCur)
                    using (var gr = Graphics.FromHwnd(IntPtr.Zero))
                        gr.DrawRectangle(Pens.Red, new System.Drawing.Rectangle(1920 / 2 - 2, 1080 / 2 - 2, 4, 4));
            };
            TimerTick_.Start();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)(sender);
            fILE.file = tb.Text;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            windEntityInf.Close();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Properties.Resources.CodeE2);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IsActiveHudCur = !IsActiveHudCur;
        }
        public static System.Drawing.Color GetColorPixel(int x , int y)
        {

            Bitmap Bitmap_ = new Bitmap(1, 1);
            System.Drawing.Rectangle Rectangle_ = new System.Drawing.Rectangle(x,y, 1, 1);
            Graphics g = Graphics.FromImage(Bitmap_);
            g.CopyFromScreen(Rectangle_.Location, System.Drawing.Point.Empty, Rectangle_.Size);
            return Bitmap_.GetPixel(0, 0);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isfilre = !isfilre;
            Button b = (Button)sender;
            b.Content = $"AutoFire {isfilre}";
        }
        public bool diapazon(int num , int min )
        {
            return num < min;
        }
        public bool SmColor(System.Drawing.Color color , System.Drawing.Color c2, int diag = 0)
        {
            
            return color.R == c2.R && diapazon(color.G, 55) && diapazon(color.B, 55);
        }
    }
}
