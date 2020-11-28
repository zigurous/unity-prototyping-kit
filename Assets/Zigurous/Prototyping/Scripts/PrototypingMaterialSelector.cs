using UnityEditor;
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a renderer's material from a preset list of options.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public sealed class PrototypingMaterialSelector : MonoBehaviour
    {
        /// <summary>
        /// The renderer that holds the material being set.
        /// </summary>
        private Renderer _renderer;

        /// <summary>
        /// The selected material preset.
        /// </summary>
        [Tooltip("The selected material preset.")]
        [SerializeField]
        private PrototypingMaterialPreset preset;

        /// <summary>
        /// The palette of available materials
        /// from which new materials are created.
        /// </summary>
        [Tooltip("The palette of available materials from which new materials are cloned.")]
        public PrototypingMaterialPalette palette;

        private void OnDestroy()
        {
            this.palette = null;

            _renderer = null;
        }

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
            if (_renderer == null) {
                _renderer = GetComponent<Renderer>();
            }

            Material presetMaterial = this.palette?.CreateMaterialInstance(this.preset);

            if (presetMaterial != null && !PrefabUtility.IsPartOfPrefabAsset(this))
            {
                Material[] materials = Application.isPlaying ? _renderer.materials : _renderer.sharedMaterials;

                for (int i = 0; i < materials.Length; i++) {
                    materials[i] = presetMaterial;
                }

                if (Application.isPlaying) {
                    _renderer.materials = materials;
                } else {
                    _renderer.sharedMaterials = materials;
                }

                GetComponent<ScaledUVTiling>()?.Tile();
                GetComponent<ScaledUVWTiling>()?.Tile();
            }
        }

    }

}
