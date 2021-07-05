#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
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
        /// The renderer that holds the material being selected.
        /// </summary>
        public new Renderer renderer { get; private set; }

        [Tooltip("The selected style preset.")]
        [SerializeField]
        private PrototypingMaterialStylePreset _style = PrototypingMaterialStylePreset.Black;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public PrototypingMaterialStylePreset style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateRenderer();
            }
        }

        [Tooltip("The selected pattern preset.")]
        [SerializeField]
        private PrototypingMaterialPatternPreset _pattern = PrototypingMaterialPatternPreset.Pattern1;

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public PrototypingMaterialPatternPreset pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                UpdateRenderer();
            }
        }

        /// <summary>
        /// The palette of available styles.
        /// </summary>
        [Tooltip("The palette of available styles.")]
        public PrototypingMaterialStylePalette styles;

        /// <summary>
        /// The palette of available patterns.
        /// </summary>
        [Tooltip("The palette of available patterns.")]
        public PrototypingMaterialPatternPalette patterns;

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
            if (this.styles == null) {
                return;
            }

            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) || PrefabStageUtility.GetCurrentPrefabStage() != null) {
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

            if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    Material material = this.styles.CreateMaterialInstance(this.style);

                    if (this.patterns != null) {
                        this.patterns.SetTexture(material, this.pattern);
                    }

                    materials[i] = material;
                }

                this.renderer.materials = materials;
            }
        }

        private void UpdateSharedMaterials()
        {
            Material[] materials = this.renderer.sharedMaterials;

            if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    Material material = this.styles.CreateMaterialInstance(this.style);

                    if (this.patterns != null) {
                        this.patterns.SetTexture(material, this.pattern);
                    }

                    materials[i] = material;
                }

                this.renderer.sharedMaterials = materials;
            }
        }

    }

}
