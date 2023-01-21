using Qol.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qol.Core.Syntax
{
    public abstract class ExpressionSyntax : SyntaxNode
    {
        public ExpressionSyntax(SyntaxTree syntaxTree)
        : base(syntaxTree)
        {
            
        }
    }
}
