using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using UserControl = System.Windows.Controls.UserControl;
using Point = System.Drawing.Point;
using Python.Included;
using System.Diagnostics;
using Python.Runtime;
using System.Threading.Tasks;
using System.Reflection;

namespace BodyPoseEstimation.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {   
        List<String> files = new List<String>();
        Dictionary<String,String> picHeights = new Dictionary<String,String>();

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
                Filter = "JPG|*jpg|PNG|*.png"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in ofd.FileNames)
                {
                    FileInfo fi = new FileInfo(fileName);
                    using (FileStream stream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                    {
                        string fname = fileName.Replace(@"\", "/");
                        files.Add(fname);
                        Debug.WriteLine(fname);
                        Image img = createImage(fi);
                        img.MouseLeftButtonDown += (s, e) => {
                            image_Click(img);
                        };
                        gallery.Children.Add(img);
                    }
                }
            }
        }

        private void image_Click(Image img)
        { 
            Form form = new Form();
            TextBox textBox = new TextBox();
            textBox.Width = 200;
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            form.Controls.Add(textBox);
            Button btn = new Button();
            btn.Width = 200;
            btn.Text = "Submit height (cm)";
            btn.Location = new Point(0, 30);
            btn.BackColor = System.Drawing.Color.Green;
            btn.Click += (s, e) =>
            {
                img.Effect = new DropShadowEffect();
                string filePath = img.Source.ToString();
                filePath = filePath.Remove(0, 8);
                picHeights[filePath] = textBox.Text;
                form.Hide();
            };
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterParent;
            form.AcceptButton = btn;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.Add(btn);
            form.Height = 75;
            form.Width = 200;
            form.ShowDialog();
        }
 
        private Image createImage(FileInfo fi)
        {
            BitmapImage bitmapImage = new BitmapImage();
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));
            img.Width = 75;
            img.Height = 75;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.Opacity = 40;
            img.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            img.Effect = new BlurEffect();
            return img;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {   
            files = new List<String>(); 
            gallery.Children.Clear();
            Debug.WriteLine("clearing");
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            if (!Installer.IsPythonInstalled())
            {
                Debug.WriteLine("Starting python.included process");
                await downloadPython();
                Debug.WriteLine("Finished");
            }
            string path = System.IO.Path.GetFullPath(@"..\..\..\");
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "input.txt"), false))
            {
                foreach (String file in files)
                {
                    string height = "NAN";
                    if (picHeights.ContainsKey(file)) height = picHeights[file];
                    await sw.WriteAsync(file + " " + height + "\n");
                }
            }
            Debug.WriteLine("done");
        }

        private async static Task downloadPython()
        {
            // see what the installer is doing
            Installer.LogMessage += Console.WriteLine;

            Installer.InstallPath = Installer.InstallPath + "\\pytest";
            Python.Deployment.Installer.InstallPath = Installer.InstallPath;
            if (!Directory.Exists(Installer.InstallPath))
            {
                Directory.CreateDirectory(Installer.InstallPath);
            }

            // install the embedded python distribution
            await Installer.SetupPython();

            string path = System.IO.Path.GetFullPath(@"..\..\..\");

            // install pip3 for package installation
            if (!Installer.IsPipInstalled())
            {
                string libDir = Path.Combine(Installer.EmbeddedPythonHome, "Lib");
                Debug.WriteLine(libDir);
                if (!Directory.Exists(libDir))
                {
                    Directory.CreateDirectory(libDir);
                }
                Process.Start("cmd", $"/C cd {libDir} && curl https://bootstrap.pypa.io/get-pip.py -o get-pip.py");
                Process.Start("cmd", $"/C cd {Installer.EmbeddedPythonHome} && python.exe Lib\\get-pip.py");
            }

            string pipPath = Path.Combine(Installer.EmbeddedPythonHome, "Scripts");

            // install libraries
            if (!Installer.IsModuleInstalled("mediapipe"))
            {
                Process.Start("cmd", $"/C cd {pipPath} && pip.exe install mediapipe");
            }

            if (!Installer.IsModuleInstalled("opencv-python"))
            {
                Process.Start("cmd", $"/C cd {pipPath} && pip.exe install opencv-python");
            }

            // start python engine
            PythonEngine.Initialize();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Installer.EmbeddedPythonHome + @"\python.exe",
                    Arguments = "Python/Script/PoseDetection.py",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = path
                },
                EnableRaisingEvents = true
            };

            process.ErrorDataReceived += Process_OutputDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            
            Debug.WriteLine(e.Data);
        }
    }


}
