using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace dotnet
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class dotnetAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    LineLengthAnalyzer.Rule,
                    FluentAnalyzer.Rule,
                    PropertyModifiersAnalyzer.Rule);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            // all statements
            var kinds = new SyntaxKind[29];
            for (int i = 8793; i < 8822; i++)
            {
                if (Enum.TryParse(i.ToString(), out SyntaxKind kind))
                    kinds[i - 8793] = kind;
            }

            //context.RegisterSyntaxTreeAction(LineLengthAnalyzer.AnalyzeTree);
            context.RegisterSyntaxNodeAction(LineLengthAnalyzer.Analyze, kinds); // what SyntaxKind we should to use?
            context.RegisterSyntaxNodeAction(FluentAnalyzer.Analyze, SyntaxKind.SimpleMemberAccessExpression);
            context.RegisterSyntaxNodeAction(PropertyModifiersAnalyzer.Analyze, SyntaxKind.PropertyDeclaration);
        }
    }
}