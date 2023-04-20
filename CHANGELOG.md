# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.0] - 2023/05/01

### Added

- New "Pattern_00" textures and material
- New `scaleMultiplier` property added to `MaterialTiling`
- New `tilingOffset` property added to `MaterialTiling`
- Set up package samples with Playground scene

### Changed

- Smoothed shading on custom models
- Changed component menu names
- Updated ramps in playground scene

### Removed

- Script `MaterialTilingSphere`
- Script `MaterialTilingPlane`

## [0.3.0] - 2022/03/26

### Added

- New 3D model primitives
- New grid prefabs (sphere, ramp)
- Normal maps and height maps for pattern textures
- Playground scene
- `MaterialSelectorGroup` script
- `MaterialTilingSphere` script
- `GridAlignment` script

### Changed

- UV coordinates on prototyping cube
- Remade reflection sphere prefab
- Pattern styles and colors

## [0.2.0] - 2021/07/21

### Added

- New prototyping cube mesh
- New grid pattern texture variants (14)
- Cubemap surface shader
- Cubemap prefabs
- 8x8 checkerboard

### Changed

- All scripts have been rewritten for simplicity, performance, and new feature support

## [0.1.0] - 2021/05/19

### Added

#### Materials

- Grid 4x4 (20 color variants)
- Checkerboard 2x2
- Checkerboard 4x4
- Cubemap Axes
- Reflection

#### Textures

- Grid Emission 4x4
- Checkerboard 2x2
- Checkerboard 4x4
- Cubemap Light/Dark
- Cubemap Axes

#### Prefabs

- Grid Cube 1x1x1
- Grid Plane 1x1x1
- Grid Room 50x50x50
- Checkerboard Cube 1x1x1
- Reflection Sphere
- Cubemap Axes

#### Shaders

- Checkerboard Lit
- Checkerboard Unlit
