using UnityEngine;

namespace Zigurous.Prototyping
{
    /// <summary>
    /// Assigns a group of renderers' materials from a preset list of options.
    /// </summary>
    [AddComponentMenu("Zigurous/Prototyping/Prototyping Material Selector Group")]
    public sealed class MaterialSelectorGroup : MaterialSelector
    {
        /// <inheritdoc/>
        public override void Apply(MaterialStyle style, MaterialPattern pattern)
        {
            m_Style = style.preset;
            m_Pattern = pattern.preset;

            MaterialSelectorBase[] selectors = GetComponentsInChildren<MaterialSelectorBase>();

            for (int i = 0; i < selectors.Length; i++)
            {
                MaterialSelectorBase selector = selectors[i];

                if (selector != this) {
                    selector.Apply(style, pattern);
                }
            }
        }

    }

}
