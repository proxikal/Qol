using Qol.Core.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Qol.Core.Binding
{
    internal abstract class BoundNode
    {
        public abstract BoundNodeKind Kind { get; }

        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                this.WriteTo(writer);
                return writer.ToString();
            }
        }
    }

}
