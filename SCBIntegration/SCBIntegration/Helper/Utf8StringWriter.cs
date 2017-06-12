using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBIntegration
{
    public class Utf8StringWriter : StringWriter
    {
        public Utf8StringWriter(StringBuilder sb)
            : base(sb)
        {
        }

        public Utf8StringWriter()
            : base()
        {
        }
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
