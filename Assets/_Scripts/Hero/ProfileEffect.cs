using UnityEngine;
using System.Collections;

[System.Serializable]
public class ProfileEffect {

    public const float INSTANT = 0;
    public const float FOREVER = float.MaxValue;

    public float TimePeriodSeconds = INSTANT;

    public float HealthPointsChange = 0;
    public float MagicPointsChange = 0;
    public float MaxHealthPointsChange = 0;
    public float MaxMagicPointsChange = 0;

    public float MeleeDamageDealthChange = 0;
    public float SecondDamageDealtChange = 0;
    public float SuperDamageDealtChange = 0;

    public float HealthRegenChange = 1f;
    public float MagicRegenChange = 1f;


}
