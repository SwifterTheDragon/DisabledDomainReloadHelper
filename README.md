# DisabledDomainReloadHelper
Source generates boilerplate required to cleanup after disabled domain reloads in Unity projects.

---

# Marker Attribute

Decorate a method with the `OnCleanupDisabledDomainReloadsAttribute` to
cleanup after disabled domain reloads.

Use `CleanupDisabledDomainReloadPhases.OnExitPlayMode` to clean up after all
static event handlers, and static fields exclusive to play mode.

Use `CleanupDisabledDomainReloadPhases.OnEnterPlayMode` to clean up after
static fields shared between edit mode and edit mode.

---

Copyright SwifterTheDragon, and the SwifterTheDragon.DisabledDomainReloadHelper contributors, 2025. All Rights Reserved.

SPDX-License-Identifier: MIT
