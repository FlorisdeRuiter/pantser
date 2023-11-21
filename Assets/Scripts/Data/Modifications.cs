using System;

public enum EAbilityModifications
{
    Damage,
    Interval,
    Downtime,
    Uptime
}

[Serializable]
public class Modification
{
    public float Damage;
    public float Interval;
    public float Downtime;
    public float Uptime;

    public float GetValue(EAbilityModifications pModification)
    {
        return pModification switch
        {
            EAbilityModifications.Damage => Damage,
            EAbilityModifications.Interval => Interval,
            EAbilityModifications.Uptime => Uptime,
            EAbilityModifications.Downtime => Downtime,
            _ => throw new NotImplementedException()
        };
    }
}