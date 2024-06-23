using System;
using System.Collections;
using RedRats.Core;
using RedRats.Systems.ObjectSwitching;
using Rogium.Gameplay.Core;
using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rogium.Gameplay.Inventory
{
    /// <summary>
    /// Represents the Weapon Select Menu where the player decides into which slot the newly picked up weapon goes.
    /// </summary>
    public class WeaponSelectMenu : MonoSingleton<WeaponSelectMenu>
    {
        public event Action OnOpen;
        public event Action OnClose;
        
        [SerializeField] private Transform snapTransform;
        [SerializeField] private Vector2 positionOffset;
        [SerializeField] private ObjectSwitcherMono layoutSwitcher;
        [SerializeField, Range(0f, 4f)] private float inputStartDelay = 1;
        [SerializeField, Range(0f, 4f)] private float hideDelay = 0;
        [SerializeField] private UIInfo ui;

        [ButtonGroup, Button("Show", ButtonSizes.Medium)]
        public void TestShow() => Show();
        [ButtonGroup, Button("Hide", ButtonSizes.Medium)]
        public void TestHide() => Hide();
        
        private Action<int> whenSelectedWeapon;
        private RectTransform ttransform;
        private RectTransform canvasTransform;
        private InputSystem inputSystem;
        private Camera cam;

        private void Start()
        {
            ttransform = GetComponent<RectTransform>();
            canvasTransform = ttransform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            cam = Camera.main;
            inputSystem = InputSystem.GetInstance();
            
            ui.ui.SetActive(false);
            Hide();
        }

        /// <summary>
        /// Opens the Weapon Select Menu with the normal layout..
        /// </summary>
        /// <param name="whenSelectedWeapon">The method to run when a slot is selected.</param>
        public void OpenForNormal(Action<int> whenSelectedWeapon)
        {
            this.whenSelectedWeapon = whenSelectedWeapon;
            layoutSwitcher.Switch(ui.normalLayout);
            ui.firstSelectedNormal.Select();
            Show();
        }
        
        /// <summary>
        /// Opens the Weapon Select Menu with the Dash layout.
        /// </summary>
        /// <param name="selectedMethod">The method to run when a slot is selected.</param>
        public void OpenForDash(Action<int> selectedMethod)
        {
            whenSelectedWeapon = selectedMethod;
            layoutSwitcher.Switch(ui.dashLayout);
            ui.firstSelectedDash.Select();
            Show();
        }

        /// <summary>
        /// Select a slot in the menu.
        /// </summary>
        /// <param name="index">The index pf the slot to select.</param>
        public void Select(int index)
        {
            whenSelectedWeapon.Invoke(index);
            Hide();
        }
        
        public void RefreshSlotIcons(Sprite main, Sprite sub, Sprite mainAlt, Sprite subAlt, Sprite dash, Sprite dashAlt)
        {
            UpdateIconImage(ui.slotIcons.mainIconImage, main);
            UpdateIconImage(ui.slotIcons.subIconImage, sub);
            UpdateIconImage(ui.slotIcons.mainAltIconImage, mainAlt);
            UpdateIconImage(ui.slotIcons.subAltIconImage, subAlt);
            UpdateIconImage(ui.slotIcons.dashIconImage, dash);
            UpdateIconImage(ui.slotIcons.dashAltIconImage, dashAlt);
        }
        
        private void Show()
        {
            SnapToTarget();
            ui.ui.SetActive(true);
            GameplayOverseerMono.GetInstance().Pause();
            inputSystem.DisableInput(this, inputStartDelay);
            
            OnOpen?.Invoke();
        }

        private void Hide()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                OnClose?.Invoke();
                GameplayOverseerMono.GetInstance().Resume();
                inputSystem.DisableInput(this, hideDelay);
                
                yield return new WaitForSecondsRealtime(hideDelay);
                
                ui.ui.SetActive(false);
            }
        }

        private void SnapToTarget()
        {
            Vector2 targetPosition = cam.WorldToScreenPoint(snapTransform.position + (Vector3) positionOffset);
            ttransform.localPosition = RedRatUtils.MoveToPositionWithinCanvas(ttransform, targetPosition, canvasTransform, cam);
        }
        
        private void UpdateIconImage(Image image, Sprite sprite) => image.sprite = (sprite == null) ? ui.slotIcons.emptyIcon : sprite;

        [Serializable]
        private struct UIInfo
        {
            public GameObject ui;
            public GameObject normalLayout;
            public Selectable firstSelectedNormal;
            public GameObject dashLayout;
            public Selectable firstSelectedDash;
            public UISlotIconInfo slotIcons;
        }

        [Serializable]
        public struct UISlotIconInfo
        {
            public Sprite emptyIcon;
            [FormerlySerializedAs("mainIcon")]public Image mainIconImage;
            [FormerlySerializedAs("subIcon")]public Image subIconImage;
            [FormerlySerializedAs("mainAltIcon")]public Image mainAltIconImage;
            [FormerlySerializedAs("subAltIcon")]public Image subAltIconImage;
            [FormerlySerializedAs("dashIcon")]public Image dashIconImage;
            [FormerlySerializedAs("dashAltIcon")]public Image dashAltIconImage;
        }
    }
}