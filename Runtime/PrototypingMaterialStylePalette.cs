using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// An enumerated collection of preset styles.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Style Palette")]
    public sealed class PrototypingMaterialStylePalette : ScriptableObject
    {
        private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int _Smoothness = Shader.PropertyToID("_Glossiness");

        [System.Serializable]
        public struct Style
        {
            /// <summary>
            /// The preset enumeration value of the style.
            /// </summary>
            [Tooltip("The preset enumeration value of the style.")]
            public PrototypingMaterialStylePreset preset;

            /// <summary>
            /// The color of the material style.
            /// </summary>
            [Tooltip("The color of the material style.")]
            public Color color;

            [Range(0.0f, 1.0f)]
            public float metallic;

            [Range(0.0f, 1.0f)]
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
        /// Returns a new material instance for a given preset value.
        /// </summary>
        public Material CreateMaterialInstance(PrototypingMaterialStylePreset preset)
        {
            if (this.baseMaterial == null) {
                return null;
            }

            Material material = new Material(this.baseMaterial);

            for (int i = 0; i < this.styles.Length; i++)
            {
                Style style = this.styles[i];

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
