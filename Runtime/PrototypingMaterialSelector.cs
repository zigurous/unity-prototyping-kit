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

        [Tooltip("The selected material preset.")]
        [SerializeField]
        private PrototypingMaterialPreset _preset;

        /// <summary>
        /// The selected material preset.
        /// </summary>
        public PrototypingMaterialPreset preset
        {
            get => _preset;
            set
            {
                _preset = value;
                UpdateRenderer();
            }
        }

        /// <summary>
        /// The palette of available materials from which new materials are
        /// created.
        /// </summary>
        [Tooltip("The palette of available materials from which new materials are cloned.")]
        public PrototypingMaterialPalette palette;

        private void OnEnable()
        {
            UpdateRenderer();
        }

        private void OnValidate()
        {
            if (this.enabled) {
                UpdateRenderer();
            }
        }

        private void UpdateRenderer()
        {
            if (this.palette == null) {
                return;
            }

            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this)) {
                return;
            }
            #endif

            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying) {
                UpdateMaterials();
            } else {
                UpdateSharedMaterials();
            }

            PrototypingMaterialTiling tiling = GetComponent<PrototypingMaterialTiling>();

            if (tiling != null) {
                tiling.Tile();
            }
        }

        private void UpdateMaterials()
        {
            Material[] materials = this.renderer.materials;

            for (int i = 0; i < materials.Length; i++) {
                materials[i] = this.palette.CreateMaterialInstance(this.preset);
            }

            this.renderer.materials = materials;
        }

        private void UpdateSharedMaterials()
        {
            Material[] materials = this.renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++) {
                materials[i] = this.palette.GetSharedMaterial(this.preset);
            }

            this.renderer.sharedMaterials = materials;
        }

    }

}
