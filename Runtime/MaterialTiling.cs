#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of an object based on its scale.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    public abstract class MaterialTiling : MonoBehaviour
    {
        private static int _MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>
        /// The renderer component of the material being tiled.
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// Whether the material texture(s) are tiled automatically when the
        /// transform changes.
        /// </summary>
        [Tooltip("Whether the material texture(s) are tiled automatically when the transform changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture(s) are tiled while in the editor.
        /// </summary>
        [Tooltip("Whether the material texture(s) are tiled while in the editor.")]
        public bool updateInEditor = true;
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

        /// <summary>
        /// Tiles the materials of the object.
        /// </summary>
        public void Tile()
        {
            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) || PrefabStageUtility.GetCurrentPrefabStage() != null) {
                return;
            }
            #endif

            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying)
            {
                UpdateMaterials();
            }
            #if UNITY_EDITOR
            else if (this.updateInEditor)
            {
                UpdateMaterialsInEditor();
            }
            #endif
        }

        /// <summary>
        /// Sets the texture scale of the <paramref name="material"/> to the
        /// provided <paramref name="scale"/>.
        /// </summary>
        /// <param name="material">The material to set the texture scale of.</param>
        /// <param name="scale">The texture scale to set.</param>
        protected void SetTextureScale(Material material, Vector2 scale)
        {
            if (material != null) {
                material.SetTextureScale(_MainTex, scale);
            }
        }

        /// <summary>
        /// Updates the materials of the object.
        /// </summary>
        protected abstract void UpdateMaterials();

        /// <summary>
        /// Updates the materials of the object while running in the editor.
        /// </summary>
        protected abstract void UpdateMaterialsInEditor();

    }

}
