using System.Collections;
using RedRats.Systems.GASCore;
using Rogium.Systems.Input;
using Rogium.UserInterface.Containers;
using Rogium.UserInterface.Core;
using UnityEngine;

namespace Rogium.Core
{
    /// <summary>
    /// Prepares the Game on Boot.
    /// </summary>
    public class MenuPreparator : MonoBehaviour
    {
        private IEnumerator Start()
        {
            CanvasOverseer canvasOverseer = CanvasOverseer.GetInstance();
            
            InputSystem.GetInstance().EnableUIMap();
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(false, canvasOverseer.NavigationBar.transform.GetChild(0).gameObject);
            GAS.ObjectSetActive(false, canvasOverseer.PickerWindow.transform.GetChild(0).gameObject);
            canvasOverseer.NavigationBar.Hide();
            canvasOverseer.SoundPickerWindow.Open();
            yield return null;
            canvasOverseer.SoundPickerWindow.Close();
        }

    }
}