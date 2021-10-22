using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// A collection of material style presets.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Style Palette")]
    public sealed class MaterialStylePalette : ScriptableObject
    {
        private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int _Smoothness = Shader.PropertyToID("_Glossiness");

        /// <summary>
        /// A material style, defined by color, metallic, and smoothness values.
        /// </summary>
        [System.Serializable]
        public struct Style
        {
            /// <summary>
            /// The preset enumeration value of the style.
            /// </summary>
            [Tooltip("The preset enumeration value of the style.")]
            public MaterialStylePreset preset;

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
        }

        /// <summary>
        /// The base material from which new materials are created.
        /// </summary>
        [Tooltip("The base material from which new materials are created.")]
        public Material baseMaterial;

        /// <summary>
        /// The available styles in the palette.
        /// </summary>
        [Tooltip("The available styles in the palette.")]
        public Style[] styles = new Style[0];

        /// <summary>
        /// Returns a new material instance for a given preset.
        /// </summary>
        /// <param name="preset">The preset for the material.</param>
        /// <returns>A new material instance for the given preset.</returns>
        public Material CreateMaterialInstance(MaterialStylePreset preset)
        {
            if (baseMaterial == null) {
                return null;
            }

            Material material = new Material(baseMaterial);

            for (int i = 0; i < styles.Length; i++)
            {
                Style style = styles[i];

                if (style.preset == preset)
                {
                    material.color = style.color;
                    material.SetFloat(_Metallic, style.metallic);
                    material.SetFloat(_Smoothness, style.smoothness);
                    break;
                }
            }

            return material;
        }

    }

}
