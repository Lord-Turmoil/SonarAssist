using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using System.Collections;
using System.Diagnostics;

public class Program
{
	public static void Main(string[] args)
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();

		string? value;
		Console.WriteLine(dict.TryGetValue("hello", out value) ? value : "Nope");
		Console.WriteLine(value == null ? "Null" : "Not Null");
		dict["me"] = "Oops";
		Console.WriteLine(dict.TryGetValue("me", out value) ? value : "Nope");
	}

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
