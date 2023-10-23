using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a group of renderers' materials from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Material Selector Group")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.prototyping/api/Zigurous.Prototyping/MaterialSelectorGroup")]
    public sealed class MaterialSelectorGroup : MaterialSelectorBase
    {
        /// <inheritdoc/>
        public override void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            m_Style = style.preset;
            m_Pattern = pattern.preset;

            MaterialSelectorRenderer[] renderers = GetComponentsInChildren<MaterialSelectorRenderer>();

            for (int i = 0; i < renderers.Length; i++) {
                renderers[i].Apply(style, pattern);
            }
        }

    }

}
