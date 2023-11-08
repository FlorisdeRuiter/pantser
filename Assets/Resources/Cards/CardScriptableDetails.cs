using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Details", menuName = "ScriptableObject/Card Details", order = 1)]
public class CardScriptableDetails : ScriptableObject
{
    public string cardName;
    public string cardInfo;

    public int cardStage;
    public int baseCardStage = 0;

    public Texture cardSprite;

    public string abilityName;

    public EAbilities abilityType;

    public List<Modification> Modifications;

    public GameObject abilityObject;

    private void OnEnable()
    {
        cardStage = baseCardStage;
    }
}
