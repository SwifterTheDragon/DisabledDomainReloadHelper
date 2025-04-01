// Copyright SwifterTheDragon, 2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

namespace SwifterTheDragon.DisabledDomainReloadHelper.Markers.Core
{
    /// <summary>
    /// Specifies which phase disabled domain reload cleanup should occur.
    /// </summary>
    public enum CleanupDisabledDomainReloadPhases
    {
        /// <summary>
        /// The default value. This should never be used intentionally.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that cleanup should be performed upon exiting play mode.
        /// </summary>
        OnExitPlayMode = 1,
        /// <summary>
        /// Specifies that cleanup should be performed upon entering play mode.
        /// </summary>
        OnEnterPlayMode = 2
    }
}
