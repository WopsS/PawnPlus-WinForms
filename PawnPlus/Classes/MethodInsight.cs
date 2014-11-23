using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Insight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    class MethodInsight : AbstractInsightDataProvider
    {
        int _argumentStartOffset;   // The offset where the method arguments starts.
        string[] _insightText;      // The insight information.

        int i = 0;

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
            return (_insightText != null) ? _insightText[number] : string.Empty;
        }


        public override void SetupDataProvider(string fileName)
        {
            int offset = TextArea.Caret.Offset;
            IDocument document = TextArea.Document;
            string methodName = TextHelper.GetIdentifierAt(document, offset - 1);

            if (Program.main.MethodsList.ContainsKey(methodName) == true)
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

            if (Program.main.MethodsList.ContainsKey(methodName) == true)
                _insightText[0] = FormatProvider(methodName, Program.main.MethodsList[methodName]);

            _argumentStartOffset = argumentStartOffset;
        }

        private string FormatProvider(string MethodName, MethodInformations Method)
        {
            string Result = null;

            Result = String.Format("public {0}({1}){2}{3}{4}", MethodName, String.Join(", ", Method.Parameters), "\n", Method.Description, Environment.NewLine + Environment.NewLine);

            if (Method.Parameters != null)
            {
                Result += String.Format("Parameters:{0}", Environment.NewLine);

                for(i = 0; i < Method.Parameters.Length; i++)
                    Result += String.Format("{0} - {1}{2}", Method.Parameters[i].Split('=')[0].Trim(), Method.ParametersDescription[i], Environment.NewLine);
            }

            Result += String.Format("{0}Return:{1}", Environment.NewLine, Environment.NewLine);

            if (Method.ReturnValues != null)
            {
                for (i = 0; i < Method.ReturnValues.Length; i++)
                    Result += String.Format("{0} - {1}{2}", Method.ReturnValues[i], Method.ReturnValueDescription[i], Environment.NewLine);
            }
            else
                for (i = 0; i < Method.ReturnValueDescription.Length; i++)
                    Result += String.Format("{0}{1}", Method.ReturnValueDescription[i], Environment.NewLine);

            return Result;
        }
    }
}
