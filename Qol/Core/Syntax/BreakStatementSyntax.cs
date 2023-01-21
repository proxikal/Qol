using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qol.Core.Syntax
{
    internal sealed partial class BreakStatementSyntax : StatementSyntax
    {
        public BreakStatementSyntax(SyntaxTree syntaxTree, SyntaxToken keyword)
        : base(syntaxTree)
        {
            Keyword = keyword;
        }

        public override SyntaxKind Kind => SyntaxKind.BreakStatement;
        public SyntaxToken Keyword { get; }
    }
}
