using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using UserControl = System.Windows.Controls.UserControl;

namespace BodyPoseEstimation.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {   
        List<String> files = new List<String>();

        public HomeView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = true,
                ValidateNames = true,
                Filter = "JPG|*jpg|JPEG|*.jpeg|PNG|*.png"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in ofd.FileNames)
                {
                    FileInfo fi = new FileInfo(fileName);
                    using (FileStream stream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                    {
                        files.Add(fileName);
                        Image img = createImage(fi);
                        gallery.Children.Add(img);
                    }
                }
            }
        }

        private Image createImage(FileInfo fi)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));
            img.Width = 75;
            img.Height = 75;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            return img;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {   
            files = new List<String>(); 
            gallery.Children.Clear();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
