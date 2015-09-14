using System.ComponentModel;

namespace PawnPlus.Language
{
    public enum LanguageEnum
    {
        [Description("Launcher_Files_Checking")]
        LauncherFilesChecking,
        [Description("Launcher_Files_Copied_Compiler")]
        LauncherFilesCopiedCompiler,
        [Description("Launcher_Files_Copied_Server")]
        LauncherFilesCopiedServer,
        [Description("Launcher_Files_Copying_Compiler")]
        LauncherFilesCopyingCompiler,
        [Description("Launcher_Files_Copying_Server")]
        LauncherFilesCopyingServer,
        [Description("Launcher_Files_Downloading_Compiler")]
        LauncherFilesDownloadingCompiler,
        [Description("Launcher_Files_Downloading_Server")]
        LauncherFilesDownloadingServer,
        [Description("Launcher_Files_Error_Compiler")]
        LauncherFilesErrorCompiler,
        [Description("Launcher_Files_Error_Server")]
        LauncherFilesErrorServer,
        [Description("Launcher_Files_Unpacking_Compiler")]
        LauncherFilesUnpackingCompiler,
        [Description("Launcher_Files_Unpacking_Server")]
        LauncherFilesUnpackingServer,
        [Description("Launcher_Old_Version_Checking")]
        LauncherOldVersionChecking,
        [Description("Launcher_Old_Version_Settings")]
        LauncherOldVersionSettings,
        [Description("Launcher_Starting_Up")]
        LauncherStartingUp
    }
}
