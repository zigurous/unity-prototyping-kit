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
    public sealed class ScaledUVWTiling : MonoBehaviour
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
        public Vector3 textureScale = Vector3.one;

        /// <summary>
        /// Whether the material texture will automatically be retiled when
        /// the transform changes.
        /// </summary>
        [Tooltip("Whether the material texture will automatically be retiled when the transform of the renderer changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture(s) will be tiled
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

            Material[] materials = FixedMaterialsArray();
            Vector3 scale = CalculateTextureScale();

            #if UNITY_EDITOR
                if (Application.isPlaying || this.updateInEditor)
                {
                    materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.z, scale.y));
                    materials[1].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.y));
                    materials[2].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.z));
                }
            #else
                materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.z, scale.y));
                materials[1].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.y));
                materials[2].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.z));
            #endif
        }

        private Material[] FixedMaterialsArray()
        {
            if (_renderer == null) {
                _renderer = GetComponent<Renderer>();
            }

            Material[] materials = Application.isPlaying ? _renderer.materials : _renderer.sharedMaterials;
            Material mainMaterial = materials[0];

            if (mainMaterial == null) {
                return null;
            }

            if (Application.isPlaying && materials.Length != 3)
            {
                materials = new Material[3] {
                    new Material(mainMaterial),
                    new Material(mainMaterial),
                    new Material(mainMaterial)
                };

                _renderer.materials = materials;
            }
            else if (!Application.isPlaying && this.updateInEditor && mainMaterial.GetInstanceID() != _materialInstanceId)
            {
                materials = new Material[3] {
                    new Material(mainMaterial),
                    new Material(mainMaterial),
                    new Material(mainMaterial)
                };

                _renderer.sharedMaterials = materials;
                _materialInstanceId = materials[0].GetInstanceID();
            }

            return materials;
        }

        public Vector3 CalculateTextureScale() => new Vector3(
                this.transform.localScale.x * this.textureScale.x,
                this.transform.localScale.y * this.textureScale.y,
                this.transform.localScale.z * this.textureScale.z);

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
