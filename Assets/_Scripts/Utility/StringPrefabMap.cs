using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("StringPrefabMap")]
public class StringPrefabMap  {
    [XmlArray("PrefabNames")]
    [XmlArrayItem("PrefabNames")]
    [SerializeField]
    public List<string> PrefabNames;

    [XmlArray("Prefabs")]
    [XmlArrayItem("Prefab")]
    [SerializeField]
    public List<GameObject> Prefabs;

    public StringPrefabMap() {
        PrefabNames = new List<string>();
        Prefabs = new List<GameObject>();
    }

    int Find(string key) {
        if (PrefabNames.Count == 0) return -1;

        return PrefabNames.BinarySearch(key);
    }

    public void Remove(string key) {
        int index = Find(key);
        if (index > -1) {
            PrefabNames.RemoveAt(index);
            Prefabs.RemoveAt(index);
        }
    }

    public void Remove(int index) {
        PrefabNames.RemoveAt(index);
        Prefabs.RemoveAt(index);
    }

    public bool ContainsKey(string key) {
        return (Find(key) > -1);
    }

    public GameObject Get(string key) {
        int index = Find(key);
        if (index > -1) {
            return Prefabs[index];
        }
        else {
            return null;
        }
    }

    int AddKey(string key) {
        int index = Find(key);

        if (index < 0) {
            PrefabNames.Insert(~index, key);
            return ~index;
        }
        return index;
    }

    public void Add(string key, GameObject value) {
        int index = AddKey(key);
        if (PrefabNames.Count > Prefabs.Count) {
            Prefabs.Insert(index, value);
        }
        else {
            Prefabs[index] = value;
        }
    }

    public GameObject this[string stat] {
        get { return Get(stat); }
        set { Add(stat, value); }
    }

    public void RemoveDuplicates() {
        List<string> foundNames = new List<string>();
        for (int i = PrefabNames.Count - 1; i >= 0; i--) {
            if (foundNames.Contains(PrefabNames[i])) {
                Remove(i);
            }
            else {
                foundNames.Add(PrefabNames[i]);
            }
        }
    }
}
