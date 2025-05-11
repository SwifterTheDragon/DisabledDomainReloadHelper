// Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System;
using System.Diagnostics;

namespace SwifterTheDragon.DisabledDomainReloadHelper.Markers.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
    /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Description/*'/>
    [AttributeUsage(
        validOn: AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = false)]
    [DebuggerDisplay(
        value: "{DebuggerDisplay,nq}")]
    public sealed class OnCleanupDisabledDomainReloadsAttribute : Attribute
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Field[@name="i_phase"]/*'/>
        private readonly CleanupDisabledDomainReloadPhases i_phase;
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Property[@name="Phase"]/*'/>
        public CleanupDisabledDomainReloadPhases Phase
        {
            get
            {
                return i_phase;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Property[@name="DebuggerDisplay"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Method[@name="#ctor(SwifterTheDragon.DisabledDomainReloadHelper.Markers.Core.CleanupDisabledDomainReloadPhases)"]/*'/>
        public OnCleanupDisabledDomainReloadsAttribute(
            CleanupDisabledDomainReloadPhases phase)
        {
            i_phase = phase;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.DisabledDomainReloadHelper.Markers.xml'
        /// path='Assembly[@name="SwifterTheDragon.DisabledDomainReloadHelper.Markers"]/Namespace[@name="Core"]/Type[@name="OnCleanupDisabledDomainReloadsAttribute"]/Method[@name="ToString"]/*'/>
        public override string ToString()
        {
            return base.ToString()
                + ": "
                + i_phase;
        }
        #endregion Methods
    }
}
