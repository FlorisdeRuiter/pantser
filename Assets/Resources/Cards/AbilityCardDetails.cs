using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Card Details", menuName = "ScriptableObject/Card Details/Ability Card", order = 0)]
public class AbilityCardDetails : CardScriptableDetails
{
    public string AbilityName;

    public EAbilities AbilityType;

    public List<Modification> Modifications;

    public GameObject AbilityObject;
}
