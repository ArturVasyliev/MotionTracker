using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using Emgu.CV.Util;

namespace MotionTracker
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture = null; //Camera
        private bool _captureInProgress = false; //Variable to track camera state
        int CameraDevice = 0; //Variable to track camera device selected
        Video_Device[] WebCams; //List containing all the camera available
        Mat currentMat = null;
        Mat oldMat = null;
        Mat resultMat = null;
        Image<Gray, byte> greyOldImage = null;
        Image<Gray, byte> greyNewImage = null;
        Image<Gray, byte> thresholdImage = null;
        Image<Gray, byte> erodedImage = null;
        Mat element = CvInvoke.GetStructuringElement(ElementShape.Cross, new Size(3, 3), new Point(-1, -1));
        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        Mat hierarchy = new Mat();

        public Form1()
        {
            InitializeComponent();

            //-> Find systems cameras with DirectShow.Net dll
            //thanks to carles lloret
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            WebCams = new Video_Device[_SystemCamereas.Length];
            for (int i = 0; i < _SystemCamereas.Length; i++)
            {
                WebCams[i] = new Video_Device(i, _SystemCamereas[i].Name, _SystemCamereas[i].ClassID); //fill web cam array
                Camera_Selection.Items.Add(WebCams[i].ToString());
            }
            if (Camera_Selection.Items.Count > 0)
            {
                Camera_Selection.SelectedIndex = 0; //Set the selected device the default
                captureButton.Enabled = true; //Enable the start
            }
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {
                    //stop the capture
                    captureButton.Text = "Start Capture"; //Change text on button
                    _capture.Pause(); //Pause the capture
                    _captureInProgress = false; //Flag the state of the camera
                }
                else
                {
                    //Check to see if the selected device has changed
                    if (Camera_Selection.SelectedIndex != CameraDevice)
                    {
                        SetupCapture(Camera_Selection.SelectedIndex); //Setup capture with the new device
                    }

                    //RetrieveCaptureInformation(); //Get Camera information
                    captureButton.Text = "Stop"; //Change text on button
                    //StoreCameraSettings(); //Save Camera Settings
                    _capture.Start(); //Start the capture
                    _captureInProgress = true; //Flag the state of the camera
                }

            }
            else
            {
                //set up capture with selected device
                SetupCapture(Camera_Selection.SelectedIndex);
                //Be lazy and Recall this method to start camera
                captureButton_Click(null, null);
            }
        }

        private void SetupCapture(int Camera_Identifier)
        {
            //update the selected device
            CameraDevice = Camera_Identifier;

            //Dispose of Capture if it was created before
            if (_capture != null) _capture.Dispose();
            try
            {
                //Set up capture device
                _capture = new VideoCapture(CameraDevice);
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            oldMat = currentMat;
            currentMat = new Mat();
            _capture.Read(currentMat);

            if (oldMat != null)
            {
                resultMat = new Mat();
                greyOldImage = oldMat.ToImage<Gray, Byte>(); //oldMat.ToImage<Bgr, Byte>();
                greyNewImage = currentMat.ToImage<Gray, Byte>();
                CvInvoke.AbsDiff(greyOldImage, greyNewImage, resultMat);
                thresholdImage = new Image<Gray, byte>(greyNewImage.Width, greyNewImage.Height);
                erodedImage = new Image<Gray, byte>(greyNewImage.Width, greyNewImage.Height);

                CvInvoke.Threshold(resultMat, thresholdImage, 20, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                CvInvoke.Erode(thresholdImage, erodedImage, element, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));


                var coloredImage = new Image<Bgr, byte>(greyNewImage.Width, greyNewImage.Height);

                CvInvoke.FindContours(erodedImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                CvInvoke.DrawContours(coloredImage, contours, -1, new MCvScalar(255, 0, 0));

                //CvInvoke.Erode(thresholdImage, erodedImage, IntPtr.Zero, 2);

                //CvInvoke.Dilate(eroded, temp, element, new Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
                //CvInvoke.Subtract(img2, temp, temp);
                //CvInvoke.BitwiseOr(skel, temp, skel);
            }

            DisplayCurrentImage(currentMat.Bitmap);

            if (resultMat != null)
            {
                DisplayDifferenceImage(erodedImage.Bitmap);
            }
        }

        private delegate void DisplayCurrentImageDelegate(Bitmap Image);
        private void DisplayCurrentImage(Bitmap Image)
        {
            captureBox.SizeMode = PictureBoxSizeMode.Zoom;

            if (captureBox.InvokeRequired)
            {
                try
                {
                    DisplayCurrentImageDelegate DI = new DisplayCurrentImageDelegate(DisplayCurrentImage);
                    this.BeginInvoke(DI, new object[] { Image });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                captureBox.Image = Image;
            }
        }

        private delegate void DisplayDifferenceImageDelegate(Bitmap Image);
        private void DisplayDifferenceImage(Bitmap Image)
        {
            diffBox.SizeMode = PictureBoxSizeMode.Zoom;

            if (diffBox.InvokeRequired)
            {
                try
                {
                    DisplayDifferenceImageDelegate DI = new DisplayDifferenceImageDelegate(DisplayDifferenceImage);
                    this.BeginInvoke(DI, new object[] { Image });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                diffBox.Image = Image;
            }
        }
    }
}
