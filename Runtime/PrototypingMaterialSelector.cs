#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a renderer's material from a preset list of options.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Zigurous/Prototyping/Prototyping Material Selector")]
    public sealed class PrototypingMaterialSelector : MonoBehaviour
    {
        /// <summary>
        /// The renderer that holds the material being set.
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The selected material preset.
        /// </summary>
        [Tooltip("The selected material preset.")]
        [SerializeField]
        private PrototypingMaterialPreset preset;

        /// <summary>
        /// The palette of available materials from which new materials are
        /// created.
        /// </summary>
        [Tooltip("The palette of available materials from which new materials are cloned.")]
        public PrototypingMaterialPalette palette;

        private void OnEnable()
        {
            UpdateMaterial();
        }

        private void OnValidate()
        {
            if (this.enabled) {
                UpdateMaterial();
            }
        }

        public void SetPreset(PrototypingMaterialPreset preset)
        {
            this.preset = preset;

            UpdateMaterial();
        }

        public void UpdateMaterial()
        {
            if (this.palette == null) {
                return;
            }

            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }

            Material presetMaterial = this.palette.CreateMaterialInstance(this.preset);

            if (presetMaterial != null && CanUpdate())
            {
                Material[] materials = Application.isPlaying ?
                    this.renderer.materials :
                    this.renderer.sharedMaterials;

                for (int i = 0; i < materials.Length; i++) {
                    materials[i] = presetMaterial;
                }

                if (Application.isPlaying) {
                    this.renderer.materials = materials;
                } else {
                    this.renderer.sharedMaterials = materials;
                }

                Retile();
            }
        }

        private bool CanUpdate()
        {
            #if UNITY_EDITOR
                return !PrefabUtility.IsPartOfPrefabAsset(this);
            #else
                return true;
            #endif
        }

        private void Retile()
        {
            ScaledUVTiling uvTiling = GetComponent<ScaledUVTiling>();
            ScaledUVWTiling uvwTiling = GetComponent<ScaledUVWTiling>();

            if (uvTiling != null) {
                uvTiling.Tile();
            }

            if (uvwTiling != null) {
                uvwTiling.Tile();
            }
        }

    }

}
