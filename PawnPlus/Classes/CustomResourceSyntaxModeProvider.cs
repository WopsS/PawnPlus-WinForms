using DigitalRune.Windows.TextEditor.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PawnPlus
{
// From the original ResourceSyntaxModeProvider in DigitalRune Texteditor.
class CustomResourceSyntaxModeProvider : ISyntaxModeFileProvider
{
    private readonly List<SyntaxMode> _syntaxModes;

    /// <summary>
    /// Gets the provided syntax highlighting modes.
    /// </summary>
    /// <value>The syntax highlighting modes.</value>
    public ICollection<SyntaxMode> SyntaxModes
    {
        get { return _syntaxModes; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomResourceSyntaxModeProvider"/> class.
    /// </summary>
    public CustomResourceSyntaxModeProvider()
    {
        Stream syntaxModeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.SyntaxModes.xml");
        _syntaxModes = (syntaxModeStream != null) ? SyntaxMode.GetSyntaxModes(syntaxModeStream) : new List<SyntaxMode>();
    }

    /// <summary>
    /// Gets the syntax highlighting definition for a certain syntax.
    /// </summary>
    /// <param name="syntaxMode">The syntax.</param>
    /// <returns>The syntax highlighting definition.</returns>
    public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
    {
        // ReSharper disable AssignNullToNotNullAttribute
        return new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources." + syntaxMode.FileName));
        // ReSharper restore AssignNullToNotNullAttribute
    }

    /// <summary>
    /// Updates the list of syntax highlighting modes.
    /// </summary>
    /// <remarks>
    /// Has no effect in this case, because the resources cannot change during
    /// runtime.
    /// </remarks>
    public void UpdateSyntaxModeList()
    {
        // resources don't change during runtime
    }
}
}
