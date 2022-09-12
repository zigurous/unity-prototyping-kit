using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A material pattern, defined by a texture asset.
    /// </summary>
    [System.Serializable]
    public struct MaterialPattern
    {
        /// <summary>
        /// An enumerated preset of material patterns.
        /// </summary>
        public enum Preset
        {
            Pattern0,
            Pattern1,
            Pattern2,
            Pattern3,
            Pattern4,
            Pattern5,
            Pattern6,
            Pattern7,
            Pattern8,
            Pattern9,
            Pattern10,
            Pattern11,
            Pattern12,
            Pattern13,
            Pattern14,
            Pattern15,
        }

        /// <summary>
        /// The preset enumeration value of the pattern.
        /// </summary>
        [Tooltip("The preset enumeration value of the pattern.")]
        public Preset preset;

        /// <summary>
        /// The emission map texture of the pattern.
        /// </summary>
        [Tooltip("The emission map texture of the pattern.")]
        public Texture2D emissionMap;

        /// <summary>
        /// The normal map texture of the pattern.
        /// </summary>
        [Tooltip("The normal map texture of the pattern.")]
        public Texture2D normalMap;

        /// <summary>
        /// The height map texture of the pattern.
        /// </summary>
        [Tooltip("The height map texture of the pattern.")]
        public Texture2D heightMap;

        /// <summary>
        /// Creates a new material pattern with the specified preset.
        /// </summary>
        /// <param name="preset">The preset enumeration value of the pattern.</param>
        /// <param name="emissionMap">The emission map texture of the pattern.</param>
        /// <param name="normalMap">The normal map texture of the pattern.</param>
        /// <param name="heightMap">The height map texture of the pattern.</param>
        public MaterialPattern(Preset preset, Texture2D emissionMap = null, Texture2D normalMap = null, Texture2D heightMap = null)
        {
            this.preset = preset;
            this.emissionMap = emissionMap;
            this.normalMap = normalMap;
            this.heightMap = heightMap;
        }

    }

}
