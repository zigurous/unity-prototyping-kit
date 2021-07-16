using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of a cube based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Prototyping Material Tiling (Cube)")]
    public sealed class MaterialTilingCube : MaterialTiling
    {
        /// <summary>
        /// The instance id of the shared material to detect when changes are
        /// made and new material copies can be created. This is required for
        /// updating in the editor.
        /// </summary>
        private int _sharedInstanceId = -1;

        /// <inheritdoc />
        protected override void UpdateMaterials()
        {
            if (this.renderer.materials != null)
            {
                Vector3 scale = this.transform.lossyScale;

                SetTextureScale(this.renderer.materials[0], new Vector2(scale.z, scale.y));
                SetTextureScale(this.renderer.materials[1], new Vector2(scale.x, scale.z));
                SetTextureScale(this.renderer.materials[2], new Vector2(scale.x, scale.y));
            }
        }

        /// <inheritdoc />
        protected override void UpdateMaterialsInEditor()
        {
            Material[] sharedMaterials = this.renderer.sharedMaterials;

            if (sharedMaterials != null)
            {
                Material sharedMaterial = this.renderer.sharedMaterial;

                if (sharedMaterial != null && _sharedInstanceId != sharedMaterial.GetInstanceID())
                {
                    _sharedInstanceId = sharedMaterial.GetInstanceID();

                    for (int i = 0; i < sharedMaterials.Length; i++) {
                        sharedMaterials[i] = new Material(sharedMaterial);
                    }

                    this.renderer.sharedMaterials = sharedMaterials;
                }

                Vector3 scale = this.transform.lossyScale;

                SetTextureScale(this.renderer.sharedMaterials[0], new Vector2(scale.z, scale.y));
                SetTextureScale(this.renderer.sharedMaterials[1], new Vector2(scale.x, scale.z));
                SetTextureScale(this.renderer.sharedMaterials[2], new Vector2(scale.x, scale.y));
            }
        }

    }

}
