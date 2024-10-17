using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace Cr7Sund
{
    public static class OpenUPMInstaller
    {
        private static bool InitializeOpenUPM()
        {
            Debug.Log("Initializing OpenUPM...");

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c npm install -g openupm-cli")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = Application.dataPath + "/.."
            };

            try
            {
                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize OpenUPM: {e.Message}");
                return false;
            }
        }

        public static void InstallPackage(string packageName)
        {
            if (!IsOpenUPMInitialized())
            {
                Debug.Log("OpenUPM is not initialized. Attempting to initialize...");
                if (!InitializeOpenUPM())
                {
                    Debug.LogError("Failed to initialize OpenUPM. Please make sure Node.js and npm are installed.");
                    return;
                }
            }

            string command = $"openupm add {packageName}";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = Application.dataPath + "/.."
            };

            try
            {
                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Debug.LogError($"Failed to install package {packageName}. Error: {error}");
                        return;
                    }

                    Debug.Log($"Successfully installed package {packageName}");
                    if (!string.IsNullOrEmpty(output))
                        Debug.Log(output);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error during package installation: {e.Message}");
                return;
            }

            AssetDatabase.Refresh();
        }
        private static bool IsOpenUPMInitialized()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c openupm --version")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = Application.dataPath + "/.."
            };

            try
            {
                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}