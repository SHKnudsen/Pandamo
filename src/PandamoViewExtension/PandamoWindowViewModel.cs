using System;
using System.IO;
using System.Linq;
using Dynamo.Core;
using Dynamo.Engine.CodeGeneration;
using Dynamo.Events;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.Graph.Workspaces;
using Dynamo.Models;
using Dynamo.Wpf.Extensions;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Threading;
using Dynamo.ViewModels;
using DynamoPandas.Pandamo.Constants;
using System.Diagnostics;

namespace DynamoPandas.PandamoViewExtension
{
    public class PandamoWindowViewModel : NotificationObject, IDisposable
    {
        private readonly ViewLoadedParams viewParameters;
        private readonly DynamoViewModel dynamoViewModel;
        private readonly DynamoModel dynamoModel;
        private Process pandamoProcess;
        private string processOutput;

        public Process PandamoProcess
        {
            get { return pandamoProcess; }
            set { pandamoProcess = value; }
        }

        public string ProcessOutput
        {
            get { return processOutput; }
            set { processOutput = value; }
        }


        public PandamoWindowViewModel(ViewLoadedParams parameters)
        {
            this.viewParameters = parameters;
            this.dynamoViewModel = this.viewParameters.DynamoWindow.DataContext as DynamoViewModel;
            this.dynamoModel = this.dynamoViewModel.Model;
            StartServer();
        }

        public void StartServer()
        {
            PandamoProcess = CreateNewProcess();
            PandamoProcess.BeginOutputReadLine();
        }

        private Process CreateNewProcess(bool showNoWindow = true)
        {
            // Set working directory and create process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = PythonConstants.StartServerBat,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = showNoWindow
                }
            };
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            return process;
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            ProcessOutput += e.Data + "\n";
            RaisePropertyChanged("ProcessOutput");

        }

        public void Dispose()
        {
            PandamoProcess.OutputDataReceived -= Process_OutputDataReceived;
        }
    }
}
