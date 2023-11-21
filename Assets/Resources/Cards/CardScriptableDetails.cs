using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Details", menuName = "ScriptableObject/Card Details", order = 2)]
public class CardScriptableDetails : ScriptableObject
{
    public string CardName;
    public string CardInfo;

    public int CardStage;
    public int BaseCardStage = 0;

    public Texture CardSprite;

    public string AbilityName;

    public EAbilities AbilityType;

    public List<Modification> Modifications;

    public GameObject AbilityObject;

    private void OnEnable()
    {
        CardStage = BaseCardStage;
    }
}
