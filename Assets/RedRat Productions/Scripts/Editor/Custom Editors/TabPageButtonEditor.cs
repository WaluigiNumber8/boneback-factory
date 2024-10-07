using RedRats.UI.Tabs;
using UnityEditor;
using UnityEditor.UI;

namespace RedRats.Editor.CustomEditors.UI.Tabs
{
    [CustomEditor(typeof(TabPageButton))]
    public class TabPageButtonEditor : ButtonEditor
    {
        private SerializedProperty backgroundProperty;
        private SerializedProperty pageProperty;
        private SerializedProperty eventsProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            backgroundProperty = serializedObject.FindProperty("background");
            pageProperty = serializedObject.FindProperty("page");
            eventsProperty = serializedObject.FindProperty("events");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(backgroundProperty);
            EditorGUILayout.PropertyField(pageProperty);
            EditorGUILayout.PropertyField(eventsProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}