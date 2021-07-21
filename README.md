# Prototyping Kit

[![](https://img.shields.io/badge/github-repo-blue?logo=github)](https://github.com/zigurous/unity-prototyping-kit) [![](https://img.shields.io/github/package-json/v/zigurous/unity-prototyping-kit)](https://github.com/zigurous/unity-prototyping-kit/releases) [![](https://img.shields.io/badge/docs-link-success)](https://docs.zigurous.com/com.zigurous.prototyping) [![](https://img.shields.io/github/license/zigurous/unity-prototyping-kit)](https://github.com/zigurous/unity-prototyping-kit/blob/main/LICENSE.md)

The Prototyping Kit package contains assets and materials for prototyping levels in Unity. The best feature of the package is the ability for objects to be styled without needing to create new materials, and the ability for those materials to be automatically tiled based on the object's size. The package is still in development, and more functionality will be added over time.

## Reference

- [Prefabs](https://docs.zigurous.com/com.zigurous.prototyping/manual/prefabs.html)
- [Materials](https://docs.zigurous.com/com.zigurous.prototyping/manual/materials.html)
- [Textures](https://docs.zigurous.com/com.zigurous.prototyping/manual/textures.html)
- [Material Selector (Script)](https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping.MaterialSelector.html)
- [Material Tiling (Script)](https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping.MaterialTiling.html)

## Installation

Use the Unity [Package Manager](https://docs.unity3d.com/Manual/upm-ui.html) to install the Prototyping Kit package.

1. Open the Package Manager in `Window > Package Manager`
2. Click the add (`+`) button in the status bar
3. Select `Add package from git URL` from the add menu
4. Enter the following Git URL in the text box and click Add:

```http
https://github.com/zigurous/unity-prototyping-kit.git
```

For more information on the Package Manager and installing packages, see the following pages:

- [Unity's Package Manager](https://docs.unity3d.com/Manual/Packages.html)
- [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

### Importing

Import the package namespace in each script or file you want to use it.

> **Note**: You may need to regenerate project files/assemblies first.

```csharp
using Zigurous.Prototyping;
```
