using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of a cube based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Material Tiling (Cube)")]
    public sealed class MaterialTilingCube : MaterialTiling
    {
        /// <inheritdoc/>
        protected override void UpdateMaterials()
        {
            if (renderer.materials != null)
            {
                Vector3 scale = transform.lossyScale;
                int length = renderer.materials.Length;

                if (length >= 1) {
                    SetTextureScale(renderer.materials[0], new Vector2(scale.z, scale.y));
                }

                if (length >= 2) {
                    SetTextureScale(renderer.materials[1], new Vector2(scale.x, scale.z));
                }

                if (length >= 3) {
                    SetTextureScale(renderer.materials[2], new Vector2(scale.x, scale.y));
                }
            }
        }

        /// <inheritdoc/>
        protected override void UpdateMaterialsInEditor()
        {
            Material[] sharedMaterials = renderer.sharedMaterials;

            if (sharedMaterials != null)
            {
                Material sharedMaterial = renderer.sharedMaterial;

                if (sharedMaterial != null && sharedInstanceId != sharedMaterial.GetInstanceID())
                {
                    sharedInstanceId = sharedMaterial.GetInstanceID();

                    for (int i = 0; i < sharedMaterials.Length; i++) {
                        sharedMaterials[i] = new Material(sharedMaterial);
                    }

                    renderer.sharedMaterials = sharedMaterials;
                }

                Vector3 scale = transform.lossyScale;

                if (sharedMaterials.Length >= 1) {
                    SetTextureScale(renderer.sharedMaterials[0], new Vector2(scale.z, scale.y));
                }

                if (sharedMaterials.Length >= 2) {
                    SetTextureScale(renderer.sharedMaterials[1], new Vector2(scale.x, scale.z));
                }

                if (sharedMaterials.Length >= 3) {
                    SetTextureScale(renderer.sharedMaterials[2], new Vector2(scale.x, scale.y));
                }
            }
        }

    }

}
