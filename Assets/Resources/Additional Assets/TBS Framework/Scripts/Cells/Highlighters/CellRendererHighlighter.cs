using UnityEngine;

namespace TbsFramework.Cells.Highlighters
{
    public class CellRendererHighlighter : CellHighlighter
    {
        public Renderer Renderer;
        public Color Color;

        public override void Apply(Cell cell)
        {
            Renderer.material.color = Color;
        }
    }
}