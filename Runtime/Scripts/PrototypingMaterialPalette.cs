using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// An enumerated collection of preset materials.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Prototyping/Material Palette")]
    public sealed class PrototypingMaterialPalette : ScriptableObject
    {
        /// <summary>
        /// A preset material defined by an enumerated value.
        /// </summary>
        [System.Serializable]
        public struct PresetMaterial
        {
            /// <summary>
            /// The preset enumeration value for the material.
            /// </summary>
            [Tooltip("The preset enumeration value for the material.")]
            public PrototypingMaterialPreset preset;

            /// <summary>
            /// The shared material asset from which new materials are created.
            /// </summary>
            [Tooltip("The shared material asset from which new materials are created.")]
            public Material sharedMaterial;
        }

        /// <summary>
        /// The available materials in the palette.
        /// </summary>
        [Tooltip("The available preset materials in the palette.")]
        public PresetMaterial[] materials = new PresetMaterial[0];

        /// <summary>
        /// Returns the shared material asset for a given preset value.
        /// </summary>
        public Material GetSharedMaterial(PrototypingMaterialPreset preset)
        {
            if (this.materials == null) {
                return null;
            }

            for (int i = 0; i < this.materials.Length; i++)
            {
                PresetMaterial presetMaterial = this.materials[i];

                if (presetMaterial.preset == preset) {
                    return presetMaterial.sharedMaterial;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a random shared material asset from the palette.
        /// </summary>
        public Material GetRandomSharedMaterial()
        {
            if (this.materials == null || this.materials.Length <= 0) {
                return null;
            }

            return this.materials[Random.Range(0, this.materials.Length)].sharedMaterial;
        }

        /// <summary>
        /// Returns a new material instance for a given preset value.
        /// </summary>
        public Material CreateMaterialInstance(PrototypingMaterialPreset preset)
        {
            Material sharedMaterial = GetSharedMaterial(preset);

            if (sharedMaterial != null) {
                return new Material(sharedMaterial);
            } else {
                return null;
            }
        }

        /// <summary>
        /// Returns a random material instance from the palette.
        /// </summary>
        public Material CreateRandomMaterialInstance()
        {
            Material sharedMaterial = GetRandomSharedMaterial();

            if (sharedMaterial != null) {
                return new Material(sharedMaterial);
            } else {
                return null;
            }
        }

    }

}
