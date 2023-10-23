using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Snaps an object's position, rotation, and scale to grid increments.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Prototyping/Grid Alignment")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/GridAlignment")]
    public sealed class GridAlignment : MonoBehaviour
    {
        /// <summary>
        /// The base unit scale of the grid.
        /// </summary>
        [Tooltip("The base unit scale of the grid.")]
        public float unitSize = 1f;

        /// <summary>
        /// The amount of units the position is rounded to on the grid.
        /// </summary>
        [Tooltip("The amount of units the position is rounded to on the grid.")]
        public float positionIncrement = 0.25f;

        /// <summary>
        /// The amount of units the scale is rounded to on the grid.
        /// </summary>
        [Tooltip("The amount of units the scale is rounded to on the grid.")]
        public float scaleIncrement = 0.25f;

        /// <summary>
        /// The amount of units the rotation is rounded to on the grid.
        /// </summary>
        [Tooltip("The amount of units the rotation is rounded to on the grid.")]
        public float rotationIncrement = 15f;

        private void OnEnable()
        {
            AlignToGrid();
        }

        private void Update()
        {
            if (transform.hasChanged) {
                AlignToGrid();
            }
        }

        /// <summary>
        /// Updates the transform of the object to align to the grid.
        /// </summary>
        public void AlignToGrid()
        {
            transform.localPosition = Snap(transform.localPosition, positionIncrement);
            transform.localScale = Snap(transform.localScale, scaleIncrement);
            transform.localEulerAngles = Snap(transform.localEulerAngles, rotationIncrement);
            transform.hasChanged = false;
        }

        /// <summary>
        /// Snaps a vector to the grid at a given increment.
        /// </summary>
        /// <param name="vector">The vector to snap.</param>
        /// <param name="increment">The amount of units the vector is rounded to.</param>
        /// <returns>The vector snapped to the grid.</returns>
        public Vector3 Snap(Vector3 vector, float increment)
        {
            if (increment == 0f) {
                return vector;
            }

            float factor = increment * unitSize;

            vector.x = Mathf.Round(vector.x / factor) * factor;
            vector.y = Mathf.Round(vector.y / factor) * factor;
            vector.z = Mathf.Round(vector.z / factor) * factor;

            return vector;
        }

    }

}
