using System;
using PKCode.Scripting;

namespace Statistics.Core.Widgets
{
    public sealed class ReportEnvironment : ICompilerEnvironment
    {
        public int LineSpan => 22;
        public string Main => "Main";
        public string Class => $"Report_{guid}";

        private string guid = Guid.NewGuid().ToString("N");
        private const string core = @"using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Statistics.Core.Widgets;
          
namespace ReportScripting
{{
    public class {0}
    {{
        public static object {1} (object parameters)
        {{
            return new ReportViewModel_{3}();
        }}    
    }}

    public class ReportViewModel_{3} : INotifyPropertyChanged, IWidget
    {{
        {2}


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {{
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }}
    }}
}}
";

        public string Format(string code)
        {
            var s = string.Format(core, Class, Main, code, guid);
            return s;
        }
    }
}
