using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    private CardScriptableDetails _cardType;

    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardInfo;
    [SerializeField] private RawImage _cardSprite;

    private CardManager _cardManager;

    private void Awake()
    {
        _cardManager = CardManager.GetInstance();
    }

    public void SetCardDetails(CardScriptableDetails details)
    {
        _cardType = details;
        _cardName.text = details.CardName;
        _cardInfo.text = details.CardInfo;
        _cardSprite.texture = details.CardSprite;  
    }

    public void OnPickCard()
    {
        GameObject abilityObject = new GameObject();

        if (_cardType.CardStage == 0)
        {
            // Adds a new ability if the picked ability wasn't active yet
            AddNewAbility();
        }
        else
        {
            // Upgrades a abilities stats if it was already active and player picked it again
            abilityObject = GameObject.Find(_cardType.AbilityName);
            abilityObject.GetComponent<IAbility>().SetConfig(_cardType.Modifications[_cardType.CardStage]);
        }

        _cardType.CardStage += 1;
        Time.timeScale = 1;
        _cardManager.Removecards();
    }

    private void AddNewAbility()
    {
        // Finds a parent object for the new ability
        GameObject abilityObject;
        GameObject abilityParent = GameObject.Find(_cardType.AbilityType.ToString());

        if (abilityParent != null)
        {
            // Creates a parent object if none are available
            Player player = FindObjectOfType<Player>();
            abilityParent = new GameObject(_cardType.AbilityType.ToString());

            // Adds the parent object to the player
            abilityParent.transform.parent = player.transform;
            abilityParent.transform.localPosition = Vector3.zero;
            abilityParent.transform.rotation = player.transform.rotation;
        }

        // Adds the ability object to the parent object
        abilityObject = Instantiate(_cardType.AbilityObject, abilityParent.transform.position, Quaternion.identity);
        abilityObject.transform.parent = abilityParent.transform;
        abilityObject.name = _cardType.AbilityName;
    }
}
