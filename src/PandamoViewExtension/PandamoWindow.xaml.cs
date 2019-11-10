using MahApps.Metro.Controls;
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


namespace DynamoPandas.PandamoViewExtension
{
    /// <summary>
    /// Interaction logic for PandamoWindow.xaml
    /// </summary>
    public partial class PandamoWindow : MetroWindow
    {
        PandamoWindowViewModel pandamoVm;
        public PandamoWindow(PandamoWindowViewModel vm)
        {
            pandamoVm = vm;
            InitializeComponent();
            scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            bool AutoScroll = true;
            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
            }
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            pandamoVm.StartServer();
        }

        private void KillServerButton_Click(object sender, RoutedEventArgs e)
        {
            pandamoVm.KillServer();
        }
    }
}
