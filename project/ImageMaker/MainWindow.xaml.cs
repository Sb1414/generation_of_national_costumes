using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace ImageMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BitmapImage> files = new List<BitmapImage>();
        private List<BitmapImage> resources = new List<BitmapImage>();
        bool flag = true;
        public MainWindow()
        {
            InitializeComponent();
            LoadResources();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true) 
            {
                files.Add(getImageSourceFromFile(File.OpenRead(openFile.FileName)));
                SelectedImagePreview.Source = files.Last();
            }
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {

            ImageMaker.Source = (flag ? resources.First() : resources.Last());

            flag = !flag;
        }


        private BitmapImage getImageSourceFromFile(FileStream image) 
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = image;
            bitmap.EndInit();
            return bitmap;
        }

        private void LoadResources() 
        {
            resources.Add(getImageSourceFromUri("dressRes1.png"));
            resources.Add(getImageSourceFromUri("dressRes2.png"));
        }

        private BitmapImage getImageSourceFromUri(string name) 
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Resources/"+name);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
