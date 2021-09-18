using System;
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

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var richtbx = sender as RichTextBox;
            int amountChars = new TextRange(richtbx.Document.ContentStart, richtbx.Document.ContentEnd).Text.Length;
            statusTextBlock.Text = $"Document length: {amountChars} Characters";
        }

        private void boldTextButton_Click(object sender, RoutedEventArgs e)
        {
            var currentFontWeight = contentRichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            var theType = currentFontWeight.GetType().Name;

            FontWeight fontWeight = theType == "NamedObject" || (currentFontWeight.GetType() == typeof(FontWeight) && (FontWeight)currentFontWeight == FontWeights.Normal)
                ? FontWeights.Bold
                : FontWeights.Normal;

            contentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
            
            
        }
    }
}
