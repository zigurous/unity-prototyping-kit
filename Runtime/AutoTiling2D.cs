using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Automatically tiles the material based on the scale of the object.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Auto Tiling 2D")]
    public sealed class AutoTiling2D : AutoTiling
    {
        /// <summary>
        /// An axis along which a material is tiled.
        /// </summary>
        public enum Axis
        {
            XZ,
            XY,
            ZY,
        }

        /// <summary>
        /// The axis along which the material is tiled.
        /// </summary>
        [Tooltip("The axis along which the material is tiled.")]
        public Axis axis;

        protected override void CreateMaterials()
        {
            this.materials = new Material[1] {
                new Material(this.sharedMaterial),
            };
        }

        protected override void SetTextureScale()
        {
            if (this.materials.Length < 1) {
                return;
            }

            Vector3 scale = GetTextureScale();

            switch (this.axis)
            {
                case Axis.XZ:
                    this.materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.z));
                    return;

                case Axis.XY:
                    this.materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.x, scale.y));
                    return;

                case Axis.ZY:
                    this.materials[0].SetTextureScale(this.texturePropertyName, new Vector2(scale.z, scale.y));
                    return;
            }
        }

    }

}
