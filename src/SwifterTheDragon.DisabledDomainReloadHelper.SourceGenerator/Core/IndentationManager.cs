// Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Text;

namespace SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.Core
{
    /// <summary>
    /// Manages indentation for source generated output.
    /// </summary>
    internal class IndentationManager
    {
        #region Fields & Properties
        /// <summary>
        /// The default white space character.
        /// </summary>
        private const char k_DefaultWhiteSpaceCharacter = ' ';
        /// <summary>
        /// The default indentation size.
        /// </summary>
        private const int k_DefaultIndentationSize = 4;
        /// <summary>
        /// The current white space character.
        /// </summary>
        private readonly char i_currentWhiteSpaceCharacter;
        /// <summary>
        /// The current size of an indentation.
        /// </summary>
        private readonly int i_currentIndentationSize;
        /// <summary>
        /// The current indentation amount.
        /// </summary>
        private int i_currentIndentationAmount;
        private int CurrentIndentationCharacterRepeatCount
        {
            get
            {
                return i_currentIndentationSize * i_currentIndentationAmount;
            }
        }
        private readonly StringBuilder i_builder = new StringBuilder();
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// Creates a new instance of <c><see cref="IndentationManager"/></c>,
        /// optionally with a non-default white space character, or indentation
        /// size.
        /// </summary>
        /// <param name="whiteSpaceCharacter">
        /// The character to use as white space.
        /// </param>
        /// <param name="indentationSize">
        /// The size of an indentation.
        /// </param>
        internal IndentationManager(
            char whiteSpaceCharacter = k_DefaultWhiteSpaceCharacter,
            int indentationSize = k_DefaultIndentationSize)
        {
            i_currentWhiteSpaceCharacter = whiteSpaceCharacter;
            i_currentIndentationSize = indentationSize;
        }
        /// <summary>
        /// Increments the current indentation amount.
        /// </summary>
        internal void IncrementIndentation()
        {
            ++i_currentIndentationAmount;
        }
        /// <summary>
        /// Decrements the current indentation amount.
        /// </summary>
        internal void DecrementIndentation()
        {
            --i_currentIndentationAmount;
        }
        /// <summary>
        /// Appends <c><paramref name="input"/></c> without prepending white
        /// space.
        /// </summary>
        /// <param name="input">
        /// The text to append.
        /// </param>
        internal void AppendWithoutWhiteSpace(
            string input)
        {
            i_builder.Append(
                value: input);
        }
        /// <summary>
        /// Appends <c><paramref name="input"/></c> with prepending white space.
        /// </summary>
        /// <param name="input">
        /// The text to append.
        /// </param>
        internal void AppendWithWhiteSpace(
            string input)
        {
            AppendWhiteSpace();
            i_builder.Append(
                value: input);
        }
        /// <summary>
        /// Appends a blank line.
        /// </summary>
        internal void AppendLine()
        {
            i_builder.AppendLine();
        }
        /// <summary>
        /// Appends white space, followed by <c><paramref name="input"/></c>,
        /// followed by a line terminator.
        /// </summary>
        /// <param name="input">
        /// The text to append.
        /// </param>
        internal void AppendLine(
            string input)
        {
            AppendWhiteSpace();
            i_builder.AppendLine(
                value: input);
        }
        /// <summary>
        /// Invokes <c><see cref="AppendLine(string)"/></c> with "<c>{</c>",
        /// then invokes <c><see cref="IncrementIndentation"/></c>.
        /// </summary>
        internal void OpenBlock()
        {
            AppendLine(
                input: "{");
            IncrementIndentation();
        }
        /// <summary>
        /// Invokes <c><see cref="DecrementIndentation"/></c>,
        /// then invokes <c><see cref="AppendLine(string)"/></c> with
        /// "<c>}</c>".
        /// </summary>
        internal void CloseBlock()
        {
            DecrementIndentation();
            AppendLine(
                input: "}");
        }
        /// <summary>
        /// Retrieves the <c><see langword="string"/></c> representation of the
        /// current output.
        /// </summary>
        /// <returns>
        /// The <c><see langword="string"/></c> representation of the current
        /// output.
        /// </returns>
        internal string Output()
        {
            return i_builder.ToString();
        }
        /// <summary>
        /// Appends <c><see cref="i_currentWhiteSpaceCharacter"/></c>,
        /// <c><see cref="CurrentIndentationCharacterRepeatCount"/></c> times.
        /// </summary>
        private void AppendWhiteSpace()
        {
            i_builder.Append(
                value: i_currentWhiteSpaceCharacter,
                repeatCount: CurrentIndentationCharacterRepeatCount);
        }
        #endregion Methods
    }
}
