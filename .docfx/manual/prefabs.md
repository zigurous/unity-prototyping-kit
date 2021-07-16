# Prefabs

The Prototyping Kit comes with several prefab objects, primarily for blocking out levels. The grid prefabs, such as the `Grid-Cube.prefab`, are the most commonly used objects as these are configured with a material selector and automatic tiling. Using the prefabs provides the most utility out of the box.

### Material Selector

The grid prefabs all have a [Material Selector](xref:Zigurous.Prototyping.MaterialSelector) script that allows you to select a material style and pattern from a list of presets. This means you can customize the look of every object, and it will change the materials automatically. By default, there are 20 styles and 14 patterns to choose from.

### Automatic Tiling

The grid and checkerboard prefabs have a [Material Tiling](xref:Zigurous.Prototyping.MaterialTiling) script that automatically tiles the material based on the object's size. This is extremely convenient so you do not need to create different material variants for different sized objects. For cubes, this is made possible from a custom mesh applied to the object.
