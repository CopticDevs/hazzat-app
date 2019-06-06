using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.Types
{
    public class LanguageHtmlProperties
    {
        /// <summary>
        /// Bootstrap column css class name.
        /// </summary>
        public string BootstrapColCssClass { get; set; }

        /// <summary>
        /// Language font css class name.
        /// </summary>
        public string FontCssClass { get; set; }

        /// <summary>
        /// Indicates if language is RTL.
        /// </summary>
        public bool IsRTL { get; set; }
    }
}
