using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[UnityEditor.CustomEditor(typeof(PrefabCache))]
public class PrefabCacheEditor : Editor {

    string newKey = "";

    public override void OnInspectorGUI() {
        PrefabCache cache = (PrefabCache)target;

        StringPrefabMap editedPrefabMap = new StringPrefabMap(cache.PrefabIndex);
        EditorGUI.BeginChangeCheck();
        string change = "";
        List<string> keylist = editedPrefabMap.PrefabNames;
        //keylist.Sort();
        for (int i = 0; i < keylist.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            bool deleted = GUILayout.Button("Delete");
            if (deleted) {
                change = "Removed key '" + keylist[i] + "'";
                editedPrefabMap.Remove(keylist[i]);
                GUI.changed = true;
            }
            else {
                string currentKey = keylist[i];
                EditorGUILayout.LabelField(currentKey);
                if (currentKey != keylist[i] && !editedPrefabMap.ContainsKey(currentKey)) {
                    GameObject currentPrefab = editedPrefabMap[keylist[i]];
                    editedPrefabMap.Remove(keylist[i]);
                    editedPrefabMap.Add(currentKey, currentPrefab);
                    change = "Renamed key";
                    GUI.changed = true;
                }
                editedPrefabMap[currentKey] = (GameObject)EditorGUILayout.ObjectField(editedPrefabMap[currentKey], typeof(GameObject), false);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        newKey = EditorGUILayout.TextField(newKey);
        bool addKey = GUILayout.Button("Add Key");
        if (addKey) {
            if (!editedPrefabMap.ContainsKey(newKey) && newKey.Length > 0) {
                editedPrefabMap.Add(newKey, null);
                change = "Added key '" + newKey + "'";
                newKey = "";
                GUI.changed = true;
            }
        }
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(target, change);
            (target as PrefabCache).PrefabIndex = editedPrefabMap;
        }
    }
}
