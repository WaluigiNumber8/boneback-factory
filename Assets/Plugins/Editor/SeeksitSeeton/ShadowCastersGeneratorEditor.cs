using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapShadowCaster2D))]
public class ShadowCastersGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TilemapShadowCaster2D generator = (TilemapShadowCaster2D)target;
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Destroy All Children"))
        {
            generator.DestroyAllChildren();
        }
    }

}