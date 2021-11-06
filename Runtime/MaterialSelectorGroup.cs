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
        private MaterialStyle.Preset m_Style = MaterialStyle.Preset.Black;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public MaterialStyle.Preset style
        {
            get => m_Style;
            set
            {
                m_Style = value;
                UpdateGroup();
            }
        }

        [SerializeField]
        [Tooltip("The selected pattern preset.")]
        private MaterialPattern.Preset m_Pattern = MaterialPattern.Preset.Pattern1;

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public MaterialPattern.Preset pattern
        {
            get => m_Pattern;
            set
            {
                m_Pattern = value;
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
            if (enabled) {
                UpdateGroup();
            }
        }

        private void UpdateGroup()
        {
            MaterialSelector[] selectors = GetComponentsInChildren<MaterialSelector>();

            for (int i = 0; i < selectors.Length; i++)
            {
                MaterialSelector selector = selectors[i];
                selector.styles = styles;
                selector.patterns = patterns;
                selectors[i].Apply(style, pattern);
            }
        }

    }

}
