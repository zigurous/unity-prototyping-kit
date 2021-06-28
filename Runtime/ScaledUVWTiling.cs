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
    [AddComponentMenu("Zigurous/Prototyping/Scaled UVW Tiling")]
    public sealed class ScaledUVWTiling : MonoBehaviour
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
        public Vector3 textureScale = Vector3.one;

        /// <summary>
        /// Whether the material texture will automatically be retiled when the
        /// transform changes.
        /// </summary>
        [Tooltip("Whether the material texture will automatically be retiled when the transform of the renderer changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture(s) will be tiled while running in the
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
                UpdateMaterialsInEditor();
            #else
                if (ValidateMaterialsLength()) {
                    UpdateMaterials();
                }
            #endif
        }

        private void UpdateMaterials()
        {
            Material[] materials = Application.isPlaying ?
                this.renderer.materials :
                this.renderer.sharedMaterials;

            if (ValidateMaterialsLength(materials)) {
                UpdateMaterials(materials);
            }
        }

        private void UpdateMaterials(Material[] materials)
        {
            if (materials != null)
            {
                Vector3 scale = CalculateTextureScale();

                materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.z, scale.y));
                materials[1].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.y));
                materials[2].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.z));
            }
        }

        #if UNITY_EDITOR
        private void UpdateMaterialsInEditor()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(this)) {
                return;
            }

            if (Application.isPlaying)
            {
                UpdateMaterials(this.renderer.materials);
            }
            else if (this.updateInEditor)
            {
                Material[] materials = this.renderer.sharedMaterials;

                if (materials != null && materials.Length > 0)
                {
                    Material mainMaterial = materials[0];

                    if (mainMaterial.GetInstanceID() != this.materialInstanceId)
                    {
                        materials = new Material[3] {
                            new Material(mainMaterial),
                            new Material(mainMaterial),
                            new Material(mainMaterial)
                        };

                        this.renderer.sharedMaterials = materials;
                        this.materialInstanceId = materials[0].GetInstanceID();
                    }

                    UpdateMaterials(materials);
                }
            }
        }
        #endif

        private bool ValidateMaterialsLength(Material[] materials)
        {
            if (materials == null) {
                return false;
            }

            if (materials.Length == 3) {
                return true;
            }

            Material mainMaterial = materials[0];

            if (mainMaterial == null) {
                return false;
            }

            materials = new Material[3] {
                new Material(mainMaterial),
                new Material(mainMaterial),
                new Material(mainMaterial)
            };

            if (Application.isPlaying) {
                this.renderer.materials = materials;
            } else {
                this.renderer.sharedMaterials = materials;
            }

            return true;
        }

        public Vector3 CalculateTextureScale()
        {
            return new Vector3(
                this.transform.localScale.x * this.textureScale.x,
                this.transform.localScale.y * this.textureScale.y,
                this.transform.localScale.z * this.textureScale.z);
        }

    }

}
