using System;
using System.Collections.Generic;
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
using ClassLibrary;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand AddCustomV4DataCollectionCommand = new RoutedCommand("AddCustomV4DataCollectionCommand", typeof(WpfApp.MainWindow));

        V4MainCollection V4MC = new V4MainCollection();
        CustomClass customClass;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = V4MC;

            customClass = new CustomClass(V4MC);
            stackPanel_AddCustom.DataContext = customClass;
        }
        
        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (V4MC.ChangesWereMade == true)
            {
                MessageBoxResult res = MessageBox.Show("Data can be lost. Continue?\nPress \"YES\", if you want to continue without saving." +
                                                        "\nPress \"NO\", if you want to save current data in the file.", "", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes)
                {
                    V4MC = new V4MainCollection();
                    DataContext = V4MC;

                    customClass = new CustomClass(V4MC);
                    stackPanel_AddCustom.DataContext = customClass;

                }
                if (res == MessageBoxResult.No)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                    if (dlg.ShowDialog() == true)
                    {
                        string filename = dlg.FileName;
                        V4MC.Save(filename);
                        V4MC.ChangesWereMade = false;
                    }
                }
            }
            else
            {
                V4MC = new V4MainCollection();
                DataContext = V4MC;

                customClass = new CustomClass(V4MC);
                stackPanel_AddCustom.DataContext = customClass;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (V4MC.ChangesWereMade == true)
            {
                MessageBoxResult res = MessageBox.Show("Data can be lost. Continue?\nPress \"YES\", if you want to continue without saving." +
                                                        "\nPress \"NO\", if you want to continue save current data in the file.", "", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel)
                {
                    flag = false;
                }
                else if (res == MessageBoxResult.No)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        string filename = dlg.FileName;
                        V4MC.Save(filename);
                        V4MC.ChangesWereMade = false;
                        flag = false;
                    }
                }
            }
            if (flag == true)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                {
                    try
                    {
                        string filename = dlg.FileName;
                        V4MC = V4MainCollection.Load(filename);
                        DataContext = V4MC;

                        customClass = new CustomClass(V4MC);
                        stackPanel_AddCustom.DataContext = customClass;
                        
                        V4MC.ChangesWereMade = true;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string filename = dlg.FileName;
                    V4MC.Save(filename);
                    V4MC.ChangesWereMade = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Add_Defaults_Click(object sender, RoutedEventArgs e)
        {
            V4MC.AddDefaults();
        }

        private void Add_Default_V4DataCollection_Click(object sender, RoutedEventArgs e)
        {
            V4DataCollection item = new V4DataCollection("random V4DC", 9d);
            item.InitRandom(5, 4, 4, 0, 9);
            V4MC.Add(item);
        }

        private void Add_Default_V4DataOnGrid_Click(object sender, RoutedEventArgs e)
        {
            Grid2D gr = new Grid2D(1,2,3,5);

            V4DataOnGrid item = new V4DataOnGrid("random V4DOG", 8d, gr);
            item.InitRandom(1, 9);
            V4MC.Add(item);
        }

        private void Add_Element_from_File_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string filename = dlg.FileName;
                    V4DataCollection item = new V4DataCollection(filename);
                    V4MC.Add(item);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            V4MC.Remove2((V4Data)listBox_Main.SelectedItem);
        }

        private void FilterByV4DataOnFrid(object sender, FilterEventArgs args)
        {
            if (args.Item != null)
            {
                if (args.Item.GetType().Name == "V4DataOnGrid") args.Accepted = true;
                else args.Accepted = false;
            }
        }
        private void FilterByV4DataCollection(object sender, FilterEventArgs args)
        {
            if (args.Item != null)
            {
                if (args.Item.GetType().Name == "V4DataCollection") args.Accepted = true;
                else args.Accepted = false;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (V4MC.ChangesWereMade == true)
            {
                MessageBoxResult res = MessageBox.Show("Data can be lost. Continue?\nPress \"YES\", if you want to continue without saving.\n" +
                                                        "Press \"NO\", if you want to save current data in the file.", "", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.No)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        string filename = dlg.FileName;
                        V4MC.Save(filename);
                        V4MC.ChangesWereMade = false;
                    }
                }
            }
        }

        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult res = MessageBoxResult.None;
            if (V4MC.ChangesWereMade == true)
            {
                res = MessageBox.Show("Data can be lost. Continue?\nPress \"OK\", if you want to continue without saving.", "", MessageBoxButton.OKCancel);
            }
            if (res == MessageBoxResult.OK)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                {
                    try
                    {
                        string filename = dlg.FileName;
                        V4MC = new V4MainCollection();
                        V4MC = V4MainCollection.Load(filename);
                        DataContext = V4MC;

                        customClass = new CustomClass(V4MC);
                        stackPanel_AddCustom.DataContext = customClass;

                        V4MC.ChangesWereMade = true;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string filename = dlg.FileName;
                    V4MC.Save(filename);
                    V4MC.ChangesWereMade = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (V4MC.ChangesWereMade == true)
            {
                e.CanExecute = true;
                return;
            }
            e.CanExecute = false;
        }

        private void RemoveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            V4MC.Remove2((V4Data)listBox_Main.SelectedItem);
        }

        private void CanRemoveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listBox_Main.SelectedIndex != -1)
            {
                e.CanExecute = true;
                return;
            }
            e.CanExecute = false;
        }

        private void AddCustomCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            customClass.AddCustom();
        }

        private void CanAddCustomCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            foreach (FrameworkElement item in stackPanel_AddCustom.Children)
            {
                if (Validation.GetHasError(item) == true)
                { 
                    e.CanExecute = false;
                    return;
                }
                e.CanExecute = true;
            }
        }
    }
}
