#region Usings

using System.Linq;
using System.Windows;
using Microsoft.Win32;

#endregion

namespace CountUtility
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        /// <summary>
        ///     Instnace used to inspect a DLL.
        /// </summary>
        private DllInspector _dllInspector;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Opens a file dialog to slect a dll.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Count_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Select a DLL",
                Filter = "Dynamic Link Library|*.dll"
            };

            if (ofd.ShowDialog() != true)
                return;
            _dllInspector = new DllInspector(ofd.FileName);
            buttonGrid.Visibility = Visibility.Hidden;
            resultGrid.Visibility = Visibility.Visible;
            StartCount();
        }

        /// <summary>
        ///     Start counting.
        /// </summary>
        private void StartCount()
        {
            var methodNames = _dllInspector.GetUniqueName(_dllInspector.GetAllStaticMethods());
            txtOverloads.Text = methodNames.Count().ToString();
            txtUnique.Text = methodNames.Distinct().Count().ToString();
        }

        /// <summary>
        ///     Display browse button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _dllInspector = null;

            buttonGrid.Visibility = Visibility.Visible;
            resultGrid.Visibility = Visibility.Hidden;
        }
    }
}