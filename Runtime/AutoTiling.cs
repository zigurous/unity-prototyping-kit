using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material based on the scale of the object.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    public abstract class AutoTiling : MonoBehaviour
    {
        /// <summary>
        /// The renderer component of the material(s) being tiled.
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The material instance(s) being tiled.
        /// </summary>
        public Material[] materials { get; protected set; }

        /// <summary>
        /// The shared material that material instances are created from.
        /// </summary>
        [Tooltip("The material that material instances are created from.")]
        public Material sharedMaterial;

        /// <summary>
        /// The id of the shared material to determine when changes are made.
        /// </summary>
        public int sharedMaterialId { get; private set; }

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
        public Vector3 textureScale = Vector3.one;

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

        private void LateUpdate()
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
            if (!Application.isPlaying && !this.updateInEditor) {
                return;
            }
            #endif

            if (this.sharedMaterial != null)
            {
                if (this.materials == null || this.sharedMaterialId != this.sharedMaterial.GetInstanceID())
                {
                    UpdateMaterials();
                }
            }

            if (this.materials != null) {
                SetTextureScale();
            }
        }

        public Vector3 GetTextureScale()
        {
            return Vector3.Scale(this.transform.localScale, this.textureScale);
        }

        private void UpdateMaterials()
        {
            CreateMaterials();

            this.sharedMaterialId = this.sharedMaterial.GetInstanceID();

            if (Application.isPlaying) {
                this.renderer.materials = this.materials;
            } else {
                this.renderer.sharedMaterials = this.materials;
            }
        }

        protected abstract void CreateMaterials();
        protected abstract void SetTextureScale();

    }

}
