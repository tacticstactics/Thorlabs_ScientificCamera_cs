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


namespace WpfApp1_Polling
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

            //private Bitmap _latestDisplayBitmap;
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

                _tlCamera.ExposureTime_us = 50000;
                if (_tlCamera.GainRange.Maximum > 0)
                {
            const double gainDb = 6.0;
            var gainIndex = this._tlCamera.ConvertDecibelsToGain(gainDb);
            _tlCamera.Gain = gainIndex;
        }
                if (_tlCamera.BlackLevelRange.Maximum > 0)
                {
            _tlCamera.BlackLevel = 48;
        }

                _isColor = _tlCamera.CameraSensorType == CameraSensorType.Bayer;
                if (_isColor)
                {
            _colorProcessorSDK = new ColorProcessorSDK();
            _colorFilterArrayPhase = _tlCamera.ColorFilterArrayPhase;
            var colorCorrectionMatrix = _tlCamera.GetCameraColorCorrectionMatrix();
            var whiteBalanceMatrix = _tlCamera.GetDefaultWhiteBalanceMatrix();
            _colorProcessor = (ColorProcessor)_colorProcessorSDK.CreateStandardRGBColorProcessor(whiteBalanceMatrix, colorCorrectionMatrix, (int)this._tlCamera.BitDepth);
        }

                _tlCamera.OperationMode = OperationMode.SoftwareTriggered;

                _tlCamera.Arm();

                _tlCamera.IssueSoftwareTrigger();

                _dispatcherTimerUpdateUI.Interval = TimeSpan.FromMilliseconds(100);
                _dispatcherTimerUpdateUI.Tick += DispatcherTimerUpdateUI_Tick;
                _dispatcherTimerUpdateUI.Start();
            
                else
                {
                //MessageBox.Show("No Thorlabs camera detected.");             
                }
               

   

            private void OnMenuExit(object sender, RoutedEventArgs e)
            {
            Close();
             }
        
    }
}
