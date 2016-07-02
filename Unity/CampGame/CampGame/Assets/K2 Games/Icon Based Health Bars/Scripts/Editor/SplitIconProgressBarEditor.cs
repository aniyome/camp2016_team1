using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(SplitIconProgressBar))]
public class SplitIconProgressBarEditor : IconProgressBarEditor
{
    SerializedProperty splitSpritePrefab;

    new void OnEnable()
    {
        splitSpritePrefab = serializedObject.FindProperty("splitSpritePrefab");

        base.OnEnable();
    }

    protected override void ShowMainImage()
    {
        EditorGUILayout.PropertyField(splitSpritePrefab, new GUIContent("Split Sprite Prefab"));
    }
}
