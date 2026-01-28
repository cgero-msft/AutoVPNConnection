using System.Diagnostics;

class Program
{
    private const string VPN_NAME = "YOUR-VPN-NAME-HERE";
    private const int CHECK_INTERVAL_MINUTES = 5;

    static async Task Main(string[] args)
    {
        while (true)
        {
            try
            {
                if (!IsConnectedToVPN(VPN_NAME))
                {
                    ConnectToVPN(VPN_NAME);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            await Task.Delay(TimeSpan.FromMinutes(CHECK_INTERVAL_MINUTES));
        }
    }

    private static bool IsConnectedToVPN(string vpnName)
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "rasdial",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using var process = Process.Start(processInfo);
            if (process == null) return false;

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output.Contains(vpnName, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private static void ConnectToVPN(string vpnName)
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "rasdial",
                Arguments = $"\"{vpnName}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using var process = Process.Start(processInfo);
            process?.WaitForExit();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private static void LogError(Exception ex)
    {
        try
        {
            string logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AutoVPNConnection",
                "errors.log"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);

            File.AppendAllText(logPath, 
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}");
        }
        catch
        {
            // Silently fail if logging fails
        }
    }
}
