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
        }

        /// <summary>
        /// The preset enumeration value of the pattern.
        /// </summary>
        [Tooltip("The preset enumeration value of the pattern.")]
        public Preset preset;

        /// <summary>
        /// The base material of the pattern.
        /// </summary>
        [Tooltip("The base material of the pattern.")]
        public Material baseMaterial;
    }

}
