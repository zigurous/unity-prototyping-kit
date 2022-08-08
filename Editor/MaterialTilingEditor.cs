using UnityEditor;
using UnityEngine;

namespace Zigurous.Prototyping.Editor
{
    public abstract class MaterialTilingEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleMultiplier"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoUpdate"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("updateInEditor"));

            if (GUILayout.Button("Force Update", GUILayout.ExpandWidth(false), GUILayout.Height(25f))) {
                ((MaterialTiling)serializedObject.targetObject).ForceUpdate();
            }
        }

    }

}
