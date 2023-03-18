using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSearchWindow : EditorWindow
{
    private string searchQuery = "";
    private List<GameObject> searchResults = new List<GameObject>();

    [MenuItem("Window/Object Search Window")]
    public static void ShowWindow()
    {
        GetWindow<ObjectSearchWindow>("Object Search Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Search", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        searchQuery = EditorGUILayout.TextField("Search Query", searchQuery);
        if (EditorGUI.EndChangeCheck())
        {
            SearchObjects();
        }

        GUILayout.Label("Results:", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(GUI.skin.box);

        foreach (GameObject obj in searchResults)
        {
            EditorGUILayout.BeginHorizontal();

            // オブジェクトフィールドをクリックしたときにオブジェクトを選択する
            EditorGUIUtility.labelWidth = 80;
            EditorGUIUtility.fieldWidth = 150;
            EditorGUI.BeginChangeCheck();
            GameObject selectedObject = EditorGUILayout.ObjectField("Object:", obj, typeof(GameObject), true) as GameObject;
            if (EditorGUI.EndChangeCheck() && selectedObject != null)
            {
                Selection.activeObject = selectedObject;
                EditorGUIUtility.PingObject(selectedObject);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }

    private void SearchObjects()
    {
        searchResults.Clear();

        Transform[] allTransforms = FindObjectsOfType<Transform>();
        List<GameObject> allObjects = new List<GameObject>();

        foreach (Transform t in allTransforms)
        {
            allObjects.Add(t.gameObject);
        }

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(searchQuery))
            {
                searchResults.Add(obj);
            }
        }
    }
}
