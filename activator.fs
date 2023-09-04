open Microsoft.Win32

// Function to get the WinRAR installation directory
let getWinRARInstallDirectory () =
    let regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WinRAR")
    match regKey with
    | null -> ""
    | _ ->
        let exePath = regKey.GetValue("exe64", "").ToString()
        let lastBackslash = exePath.LastIndexOf('\\')
        if lastBackslash > 0 then
            exePath.Substring(0, lastBackslash)
        else
            ""

[<EntryPoint>]
let main argv =
    // Prompt the user for confirmation
    printfn "This will save the 'rarreg.key' file in the directory where WinRAR is installed."
    printfn "Do you want to continue? (Y/N): "
    
    let response = Console.ReadLine().Trim()
    
    if response.ToUpper() <> "Y" then
        printfn "Operation cancelled."
    else
        // Get the WinRAR installation directory
        let winrarInstallDir = getWinRARInstallDirectory()
        
        if winrarInstallDir = "" then
            printfn "WinRAR installation directory not found."
        else
            // Content to be saved in rarreg.key
            let rarRegistrationData =
                "RAR registration data\n" +
                "WinRAR\n" +
                "Unlimited Company License\n" +
                "UID=4b914fb772c8376bf571\n" +
                "6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d\n" +
                "cd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717\n" +
                "7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565\n" +
                "b41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd\n" +
                "982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190\n" +
                "6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9\n"
            
            // Combine the directory and file name to create the full path
            let filePath = System.IO.Path.Combine(winrarInstallDir, "rarreg.key")
            
            // Save the content to rarreg.key file
            try
                System.IO.File.WriteAllText(filePath, rarRegistrationData)
                printfn "rarreg.key file has been saved to %s" filePath
            with
                | :? System.IO.IOException as ex ->
                    printfn "An error occurred while saving the file: %s" ex.Message
    
    0 // Return an integer exit code
