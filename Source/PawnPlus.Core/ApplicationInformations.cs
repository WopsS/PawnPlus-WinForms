using PawnPlus.Core.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PawnPlus.Core
{
    public class CompileInformationsType
    {
        /// <summary>
        /// Get or set informations about compile errors.
        /// </summary>
        public string Errors { get; set; }

        /// <summary>
        /// Get or set output.
        /// </summary>
        public string Output { get; set; }
    }

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

        /// <summary>
        /// Get or set full path to file with extesion "pawnplusproject".
        /// </summary>
        public string FullPath { get; set; }
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
        /// Get or set total lines.
        /// </summary>
        public int Lines { get; set; }

        /// <summary>
        /// Get or set last time when the file was changed.
        /// </summary>
        public FileInfo fileInfo { get; set; }
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
        public CodeEditor CurrentFile { get; set; }

        /// <summary>
        /// Contains all informations about all opened files.
        /// </summary>
        public Dictionary<string, CodeEditor> CodeEditors { get; set; }

        /// <summary>
        /// Get or set informations about the last compile.
        /// </summary>
        public CompileInformationsType CompileInformations { get; set; }

        /// <summary>
        /// Class constructor! DON'T CALL IT!
        /// </summary>
        public InformationsType()
        {
            Project = new ProjectType();
            CurrentFile = new CodeEditor();
            CodeEditors = new Dictionary<string, CodeEditor>();
            CompileInformations = new CompileInformationsType();
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
