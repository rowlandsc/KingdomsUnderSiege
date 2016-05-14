using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerUpgradeManager : MonoBehaviour {
    
    
    public List<ProfileEffect> UpgradeStatEffects;
    public bool AlwaysUseSameUpgrade = false;

    public int MaxLevel {
        get {
            return UpgradeStatEffects.Count + 1;
        }
    }

    private ProfileSystem _profile;
    private Tower _tower;


    void Start() {
        _profile = GetComponent<ProfileSystem>();
        _tower = GetComponent<Tower>();
    }

    public bool CanLevelUp() {
        if (AlwaysUseSameUpgrade) return true;
        if (_profile.Level >= MaxLevel) return false;
        return true;
    }

    public bool LevelUp() {
        if (!CanLevelUp()) return false;

        if (AlwaysUseSameUpgrade) {
            _profile.AddEffect(UpgradeStatEffects[0]);
            _profile.Level++;
            _tower.LevelUp(_profile.Level);
        }
        else {
            _profile.AddEffect(UpgradeStatEffects[_profile.Level - 1]);
            _profile.Level++;
            _tower.LevelUp(_profile.Level);
        }
        return true;
    }
}
