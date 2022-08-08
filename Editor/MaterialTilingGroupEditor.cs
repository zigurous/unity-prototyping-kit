using UnityEditor;
using UnityEngine;

namespace Zigurous.Prototyping.Editor
{
    [CustomEditor(typeof(MaterialTilingGroup))]
    public sealed class MaterialTilingGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoUpdate"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("updateInEditor"));

            if (GUILayout.Button("Force Update", GUILayout.ExpandWidth(false), GUILayout.Height(25f))) {
                ((MaterialTilingGroup)serializedObject.targetObject).ForceUpdate();
            }
        }

    }

}
