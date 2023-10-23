using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A collection of material pattern presets.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Pattern Palette")]
    public sealed class MaterialPatternPalette : ScriptableObject
    {
        /// <summary>
        /// The available patterns in the palette.
        /// </summary>
        [Tooltip("The available patterns in the palette.")]
        public MaterialPattern[] patterns = new MaterialPattern[0];

        /// <summary>
        /// Gets the material pattern for the given preset.
        /// </summary>
        /// <param name="preset">The preset to get the material pattern for.</param>
        /// <returns>The material pattern for the given preset.</returns>
        public MaterialPattern GetPattern(MaterialPattern.Preset preset)
        {
            for (int i = 0; i < patterns.Length; i++)
            {
                if (patterns[i].preset == preset) {
                    return patterns[i];
                }
            }

            return default;
        }

    }

}
