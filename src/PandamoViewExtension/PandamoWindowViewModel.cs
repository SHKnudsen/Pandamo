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
using DynamoPandas.Pandamo.Server;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Management;

namespace DynamoPandas.PandamoViewExtension
{
    public class PandamoWindowViewModel : NotificationObject, IDisposable
    {
        private readonly ViewLoadedParams viewParameters;
        private readonly DynamoViewModel dynamoViewModel;
        private readonly DynamoModel dynamoModel;
        private Process pandamoProcess;
        private string processOutput;
        private static readonly string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string userPath = System.Environment.GetEnvironmentVariable("USERPROFILE");
        private static readonly string currentUserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static readonly string extraPath = Path.GetFullPath(Path.Combine(assemblyFolder, @"..\extra"));

        public Process PandamoProcess
        {
            get { return pandamoProcess; }
            set { pandamoProcess = value; }
        }

        public string ProcessOutput
        {
            get { return processOutput; }
            set { processOutput = value; RaisePropertyChanged("ProcessOutput"); }
        }


        public PandamoWindowViewModel(ViewLoadedParams parameters)
        {
            this.viewParameters = parameters;
            this.dynamoViewModel = this.viewParameters.DynamoWindow.DataContext as DynamoViewModel;
            this.dynamoModel = this.dynamoViewModel.Model;
        }

        public void StartServer()
        {
            if (!DoesEnvironmentExsist())
            {
                ProcessOutput += "Pandamo Python environment does not exist, starting to create....\n";
                CreatePandamoEnvironmentFromYml();
                ProcessOutput += "Environment created, starting server...\n";
            }

            PandamoProcess = CreateNewProcess();
            
            string hasServerStarted = PandasServer.HasServerStarted();
            ProcessOutput += hasServerStarted + "\n";
        }

        private bool DoesEnvironmentExsist()
        {
            string pandamoEnvironmentPath = String.Format(@"{0}\Miniconda3\envs\pandamo\conda-meta", userPath);
            if (!Directory.Exists(pandamoEnvironmentPath))
            {
                return false;
            }
            return true;
        }

        private void CreatePandamoEnvironmentFromYml()
        {
            string minicondaPath = String.Format(@"{0}\Miniconda3\Scripts\activate.bat", userPath);
            string createPandamoEnvironment = "conda env create -f environment.yml";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = extraPath + @"\pandasDynamo",
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            using (StreamWriter sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(minicondaPath);
                    sw.WriteLine(createPandamoEnvironment);
                }
            }
            string output = process.StandardOutput.ReadToEnd();
            ProcessOutput += output + "\n";
            process.Kill();
        }

        private Process CreateNewProcess(bool showNoWindow = true)
        {         
            string startServerBat = "startPandamoServer.bat";
            // Set working directory and create process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = extraPath + @"\pandasDynamo",
                    FileName = "cmd.exe",
                    Arguments = string.Format("/k {0}", startServerBat), //start cmd /k
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = showNoWindow
                }
            };
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return process;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ProcessOutput += e.Data + "\n";
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            ProcessOutput += e.Data + "\n";
            //RaisePropertyChanged("ProcessOutput");

        }

        public void KillServer()
        {
            Process p = PandamoProcess;
            Process currentProcess = Process.GetCurrentProcess();
            KillAllProcessesSpawnedBy((uint)currentProcess.Id);
            p.Dispose();
            ProcessOutput += "Pandamo server has been shutdown";
        }

        private static void KillAllProcessesSpawnedBy(UInt32 parentProcessId)
        {
        
            // gets all child process from the current Dynamo process
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "SELECT * " +
                "FROM Win32_Process " +
                "WHERE ParentProcessId=" + parentProcessId);
            ManagementObjectCollection collection = searcher.Get();

            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    UInt32 childProcessId = (UInt32)item["ProcessId"];
                    Process childProcess = Process.GetProcessById((int)childProcessId);
                    string name = childProcess.ProcessName;
                    if (name == "cmd" || name == "flask" || name == "python" || name == "conhost")
                    {
                        KillAllProcessesSpawnedBy(childProcessId);
                        // We need to check that the process is not "flask" as the "flask
                        // process gets killed by its child process "python"
                        if (name != "flask")
                        {
                            childProcess = Process.GetProcessById((int)childProcessId);
                            Process[] pname = Process.GetProcessesByName(childProcess.ProcessName);
                            if (pname.Length > 0)
                            {
                                childProcess.Kill();
                            }
                        }   
                    }
                }
            }
        }

        public void Dispose()
        {
            PandamoProcess.OutputDataReceived -= Process_OutputDataReceived;
        }
    }
}
