using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeTheRecoveryPartition
{
    internal class CMDCommands
    {
        /// <summary>
        /// Executes a command using the cmd.exe process.
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="exitCode">The exit code of the executed command</param>
        /// <returns>The output of the command</returns>
        public static string ExecuteCmdCommand(string command, ref int exitCode)
        {
            ProcessStartInfo processInfo;
            Process process = new Process();
            string output = string.Empty;
            processInfo = new ProcessStartInfo("cmd.exe", "/C " + command);
            processInfo.CreateNoWindow = true;
            //processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            process.StartInfo = processInfo;
            process = Process.Start(processInfo);
            StreamReader streamReader = process.StandardOutput;
            output = streamReader.ReadToEnd();
            exitCode = process.ExitCode;
            process.Close();
            return output;
        }

        public static string ExecuteCmdCommandWithoutExitCode(string command)
        {
            int exitCode = 0;
            return ExecuteCmdCommand(command, ref exitCode);
        }

        static string ReadAsync(StreamReader stream)
        {
            var task = stream.ReadToEndAsync();
            task.Wait(); // Wait for the asynchronous read to complete

            if (task.Exception != null)
            {
                Console.WriteLine($"Error reading output: {task.Exception}");
                return string.Empty;
            }

            return task.Result;
        }

        public static string CreateDiskpartScriptCommand(string command)
        {
            string sd = string.Empty;
            // Get all drives excluding CD drives and network drives.
            //List<DriveInfo> allDrivesInfo = DriveInfo.GetDrives().Where(x => x.DriveType != DriveType.Network).OrderBy(c => c.Name).ToList();

            try
            {
                string scriptFilePath = System.IO.Path.GetTempPath() + @"dpScript.txt";
                if (File.Exists(scriptFilePath))
                {
                    File.Delete(scriptFilePath); // Delete the script if it exists.
                }
                File.AppendAllText(scriptFilePath, command); // Exit.
                int exitCode = 0;

                string commandWithPath = "diskpart /s \"" + scriptFilePath + "\"";
                //string resultStr = ExecuteCmdCommand("DiskPart.exe" + " /s " + scriptFilePath, ref exitCode);
                string resultStr = CMDCommands.ExecuteCmdCommand(commandWithPath, ref exitCode);
                File.Delete(scriptFilePath); // Delete the script file.
                                             
                //Console.WriteLine(resultStr);
                return resultStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
