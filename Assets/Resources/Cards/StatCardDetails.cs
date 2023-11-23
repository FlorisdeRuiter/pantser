using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Card Details", menuName = "ScriptableObject/Card Details/Stat Card", order = 1)]
public class StatCardDetails : CardScriptableDetails
{
    public EStatType StatType;

    [Range(0, 100)]
    public float StatModifier;

    public bool isPercentage = true;
}
