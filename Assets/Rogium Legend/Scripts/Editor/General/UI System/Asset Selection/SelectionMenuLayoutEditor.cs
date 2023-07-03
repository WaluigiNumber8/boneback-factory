using UnityEditor;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    [CustomEditor(typeof(SelectionMenuLayout))]
    public class SelectionMenuLayoutEditor : UnityEditor.Editor
    {
        private SerializedProperty menu;
        private SerializedProperty content;
        private IconPositionType iconPosType;
        private int iconPosTypeIndex;
        private SerializedProperty iconPosition;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawComponents();

            serializedObject.ApplyModifiedProperties();
            
        }

        private void DrawComponents()
        {
            EditorGUILayout.ObjectField(menu, new GUIContent("Menu"));
            EditorGUILayout.ObjectField(content, new GUIContent("Content"));
            iconPosType = (IconPositionType)EditorGUILayout.EnumPopup("Icon Postion Type", iconPosType);
            iconPosTypeIndex = (int)iconPosType;

            if (iconPosType != IconPositionType.Global) return;
            EditorGUILayout.ObjectField(iconPosition, new GUIContent("Icon Position Image"));
        }

        private void OnEnable()
        {
            menu = serializedObject.FindProperty("menu");
            content = serializedObject.FindProperty("content");
            iconPosTypeIndex = serializedObject.FindProperty("iconPositionType").intValue;
            iconPosition = serializedObject.FindProperty("iconPosition");
        }
    }
}