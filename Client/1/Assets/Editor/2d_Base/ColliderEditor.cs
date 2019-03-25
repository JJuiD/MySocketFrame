using Scripts.Logic._2D_Base;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxColliderBase))]
public class BoxColliderBase : Editor
{
    BoxColliderBase collider;
    private void OnEnable()
    {
        collider = (BoxColliderBase)target;
    }
    public override void OnInspectorGUI()
    {
        //EditorGUILayout.BeginVertical();
        //EditorGUILayout.LabelField("offset");
        ////以水平方向绘制
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("x", GUILayout.MaxWidth(50));
        //collider.offset.x = EditorGUILayout.FloatField(collider.offset.x);
        //EditorGUILayout.LabelField("y", GUILayout.MaxWidth(50));
        //collider.offset.y = EditorGUILayout.FloatField(collider.offset.y);
        //EditorGUILayout.LabelField("z", GUILayout.MaxWidth(50));
        //collider.offset.z = EditorGUILayout.FloatField(collider.offset.z);
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.LabelField("size");
        ////以水平方向绘制
        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("x", GUILayout.MaxWidth(50));
        //collider.size.x = EditorGUILayout.FloatField(collider.size.x);
        //EditorGUILayout.LabelField("y", GUILayout.MaxWidth(50));
        //collider.size.y = EditorGUILayout.FloatField(collider.size.y);
        //EditorGUILayout.LabelField("z", GUILayout.MaxWidth(50));
        //collider.size.z = EditorGUILayout.FloatField(collider.size.z);
        //EditorGUILayout.EndHorizontal();
        //EditorGUILayout.EndVertical();
    }
}
