using Python.Included;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Drawing.Point;
using UserControl = System.Windows.Controls.UserControl;

namespace BodyPoseEstimation.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        List<String> files = new List<String>();
        Dictionary<String, String> picHeights = new Dictionary<String, String>();
        string outputFolder = "";
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
                        img.MouseLeftButtonDown += (s, e) =>
                        {
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
            picHeights = new Dictionary<string, string>();
            gallery.Children.Clear();
            Debug.WriteLine("clearing");
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            if (outputFolder.Equals(""))
            {
                using (var dialog = new FolderBrowserDialog())
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        outputFolder = dialog.SelectedPath;
                    }
            }
            else
            {
                string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                using (StreamWriter sw = new StreamWriter(Path.Combine(path, "input.txt"), false))
                {
                    foreach (String file in files)
                    {
                        string height = "1";
                        if (picHeights.ContainsKey(file)) height = picHeights[file];
                        await sw.WriteAsync(file + "," + height + "\n");
                    }
                }
                await downloadPython(outputFolder);

                Debug.WriteLine("done");
                Form form = new Form();
                Button btn = new Button();
                btn.Width = 300;
                btn.Height = 300;
                btn.Text = "Finished. Click to close.";
                btn.Location = new Point(0, 30);
                btn.BackColor = System.Drawing.Color.Green;
                btn.Click += (s, e) =>
                {
                    form.Hide();
                };
                form.FormBorderStyle = FormBorderStyle.None;
                form.StartPosition = FormStartPosition.CenterParent;
                form.AcceptButton = btn;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.Controls.Add(btn);
                form.Height = 300;
                form.Width = 300;
                form.ShowDialog();
            }
        }

        private async static Task downloadPython(string outputFolder)
        {
            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string inputPath = Path.Combine(path, "input.txt");

            if (!Installer.IsPythonInstalled())
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

                // install pip3 for package installation
                if (!Installer.IsPipInstalled())
                {
                    string libDir = Path.Combine(Installer.EmbeddedPythonHome, "Lib");
                    Debug.WriteLine(libDir);
                    if (!Directory.Exists(libDir))
                    {
                        Directory.CreateDirectory(libDir);
                    }
                    var p1 = Process.Start("cmd", $"/C cd {libDir} && curl https://bootstrap.pypa.io/get-pip.py -o get-pip.py");
                    p1.Start();
                    p1.WaitForExit();
                    p1.Close();
                    var p = Process.Start("cmd", $"/K cd {Installer.EmbeddedPythonHome} && python.exe Lib\\get-pip.py");
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }

                string pipPath = Path.Combine(Installer.EmbeddedPythonHome, "Scripts");

                // install libraries
                if (!Installer.IsModuleInstalled("mediapipe"))
                {
                    var p = Process.Start("cmd", $"/C cd {pipPath} && pip.exe install mediapipe");
                    p.Start();
                    p.WaitForExit();
                }

                if (!Installer.IsModuleInstalled("opencv"))
                {
                    var p = Process.Start("cmd", $"/C cd {pipPath} && pip.exe install opencv-python");
                    p.Start();
                    p.WaitForExit();
                }

            }

            // start python engine
            PythonEngine.Initialize();
            string scriptpath = Path.Combine(path, "PoseDetection.py");
            string command = Path.Combine(path, scriptpath + " " + inputPath + " " + outputFolder);
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Installer.EmbeddedPythonHome + @"\python.exe",
                    Arguments = command,
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
            Debug.WriteLine("caling script");
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
