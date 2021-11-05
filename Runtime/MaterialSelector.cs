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
        private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int _Smoothness = Shader.PropertyToID("_Glossiness");

        /// <summary>
        /// The renderer that holds the material being selected (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        [SerializeField]
        [Tooltip("The selected style preset.")]
        private MaterialStyle.Preset m_Style = MaterialStyle.Preset.Black;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public MaterialStyle.Preset style
        {
            get => m_Style;
            set
            {
                m_Style = value;
                UpdateRenderer();
            }
        }

        [SerializeField]
        [Tooltip("The selected pattern preset.")]
        private MaterialPattern.Preset m_Pattern = MaterialPattern.Preset.Pattern1;

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public MaterialPattern.Preset pattern
        {
            get => m_Pattern;
            set
            {
                m_Pattern = value;
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
        public void Apply(MaterialStyle.Preset style, MaterialPattern.Preset pattern)
        {
            m_Style = style;
            m_Pattern = pattern;

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
                for (int i = 0; i < materials.Length; i++) {
                    materials[i] = CreateMaterial();
                }

                renderer.materials = materials;
            }
        }

        private void UpdateSharedMaterials()
        {
            Material[] materials = renderer.sharedMaterials;

            if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++) {
                    materials[i] = CreateMaterial();
                }

                renderer.sharedMaterials = materials;
            }
        }

        private Material CreateMaterial()
        {
            MaterialPattern pattern = patterns.GetPattern(m_Pattern);
            MaterialStyle style = styles.GetStyle(m_Style);

            if (pattern.baseMaterial == null) {
                return null;
            }

            Material material = new Material(pattern.baseMaterial);
            material.color = style.color;
            material.SetFloat(_Metallic, style.metallic);
            material.SetFloat(_Smoothness, style.smoothness);

            return material;
        }

    }

}
