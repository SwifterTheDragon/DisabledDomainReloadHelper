// Copyright SwifterTheDragon, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System;
using System.Diagnostics;

namespace SwifterTheDragon.DisabledDomainReloadHelper.SourceGenerator.Core
{
    /// <summary>
    /// Methods decorated with this attribute will be invoked when disabled
    /// domain reload cleanup is needed.
    /// </summary>
    [AttributeUsage(
        validOn: AttributeTargets.Method)]
    [DebuggerDisplay(
        value: "{DebuggerDisplay,nq}")]
    public sealed class OnCleanupDisabledDomainReloadsAttribute : Attribute
    {
        #region Fields & Properties
        /// <summary>
        /// The backing store for <c><see cref="Phase"/></c>.
        /// </summary>
        private readonly CleanupDisabledDomainReloadPhases i_phase;
        /// <summary>
        /// The phase the decorated method will be invoked for.
        /// </summary>
        public CleanupDisabledDomainReloadPhases Phase
        {
            get
            {
                return i_phase;
            }
        }
        /// <summary>
        /// Specifies how <c><see langword="this"/></c> is displayed in
        /// debugger windows.
        /// </summary>
        [DebuggerBrowsable(
            state: DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get
            {
                return ToString();
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="phase">
        /// Specifies which phase the decorated method will be invoked for.
        /// </param>
        public OnCleanupDisabledDomainReloadsAttribute(
            CleanupDisabledDomainReloadPhases phase)
        {
            i_phase = phase;
        }
        public override string ToString()
        {
            return base.ToString()
                + ": "
                + i_phase;
        }
        #endregion Methods
    }
}
