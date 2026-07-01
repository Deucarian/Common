# Deucarian Common

## Overview

Deucarian Common is a tiny, dependency-free Unity runtime package for approved low-level primitives shared across Deucarian packages.

Package ID: `com.deucarian.common`

Current package version: `0.2.0`.

Common is not a miscellaneous utility package. It owns only approved low-level shared runtime primitives that have organization-wide reuse evidence.

## Responsibilities

- Provide a canonical helper for safe destruction of transient `UnityEngine.Object` instances across Play Mode and Edit Mode.
- Provide shared easing presets for package-owned motion systems.
- Keep shared primitives small, runtime-safe, and dependency-free.
- Preserve Unity semantics instead of hiding ownership or lifecycle decisions.

## Non-Goals

Common does not contain logging, editor UI, networking, diagnostics, state, package installation, test fixtures, JSON helpers, dependency injection, service location, or domain behavior.

Common does not delete assets through `AssetDatabase`, does not own project publishing workflows, and does not replace package-specific architecture.

New APIs require organization-wide reuse evidence and an explicit extraction decision.

## Installation

Install through Unity Package Manager with a Git URL:

```json
{
  "dependencies": {
    "com.deucarian.common": "https://github.com/Deucarian/Common.git#main"
  }
}
```

For development builds, use:

```json
"com.deucarian.common": "https://github.com/Deucarian/Common.git#develop"
```

npm/scoped-registry distribution is deferred for now. Use Git URLs until the manual release process is finalized.

The package requires Unity `2021.3` or newer.

For local development, reference the package by file path from a separate Unity test project:

```json
"com.deucarian.common": "file:C:/Repositories/Common"
```

## Public API

- `UnityObjectUtility.DestroySafely(UnityEngine.Object target)`: destroys a transient Unity object using Play Mode or Edit Mode semantics.
- `DeucarianEasingUtility.Evaluate(DeucarianEasing easing, float value)`: evaluates an approved easing preset from clamped normalized input.

## Play Mode versus Edit Mode semantics

`DestroySafely` is intended for transient scene/runtime Unity objects, not AssetDatabase deletion.

- `null` and Unity fake-null targets are no-ops.
- In Play Mode, it calls `UnityEngine.Object.Destroy(target)` and preserves Unity's deferred destruction behavior.
- Outside Play Mode, it calls `UnityEngine.Object.DestroyImmediate(target)`.
- It does not clear caller references.
- It does not destroy collections.
- It does not catch unrelated exceptions.
- It does not pass `allowDestroyingAssets`.

## Example

```csharp
using Deucarian.Common;
using UnityEngine;

public sealed class RuntimeCleanupExample : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;

    public void Release()
    {
        UnityObjectUtility.DestroySafely(spawnedObject);
        spawnedObject = null;
    }
}
```

## Dependencies

Common has no package dependencies. It does not reference Deucarian Logging, Deucarian Editor, Newtonsoft Json, or any Deucarian domain package.

## Versioning

`0.1.0` is the initial package release. Future public APIs must be approved through a reuse audit before they are added.

## Tests

Common includes EditMode and PlayMode tests for `DestroySafely`. Until shared package validation and reusable CI are introduced, validation is performed through Unity compile/tests and Package Registry governance audits.

## Architecture / Contributor Notes

- [AGENTS.md](AGENTS.md) contains repository-specific ownership and Codex guidance.
- Deucarian architecture rules live in [Package Registry](https://github.com/Deucarian/Package-Registry/blob/develop/ARCHITECTURE.md).
- Capability ownership is tracked in [CAPABILITY_OWNERSHIP.md](https://github.com/Deucarian/Package-Registry/blob/develop/CAPABILITY_OWNERSHIP.md).

## License

See `LICENSE.md`.
