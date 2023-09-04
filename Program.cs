using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Prompt the user for confirmation
        Console.WriteLine("This will save the 'rarreg.key' file in the directory where WinRAR is installed.");
        Console.Write("Do you want to continue? (Y/N): ");
        string response = Console.ReadLine();

        if (response.Trim().ToUpper() != "Y")
        {
            Console.WriteLine("Operation cancelled.");
            return;
        }

        // Get the WinRAR installation directory
        string winrarInstallDir = GetWinRARInstallDirectory();

        if (string.IsNullOrEmpty(winrarInstallDir))
        {
            Console.WriteLine("WinRAR installation directory not found.");
            return;
        }

        // Content to be saved in rarreg.key
        string rarRegistrationData = @"RAR registration data
WinRAR
Unlimited Company License
UID=4b914fb772c8376bf571
6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d
cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717
7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565
b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd
982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190
6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9";

        // Combine the directory and file name to create the full path
        string filePath = Path.Combine(winrarInstallDir, "rarreg.key");

        // Save the content to rarreg.key file
        try
        {
            File.WriteAllText(filePath, rarRegistrationData);
            Console.WriteLine($"rarreg.key file has been saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
        }
    }

    static string GetWinRARInstallDirectory()
    {
        // Check the common WinRAR installation paths
        string[] commonPaths = new string[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
        };

        foreach (var path in commonPaths)
        {
            string winrarDir = Path.Combine(path, "WinRAR");

            if (Directory.Exists(winrarDir))
            {
                return winrarDir;
            }
        }

        return null; // WinRAR not found in common installation paths
    }
}
