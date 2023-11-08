using System;

public enum EModifications
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

    public float GetValue(EModifications pModification)
    {
        return pModification switch
        {
            EModifications.Damage => Damage,
            EModifications.Interval => Interval,
            EModifications.Uptime => Uptime,
            EModifications.Downtime => Downtime,
            _ => throw new NotImplementedException()
        };
    }
}