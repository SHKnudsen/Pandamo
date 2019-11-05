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

            pandamoMenuItem.Click += (sender, args) =>
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

            //parameters.AddSeparator(MenuBarType.View, new Separator());
            //parameters.AddMenuItem(MenuBarType.View, pandamoMenuItem);
            parameters.dynamoMenu.Items.Add(pandamoMenuItem);
            pandamoViewModel.StartServer();
        }



        public void Dispose()
        {
        }

        public void Shutdown()
        {
            pandamoViewModel.KillServer();
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
    }
}