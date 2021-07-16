using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A collection of material pattern presets.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Pattern Palette")]
    public sealed class MaterialPatternPalette : ScriptableObject
    {
        private static readonly int _EmissionMap = Shader.PropertyToID("_EmissionMap");

        /// <summary>
        /// A material pattern, indicating a texture asset.
        /// </summary>
        [System.Serializable]
        public struct Pattern
        {
            /// <summary>
            /// The preset enumeration value of the pattern.
            /// </summary>
            [Tooltip("The preset enumeration value of the pattern.")]
            public MaterialPatternPreset preset;

            /// <summary>
            /// The texture of the pattern.
            /// </summary>
            [Tooltip("The texture of the pattern.")]
            public Texture2D texture;
        }

        /// <summary>
        /// The available patterns in the palette.
        /// </summary>
        [Tooltip("The available patterns in the palette.")]
        public Pattern[] patterns = new Pattern[0];

        /// <summary>
        /// Returns the texture asset for the <paramref name="preset"/>.
        /// </summary>
        /// <param name="preset">The preset to get the texture for.</param>
        public Texture2D GetTexture(MaterialPatternPreset preset)
        {
            if (this.patterns == null) {
                return null;
            }

            for (int i = 0; i < this.patterns.Length; i++)
            {
                Pattern pattern = this.patterns[i];

                if (pattern.preset == preset) {
                    return pattern.texture;
                }
            }

            return null;
        }

        /// <summary>
        /// Applies the preset pattern texture to the material.
        /// </summary>
        /// <param name="material">The material to apply the pattern to.</param>
        /// <param name="preset">The preset to apply.</param>
        public void SetTexture(Material material, MaterialPatternPreset preset)
        {
            if (this.patterns == null) {
                return;
            }

            for (int i = 0; i < this.patterns.Length; i++)
            {
                Pattern pattern = this.patterns[i];

                if (pattern.preset == preset)
                {
                    material.SetTexture(_EmissionMap, pattern.texture);
                    return;
                }
            }
        }

    }

}
