using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Set up inspector
        CompositeBehavior compositeBehavior = (CompositeBehavior)target;
        Rect r = EditorGUILayout.BeginHorizontal();
        r.height = EditorGUIUtility.singleLineHeight;

        // Check for null or empty behavior array
        if (compositeBehavior.behaviors == null || compositeBehavior.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviors in composite behavior.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            r = EditorGUILayout.BeginHorizontal();
            r.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {
            r.x = 30f;
            r.width = EditorGUIUtility.currentViewWidth - 95f;
            EditorGUI.LabelField(r, "Behaviors");
            r.x = EditorGUIUtility.currentViewWidth - 65f;
            r.width = 60;
            EditorGUI.LabelField(r, "Weights");
            r.y += EditorGUIUtility.singleLineHeight + 1.2f;

            for (int i = 0; i < compositeBehavior.behaviors.Length; i++)
            {
                r.x = 5f;
                r.width = 20f;
                EditorGUI.LabelField(r, (i + 1).ToString());
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                compositeBehavior.behaviors[i] = (FlockBehavior)EditorGUI.ObjectField(r, compositeBehavior.behaviors[i], typeof(FlockBehavior), false);
                r.x = EditorGUIUtility.currentViewWidth - 65f;
                r.width = 60;
                compositeBehavior.weights[i] = EditorGUI.FloatField(r, compositeBehavior.weights[i]);
                r.y += EditorGUIUtility.singleLineHeight * 1.1f;
            }

            EditorGUILayout.EndHorizontal();
            r.x = 5f;
            r.width = EditorGUIUtility.currentViewWidth - 10f;
            r.y += EditorGUIUtility.singleLineHeight * 0.5f;
            if (GUI.Button(r, "Add Behavior"))
            {
                AddBehavior(compositeBehavior);
            }

            r.y += EditorGUIUtility.singleLineHeight * 1.5f;
            if (compositeBehavior.behaviors != null && compositeBehavior.behaviors.Length > 0)
            {
                if (GUI.Button(r, "Remove Behavior"))
                {
                    RemoveBehavior(compositeBehavior);
                }
            }
        }


    }

    void AddBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors == null ? 0 : cb.behaviors.Length;
        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }

    void RemoveBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors.Length;
        if (oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }
        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];
        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
}
