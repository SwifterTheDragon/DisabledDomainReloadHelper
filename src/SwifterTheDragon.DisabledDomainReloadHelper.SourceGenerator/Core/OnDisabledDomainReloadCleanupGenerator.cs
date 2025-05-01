// Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SwifterTheDragon.DisabledDomainReloadHelper.Markers.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.Core
{
    /// <summary>
    /// Incrementally generates an invocation for methods decorated with
    /// <c><see cref="MarkerAttributeName"/></c>.
    /// </summary>
    [Generator]
    internal sealed class OnDisabledDomainReloadCleanupGenerator : IIncrementalGenerator
    {
        #region Nested Types
        #region EquatableList<T> Legal Attribution
        /* Solely the type EquatableList<T>,
         * and its implementation originate from:
        https://github.com/dotnet/roslyn/blob/e6d1a8c05334b36f2c9ecb515f1e35b137e16adb/docs/features/incremental-generators.cookbook.md?plain=1#L808C5-L848C2
        and is therefore subject to the following license:

The MIT License (MIT)

Copyright (c) .NET Foundation and Contributors

All rights reserved.
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
         */
        #endregion EquatableList<T> Legal Attribution
        private sealed class EquatableList<T> : List<T>
        {
            #region Methods
            public static bool operator ==(EquatableList<T> list1, EquatableList<T> list2)
            {
                return ReferenceEquals(
                    objA: list1,
                    objB: list2)
                    || (!(list1 is null)
                        && !(list2 is null)
                        && list1.Equals(
                            obj: list2));
            }
            public static bool operator !=(EquatableList<T> list1, EquatableList<T> list2)
            {
                return !(list1 == list2);
            }
            public override bool Equals(
                object obj)
            {
                return Equals(
                    other: obj as EquatableList<T>);
            }
            public override int GetHashCode()
            {
                return this.Select(
                    selector: (item) =>
                    {
                        return item?.GetHashCode() ?? 0;
                    })
                    .Aggregate(
                        func: (x, y) =>
                        {
                            return x ^ y;
                        });
            }
            internal bool Equals(
                IList<T> other)
            {
                if (other is null || Count != other.Count)
                {
                    return false;
                }
                for (int index = 0; index < Count; index++)
                {
                    if (!EqualityComparer<T>.Default.Equals(
                        x: this[index],
                        y: other[index]))
                    {
                        return false;
                    }
                }
                return true;
            }
            #endregion Methods
        }
        #endregion Nested Types
        #region Fields & Properties
        /// <summary>
        /// The fully qualified metadata name for the marker attribute for this
        /// generator.
        /// </summary>
        private static string MarkerAttributeName
        {
            get
            {
                return "SwifterTheDragon.DisabledDomainReloadHelper.Markers.Core.OnCleanupDisabledDomainReloadsAttribute";
            }
        }
        /// <summary>
        /// The name of the tool providing this generator.
        /// </summary>
        private static string ToolName
        {
            get
            {
                return typeof(OnDisabledDomainReloadCleanupGenerator).Assembly.GetName().Name;
            }
        }
        /// <summary>
        /// The version of the tool providing this generator.
        /// </summary>
        private static string ToolVersion
        {
            get
            {
                return FileVersionInfo.GetVersionInfo(typeof(OnDisabledDomainReloadCleanupGenerator).Assembly.Location).ProductVersion;
            }
        }
        /// <summary>
        /// The name of this generator.
        /// </summary>
        private static string GeneratorName
        {
            get
            {
                return nameof(OnDisabledDomainReloadCleanupGenerator);
            }
        }
        #endregion Fields & Properties
        #region Methods
        public void Initialize(
            IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<(
                string generatedNamespace,
                EquatableList<string> generatedTypeNames,
                string generatedMethodName,
                CleanupDisabledDomainReloadPhases phase)> pipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                    fullyQualifiedMetadataName: MarkerAttributeName,
                    predicate: SyntaxNodePredicate,
                    transform: Transform);
            IncrementalValuesProvider<(
                string generatedNamespace,
                EquatableList<string> generatedTypeNames,
                string generatedMethodName,
                CleanupDisabledDomainReloadPhases phase)> pipeline2 = pipeline.Where(
                    predicate: PipelinePredicate);
            context.RegisterSourceOutput(
                source: pipeline2,
                action: RegisterSourceOutput);
        }
        /// <summary>
        /// Checks if <c><paramref name="syntaxNode"/></c> is
        /// <c><see cref="BaseMethodDeclarationSyntax"/></c>.
        /// </summary>
        /// <param name="syntaxNode">
        /// The <c><see cref="SyntaxNode"/></c> to be checked.
        /// </param>
        /// <param name="cancellationToken">
        /// See <c><see cref="CancellationToken"/></c>.
        /// </param>
        /// <returns>
        /// Is <c><paramref name="syntaxNode"/></c> an instance of
        /// <c><see cref="BaseMethodDeclarationSyntax"/></c>?
        /// </returns>
        private static bool SyntaxNodePredicate(
            SyntaxNode syntaxNode,
            CancellationToken cancellationToken)
        {
            return syntaxNode is BaseMethodDeclarationSyntax;
        }
        /// <summary>
        /// Gathers data regarding the method decorated with the marker
        /// attribute.
        /// </summary>
        /// <param name="context">
        /// See <c><see cref="GeneratorAttributeSyntaxContext"/></c>.
        /// </param>
        /// <param name="cancellationToken">
        /// See <c><see cref="CancellationToken"/></c>.
        /// </param>
        /// <returns>
        /// The containing namespace, containing type name, and name of the
        /// target method from <c><paramref name="context"/></c>.
        /// </returns>
        private static (
            string generatedNamespace,
            EquatableList<string> generatedTypeNames,
            string generatedMethodName,
            CleanupDisabledDomainReloadPhases phase)
        Transform(
            GeneratorAttributeSyntaxContext context,
            CancellationToken cancellationToken)
        {
            INamedTypeSymbol containingType = context.TargetSymbol.ContainingType;
            string generatedNamespace = containingType.ContainingNamespace.ToDisplayString(
                format: SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(
                    style: SymbolDisplayGlobalNamespaceStyle.Omitted));
            var generatedTypeNames = new EquatableList<string>
            {
                containingType.Name
            };
            INamedTypeSymbol currentOuterType = containingType.ContainingType;
            if (currentOuterType != null)
            {
                do
                {
                    generatedTypeNames.Add(
                        item: currentOuterType.Name);
                    currentOuterType = currentOuterType.ContainingType;
                }
                while (currentOuterType != null);
                generatedTypeNames.Reverse();
            }
            string generatedMethodName = context.TargetSymbol.Name;
            var phase = (CleanupDisabledDomainReloadPhases)context.Attributes[0].ConstructorArguments[0].Value;
            return (
                generatedNamespace,
                generatedTypeNames,
                generatedMethodName,
                phase);
        }
        /// <summary>
        /// Checks if <c><paramref name="model"/></c> contains either
        /// <c><see cref="CleanupDisabledDomainReloadPhases.OnExitPlayMode"/></c>,
        /// or
        /// <c><see cref="CleanupDisabledDomainReloadPhases.OnEnterPlayMode"/></c>.
        /// </summary>
        /// <param name="model">
        /// The data provided by the pipeline.
        /// </param>
        /// <returns>
        /// Does <c><paramref name="model"/></c> contain either
        /// <c><see cref="CleanupDisabledDomainReloadPhases.OnExitPlayMode"/></c>,
        /// or
        /// <c><see cref="CleanupDisabledDomainReloadPhases.OnEnterPlayMode"/></c>?
        /// </returns>
        private static bool PipelinePredicate(
            (
                string generatedNamespace,
                EquatableList<string> generatedTypeNames,
                string generatedMethodName,
                CleanupDisabledDomainReloadPhases phase) model)
        {
            return Enum.IsDefined(
                enumType: typeof(CleanupDisabledDomainReloadPhases),
                value: model.phase)
                && !model.phase.Equals(
                    obj: default);
        }
        /// <summary>
        /// Registers source output to <c><paramref name="context"/></c>.
        /// </summary>
        /// <param name="context">
        /// See <c><see cref="SourceProductionContext"/></c>.
        /// </param>
        /// <param name="model">
        /// The data provided by the pipeline.
        /// </param>
        private static void RegisterSourceOutput(
            SourceProductionContext context,
            (
                string generatedNamespace,
                EquatableList<string> generatedTypeNames,
                string generatedMethodName,
                CleanupDisabledDomainReloadPhases phase) model)
        {
            StringBuilder typeNameIncludingNestingBuilder = new StringBuilder()
                .Append(
                    value: model.generatedTypeNames[0]);
            if (model.generatedTypeNames.Count > 1)
            {
                var remainingTypeNames = new List<string>(
                    collection: model.generatedTypeNames);
                remainingTypeNames.RemoveAt(
                    index: 0);
                foreach (string typeName in remainingTypeNames)
                {
                    typeNameIncludingNestingBuilder.Append(
                        value: '.')
                        .Append(
                            value: typeName);
                }
            }
            typeNameIncludingNestingBuilder.Append(
                value: '.')
                .Append(
                    value: model.generatedMethodName);
            string nonGlobalTargetMethodCodeReference = typeNameIncludingNestingBuilder.ToString();
            string uniqueIdentifier = nonGlobalTargetMethodCodeReference.Replace(
                oldChar: '.',
                newChar: '_');
            StringBuilder globalNamespaceToTargetMethodCodeReferenceBuilder = new StringBuilder()
                .Append(
                    value: "global::");
            if (!string.IsNullOrWhiteSpace(
                value: model.generatedNamespace))
            {
                globalNamespaceToTargetMethodCodeReferenceBuilder.Append(
                    value: model.generatedNamespace)
                    .Append(
                        value: '.');
            }
            globalNamespaceToTargetMethodCodeReferenceBuilder.Append(
                value: nonGlobalTargetMethodCodeReference);
            string globalNamespaceToTargetMethodCodeReference = globalNamespaceToTargetMethodCodeReferenceBuilder.ToString();
            StringBuilder sourceBuilder = new StringBuilder()
                .Append(
                    value: BuildAutoGeneratedCodeHeader())
                .AppendLine(
                    value: "#region Editor Only")
                .AppendLine(
                    value: "#if UNITY_EDITOR")
                .AppendLine(
                    value: "using System.CodeDom.Compiler;")
                .AppendLine(
                    value: "using System.Reflection;")
                .AppendLine(
                    value: "using UnityEditor;")
                .AppendLine();
            if (!string.IsNullOrWhiteSpace(
                value: model.generatedNamespace))
            {
                sourceBuilder.Append(
                    value: "namespace ")
                    .AppendLine(
                        value: model.generatedNamespace)
                    .AppendLine(
                        value: "{");
            }
            sourceBuilder.AppendLine(
                value: "    /// <summary>")
                .AppendLine(
                    value: "    /// Invokes")
                .Append(
                    value: "    /// <c><see cref=\"")
                .Append(
                    value: globalNamespaceToTargetMethodCodeReference)
                .AppendLine(
                    value: "\" /></c>")
                .Append(
                    value: "    /// upon ");
            if (model.phase is CleanupDisabledDomainReloadPhases.OnExitPlayMode)
            {
                sourceBuilder.Append(
                    value: "exiting");
            }
            else
            {
                sourceBuilder.Append(
                    value: "entering");
            }
            sourceBuilder.AppendLine(
                value: " play mode.")
                .AppendLine(
                    value: "    /// </summary>")
                .AppendLine(
                    value: "    [GeneratedCode(")
                .Append(
                    value: "        tool: \"")
                .Append(
                    value: ToolName)
                .AppendLine(
                    value: "\",")
                .Append(
                    value: "        version: \"")
                .Append(
                    value: ToolVersion)
                .AppendLine(
                    value: "\")]")
                .Append(
                    value: "    internal static class ")
                .AppendLine(
                    value: uniqueIdentifier)
                .AppendLine(
                    value: "    {")
                .AppendLine(
                    value: "        #region Methods")
                .AppendLine(
                    value: "        /// <summary>")
                .AppendLine(
                    value: "        /// A callback for ")
                .AppendLine(
                    value: "        /// <c><see cref=\"InitializeOnEnterPlayModeAttribute\" /></c>")
                .Append(
                    value: "        /// that ");
            if (model.phase is CleanupDisabledDomainReloadPhases.OnExitPlayMode)
            {
                sourceBuilder.AppendLine(
                    value: "subscribes")
                    .AppendLine(
                        value: "        /// <c><see cref=\"OnPlayModeStateChanged\" /></c>")
                    .AppendLine(
                        value: "        /// to")
                    .AppendLine(
                        value: "        /// <c><see cref=\"EditorApplication.playModeStateChanged\" /></c>.");
            }
            else
            {
                sourceBuilder.AppendLine(
                    value: "invokes")
                    .AppendLine(
                        value: "        /// <c><see cref=\"InvokeTargetMethod\" /></c>.");
            }
            sourceBuilder.AppendLine(
                value: "        /// </summary>")
                .AppendLine(
                    value: "        /// <param name=\"options\">")
                .AppendLine(
                    value: "        /// The current <c><see cref=\"EnterPlayModeOptions\" /></c>.")
                .AppendLine(
                    value: "        /// </param>")
                .AppendLine(
                    value: "        [InitializeOnEnterPlayMode]")
                .AppendLine(
                    value: "        private static void OnInitialiseOnEnterPlayMode(")
                .AppendLine(
                    value: "            EnterPlayModeOptions options)")
                .AppendLine(
                    value: "        {")
                .AppendLine(
                    value: "            if (!options.HasFlag(EnterPlayModeOptions.DisableDomainReload))")
                .AppendLine(
                    value: "            {")
                .AppendLine(
                    value: "                return;")
                .AppendLine(
                    value: "            }");
            if (model.phase is CleanupDisabledDomainReloadPhases.OnExitPlayMode)
            {
                sourceBuilder.AppendLine(
                    value: "            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;")
                    .AppendLine(
                        value: "            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;")
                    .AppendLine(
                        value: "        }")
                    .AppendLine(
                        value: "        /// <summary>")
                    .AppendLine(
                        value: "        /// A callback for")
                    .AppendLine(
                        value: "        /// <c><see cref=\"EditorApplication.playModeStateChanged\" /></c>")
                    .AppendLine(
                        value: "        /// that invokes <c><see cref=\"InvokeTargetMethod\" /></c>")
                    .AppendLine(
                        value: "        /// if <c><paramref name=\"state\" /></c> is")
                    .AppendLine(
                        value: "        /// <c><see cref=\"PlayModeStateChange.ExitingPlayMode\" /></c>.")
                    .AppendLine(
                        value: "        /// </summary>")
                    .AppendLine(
                        value: "        private static void OnPlayModeStateChanged(")
                    .AppendLine(
                        value: "            PlayModeStateChange state)")
                    .AppendLine(
                        value: "        {")
                    .AppendLine(
                        value: "            if (state is not PlayModeStateChange.ExitingPlayMode)")
                    .AppendLine(
                        value: "            {")
                    .AppendLine(
                        value: "                return;")
                    .AppendLine(
                        value: "            }")
                    .AppendLine(
                        value: "            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;")
                    .AppendLine(
                        value: "            OnExitingPlayMode();")
                    .AppendLine(
                        value: "        }")
                    .AppendLine(
                        value: "        /// <summary>")
                    .AppendLine(
                        value: "        /// A callback for when")
                    .AppendLine(
                        value: "        /// <c><see cref=\"OnPlayModeStateChanged\" /></c>")
                    .AppendLine(
                        value: "        /// is called with")
                    .AppendLine(
                        value: "        /// <c><see cref=\"PlayModeStateChange.ExitingPlayMode\" /></c>.")
                    .AppendLine(
                        value: "        /// </summary>")
                    .AppendLine(
                        value: "        private static void OnExitingPlayMode()")
                    .AppendLine(
                        value: "        {")
                    .AppendLine(
                        value: "            InvokeTargetMethod();");
            }
            else
            {
                sourceBuilder.AppendLine(
                    value: "            InvokeTargetMethod();");
            }
            sourceBuilder.AppendLine(
                value: "        }")
                .AppendLine(
                    value: "        /// <summary>")
                .AppendLine(
                    value: "        /// Invokes")
                .Append(
                    value: "        /// <c><see cref=\"")
                .Append(
                    value: globalNamespaceToTargetMethodCodeReference)
                .AppendLine(
                    value: "\" /></c>.")
                .AppendLine(
                    value: "        /// </summary>")
                .AppendLine(
                    value: "        private static void InvokeTargetMethod()")
                .AppendLine(
                    value: "        {")
                .Append(
                    value: "            _ = typeof(")
                .Append(
                    value: model.generatedTypeNames[0])
                .Append(
                    value: ')');
            if (model.generatedTypeNames.Count > 1)
            {
                var remainingTargetTypes = new List<string>(
                    collection: model.generatedTypeNames);
                remainingTargetTypes.RemoveAt(
                    index: 0);
                foreach (string currentNestedTypeName in remainingTargetTypes)
                {
                    sourceBuilder.AppendLine()
                        .AppendLine(
                            value: "                .GetNestedType(")
                        .Append(
                            value: "                    name: \"")
                        .Append(
                            value: currentNestedTypeName)
                        .AppendLine(
                            value: "\",")
                        .Append(
                            value: "                    bindingAttr: BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)");
                }
            }
            sourceBuilder.AppendLine()
                .AppendLine(
                    value: "                .GetMethod(")
                .Append(
                    value: "                    name: \"")
                .Append(
                    value: model.generatedMethodName)
                .AppendLine(
                    value: "\",")
                .AppendLine(
                    value: "                    bindingAttr: BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)")
                .AppendLine(
                    value: "                .Invoke(")
                .AppendLine(
                    value: "                    obj: null,")
                .AppendLine(
                    value: "                    parameters: null);")
                .AppendLine(
                    value: "        }")
                .AppendLine(
                    value: "        #endregion Methods")
                .AppendLine(
                    value: "    }");
            if (!string.IsNullOrWhiteSpace(
                value: model.generatedNamespace))
            {
                sourceBuilder.AppendLine(
                    value: "}");
            }
            sourceBuilder.AppendLine(
                value: "#endif")
                .AppendLine(
                    value: "#endregion Editor Only")
                .Append(
                    value: BuildAutoGeneratedCodeFooter());
            var sourceText = SourceText.From(
                text: sourceBuilder.ToString(),
                encoding: Encoding.UTF8);
            context.AddSource(
                hintName: uniqueIdentifier
                    + ".Generated.cs",
                sourceText: sourceText);
        }
        /// <summary>
        /// Builds a code header comment to label auto-generated code.
        /// </summary>
        /// <returns>
        /// A header comment for labelling auto-generated code as auto-generated
        /// when scanning the contents of a source code file.
        /// </returns>
        private static string BuildAutoGeneratedCodeHeader()
        {
            var headerBuilder = new StringBuilder();
            void AppendFullLine()
            {
                headerBuilder.Append(
                    value: "// ---------------------------")
                    .AppendLine(
                        value: "--------------------------------------------------");
            }
            AppendFullLine();
            headerBuilder.AppendLine(
                value: "// <auto-generated>")
                .Append(
                    value: "//     This code was generated by: \"")
                .Append(
                    value: ToolName)
                .AppendLine(
                    value: "\"")
                .Append(
                    value: "//     Version: ")
                .AppendLine(
                    value: ToolVersion)
                .AppendLine(
                    value: "//")
                .AppendLine(
                    value: "//     Changes to this file may cause incorrect behaviour and will be lost if")
                .AppendLine(
                    value: "//     the code is regenerated.")
                .AppendLine(
                    value: "// </auto-generated>");
            AppendFullLine();
            headerBuilder.AppendLine()
                .AppendLine(
                    value: "// Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.")
                .AppendLine(
                    value: "// SPDX-License-Identifier: MIT")
                .AppendLine();
            return headerBuilder.ToString();
        }
        /// <summary>
        /// Builds a code footer comment to label auto-generated code.
        /// </summary>
        /// <returns>
        /// A footer comment for labelling auto-generated code as
        /// auto-generated.
        /// </returns>
        private static string BuildAutoGeneratedCodeFooter()
        {
            StringBuilder footerBuilder = new StringBuilder()
                .AppendLine()
                .Append(
                    value: "// Generated by \"")
                .Append(
                    value: ToolName)
                .Append(
                    value: "\"'s \"")
                .Append(
                    value: GeneratorName)
                .AppendLine(
                    value: "\" class.");
            return footerBuilder.ToString();
        }
        #endregion Methods
    }
}
