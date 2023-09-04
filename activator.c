#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <windows.h>

// Function to get the WinRAR installation directory
bool getWinRARInstallDirectory(char* installDir, size_t installDirSize) {
    HKEY hKey;
    LONG result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, "SOFTWARE\\WinRAR", 0, KEY_READ, &hKey);

    if (result == ERROR_SUCCESS) {
        DWORD dataSize = (DWORD)installDirSize;
        result = RegQueryValueEx(hKey, "exe64", NULL, NULL, (LPBYTE)installDir, &dataSize);
        RegCloseKey(hKey);

        if (result == ERROR_SUCCESS) {
            // Extract the directory path
            char* lastBackslash = strrchr(installDir, '\\');
            if (lastBackslash != NULL) {
                *lastBackslash = '\0';
                return true;
            }
        }
    }

    return false;
}

int main() {
    // Prompt the user for confirmation
    printf("This will save the 'rarreg.key' file in the directory where WinRAR is installed.\n");
    printf("Do you want to continue? (Y/N): ");
    
    char response;
    if (scanf(" %c", &response) != 1 || (response != 'Y' && response != 'y')) {
        printf("Operation cancelled.\n");
        return 0;
    }

    // Get the WinRAR installation directory
    char winrarInstallDir[MAX_PATH];
    if (!getWinRARInstallDirectory(winrarInstallDir, sizeof(winrarInstallDir))) {
        printf("WinRAR installation directory not found.\n");
        return 1;
    }

    // Content to be saved in rarreg.key
    const char* rarRegistrationData =
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
    char filePath[MAX_PATH];
    snprintf(filePath, sizeof(filePath), "%s\\rarreg.key", winrarInstallDir);

    // Save the content to rarreg.key file
    FILE* file = fopen(filePath, "w");
    if (file != NULL) {
        fputs(rarRegistrationData, file);
        fclose(file);
        printf("rarreg.key file has been saved to %s\n", filePath);
    } else {
        printf("An error occurred while saving the file.\n");
        return 1;
    }

    return 0;
}
