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
                UpdateMaterial();
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
            UpdateMaterial();
        }

        private void OnValidate()
        {
            if (this.enabled) {
                UpdateMaterial();
            }
        }

        public void UpdateMaterial()
        {
            if (this.palette == null) {
                return;
            }

            AutoTiling tiling = GetComponent<AutoTiling>();

            if (tiling != null)
            {
                tiling.sharedMaterial = this.palette.GetSharedMaterial(this.preset);
                tiling.Tile();
            }
            else
            {
                Material presetMaterial = this.palette.CreateMaterialInstance(this.preset);
                AssignMaterialInstances(presetMaterial);
            }
        }

        private void AssignMaterialInstances(Material presetMaterial)
        {
            if (presetMaterial == null) {
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
        }

    }

}
