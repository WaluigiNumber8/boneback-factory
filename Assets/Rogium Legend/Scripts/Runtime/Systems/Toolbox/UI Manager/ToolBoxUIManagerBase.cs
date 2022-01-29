using System;
using Rogium.Editors.Sprites;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public abstract class ToolBoxUIManagerBase : MonoBehaviour
    {
        [SerializeField] private Toggle buttonBrush;
        [SerializeField] private Toggle buttonEraser;
        [SerializeField] private Toggle buttonBucket;
        [SerializeField] private Toggle buttonPicker;

        private ToolType currentToolType;

        protected void SwitchTool(ToolType tool)
        {
            if (currentToolType == tool) return;
            switch(tool)
            {
                case ToolType.Brush:
                    buttonBrush.isOn = true;
                    break;
                case ToolType.Eraser:
                    buttonEraser.isOn = true;
                    break;
                case ToolType.Bucket:
                    buttonBucket.isOn = true;
                    break;
                case ToolType.ColorPicker:
                    buttonPicker.isOn = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tool), tool, null);
            }

            currentToolType = tool;
        }
    }
}