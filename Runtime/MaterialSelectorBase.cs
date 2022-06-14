#if UNITY_EDITOR
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
#endif
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Applies a material style and pattern to a renderer.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Zigurous/Prototyping/Material Selector Base")]
    public class MaterialSelectorBase : MonoBehaviour
    {
        private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
        private static readonly int _Smoothness = Shader.PropertyToID("_Glossiness");

        /// <summary>
        /// The renderer that holds the material being selected (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// Applies the selected style and pattern to the renderer.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <param name="pattern">The pattern to apply.</param>
        public virtual void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) || PrefabStageUtility.GetCurrentPrefabStage() != null) {
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

            MaterialTiling tiling = GetComponent<MaterialTiling>();

            if (tiling != null) {
                tiling.Tile();
            }
        }

        protected Material CreateMaterial(MaterialStyle style, MaterialPattern pattern)
        {
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
