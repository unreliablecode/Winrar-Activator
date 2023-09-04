import java.io.*;
import java.util.prefs.Preferences;

public class Main {
    public static void main(String[] args) {
        // Prompt the user for confirmation
        System.out.println("This will save the 'rarreg.key' file in the directory where WinRAR is installed.");
        System.out.print("Do you want to continue? (Y/N): ");
        
        try (BufferedReader reader = new BufferedReader(new InputStreamReader(System.in))) {
            String response = reader.readLine().trim();
            
            if (!response.equalsIgnoreCase("Y")) {
                System.out.println("Operation cancelled.");
                return;
            }
        } catch (IOException e) {
            System.err.println("Error reading user input.");
            e.printStackTrace();
            return;
        }

        // Get the WinRAR installation directory
        String winrarInstallDir = getWinRARInstallDirectory();

        if (winrarInstallDir.isEmpty()) {
            System.out.println("WinRAR installation directory not found.");
            return;
        }

        // Content to be saved in rarreg.key
        String rarRegistrationData = "RAR registration data\n" +
                "WinRAR\n" +
                "Unlimited Company License\n" +
                "UID=4b914fb772c8376bf571\n" +
                "6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d\n" +
                "cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717\n" +
                "7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565\n" +
                "b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd\n" +
                "982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190\n" +
                "6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9\n";

        // Combine the directory and file name to create the full path
        String filePath = winrarInstallDir + File.separator + "rarreg.key";

        // Save the content to rarreg.key file
        try (PrintWriter writer = new PrintWriter(filePath)) {
            writer.write(rarRegistrationData);
            System.out.println("rarreg.key file has been saved to " + filePath);
        } catch (IOException e) {
            System.err.println("An error occurred while saving the file.");
            e.printStackTrace();
        }
    }

    private static String getWinRARInstallDirectory() {
        Preferences prefs = Preferences.userRoot().node("Software\\WinRAR");
        String winrarPath = prefs.get("exe64", "");

        if (!winrarPath.isEmpty()) {
            File winrarFile = new File(winrarPath);
            File winrarDir = winrarFile.getParentFile();

            if (winrarDir != null && winrarDir.exists()) {
                return winrarDir.getAbsolutePath();
            }
        }

        return "";
    }
}
