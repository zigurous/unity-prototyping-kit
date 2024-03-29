﻿using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the materials of a cube based on its scale.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/MaterialTilingCube")]
    internal sealed class MaterialTilingCube : MaterialTilingBase
    {
        /// <inheritdoc/>
        protected override void UpdateMaterials()
        {
            Material[] materials = renderer.materials;

            if (materials == null) {
                return;
            }

            Vector3 scale = transform.lossyScale;

            SetTextureScale(materials[0], new Vector2(scale.z, scale.y));
            SetTextureScale(materials[1], new Vector2(scale.x, scale.z));
            SetTextureScale(materials[2], new Vector2(scale.x, scale.y));

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

            SetTextureScale(sharedMaterials[0], new Vector2(scale.z, scale.y));
            SetTextureScale(sharedMaterials[1], new Vector2(scale.x, scale.z));
            SetTextureScale(sharedMaterials[2], new Vector2(scale.x, scale.y));

            renderer.sharedMaterials = sharedMaterials;
        }

    }

}
