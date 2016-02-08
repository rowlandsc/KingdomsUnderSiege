using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[UnityEditor.CustomEditor(typeof(PrefabCache))]
public class PrefabCacheEditor : Editor {

    string newKey = "";

    public override void OnInspectorGUI() {
        PrefabCache cache = (PrefabCache)target;
        GUI.changed = false;

        List<string> keylist = cache.PrefabIndex.PrefabNames;
        //keylist.Sort();
        for (int i = 0; i < keylist.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            bool deleted = GUILayout.Button("Delete");
            if (deleted) {
                cache.PrefabIndex.Remove(keylist[i]);
                EditorUtility.SetDirty(target);
            }
            else {
                string currentKey = EditorGUILayout.TextField(keylist[i]);
                if (currentKey != keylist[i] && !cache.PrefabIndex.ContainsKey(currentKey)) {
                    GameObject currentSprite = cache.PrefabIndex[keylist[i]];
                    cache.PrefabIndex.Remove(keylist[i]);
                    cache.PrefabIndex.Add(currentKey, currentSprite);
                    EditorUtility.SetDirty(target);
                }
                //sgui.CurrentPortraits[i] = (Sprite)EditorGUILayout.ObjectField("Portrait " + i, sgui.CurrentPortraits[i], typeof(Sprite), false);

                cache.PrefabIndex[currentKey] = (GameObject)EditorGUILayout.ObjectField(cache.PrefabIndex[currentKey], typeof(GameObject), false);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        newKey = EditorGUILayout.TextField(newKey);
        bool addKey = GUILayout.Button("Add Key");
        if (addKey) {
            if (!cache.PrefabIndex.ContainsKey(newKey) && newKey.Length > 0) {
                cache.PrefabIndex.Add(newKey, null);
                newKey = "";
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
