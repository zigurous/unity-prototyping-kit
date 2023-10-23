using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a renderer's material from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Material Selector")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/MaterialSelector")]
    public sealed class MaterialSelector : MaterialSelectorBase
    {
        /// <inheritdoc/>
        public override void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            m_Style = style.preset;
            m_Pattern = pattern.preset;

            MaterialSelectorRenderer renderer = GetComponentInChildren<MaterialSelectorRenderer>();

            if (renderer != null) {
                renderer.Apply(style, pattern);
            }
        }

    }

}
