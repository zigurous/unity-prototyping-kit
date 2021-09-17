using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a group of renderers' materials from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Prototyping Material Selector Group")]
    public sealed class MaterialSelectorGroup : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The selected style preset.")]
        private MaterialStylePreset _style = MaterialStylePreset.Black;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public MaterialStylePreset style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateGroup();
            }
        }

        [SerializeField]
        [Tooltip("The selected pattern preset.")]
        private MaterialPatternPreset _pattern = MaterialPatternPreset.Pattern1;

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public MaterialPatternPreset pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                UpdateGroup();
            }
        }

        /// <summary>
        /// The palette of available styles.
        /// </summary>
        [Tooltip("The palette of available styles.")]
        public MaterialStylePalette styles;

        /// <summary>
        /// The palette of available patterns.
        /// </summary>
        [Tooltip("The palette of available patterns.")]
        public MaterialPatternPalette patterns;

        private void OnEnable()
        {
            UpdateGroup();
        }

        private void OnValidate()
        {
            if (this.enabled) {
                UpdateGroup();
            }
        }

        private void UpdateGroup()
        {
            MaterialSelector[] selectors = GetComponentsInChildren<MaterialSelector>();

            for (int i = 0; i < selectors.Length; i++) {
                selectors[i].Apply(this.style, this.pattern);
            }
        }

    }

}
