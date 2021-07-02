using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material textures based on the object's scale.
    /// </summary>
    [ExecuteAlways]
    public sealed class PrototypingMaterialTilingCube : PrototypingMaterialTiling
    {
        /// <summary>
        /// The instance id of the shared material to detect when changes are
        /// made and new material copies can be created. This is required for
        /// updating in the editor.
        /// </summary>
        private int _sharedInstanceId = -1;

        protected override void UpdateMaterials()
        {
            if (this.renderer.materials != null)
            {
                Vector3 scale = this.transform.lossyScale;

                UpdateMaterial(this.renderer.materials[0], new Vector2(scale.z, scale.y));
                UpdateMaterial(this.renderer.materials[1], new Vector2(scale.x, scale.z));
                UpdateMaterial(this.renderer.materials[2], new Vector2(scale.x, scale.y));
            }
        }

        #if UNITY_EDITOR
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

                UpdateMaterial(this.renderer.sharedMaterials[0], new Vector2(scale.z, scale.y));
                UpdateMaterial(this.renderer.sharedMaterials[1], new Vector2(scale.x, scale.z));
                UpdateMaterial(this.renderer.sharedMaterials[2], new Vector2(scale.x, scale.y));
            }
        }
        #endif

    }

}
