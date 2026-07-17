# Deucarian Common

## What this is

`com.deucarian.common` is a tiny, dependency-free Unity runtime package for approved low-level primitives shared across Deucarian packages.

Common is not a miscellaneous utility package. It owns only approved low-level shared runtime primitives that have organization-wide reuse evidence.

Current package version: `0.2.1`.

## When to use it

- You need the approved transient Unity object cleanup helper.
- You need the approved shared Deucarian easing presets for package-owned runtime motion.
- You are writing a Deucarian package that already has a governed reason to depend on Common.

## When not to use it

- Do not use Common as a general utility bucket.
- Do not add logging, editor UI, networking, diagnostics, state, package installation, JSON, dependency injection, service location, test fixtures, or domain behavior here.
- Do not add a Common dependency just to share local helper code between one or two packages.
- Do not use `UnityObjectUtility.DestroySafely` for AssetDatabase deletion or ownership transfer.

## Install

Install through Unity Package Manager with a Git URL.

Stable:

```json
"com.deucarian.common": "https://github.com/Deucarian/Common.git#main"
```

Development:

```json
"com.deucarian.common": "https://github.com/Deucarian/Common.git#develop"
```

For local development, reference the package by file path from a separate Unity test project:

```json
"com.deucarian.common": "file:C:/Repositories/Common"
```

npm/scoped-registry distribution is deferred for now. Use Git URLs until the manual release process is finalized.

## Unity compatibility

Requires Unity 2021.3 or newer.

## 60-second quick start

Use `DestroySafely` when code owns a transient Unity object and needs Play Mode/Edit Mode destruction semantics:

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

Use the shared easing presets when a Deucarian runtime motion system needs a governed easing curve:

```csharp
float eased = DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutSoftBack, normalizedTime);
```

## Samples

This package does not include `Samples~`. The public API is intentionally small and covered by package tests.

## Public API map

- `UnityObjectUtility.DestroySafely(UnityEngine.Object target)`: destroys a transient Unity object using Play Mode or Edit Mode semantics.
- `DeucarianEasing`: approved shared easing presets: `Linear`, `EaseInCubic`, `EaseOutCubic`, `EaseOutBack`, and `EaseOutSoftBack`.
- `DeucarianEasingUtility.Evaluate(DeucarianEasing easing, float value)`: evaluates an approved easing preset from clamped normalized input.

## Integrations

Works with:

- runtime packages that directly own transient Unity object cleanup,
- runtime motion systems that need approved shared Deucarian easing presets.

Does not own:

- logging,
- editor UI,
- diagnostics,
- package installation,
- domain-specific helpers,
- general-purpose utility APIs.

New APIs require organization-wide reuse evidence and an explicit extraction decision.

## Play Mode versus Edit Mode semantics

`DestroySafely` is intended for transient scene/runtime Unity objects, not AssetDatabase deletion.

- `null` and Unity fake-null targets are no-ops.
- In Play Mode, it calls `UnityEngine.Object.Destroy(target)` and preserves Unity's deferred destruction behavior.
- Outside Play Mode, it calls `UnityEngine.Object.DestroyImmediate(target)`.
- It does not clear caller references.
- It does not destroy collections.
- It does not catch unrelated exceptions.
- It does not pass `allowDestroyingAssets`.

## Troubleshooting

- If validation flags an unexpected Common dependency, confirm the consuming package directly uses an approved Common API in production code.
- If an object reference is still non-null after `DestroySafely`, clear the caller-owned field yourself; Common preserves Unity semantics and does not rewrite references.
- If an object is a project asset, do not destroy it with `DestroySafely`; use the owning editor workflow for asset deletion.
- If a new helper looks useful, keep it local until reuse evidence and ownership are approved.

## Validation

Run the shared package validator from the repository root:

```powershell
python C:/Repositories/Package-Registry/Tools/deucarian_package_validator.py --registry-root C:/Repositories/Package-Registry --repository-root . --config deucarian-package.json
```

Run the package's EditMode and PlayMode tests in Unity after code or assembly definition changes.

Documentation-only updates should still pass:

```powershell
git diff --check
```

## Architecture / Contributor Notes

- [AGENTS.md](AGENTS.md) contains repository-specific ownership and Codex guidance.
- Deucarian architecture rules live in [Package Registry](https://github.com/Deucarian/Package-Registry/blob/develop/ARCHITECTURE.md).
- Capability ownership is tracked in [CAPABILITY_OWNERSHIP.md](https://github.com/Deucarian/Package-Registry/blob/develop/CAPABILITY_OWNERSHIP.md).

## License

See [LICENSE.md](LICENSE.md).
