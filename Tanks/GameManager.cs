using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Tanks
{
    public class GameManager
    {
        private List<Process> processes = new List<Process>();

        private List<Tank> tanks = new List<Tank>();

        private List<string> lastCommand = new List<string>();
        private List<bool> lastCommandDone = new List<bool>();
        bool started = false;

        private System.Timers.Timer sensorTimer;

        public class WeaponCommand
        {
            public int weaponID;
            public int targetID;
        }

        public class MoveCommand
        {
            public double targetSpeed;
            public double targetOrientation;
        }

        private KeyValuePair<List<WeaponCommand>, MoveCommand> getCommandFromString(string commandString)
        {

        }

        public GameManager(List<string> executablePaths)
        {
            sensorTimer = new System.Timers.Timer();
            sensorTimer.Interval = 40;
            sensorTimer.Elapsed += sensorTimer_Elapsed;

            /// TODO: create executables here(bind stdins and stdouts)

            for(int i = 0; i < executablePaths.Count; i++)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = executablePaths[i];

                Process process = new Process();
                process.StartInfo = startInfo;
                process.OutputDataReceived += new DataReceivedEventHandler
                (
                    delegate(object sender, DataReceivedEventArgs e)
                    {
                        using (StreamReader output = process.StandardOutput)
                        {
                            lastCommand[i] += output.ReadToEnd();
                            if (lastCommand[i].Contains('\n'))
                            {
                                lastCommandDone[i] = true;
                                if (started)
                                {
                                    var command = getCommandFromString(lastCommand[i].Split('\n')[0]);
                                    tanks[i].StepTank(command.Key, command.Value);

                                    lastCommand[i] = "";
                                    lastCommandDone[i] = false;
                                }
                            }
                        }
                    }
                );
                process.Start();

                processes.Add(process);
                process.BeginOutputReadLine();
            }

            if (!FirstRun())
            {
                Console.WriteLine("An Error Occurred!!!");
                return;
            }
        }

        private Tank getTankFromString(string tankString)
        {

        }

        private bool FirstRun()
        {
            /// send getTank command to each process
            /// create tanks accordingly
            for(int i = 0; i < processes.Count; i++)
            {
                processes[i].StandardInput.WriteLine("getTank");
                Console.WriteLine(processes[i].ProcessName + ": Get Tank!!");


                while (!lastCommandDone[i])
                    Thread.Sleep(100);
                Tank t = getTankFromString(lastCommand[i]);
                lastCommand[i] = "";

                if (t == null)
                {
                    Console.WriteLine(processes[i].ProcessName + ": Get Tank Done!!");
                    return false;
                }
                else
                {
                    tanks.Add(t);
                    Console.WriteLine(processes[i].ProcessName + ": Get Tank Done!!");
                }
            }

            /// send start command to each process
            foreach (var pr in processes)
                pr.StandardInput.WriteLine("start");
            started = true;
            Console.WriteLine("Start!!");

            sensorTimer.Enabled = true;

            return true;
        }

        #region sensors
        void sensorTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StepSensors();
        }

        private string getSensorResultString(List<string> sensorResult)
        {
            string result = "";
            sensorResult.ForEach(sr => result += sr + ";");
            return result.Remove(result.Length - 1);
        }

        private void StepSensors()
        {
            Console.WriteLine("Step Sensors!!");

            List<List<string>> sensorResults = new List<List<string>>();
            tanks.ForEach(t => sensorResults.Add(t.RunSensors()));

            List<string> sensorResultsAsString = new List<string>();
            sensorResults.ForEach(sr => sensorResultsAsString.Add(getSensorResultString(sr)));

            /// TODO: send sensor results to each process through stdin
            for (int i = 0; i < processes.Count; i++)
                processes[i].StandardInput.WriteLine(sensorResults[i]);
        }
        #endregion
    }
}
