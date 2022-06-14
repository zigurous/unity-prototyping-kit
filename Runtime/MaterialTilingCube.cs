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
        /// <summary>
        /// The instance id of the shared material to detect when changes are
        /// made and new material copies can be created. This is required for
        /// updating in the editor.
        /// </summary>
        private int sharedInstanceId = -1;

        /// <inheritdoc/>
        protected override void UpdateMaterials()
        {
            if (renderer.materials != null)
            {
                Vector3 scale = transform.lossyScale;

                SetTextureScale(renderer.materials[0], new Vector2(scale.z, scale.y));
                SetTextureScale(renderer.materials[1], new Vector2(scale.x, scale.z));
                SetTextureScale(renderer.materials[2], new Vector2(scale.x, scale.y));
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

                SetTextureScale(renderer.sharedMaterials[0], new Vector2(scale.z, scale.y));
                SetTextureScale(renderer.sharedMaterials[1], new Vector2(scale.x, scale.z));
                SetTextureScale(renderer.sharedMaterials[2], new Vector2(scale.x, scale.y));
            }
        }

    }

}
