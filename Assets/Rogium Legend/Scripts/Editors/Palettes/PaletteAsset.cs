using RogiumLegend.Editors.Core;
using System;
using UnityEngine;

namespace RogiumLegend.Editors.PaletteData
{
    public class PaletteAsset : IAsset
    {
        private string title;
        private Sprite icon;
        private string author;
        private DateTime creationDate;
        private Color[] colors;

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author  { get => author; }
        public DateTime CreationDate { get => creationDate; }
    }
}
