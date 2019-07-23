using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Dynamo.Wpf.Extensions;
using Dynamo.Engine.CodeGeneration;
using Dynamo.ViewModels;
using Dynamo.Models;
using System.IO;
using Dynamo.Events;
using Dynamo.Graph.Workspaces;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Dynamo.Core;

namespace DynamoPandas.PandamoViewExtension
{
    /// <summary>
    /// DiagnosticToolkik view extension
    /// </summary>
    public class PandamoViewExtension : IViewExtension, IDisposable
    {
        private MenuItem pandamoMenuItem;
        private DynamoViewModel vm;
        
        PandamoWindowViewModel pandamoViewModel { get; set; }

        public void Startup(ViewStartupParams p)
        {

        }

        public void Loaded(ViewLoadedParams parameters)
        {
            // Save a reference to your loaded parameters.
            // You'll need these later when you want to use
            // the supplied workspaces
            vm = parameters.DynamoWindow.DataContext as DynamoViewModel;
            pandamoMenuItem = new MenuItem { Header = "Pandamo" };

            MenuItem startServer = new MenuItem { Header = "Start Server" };
            startServer.Click += (sender, args) =>
            {
                pandamoViewModel = new PandamoWindowViewModel(parameters);
                var window = new PandamoWindow(pandamoViewModel)
                {
                    DataContext = pandamoViewModel,
                    Owner = parameters.DynamoWindow
                };
                // Show a modeless window.
                window.Show();
            };
            pandamoMenuItem.Items.Add(startServer);

            //parameters.AddSeparator(MenuBarType.View, new Separator());
            //parameters.AddMenuItem(MenuBarType.View, pandamoMenuItem);
            parameters.dynamoMenu.Items.Add(pandamoMenuItem);
        }



        public void Dispose()
        {
        }

        public void Shutdown()
        {
            // To shut down the server we need to send Ctrl+C to the cmd process
            Process p = pandamoViewModel.PandamoProcess;
            if (AttachConsole((uint)p.Id))
            {
                SetConsoleCtrlHandler(null, true);
                try
                {
                    if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0))
                        p.WaitForExit();
                }
                finally
                {
                    FreeConsole();
                    SetConsoleCtrlHandler(null, false);
                }
            }
        }

        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public string Name
        {
            get
            {
                return "Pandamo";
            }
        }


        // SetConsoleCtrlHandler, FreeConsole, AttachConsole and GenerateConsoleCtrlEvent are native WinAPI methods
        // that we need to send Ctrl+C to the cmd
        internal const int CTRL_C_EVENT = 0;
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);
    }
}