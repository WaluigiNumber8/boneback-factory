using UnityEditor;
using Rogium.Global.UISystem.UI;
using UnityEngine;

namespace Rogium.Global.UISystem.Interactables
{
    [CustomEditor(typeof(InteractableButton))]
    public class InteractiveButtonEditor : Editor
    {
        private SerializedProperty action;
        private ButtonType actionType;
        private int number;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            actionType = (ButtonType)action.enumValueIndex;
            EditorGUILayout.PropertyField(action);
            ShowNumberField();

            serializedObject.ApplyModifiedProperties();

        }

        private void OnEnable()
        {
            action = serializedObject.FindProperty("action");
            number = serializedObject.FindProperty("number").intValue;
        }

        /// <summary>
        /// Show <see cref="AssetCardController"/> for ID Input
        /// </summary>
        private void ShowNumberField()
        {
            if ((actionType == ButtonType.SelectionOpenAssetType) || 
               (actionType == ButtonType.EditorOpenPalette) ||
               (actionType == ButtonType.EditorOpenSprite) ||
               (actionType == ButtonType.EditorOpenWeapon) ||
               (actionType == ButtonType.EditorOpenProjectile) ||
               (actionType == ButtonType.EditorOpenEnemy) ||
               (actionType == ButtonType.EditorOpenRoom) ||
               (actionType == ButtonType.EditorOpenTile) ||
               (actionType == ButtonType.EditPackProperties) ||
               (actionType == ButtonType.EditorOpenPalette) ||
               (actionType == ButtonType.EditSpriteProperties) ||
               (actionType == ButtonType.EditWeaponProperties) ||
               (actionType == ButtonType.EditEnemyProperties) ||
               (actionType == ButtonType.EditRoomProperties) ||
               (actionType == ButtonType.EditTileProperties) ||
               (actionType == ButtonType.EditProjectileProperties) ||
               (actionType == ButtonType.DeletePalette) ||
               (actionType == ButtonType.DeleteSprite) ||
               (actionType == ButtonType.DeleteWeapon) ||
               (actionType == ButtonType.DeleteEnemy) ||
               (actionType == ButtonType.DeleteRoom) ||
               (actionType == ButtonType.DeleteTile) ||
               (actionType == ButtonType.DeleteProjectile) ||
               (actionType == ButtonType.DeletePack))
            {
                number = EditorGUILayout.IntField("Number", number);
            }
        }
    }
}