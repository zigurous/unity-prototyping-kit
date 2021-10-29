using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material of a sphere based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Prototyping Material Tiling (Sphere)")]
    public sealed class MaterialTilingSphere : MaterialTiling
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
            Vector3 scale = Vector3.Scale(transform.lossyScale, Vector3.one * 4f);
            SetTextureScale(renderer.material, new Vector2(scale.x, scale.z));
        }

        /// <inheritdoc/>
        protected override void UpdateMaterialsInEditor()
        {
            Material sharedMaterial = renderer.sharedMaterial;

            if (sharedMaterial != null)
            {
                if (sharedInstanceId != sharedMaterial.GetInstanceID())
                {
                    renderer.sharedMaterial = new Material(sharedMaterial);
                    sharedInstanceId = sharedMaterial.GetInstanceID();
                }

                Vector3 scale = Vector3.Scale(transform.lossyScale, Vector3.one * 4f);
                SetTextureScale(renderer.sharedMaterial, new Vector2(scale.x, scale.z));
            }
        }

    }

}
