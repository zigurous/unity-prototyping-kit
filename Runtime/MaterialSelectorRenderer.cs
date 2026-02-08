#if UNITY_EDITOR
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
#endif
using UnityEngine;
using UnityEngine.Rendering;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Applies a material style and pattern to a renderer.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Zigurous/Prototyping/Material Selector Renderer")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/MaterialSelectorRenderer")]
    public sealed class MaterialSelectorRenderer : MonoBehaviour
    {
        private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int _Glossiness = Shader.PropertyToID("_Glossiness");
        private static readonly int _Smoothness = Shader.PropertyToID("_Smoothness");
        private static readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int _EmissionMap = Shader.PropertyToID("_EmissionMap");
        private static readonly int _EmissiveColor = Shader.PropertyToID("_EmissiveColor");
        private static readonly int _EmissiveColorMap = Shader.PropertyToID("_EmissiveColorMap");
        private static readonly int _NormalMap = Shader.PropertyToID("_NormalMap");
        private static readonly int _BumpMap = Shader.PropertyToID("_BumpMap");
        private static readonly int _HeightMap = Shader.PropertyToID("_HeightMap");
        private static readonly int _ParallaxMap = Shader.PropertyToID("_ParallaxMap");
        private static readonly int _DoubleSidedEnable = Shader.PropertyToID("_DoubleSidedEnable");

        /// <summary>
        /// The renderer that holds the material being selected (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// Renders materials on both sides of the mesh.
        /// </summary>
        public bool doubleSided
        {
            get => m_DoubleSided;
            set => m_DoubleSided = value;
        }

        [SerializeField]
        [Tooltip("Renders materials on both sides of the mesh.")]
        private bool m_DoubleSided;

        /// <summary>
        /// Applies the selected style and pattern to the renderer.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <param name="pattern">The pattern to apply.</param>
        public void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) ||
               (PrefabStageUtility.GetCurrentPrefabStage() != null && !Application.isPlaying)) {
                return;
            }
            #endif

            if (renderer == null) {
                renderer = GetComponent<Renderer>();
            }

            Material[] materials;

            if (Application.isPlaying) {
                materials = renderer.materials;
            } else {
                materials = renderer.sharedMaterials;
            }

            if (materials == null) {
                return;
            }

            for (int i = 0; i < materials.Length; i++) {
                materials[i] = CreateMaterial(style, pattern);
            }

            if (Application.isPlaying) {
                renderer.materials = materials;
            } else {
                renderer.sharedMaterials = materials;
            }

            if (TryGetComponent(out MaterialTilingBase tiling)) {
                tiling.Tile();
            }

            DynamicGI.UpdateEnvironment();
        }

        /// <summary>
        /// Creates a material from the selected style and pattern.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <param name="pattern">The pattern to apply.</param>
        /// <returns>The created material.</returns>
        public Material CreateMaterial(MaterialStyle style, MaterialPattern pattern)
        {
            Shader shader;
            RenderPipelineAsset renderPipeline = GraphicsSettings.currentRenderPipeline;

            if (renderPipeline != null) {
                shader = renderPipeline.defaultShader;
            } else {
                shader = Shader.Find("Standard");
            }

            Material material = new(shader)
            {
                color = style.color,
                globalIlluminationFlags = MaterialGlobalIlluminationFlags.None,
                mainTexture = pattern.baseMap,
            };

            material.EnableKeyword("_EMISSION");
            material.EnableKeyword("_NORMALMAP");
            material.EnableKeyword("_PARALLAXMAP");

            if (renderPipeline != null && (renderPipeline.name.Contains("HDRP") || renderPipeline.name.Contains("URP")))
            {
                material.SetFloat(_Smoothness, style.smoothness);
                material.SetColor(_EmissiveColor, Color.white);
                material.SetTexture(_EmissiveColorMap, pattern.emissionMap);
                material.SetTexture(_NormalMap, pattern.normalMap);
                material.SetTexture(_HeightMap, pattern.heightMap);
            }
            else
            {
                material.SetFloat(_Metallic, style.metallic);
                material.SetFloat(_Glossiness, style.smoothness);
                material.SetColor(_EmissionColor, Color.white);
                material.SetTexture(_EmissionMap, pattern.emissionMap);
                material.SetTexture(_BumpMap, pattern.normalMap);
                material.SetTexture(_ParallaxMap, pattern.heightMap);
            }

            material.SetInt(_DoubleSidedEnable, m_DoubleSided ? 1 : 0);
            material.doubleSidedGI = m_DoubleSided;

            #if UNITY_EDITOR
            UnityEditor.Rendering.HighDefinition.HDShaderUtils.ResetMaterialKeywords(material);
            #endif

            return material;
        }

    }

}
