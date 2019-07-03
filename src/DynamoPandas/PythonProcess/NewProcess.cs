using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Constants;

namespace DynamoPandas.PythonProcess
{
    internal class NewProcess
    {
        internal static string CreateNewProcess(string arguments)
        {
            // Set working directory and create process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = "",
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            // Pass multiple commands to cmd.exe
            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(PythonConstants.EnviormentActivateBat);
                    // run your script. You can also pass in arguments
                    sw.WriteLine("python " + arguments);
                }
            }
            process.WaitForExit();
            string outputErrors = string.Empty;
            StringBuilder outputResults = new StringBuilder();

            // Create an instance of StreamReader to read from the cmd output.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = process.StandardOutput)
            {
                string line = sr.ReadLine();
                int counter = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    if (counter > 4 && line != "(pandamo) C:\\Users\\SylvesterKnudsen\\Desktop\\pandasDynamo>" && line != "")
                    {
                        outputResults.AppendLine(line);
                    }
                    counter++;
                }
                    
            }
            outputErrors = process.StandardError.ReadToEnd();
            if (outputErrors != string.Empty)
            {
                return outputErrors;
            }
            return outputResults.ToString();
 
        }

        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
     
        }
    }
}
