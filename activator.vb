Imports Microsoft.Win32
Imports System.IO

Module Program
    Sub Main()
        ' Prompt the user for confirmation
        Console.WriteLine("This will save the 'rarreg.key' file in the directory where WinRAR is installed.")
        Console.Write("Do you want to continue? (Y/N): ")

        Dim response As String = Console.ReadLine().Trim()

        If response.ToUpper() <> "Y" Then
            Console.WriteLine("Operation cancelled.")
            Return
        End If

        ' Get the WinRAR installation directory
        Dim winrarInstallDir As String = GetWinRARInstallDirectory()

        If String.IsNullOrEmpty(winrarInstallDir) Then
            Console.WriteLine("WinRAR installation directory not found.")
            Return
        End If

        ' Content to be saved in rarreg.key
        Dim rarRegistrationData As String =
            "RAR registration data" & Environment.NewLine &
            "WinRAR" & Environment.NewLine &
            "Unlimited Company License" & Environment.NewLine &
            "UID=4b914fb772c8376bf571" & Environment.NewLine &
            "6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d" & Environment.NewLine &
            "cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717" & Environment.NewLine &
            "7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565" & Environment.NewLine &
            "b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd" & Environment.NewLine &
            "982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190" & Environment.NewLine &
            "6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9"

        ' Combine the directory and file name to create the full path
        Dim filePath As String = Path.Combine(winrarInstallDir, "rarreg.key")

        ' Save the content to rarreg.key file
        Try
            File.WriteAllText(filePath, rarRegistrationData)
            Console.WriteLine("rarreg.key file has been saved to " & filePath)
        Catch ex As IOException
            Console.WriteLine("An error occurred while saving the file: " & ex.Message)
        End Try
    End Sub

    Function GetWinRARInstallDirectory() As String
        Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WinRAR")

        If regKey IsNot Nothing Then
            Dim exePath As String = regKey.GetValue("exe64", "").ToString()
            Dim lastBackslash As Integer = exePath.LastIndexOf("\")

            If lastBackslash > 0 Then
                Return exePath.Substring(0, lastBackslash)
            End If
        End If

        Return ""
    End Function
End Module
