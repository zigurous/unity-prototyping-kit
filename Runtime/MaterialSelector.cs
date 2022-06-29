using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a renderer's material from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Material Selector")]
    public class MaterialSelector : MaterialSelectorBase
    {
        [SerializeField]
        [Tooltip("The selected style preset.")]
        protected MaterialStyle.Preset m_Style = MaterialStyle.Preset.Black;

        [SerializeField]
        [Tooltip("The selected pattern preset.")]
        protected MaterialPattern.Preset m_Pattern = MaterialPattern.Preset.Pattern1;

        [SerializeField]
        [Tooltip("The palette of available styles.")]
        protected MaterialStylePalette m_Styles;

        [SerializeField]
        [Tooltip("The palette of available patterns.")]
        protected MaterialPatternPalette m_Patterns;

        /// <summary>
        /// The selected style preset.
        /// </summary>
        public MaterialStyle.Preset style
        {
            get => m_Style;
            set => Apply(value, m_Pattern);
        }

        /// <summary>
        /// The palette of available styles.
        /// </summary>
        public MaterialStylePalette styles
        {
            get => m_Styles;
            set { m_Styles = value; Apply(); }
        }

        /// <summary>
        /// The selected pattern preset.
        /// </summary>
        public MaterialPattern.Preset pattern
        {
            get => m_Pattern;
            set => Apply(m_Style, value);
        }

        /// <summary>
        /// The palette of available patterns.
        /// </summary>
        public MaterialPatternPalette patterns
        {
            get => m_Patterns;
            set { m_Patterns = value; Apply(); }
        }

        /// <inheritdoc/>
        public override void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            m_Style = style.preset;
            m_Pattern = pattern.preset;

            base.Apply(style, pattern);
        }

        /// <summary>
        /// Applies the preset style and pattern to the object.
        /// </summary>
        /// <param name="stylePreset">The style preset to apply.</param>
        /// <param name="patternPreset">The pattern preset to apply.</param>
        public void Apply(MaterialStyle.Preset stylePreset, MaterialPattern.Preset patternPreset)
        {
            MaterialStyle style = styles != null ? styles.GetStyle(stylePreset) : new MaterialStyle(stylePreset);
            MaterialPattern pattern = patterns != null ? patterns.GetPattern(patternPreset) : new MaterialPattern(patternPreset);

            Apply(style, pattern);
        }

        /// <summary>
        /// Applies the current style and pattern to the object.
        /// </summary>
        public void Apply()
        {
            Apply(m_Style, m_Pattern);
        }

        private void OnEnable()
        {
            Apply();
        }

        private void OnValidate()
        {
            if (enabled) {
                Apply();
            }
        }

    }

}
