using Qol.Core.Symbols;
using System;

namespace Qol.Core.Binding
{
    internal abstract class BoundExpression : BoundNode
    {
        public abstract TypeSymbol Type { get; }
    }
}
