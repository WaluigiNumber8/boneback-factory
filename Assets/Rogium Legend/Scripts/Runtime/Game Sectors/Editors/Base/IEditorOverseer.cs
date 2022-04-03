using System;
using System.Collections;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Editors
{
    public interface IEditorOverseer
    {
        /// <summary>
        /// Finish up editing current asset and send it to a higher overseer.
        /// </summary>
        public void CompleteEditing();
    }
}