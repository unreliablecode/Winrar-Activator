@echo off
setlocal enabledelayedexpansion

:: Prompt the user for confirmation
echo This will save the 'rarreg.key' file in the directory where WinRAR is installed.
set /p "continue=Do you want to continue? (Y/N): "

if /i "!continue!" neq "Y" (
    echo Operation cancelled.
    exit /b
)

:: Get the WinRAR installation directory from the registry
for /f "tokens=2*" %%A in (
    'reg query "HKEY_LOCAL_MACHINE\SOFTWARE\WinRAR" /v "exe64" ^| find "exe64"'
) do (
    set "winrarInstallDir=%%B"
)

if not defined winrarInstallDir (
    echo WinRAR installation directory not found.
    exit /b 1
)

:: Content to be saved in rarreg.key
set "rarRegistrationData=RAR registration data
WinRAR
Unlimited Company License
UID=4b914fb772c8376bf571
6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d
cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717
7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565
b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd
982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190
6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9"

:: Combine the directory and file name to create the full path
set "filePath=%winrarInstallDir%\rarreg.key"

:: Save the content to rarreg.key file
echo !rarRegistrationData! > "!filePath!"
if errorlevel 1 (
    echo An error occurred while saving the file.
    exit /b 1
)

echo rarreg.key file has been saved to !filePath!
exit /b 0
