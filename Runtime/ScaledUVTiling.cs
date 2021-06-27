#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Changes the tiling properties of a material's texture based on the scale
    /// of the rendered object.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    public sealed class ScaledUVTiling : MonoBehaviour
    {
        /// <summary>
        /// The instance that renders the material of the tiled texture.
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The instance id of the cloned material to keep track of when the
        /// shared material has changed.
        /// </summary>
        public int materialInstanceId { get; private set; }

        /// <summary>
        /// The shader property name of the texture being tiled, usually
        /// "_MainTex".
        /// </summary>
        [Tooltip("The shader property name of the texture being tiled.")]
        public string texturePropertyName = "_MainTex";

        /// <summary>
        /// The base texture scale, e.g., planes usually have a scale of 10,
        /// otherwise most objects have a scale of one.
        /// </summary>
        [Tooltip("The base texture scale, e.g., planes usually have a scale of 10, otherwise most objects have a scale of one.")]
        public Vector2 textureScale = Vector2.one;

        /// <summary>
        /// Whether the material texture will automatically be retiled when the
        /// transform changes.
        /// </summary>
        [Tooltip("Whether the material texture will automatically be retiled when the transform of the renderer changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture will be tiled while running in the
        /// Unity editor.
        /// </summary>
        [Tooltip("Whether the material texture will be tiled while running in the Unity editor.")]
        public bool updateInEditor = false;
        #endif

        private void OnValidate()
        {
            if (this.enabled) {
                Tile();
            }
        }

        private void Update()
        {
            if (this.autoUpdate && this.transform.hasChanged)
            {
                Tile();
                this.transform.hasChanged = false;
            }
        }

        public void Tile()
        {
            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }

            #if UNITY_EDITOR
                UpdateMaterialInEditor();
            #else
                UpdateMaterial();
            #endif
        }

        private void UpdateMaterial()
        {
            Material material = Application.isPlaying ?
                this.renderer.material :
                this.renderer.sharedMaterial;

            UpdateMaterial(material);
        }

        private void UpdateMaterial(Material material)
        {
            if (material != null)
            {
                Vector2 scale = CalculateTextureScale();
                material.SetTextureScale(this.texturePropertyName, scale);
            }
        }

        #if UNITY_EDITOR
        private void UpdateMaterialInEditor()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(this)) {
                return;
            }

            if (Application.isPlaying)
            {
                UpdateMaterial(this.renderer.material);
            }
            else if (this.updateInEditor)
            {
                Material material = this.renderer.sharedMaterial;

                if (material != null && material.GetInstanceID() != this.materialInstanceId)
                {
                    material = new Material(material);
                    this.materialInstanceId = material.GetInstanceID();
                    this.renderer.sharedMaterial = material;
                }

                UpdateMaterial(material);
            }
        }
        #endif

        public Vector2 CalculateTextureScale()
        {
            return new Vector2(
                this.transform.localScale.x * this.textureScale.x,
                this.transform.localScale.z * this.textureScale.y);
        }

    }

}
