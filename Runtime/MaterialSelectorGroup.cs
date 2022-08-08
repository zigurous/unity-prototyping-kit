using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a group of renderers' materials from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Material Selector Group")]
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
