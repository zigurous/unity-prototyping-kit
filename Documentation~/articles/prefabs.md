---
slug: "/manual/prefabs"
---

# Prefabs

The **Prototyping Kit** comes with several prefab objects, primarily for blocking out levels. The grid prefabs, such as the `Grid-Cube.prefab`, are the most commonly used objects as these are configured with a material selector and automatic tiling. Using the prefabs provides the most utility out of the box.

<hr/>

## 🔘 Material Selector

The grid prefabs have a [Material Selector](/api/Zigurous.Prototyping/MaterialSelector) script that allows you to select a material style and pattern from a list of presets. This means you can customize the look of every object, and it will change the materials automatically. There are 20 styles and 14 patterns to choose from.

<hr/>

## 🀄 Automatic Tiling

The grid and checkerboard prefabs have a [Material Tiling](/api/Zigurous.Prototyping/MaterialTiling) script that automatically tiles the material based on the object's size. This is very convenient so you do not need to create different material variants for different sized objects. For cubes, this is made possible from a custom mesh applied to the object.
