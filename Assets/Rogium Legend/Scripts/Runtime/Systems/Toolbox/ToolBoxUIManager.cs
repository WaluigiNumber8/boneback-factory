using System;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Controls the UI of the Toolbox system.
    /// </summary>
    public class ToolBoxUIManager : MonoBehaviour
    {
        public event Action<ToolType> OnSwitchTool;
        
        [SerializeField] private Toggle buttonSelection;
        [SerializeField] private Toggle buttonBrush;
        [SerializeField] private Toggle buttonEraser;
        [SerializeField] private Toggle buttonBucket;
        [SerializeField] private Toggle buttonPicker;

        private ToolType currentToolType;

        private void Start()
        {
            RoomEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool += SwitchTool;
            SpriteEditorOverseerMono.GetInstance().Toolbox.OnSwitchTool += SwitchTool;
        }

        private void SwitchTool(ToolType toolType)
        {
            if (currentToolType == toolType) return;
            switch(toolType)
            {
                case ToolType.Brush:
                    buttonBrush.isOn = true;
                    break;
                case ToolType.Eraser:
                    buttonEraser.isOn = true;
                    break;
                case ToolType.Fill:
                    buttonBucket.isOn = true;
                    break;
                case ToolType.ColorPicker:
                    buttonPicker.isOn = true;
                    break;
                case ToolType.Selection:
                    buttonSelection.isOn = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolType), toolType, null);
            }

            currentToolType = toolType;
            OnSwitchTool?.Invoke(currentToolType);
        }
    }
}