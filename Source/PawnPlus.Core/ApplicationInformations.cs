using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PawnPlus.Core
{
    public class ProjectType
    {
        /// <summary>
        /// Get or set project name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set project path.
        /// </summary>
        public string Path { get; set; }
    }

    public class CurrentFileType
    {
        /// <summary>
        /// Get or set file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set file path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Get or set last time when the file was changed.
        /// </summary>
        public FileInfo LastWriteTime { get; set; }
    }

    public class CodeEditorsType
    {
        /// <summary>
        /// Get or set informations about the code editor.
        /// </summary>
        public CurrentFileType Informations { get; set; }

        public CodeEditor
    }

    public class InformationsType
    {
        /// <summary>
        /// Get or set informations about the project.
        /// </summary>
        public ProjectType Project { get; set; }

        /// <summary>
        /// Get or set informations about active code editor.
        /// </summary>
        public CurrentFileType CurrentFile { get; set; }

        /// <summary>
        /// Contains all informations about all opened files.
        /// </summary>
        public List<string> CodeEditors { get; set; }

        /// <summary>
        /// Class constructor! DON'T CALL IT!
        /// </summary>
        public InformationsType()
        {
            Project = new ProjectType();
            CurrentFile = new CurrentFileType();
            CodeEditors = new List<string>();
        }
    }

    public static class ApplicationInformations
    {
        /// <summary>
        /// Contains all informations about the application.
        /// </summary>
        public static InformationsType Informations = new InformationsType();
    }
}
