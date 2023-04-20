using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of a cube based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Material Tiling (Cube)")]
    public sealed class MaterialTilingCube : MaterialTilingBase
    {
        /// <inheritdoc/>
        protected override void UpdateMaterials()
        {
            Material[] materials = renderer.materials;

            if (materials == null) {
                return;
            }

            Vector3 scale = transform.lossyScale;
            int length = materials.Length;

            if (length >= 1) {
                SetTextureScale(materials[0], new Vector2(scale.z, scale.y));
            }

            if (length >= 2) {
                SetTextureScale(materials[1], new Vector2(scale.x, scale.z));
            }

            if (length >= 3) {
                SetTextureScale(materials[2], new Vector2(scale.x, scale.y));
            }

            renderer.materials = materials;
        }

        /// <inheritdoc/>
        protected override void UpdateMaterialsInEditor()
        {
            Material[] sharedMaterials = renderer.sharedMaterials;

            if (sharedMaterials == null) {
                return;
            }

            Material sharedMaterial = renderer.sharedMaterial;

            if (sharedMaterial != null && sharedInstanceId != sharedMaterial.GetInstanceID())
            {
                sharedInstanceId = sharedMaterial.GetInstanceID();

                for (int i = 0; i < sharedMaterials.Length; i++) {
                    sharedMaterials[i] = new Material(sharedMaterial);
                }
            }

            Vector3 scale = transform.lossyScale;

            if (sharedMaterials.Length >= 1) {
                SetTextureScale(sharedMaterials[0], new Vector2(scale.z, scale.y));
            }

            if (sharedMaterials.Length >= 2) {
                SetTextureScale(sharedMaterials[1], new Vector2(scale.x, scale.z));
            }

            if (sharedMaterials.Length >= 3) {
                SetTextureScale(sharedMaterials[2], new Vector2(scale.x, scale.y));
            }

            renderer.sharedMaterials = sharedMaterials;
        }

    }

}
