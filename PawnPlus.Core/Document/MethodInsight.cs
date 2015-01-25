using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Insight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus.Core.Document
{
    public class MethodInsight : AbstractInsightDataProvider
    {
        int _argumentStartOffset;   // The offset where the method arguments starts.
        string[] _insightText;      // The insight information.

        protected override int ArgumentStartOffset
        {
            get { return _argumentStartOffset; }
        }

        public override int InsightDataCount
        {
            get { return (_insightText != null) ? _insightText.Length : 0; }
        }

        public override string GetInsightData(int number)
        {
            return (_insightText != null) ? _insightText[number] : String.Empty;
        }

        public override void SetupDataProvider(string fileName)
        {
            int offset = TextArea.Caret.Offset;
            IDocument document = TextArea.Document;
            string methodName = TextHelper.GetIdentifierAt(document, offset - 1);

            if (MethodsProvider.Methods.ContainsKey(methodName) == true)
            {
                SetupDataProviderForMethod(methodName, offset);
            }
            else
            {
                offset = TextHelper.FindOpeningBracket(document, offset - 1, '(', ')');

                if (offset >= 1)
                {
                    methodName = TextHelper.GetIdentifierAt(document, offset - 1);
                    SetupDataProviderForMethod(methodName, offset);
                }
            }
        }

        private void SetupDataProviderForMethod(string methodName, int argumentStartOffset)
        {
            _insightText = new string[1];

            if (MethodsProvider.Methods.ContainsKey(methodName) == true)
                _insightText[0] = MethodsProvider.FormatProvider(methodName, MethodsProvider.Methods[methodName]);

            _argumentStartOffset = argumentStartOffset;
        }
    }
}
