using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WPFReports.Controls
{
    /// <summary>
    /// Interaction logic for DisplayControl.xaml
    /// </summary>
    public partial class DisplayControl : UserControl
    {
        public DisplayControl()
        {
            InitializeComponent();
        }

        #region Properties
        public string Layout
        {
            get { return (string)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }
        public string Exception
        {
            get { return (string)GetValue(ExceptionProperty); }
            set { SetValue(ExceptionProperty, value); }
        }

        #endregion
        #region Dependency
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register("Layout",
            typeof(string), typeof(DisplayControl), new FrameworkPropertyMetadata(OnLayoutChanged));

        public static readonly DependencyProperty ExceptionProperty = DependencyProperty.Register("Exception",
            typeof(string), typeof(DisplayControl), new PropertyMetadata(null));

        #endregion
        #region Methods
        private void SetContent(object data) => content.Content = data;

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DisplayControl;
            var data = e.NewValue as string;
            if (string.IsNullOrEmpty(data))
            {
                control.SetContent(null);
                return;
            }
            object content = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.Default.GetBytes(data)))
                {
                    content = XamlReader.Load(stream);
                }
            }
            catch (Exception exc)
            {
                control.Exception = exc.ToString();
            }

            control.SetContent(content);
        }
        #endregion
    }
}
