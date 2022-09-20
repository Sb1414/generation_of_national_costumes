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
        private BitmapImage userFile;
        private List<BitmapImage> resources = new List<BitmapImage>();
        int counter = 0;
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
                userFile = getImageSourceFromFile(File.OpenRead(openFile.FileName));
                SelectedImagePreview.Source = userFile;
            }
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {


            if (userFile != null)
            {
                var userDrawing = new ImageDrawing();
                userDrawing.ImageSource = userFile;
                userDrawing.Rect = new Rect(new Size(userFile.Width, userFile.Height));

                var resourceDrawing = new ImageDrawing();
                resourceDrawing.ImageSource = resources[counter];
                resourceDrawing.Rect = new Rect(new Size(userFile.Width, userFile.Height));


                DrawingGroup dg = new DrawingGroup();
                //dg.Children.Add(new ImageDrawing(test, new Rect(0,0,(ImageMaker.Parent as Grid).ActualWidth, (ImageMaker.Parent as Grid).ActualHeight)));
                dg.Children.Add(userDrawing);
                dg.Children.Add(resourceDrawing);
               // dg.Children.Add(new ImageDrawing(resources[counter], new Size(userFile.Width, userFile.Height));
                
                DrawingImage di = new DrawingImage(dg);
                ImageMaker.Source = di;
                counter++;
                if (counter == resources.Count) 
                {
                    counter = 0;
                }
            }
        }


        private BitmapImage getImageSourceFromFile(FileStream image) 
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = image;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        private void LoadResources() 
        {
            resources.Add(getImageSourceFromUri("dressRes3.png"));
            resources.Add(getImageSourceFromUri("dressRes4.png"));
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
