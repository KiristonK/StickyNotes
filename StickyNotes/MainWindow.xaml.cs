using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32.SafeHandles;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NoteBlock.Focusable = true;
            NoteBlock.IsReadOnly = false;
            NoteBlock.Focus();
            NoteBlock.Text = "";
            NoteBlock.ToolTip = "Enter note name and press enter.";
        }

        private void NoteBlock_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            NoteBox.Focus();
        }

        private void NoteBlock_OnLostFocus(object sender, RoutedEventArgs e)
        {
            NoteBlock.IsReadOnly = true;
            NoteBlock.Focusable = false;
            NoteBlock.ToolTip = null;
            NoteBox.Focus();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            File.WriteAllTextAsync($"{NoteBlock.Text}.txt",NoteBox.Text);
            MenuItem menuItem = new MenuItem();
            
        }
    }
}
