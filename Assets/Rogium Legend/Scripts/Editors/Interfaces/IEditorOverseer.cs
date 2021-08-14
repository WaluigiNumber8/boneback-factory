using System.Collections;
using UnityEngine;

namespace RogiumLegend.Editors
{
    public interface IEditorOverseer
    {
        /// <summary>
        /// Finish up editing current asset and send it to a higher overseer.
        /// </summary>
        public void CompleteEditing();
    }
}