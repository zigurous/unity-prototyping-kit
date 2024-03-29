﻿#if UNITY_EDITOR
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
    public abstract class MaterialTilingBase : MonoBehaviour
    {
        /// <summary>
        /// The renderer component of the material being tiled (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The texture tiling offset of the material being tiled.
        /// </summary>
        [Tooltip("The texture tiling offset of the material being tiled.")]
        public Vector3 tilingOffset = Vector3.zero;

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

        /// <summary>
        /// The instance id of the shared material to detect when changes are
        /// made and new material copies can be created. This is required for
        /// updating in the editor.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        internal int sharedInstanceId = -1;

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
            if (PrefabUtility.IsPartOfPrefabAsset(this) ||
               (PrefabStageUtility.GetCurrentPrefabStage() != null && !Application.isPlaying)) {
                return;
            }
            #endif

            if (renderer == null) {
                renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying) {
                UpdateMaterials();
            }
            #if UNITY_EDITOR
            else if (updateInEditor) {
                UpdateMaterialsInEditor();
            }
            #endif
        }

        /// <summary>
        /// Forces the materials to be updated.
        /// </summary>
        public void ForceUpdate()
        {
            sharedInstanceId = -1;
            Tile();
        }

        /// <summary>
        /// Sets the texture scale of a material.
        /// </summary>
        /// <param name="material">The material to set the texture scale of.</param>
        /// <param name="scale">The texture scale to set.</param>
        protected void SetTextureScale(Material material, Vector2 scale)
        {
            if (material != null)
            {
                material.mainTextureScale = scale * scaleMultiplier;
                material.mainTextureOffset = tilingOffset;
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

        #if UNITY_EDITOR
        [MenuItem("CONTEXT/MaterialTilingBase/Force Update")]
        private static void ContextMenu_ForceUpdate()
        {
            if (Selection.activeGameObject != null)
            {
                MaterialTilingBase tiling = Selection.activeGameObject.GetComponent<MaterialTilingBase>();

                if (tiling != null) {
                    tiling.ForceUpdate();
                }
            }
        }
        #endif

    }

}
