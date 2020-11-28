using UnityEditor;
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Changes the tiling properties of a material's texture
    /// based on the scale of the rendered object.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    public sealed class ScaledUVTiling : MonoBehaviour
    {
        /// <summary>
        /// The instance that renders the material of the tiled texture.
        /// </summary>
        [Tooltip("The instance that renders the material of the tiled texture.")]
        private Renderer _renderer;

        /// <summary>
        /// The instance id of the cloned material to keep track of when the
        /// shared material has changed.
        /// </summary>
        private int _materialInstanceId;

        /// <summary>
        /// The shader property name of the texture being tiled,
        /// usually "_MainTex".
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
        /// Whether the material texture will automatically be retiled when
        /// the transform changes.
        /// </summary>
        [Tooltip("Whether the material texture will automatically be retiled when the transform of the renderer changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture will be tiled
        /// while running in the Unity editor.
        /// </summary>
        [Tooltip("Whether the material texture will be tiled while running in the Unity editor.")]
        public bool updateInEditor = false;
        #endif

        private void OnDestroy()
        {
            _renderer = null;
        }

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
            #if UNITY_EDITOR
            if (!CanUpdateInEditor()) {
                return;
            }
            #endif

            if (_renderer == null) {
                _renderer = GetComponent<Renderer>();
            }

            Material material = Application.isPlaying ? _renderer.material : _renderer.sharedMaterial;

            if (material == null) {
                return;
            }

            #if UNITY_EDITOR
                if (Application.isPlaying) {
                    material.SetTextureScale(this.texturePropertyName, CalculateTextureScale());
                }
                else if (this.updateInEditor)
                {
                    if (material.GetInstanceID() != _materialInstanceId)
                    {
                        material = new Material(material);
                        _materialInstanceId = material.GetInstanceID();
                        _renderer.sharedMaterial = material;
                    }

                    material.SetTextureScale(this.texturePropertyName, CalculateTextureScale());
                }
            #else
                material.SetTextureScale(this.texturePropertyName, CalculateTextureScale());
            #endif
        }

        public Vector2 CalculateTextureScale() => new Vector2(
                this.transform.localScale.x * this.textureScale.x,
                this.transform.localScale.z * this.textureScale.y);

        #if UNITY_EDITOR
        private bool CanUpdateInEditor()
        {
            if (!Application.isPlaying && !this.updateInEditor) {
                return false;
            }

            if (PrefabUtility.IsPartOfPrefabAsset(this)) {
                return false;
            }

            return true;
        }
        #endif

    }

}
