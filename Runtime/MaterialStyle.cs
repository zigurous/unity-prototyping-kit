using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A material style, defined by color, metallic, and smoothness values.
    /// </summary>
    [System.Serializable]
    public struct MaterialStyle
    {
        /// <summary>
        /// An enumerated preset of material styles.
        /// </summary>
        public enum Preset
        {
            None,
            Glass,
            Black,
            White,
            Gray,
            DarkGray,
            LightGray,
            Brown,
            Red,
            Orange,
            Yellow,
            Lime,
            Green,
            Teal,
            Cyan,
            Azure,
            Blue,
            Indigo,
            Purple,
            Magenta,
            Pink,
            Custom1,
            Custom2,
            Custom3,
            Custom4,
            Custom5
        }

        /// <summary>
        /// The preset enumeration value of the style.
        /// </summary>
        [Tooltip("The preset enumeration value of the style.")]
        public Preset preset;

        /// <summary>
        /// The color of the material style.
        /// </summary>
        [Tooltip("The color of the material style.")]
        public Color color;

        /// <summary>
        /// The metallic value of the material style.
        /// </summary>
        [Tooltip("The metallic value of the material style.")]
        [Range(0f, 1f)]
        public float metallic;

        /// <summary>
        /// The smoothness value of the material style.
        /// </summary>
        [Tooltip("The smoothness value of the material style.")]
        [Range(0f, 1f)]
        public float smoothness;

        /// <summary>
        /// Creates a new material style with the specified preset.
        /// </summary>
        /// <param name="preset">The preset enumeration value of the style.</param>
        /// <param name="color">The color of the material style.</param>
        /// <param name="metallic">The metallic value of the material style.</param>
        /// <param name="smoothness">The smoothness value of the material style.</param>
        public MaterialStyle(Preset preset, Color color = default(Color), float metallic = 0f, float smoothness = 0f)
        {
            this.preset = preset;
            this.color = color;
            this.metallic = metallic;
            this.smoothness = smoothness;
        }

    }

}
