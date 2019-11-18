using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
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

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class Start : Window
    {
        [DllImport("Kernel32")]
        private static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        private Pos pos;
        private Point LastPoint { get; set; }
        public Start()
        {
            InitializeComponent();
            //AllocConsole();
           // Console.WriteLine($"Screen cord: {Top} {Left}\nLastPoint: {LastPoint.X} {LastPoint.Y}");
            Width = 70;
            Height = 70;
            Left = SystemParameters.PrimaryScreenWidth - 35;
        }

        private void Start_OnMouseEnter(object sender, MouseEventArgs e)
        {
            short cnt = 0;
            while (cnt != 2)
            {
                if (pos.Upper)
                    Top += 10;
                else if (pos.Down)
                    Top -= 10;
                else if (pos.Right)
                    Left -= 10;
                else if (pos.Left)
                    Left += 10;
                cnt++;
            }
        }

        private void Start_OnMouseLeave(object sender, MouseEventArgs e)
        {
            short cnt = 0;
            while (cnt != 2)
            {
                if (pos.Upper)
                    Top -= 10;
                else if (pos.Down)
                    Top += 10;
                else if (pos.Right)
                    Left += 10;
                else if (pos.Left)
                    Left -= 10;
                cnt++;
            }
            LocPos();
            
        }

        private void Start_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
           // if (e.RightButton == MouseButtonState.Pressed)
           LastPoint = e.GetPosition(GetWindow(StickyCircle));
        }

        private void Start_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            this.Top += e.GetPosition(GetWindow(StickyCircle)).Y - LastPoint.Y;
            this.Left += e.GetPosition(GetWindow(StickyCircle)).X - LastPoint.X;

            //  Console.WriteLine($"Screen cord: {Top} {Left}\nLastPoint: {LastPoint.X} {LastPoint.Y}");
        }

        private void LocPos()
        {
            LastPoint = new Point(0, 0);
            if (Top <= SystemParameters.WorkArea.Height / 2)
            {
                if (Left <= SystemParameters.WorkArea.Width / 15)
                {
                    Left = -35;
                    pos = new Pos(false, false, false, true);
                }
                else if (Left >= SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width / 15)
                {
                    Left = SystemParameters.WorkArea.Width - 35;
                    pos = new Pos(false, false, true, false);
                }
                else
                {
                    Top = -35;
                    pos = new Pos(true, false, false, false);
                }

            }
            else if (Top >= SystemParameters.WorkArea.Height / 2)
            {
                if (Left <= SystemParameters.WorkArea.Width / 15)
                {
                    Left = -35;
                    pos = new Pos(false, false, false, true);
                }
                else if (Left >= SystemParameters.WorkArea.Width - SystemParameters.WorkArea.Width / 15)
                {
                    Left = SystemParameters.WorkArea.Width - 35;
                    pos = new Pos(false, false, true, false);
                }
                else
                {
                    Top = SystemParameters.WorkArea.Height - 35;
                    pos = new Pos(false, true, false, true);
                }

            }
            //Console.WriteLine($"Pos: {pos.Upper} {pos.Down} {pos.Right} {pos.Left}");
            //Console.WriteLine($"WorkArea.Height / 2: {SystemParameters.WorkArea.Height / 2}");
            //Console.WriteLine($"WorkArea.Width / 15: {SystemParameters.WorkArea.Width / 15}");
        }

        private void Start_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            LocPos();   
        }

        private void Start_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MainWindow window = new MainWindow();
                this.Close();
                window.Show();
            }
        }
    }

    internal struct Pos
    {
        public Pos(bool upper, bool down, bool right, bool left)
        {
            Upper = upper;
            Down = down;
            Right = right;
            Left = left;
        }

        public bool Upper { get; }
        public bool Down { get; }
        public bool Right { get; }
        public bool Left { get; }
    }
}
