using System.Diagnostics;

namespace AssemblyCompilerPipeline;

public static class ExecuteCommands
{
    public static string ExecuteCommand(string command)
    {
        try
        {
            using var compiler = new Process();
            
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.Arguments = "/C " + command;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            compiler.Start();

            var output = compiler.StandardOutput.ReadToEnd();

            compiler.WaitForExit();
           
            return output.Replace("\n", "").Replace("\r", "").Trim();
        }

        catch (Exception err)
        {
            return $"Error running command : {err.Message}\n";
        }
    }
}