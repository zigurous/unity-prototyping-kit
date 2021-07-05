using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// An enumerated collection of preset patterns.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Pattern Palette")]
    public sealed class PrototypingMaterialPatternPalette : ScriptableObject
    {
        private static readonly int _EmissionMap = Shader.PropertyToID("_EmissionMap");

        [System.Serializable]
        public struct Pattern
        {
            /// <summary>
            /// The preset enumeration value of the pattern.
            /// </summary>
            [Tooltip("The preset enumeration value of the pattern.")]
            public PrototypingMaterialPatternPreset preset;

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
        /// Returns the texture asset for a given preset value.
        /// </summary>
        public Texture2D GetTexture(PrototypingMaterialPatternPreset preset)
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

        public void SetTexture(Material material, PrototypingMaterialPatternPreset preset)
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
