using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using UserControl = System.Windows.Controls.UserControl;

namespace BodyPoseEstimation.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            Console.WriteLine("nope" + open.FileName);
            if (open.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("OKAY");
                Console.WriteLine(open.FileName);
                // display image in picture box  
                img1.Source = new BitmapImage(new Uri(open.FileName, UriKind.Relative));
                img1.BringIntoView();
            }
        }
    }
}
