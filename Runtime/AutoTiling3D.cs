using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material based on the scale of the object.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Auto Tiling 3D")]
    public sealed class AutoTiling3D : AutoTiling
    {
        protected override void CreateMaterials()
        {
            this.materials = new Material[3] {
                new Material(this.sharedMaterial),
                new Material(this.sharedMaterial),
                new Material(this.sharedMaterial),
            };
        }

        protected override void SetTextureScale()
        {
            if (this.materials.Length < 3) {
                return;
            }

            Vector3 scale = GetTextureScale();

            this.materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.z, scale.y));
            this.materials[1].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.z));
            this.materials[2].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.y));
        }

    }

}
