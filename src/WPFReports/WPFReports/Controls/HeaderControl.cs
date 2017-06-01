using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFReports
{
    public class HeaderControl : ContentControl
    {
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(HeaderControl), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(HeaderControl), new PropertyMetadata(null));
    }
}
