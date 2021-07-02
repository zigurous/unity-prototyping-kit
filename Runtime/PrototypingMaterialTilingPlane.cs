using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material textures based on the object's scale.
    /// </summary>
    [ExecuteAlways]
    public sealed class PrototypingMaterialTilingPlane : PrototypingMaterialTiling
    {
        /// <summary>
        /// The instance id of the shared material to detect when changes are
        /// made and new material copies can be created. This is required for
        /// updating in the editor.
        /// </summary>
        private int _sharedInstanceId = -1;

        protected override void UpdateMaterials()
        {
            Vector3 scale = Vector3.Scale(this.transform.lossyScale, Vector3.one * 10.0f);
            UpdateMaterial(this.renderer.material, new Vector2(scale.x, scale.z));
        }

        protected override void UpdateMaterialsInEditor()
        {
            Material sharedMaterial = this.renderer.sharedMaterial;

            if (sharedMaterial != null)
            {
                if (_sharedInstanceId != sharedMaterial.GetInstanceID())
                {
                    this.renderer.sharedMaterial = new Material(sharedMaterial);
                    _sharedInstanceId = sharedMaterial.GetInstanceID();
                }

                Vector3 scale = Vector3.Scale(this.transform.lossyScale, Vector3.one * 10.0f);
                UpdateMaterial(this.renderer.sharedMaterial, new Vector2(scale.x, scale.z));
            }
        }

    }

}
