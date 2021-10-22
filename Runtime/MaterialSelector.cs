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
    public sealed class MaterialSelector : MonoBehaviour
    {
        /// <summary>
        /// The renderer that holds the material being selected (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        [SerializeField]
        [Tooltip("The selected style preset.")]
        private MaterialStylePreset _style = MaterialStylePreset.Black;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public MaterialStylePreset style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateRenderer();
            }
        }

        [SerializeField]
        [Tooltip("The selected pattern preset.")]
        private MaterialPatternPreset _pattern = MaterialPatternPreset.Pattern1;

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public MaterialPatternPreset pattern
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
        public MaterialStylePalette styles;

        /// <summary>
        /// The palette of available patterns.
        /// </summary>
        [Tooltip("The palette of available patterns.")]
        public MaterialPatternPalette patterns;

        /// <summary>
        /// Applies the selected style and pattern to the renderer.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <param name="pattern">The pattern to apply.</param>
        public void Apply(MaterialStylePreset style, MaterialPatternPreset pattern)
        {
            _style = style;
            _pattern = pattern;

            UpdateRenderer();
        }

        private void OnEnable()
        {
            UpdateRenderer();
        }

        private void OnValidate()
        {
            if (enabled) {
                UpdateRenderer();
            }
        }

        private void UpdateRenderer()
        {
            if (styles == null) {
                return;
            }

            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) || PrefabStageUtility.GetCurrentPrefabStage() != null) {
                return;
            }
            #endif

            if (renderer == null) {
                renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying) {
                UpdateMaterials();
            } else {
                UpdateSharedMaterials();
            }

            MaterialTiling tiling = GetComponent<MaterialTiling>();

            if (tiling != null) {
                tiling.Tile();
            }
        }

        private void UpdateMaterials()
        {
            Material[] materials = renderer.materials;

            if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    Material material = styles.CreateMaterialInstance(style);

                    if (patterns != null) {
                        patterns.SetTexture(material, pattern);
                    }

                    materials[i] = material;
                }

                renderer.materials = materials;
            }
        }

        private void UpdateSharedMaterials()
        {
            Material[] materials = renderer.sharedMaterials;

            if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    Material material = styles.CreateMaterialInstance(style);

                    if (patterns != null) {
                        patterns.SetTexture(material, pattern);
                    }

                    materials[i] = material;
                }

                renderer.sharedMaterials = materials;
            }
        }

    }

}
