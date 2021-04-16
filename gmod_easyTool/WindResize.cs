
using gmod_easyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoRun.lib
{
    public class WindResize /*: IDisposable*/
    {
        private Window Wind = null;
        UIElement Element;
        private TimerTick timerTick = new TimerTick();
        API.Pos poscur;
        API.Pos last_poscur;
        double Width = 0;
        double Height = 0;
        private Size resizeSize;
        private Point resizeWindowPoint;
        bool isMouseDoun = false;
        bool isMouseMoove = false;
        public WindResize(Window w)
        {
            Wind = w;
            Width = Wind.Width;
            Height = Wind.Height;
            timerTick.Tick += (sender, e) => 
            {
                if (Mouse.LeftButton == MouseButtonState.Released)
                    isMouseDoun = false;
                if (isMouseDoun)
                    Update();
                Wind.Width = Width < 40?40: Width;
                Wind.Height = Height < 40 ? 40 : Height;                
            };           
            timerTick.Start();

            Wind.MouseLeftButtonDown += (sender, e) =>
            {
                if (isMouseMoove && isMouseDoun == false)
                    Wind.DragMove();  
            };
            Wind.MouseEnter += (sender, e) => isMouseMoove = true;
            Wind.MouseLeave += (sender, e) => isMouseMoove = false;
        }
        public void RightDown(UIElement element)
        {
            Element = element;
            MouseHandlers(Element);
        }

        private void MouseHandlers(UIElement element)
        {
            element.MouseLeftButtonDown += new MouseButtonEventHandler(element_MouseLeftButtonDown);
            element.MouseLeftButtonUp += new MouseButtonEventHandler(element_MouseLeftButtonUp);
            element.MouseEnter += (sender, e) => 
            {
                Wind.Cursor = Cursors.SizeNWSE;
                isMouseMoove = false;
            };
            element.MouseLeave += (sender, e) =>
            {
                Wind.Cursor = Cursors.Arrow;
                isMouseMoove = true;
            };
        }

        private void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDoun = false;
        }

        private void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API.GetCursorPos(out last_poscur);
            resizeSize = new Size(Wind.Width, Wind.Height);
            resizeWindowPoint = new Point(Wind.Left, Wind.Top);
            isMouseDoun = true;

        }
        
        public void Update()
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                isMouseDoun = false;
            if (isMouseDoun)
            {
                API.GetCursorPos(out poscur);

                Width = resizeSize.Width - (last_poscur.X - poscur.X);

                Height = resizeSize.Height - (last_poscur.Y - poscur.Y);
            }
        }
    }
    public static class API
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Pos lpPoint);
        public struct Pos
        {
            public int X;
            public int Y;
        }
    }
}
