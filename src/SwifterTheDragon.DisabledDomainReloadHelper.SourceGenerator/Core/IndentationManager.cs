// Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Text;

namespace SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Description/*'/>
    internal sealed class IndentationManager
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="k_DefaultWhiteSpaceCharacter"]/*'/>
        private const char k_DefaultWhiteSpaceCharacter = ' ';
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="k_DefaultIndentationSize"]/*'/>
        private const int k_DefaultIndentationSize = 4;
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="i_currentWhiteSpaceCharacter"]/*'/>
        private readonly char i_currentWhiteSpaceCharacter;
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="i_currentIndentationSize"]/*'/>
        private readonly int i_currentIndentationSize;
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="i_currentIndentationAmount"]/*'/>
        private int i_currentIndentationAmount;
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Property[@name="CurrentIndentationCharacterRepeatCount"]/*'/>
        private int CurrentIndentationCharacterRepeatCount
        {
            get
            {
                return i_currentIndentationSize * i_currentIndentationAmount;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Field[@name="i_builder"]/*'/>
        private readonly StringBuilder i_builder = new StringBuilder();
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="#ctor(System.Char,System.Int32)"]/*'/>
        internal IndentationManager(
            char whiteSpaceCharacter = k_DefaultWhiteSpaceCharacter,
            int indentationSize = k_DefaultIndentationSize)
        {
            i_currentWhiteSpaceCharacter = whiteSpaceCharacter;
            i_currentIndentationSize = indentationSize;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="IncrementIndentation"]/*'/>
        internal void IncrementIndentation()
        {
            ++i_currentIndentationAmount;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="DecrementIndentation"]/*'/>
        internal void DecrementIndentation()
        {
            --i_currentIndentationAmount;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendWithoutWhiteSpace(System.String)"]/*'/>
        internal void AppendWithoutWhiteSpace(
            string input)
        {
            i_builder.Append(
                value: input);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendWithWhiteSpace(System.String)"]/*'/>
        internal void AppendWithWhiteSpace(
            string input)
        {
            AppendWhiteSpace();
            i_builder.Append(
                value: input);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendLine"]/*'/>
        internal void AppendLine()
        {
            i_builder.AppendLine();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendLineWithWhiteSpace(System.String)"]/*'/>
        internal void AppendLineWithWhiteSpace(
            string input)
        {
            AppendWhiteSpace();
            i_builder.AppendLine(
                value: input);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendLineWithoutWhiteSpace(System.String)"]/*'/>
        internal void AppendLineWithoutWhiteSpace(
            string input)
        {
            i_builder.AppendLine(
                value: input);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="OpenBlock"]/*'/>
        internal void OpenBlock()
        {
            AppendLineWithWhiteSpace(
                input: "{");
            IncrementIndentation();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="CloseBlock"]/*'/>
        internal void CloseBlock()
        {
            DecrementIndentation();
            AppendLineWithWhiteSpace(
                input: "}");
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="Output"]/*'/>
        internal string Output()
        {
            return i_builder.ToString();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="IndentationManager"]/Method[@name="AppendWhiteSpace"]/*'/>
        private void AppendWhiteSpace()
        {
            i_builder.Append(
                value: i_currentWhiteSpaceCharacter,
                repeatCount: CurrentIndentationCharacterRepeatCount);
        }
        #endregion Methods
    }
}
