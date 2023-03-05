// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Diagnostics;

public class Program
{
	private static int Execute(string? args)
	{
		Process? process = Process.Start(new ProcessStartInfo()
		{
			FileName = "cmd.exe",
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
			CreateNoWindow = true,
			Arguments = string.IsNullOrEmpty(args) ? "/C" : "/C " + args
		});
		if (process == null)
		{
			Console.WriteLine("Error");
			return -1;
		}
		process.WaitForExit();
		Console.WriteLine(process.StandardOutput.ReadToEnd());
		Console.WriteLine(process.StandardError.ReadToEnd());
		int ret = process.ExitCode;
		process.Close();


		return ret;
	}
}
