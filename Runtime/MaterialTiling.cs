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
    /// Automatically tiles the materials of an object based on its scale.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    public abstract class MaterialTiling : MonoBehaviour
    {
        /// <summary>
        /// The renderer component of the material being tiled (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The texture scale multiplier of the material being tiled.
        /// </summary>
        [Tooltip("The texture scale multiplier of the material being tiled.")]
        public float scaleMultiplier = 1f;

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
            if (enabled) {
                Tile();
            }
        }

        private void LateUpdate()
        {
            if (autoUpdate && transform.hasChanged)
            {
                Tile();
                transform.hasChanged = false;
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

            if (renderer == null) {
                renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying)
            {
                UpdateMaterials();
            }
            #if UNITY_EDITOR
            else if (updateInEditor)
            {
                UpdateMaterialsInEditor();
            }
            #endif
        }

        /// <summary>
        /// Sets the texture scale of a material.
        /// </summary>
        /// <param name="material">The material to set the texture scale of.</param>
        /// <param name="scale">The texture scale to set.</param>
        protected void SetTextureScale(Material material, Vector2 scale)
        {
            if (material != null) {
                material.mainTextureScale = scale * scaleMultiplier;
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
