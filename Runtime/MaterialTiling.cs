using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of an object based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Material Tiling")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/MaterialTiling")]
    public class MaterialTiling : MaterialTilingBase
    {
        /// <inheritdoc/>
        protected override void UpdateMaterials()
        {
            Vector3 scale = Vector3.Scale(transform.lossyScale, Vector3.one);
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

                Vector3 scale = Vector3.Scale(transform.lossyScale, Vector3.one);
                SetTextureScale(renderer.sharedMaterial, new Vector2(scale.x, scale.z));
            }
        }

    }

}
