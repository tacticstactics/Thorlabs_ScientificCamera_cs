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
using System.Windows.Threading;
using Thorlabs.TSI.ColorInterfaces;
using Thorlabs.TSI.ColorProcessor;
using Thorlabs.TSI.Core;
using Thorlabs.TSI.CoreInterfaces;
using Thorlabs.TSI.Demosaicker;
using Thorlabs.TSI.ImageData;
using Thorlabs.TSI.ImageDataInterfaces;
using Thorlabs.TSI.TLCamera;
using Thorlabs.TSI.TLCameraInterfaces;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonInit_Click(object sender, RoutedEventArgs e)
        {

            private readonly DispatcherTimer _dispatcherTimerUpdateUI = new DispatcherTimer();

        private Bitmap _latestDisplayBitmap;
        private ITLCameraSDK _tlCameraSDK;
        private ITLCamera _tlCamera;

        private ushort[] _demosaickedData = null;
        private ushort[] _processedImage = null;
        private Demosaicker _demosaicker = new Demosaicker();
        private ColorFilterArrayPhase _colorFilterArrayPhase;
        private ColorProcessor _colorProcessor = null;
        private bool _isColor = false;
        private ColorProcessorSDK _colorProcessorSDK = null;

                   
        _tlCameraSDK = TLCameraSDK.OpenTLCameraSDK();
            var serialNumbers = _tlCameraSDK.DiscoverAvailableCameras();

            if (serialNumbers.Count > 0)
            {
                _tlCamera = _tlCameraSDK.OpenCamera(serialNumbers.First(), false);

                this._tlCamera.ExposureTime_us = 50000;
                if (this._tlCamera.GainRange.Maximum > 0)
                {
            const double gainDb = 6.0;
            var gainIndex = this._tlCamera.ConvertDecibelsToGain(gainDb);
            this._tlCamera.Gain = gainIndex;
        }
                if (this._tlCamera.BlackLevelRange.Maximum > 0)
                {
            this._tlCamera.BlackLevel = 48;
        }

                this._isColor = this._tlCamera.CameraSensorType == CameraSensorType.Bayer;
                if (this._isColor)
                {
            this._colorProcessorSDK = new ColorProcessorSDK();
            this._colorFilterArrayPhase = this._tlCamera.ColorFilterArrayPhase;
            var colorCorrectionMatrix = this._tlCamera.GetCameraColorCorrectionMatrix();
            var whiteBalanceMatrix = this._tlCamera.GetDefaultWhiteBalanceMatrix();
            this._colorProcessor = (ColorProcessor)this._colorProcessorSDK.CreateStandardRGBColorProcessor(whiteBalanceMatrix, colorCorrectionMatrix, (int)this._tlCamera.BitDepth);
        }

                this._tlCamera.OperationMode = OperationMode.SoftwareTriggered;

                this._tlCamera.Arm();

                this._tlCamera.IssueSoftwareTrigger();

                this._dispatcherTimerUpdateUI.Interval = TimeSpan.FromMilliseconds(100);
                this._dispatcherTimerUpdateUI.Tick += this.DispatcherTimerUpdateUI_Tick;
                this._dispatcherTimerUpdateUI.Start();
            }
            else
            {
                MessageBox.Show("No Thorlabs camera detected.");
            }
        }


       }

    



        private void OnMenuExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}
