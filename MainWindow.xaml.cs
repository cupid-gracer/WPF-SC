using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int timeout = 60 * 1000;
        public MainWindow()
        {
            InitializeComponent();
            Process currentProcess = Process.GetCurrentProcess();
            var runningProcess = (from process in Process.GetProcesses()
                                  where
                                    process.Id != currentProcess.Id &&
                                    process.ProcessName.Equals(
                                      currentProcess.ProcessName,
                                      StringComparison.Ordinal)
                                  select process).FirstOrDefault();
            if (runningProcess != null)
            {
                System.Windows.MessageBox.Show("App is running!");
                System.Windows.Application.Current.Shutdown();
            }
            interval();
        }

        private void btn_sc_Click(object sender, RoutedEventArgs e)
        {
            interval();
        }

        private async void interval()
        {
            
            for (; ; )
            {
                await Task.Run(()=> {
                    sc();
                    Thread.Sleep(timeout);
                   
                });
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

           
        }

        private void sc()
        {
            try

            {

                //Creating a new Bitmap object

                Bitmap captureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);

                //Creating a Rectangle object which will  

                //capture our Current Screen

                System.Drawing.Rectangle captureRectangle = Screen.AllScreens[0].Bounds;



                //Creating a New Graphics Object

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);



                //Copying Image from The Screen

                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);



                //Saving the Image File (I am here Saving it in My E drive).

                //captureBitmap.Save(@"E:\" + "1" + ".jpg", ImageFormat.Jpeg);
                string name = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                captureBitmap.Save(@"F:\01_web\wp-themes\sc\" + name + ".jpg", ImageFormat.Jpeg);



                //Displaying the Successfull Result



                //System.Windows.MessageBox.Show("Screen Captured");

            }

            catch (Exception ex)

            {
                Debug.Print(ex.ToString());
                //System.Windows.Forms.MessageBox.Show(ex.Message);

            }
        }
    }
}
