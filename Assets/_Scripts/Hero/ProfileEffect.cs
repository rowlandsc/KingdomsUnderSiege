using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProfileEffectList : SyncListStruct<ProfileEffect> {

}

[System.Serializable]
public struct ProfileEffect {

    public const float INSTANT = -1;
    public const float FOREVER = float.MaxValue;

    public float StartingDuration;
    public float RemainingDuration;

    public float HealthPointsAdd;
    public float MagicPointsAdd;
    public float MaxHealthPointsMult;
    public float MaxHealthPointsAdd;
    public float MaxMagicPointsMult;
    public float MaxMagicPointsAdd;

    public float MeleeDamageDealtMult;
    public float MeleeDamageDealtAdd;
    public float SecondDamageDealtMult;
    public float SecondDamageDealtAdd;
    public float SuperDamageDealtMult;
    public float SuperDamageDealtAdd;

    public float HealthRegenMult;
    public float HealthRegenAdd;
    public float MagicRegenMult;
    public float MagicRegenAdd;

    public float MoveSpeedMult;
    public float MoveSpeedAdd;

    public ProfileEffect(float startingDuration = INSTANT, float healthPointsAdd = 0, float magicPointsAdd = 0, 
        float maxHealthPointsMult = 1, float maxHealthPointsAdd = 0, float maxMagicPointsMult = 1, float maxMagicPointsAdd = 0,
        float meleeDamageMult = 1, float meleeDamageAdd = 0, float secondDamageMult = 1, float secondDamageAdd = 0,
        float superDamageMult = 1, float superDamageAdd = 0, float healthRegenMult = 1, float healthRegenAdd = 0,
        float magicRegenMult = 1, float magicRegenAdd = 0, float moveSpeedMult = 0, float moveSpeedAdd = 0) {

        StartingDuration = startingDuration;
        RemainingDuration = startingDuration;

        HealthPointsAdd = healthPointsAdd;
        MagicPointsAdd = magicPointsAdd;
        MaxHealthPointsMult = maxHealthPointsMult;
        MaxHealthPointsAdd = maxHealthPointsAdd;
        MaxMagicPointsMult = maxMagicPointsMult;
        MaxMagicPointsAdd = maxMagicPointsAdd;

        MeleeDamageDealtMult = meleeDamageMult;
        MeleeDamageDealtAdd = meleeDamageAdd;
        SecondDamageDealtMult = secondDamageMult;
        SecondDamageDealtAdd = secondDamageAdd;
        SuperDamageDealtMult = superDamageMult;
        SuperDamageDealtAdd = superDamageAdd;

        HealthRegenMult = healthRegenMult;
        HealthRegenAdd = healthRegenAdd;
        MagicRegenMult = magicRegenMult;
        MagicRegenAdd = magicRegenAdd;

        MoveSpeedMult = moveSpeedMult;
        MoveSpeedAdd = moveSpeedAdd;
    }
    public ProfileEffect(ProfileEffect original) {
        StartingDuration = original.StartingDuration;
        RemainingDuration = original.RemainingDuration;

        HealthPointsAdd = original.HealthPointsAdd;
        MagicPointsAdd = original.MagicPointsAdd;
        MaxHealthPointsMult = original.MaxHealthPointsMult;
        MaxHealthPointsAdd = original.MaxHealthPointsAdd;
        MaxMagicPointsMult = original.MaxMagicPointsMult;
        MaxMagicPointsAdd = original.MaxMagicPointsAdd;

        MeleeDamageDealtMult = original.MeleeDamageDealtMult;
        MeleeDamageDealtAdd = original.MeleeDamageDealtAdd;
        SecondDamageDealtMult = original.SecondDamageDealtMult;
        SecondDamageDealtAdd = original.SecondDamageDealtAdd;
        SuperDamageDealtMult = original.SuperDamageDealtMult;
        SuperDamageDealtAdd = original.SuperDamageDealtAdd;

        HealthRegenMult = original.HealthRegenMult;
        HealthRegenAdd = original.HealthRegenAdd;
        MagicRegenMult = original.MagicRegenMult;
        MagicRegenAdd = original.MagicRegenAdd;

        MoveSpeedMult = original.MoveSpeedMult;
        MoveSpeedAdd = original.MoveSpeedAdd;
    }
}
