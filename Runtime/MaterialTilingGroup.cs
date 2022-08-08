using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of a group of objects based on their
    /// individual scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Material Tiling Group")]
    public sealed class MaterialTilingGroup : MonoBehaviour
    {
        /// <summary>
        /// Whether the material textures are tiled automatically when the
        /// transform changes.
        /// </summary>
        [Tooltip("Whether the material textures are tiled automatically when the transform changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material textures are tiled while in the editor.
        /// </summary>
        [Tooltip("Whether the material textures are tiled while in the editor.")]
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
        /// Tiles the materials of each object in the group.
        /// </summary>
        public void Tile()
        {
            MaterialTiling[] tilings = GetComponentsInChildren<MaterialTiling>();

            for (int i = 0; i < tilings.Length; i++) {
                tilings[i].Tile();
            }
        }

        /// <summary>
        /// Forces the materials of each object in the group to be updated.
        /// </summary>
        public void ForceUpdate()
        {
            MaterialTiling[] tilings = GetComponentsInChildren<MaterialTiling>();

            for (int i = 0; i < tilings.Length; i++) {
                tilings[i].ForceUpdate();
            }
        }

    }

}
