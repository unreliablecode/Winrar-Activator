#include <iostream>
#include <fstream>
#include <cstdlib>
#include <string>
#include <Windows.h>

using namespace std;

// Function to get the WinRAR installation directory
string GetWinRARInstallDirectory() {
    char winrarPath[MAX_PATH];
    DWORD pathSize = sizeof(winrarPath);

    if (RegGetValue(HKEY_LOCAL_MACHINE, "SOFTWARE\\WinRAR", "exe64", RRF_RT_REG_SZ, nullptr, winrarPath, &pathSize) == ERROR_SUCCESS) {
        string winrarDir(winrarPath);
        size_t lastBackslash = winrarDir.find_last_of('\\');

        if (lastBackslash != string::npos) {
            return winrarDir.substr(0, lastBackslash);
        }
    }

    return "";
}

int main() {
    // Prompt the user for confirmation
    cout << "This will save the 'rarreg.key' file in the directory where WinRAR is installed." << endl;
    cout << "Do you want to continue? (Y/N): ";
    char response;
    cin >> response;

    if (response != 'Y' && response != 'y') {
        cout << "Operation cancelled." << endl;
        return 0;
    }

    // Get the WinRAR installation directory
    string winrarInstallDir = GetWinRARInstallDirectory();

    if (winrarInstallDir.empty()) {
        cout << "WinRAR installation directory not found." << endl;
        return 1;
    }

    // Content to be saved in rarreg.key
    string rarRegistrationData =
        "RAR registration data\n"
        "WinRAR\n"
        "Unlimited Company License\n"
        "UID=4b914fb772c8376bf571\n"
        "6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d\n"
        "cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717\n"
        "7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565\n"
        "b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd\n"
        "982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190\n"
        "6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9\n";

    // Combine the directory and file name to create the full path
    string filePath = winrarInstallDir + "\\rarreg.key";

    // Save the content to rarreg.key file
    ofstream outFile(filePath);
    
    if (outFile.is_open()) {
        outFile << rarRegistrationData;
        outFile.close();
        cout << "rarreg.key file has been saved to " << filePath << endl;
    } else {
        cout << "An error occurred while saving the file." << endl;
        return 1;
    }

    return 0;
}
