using UnityEditor;
using RogiumLegend.Global.MenuSystem.UI;
using UnityEngine;

namespace RogiumLegend.Global.MenuSystem.Interactables
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
            if ((actionType == ButtonType.EditPack) ||
               (actionType == ButtonType.DeletePack))
            {
            number = EditorGUILayout.IntField("Number", number);
            }
        }
    }
}