﻿using UnityEngine;
using System.Collections;

public class PrefabCache : MonoBehaviour {

    public static PrefabCache Instance = null;
    void Awake() {
        if (Instance == null) {
            Instance = this;
			DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public StringPrefabMap PrefabIndex = new StringPrefabMap();

	
}
