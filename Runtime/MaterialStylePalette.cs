using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A collection of material style presets.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Style Palette")]
    public sealed class MaterialStylePalette : ScriptableObject
    {
        /// <summary>
        /// The available styles in the palette.
        /// </summary>
        [Tooltip("The available styles in the palette.")]
        public MaterialStyle[] styles = new MaterialStyle[0];

        /// <summary>
        /// Gets the material style for the given preset.
        /// </summary>
        /// <param name="preset">The preset to get the material style for.</param>
        /// <returns>The material style for the given preset.</returns>
        public MaterialStyle GetStyle(MaterialStyle.Preset preset)
        {
            for (int i = 0; i < styles.Length; i++)
            {
                if (styles[i].preset == preset) {
                    return styles[i];
                }
            }

            return default(MaterialStyle);
        }

    }

}
