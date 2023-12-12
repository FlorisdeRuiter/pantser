using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private CardScriptableDetails _cardType;

    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardInfo;
    [SerializeField] private RawImage _cardSprite;

    private CardManager _cardManager;

    private void Awake()
    {
        _cardManager = GameManager.GetInstance().CardManager;
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
        if (_cardType is AbilityCardDetails abilityCard)
        {
            GameObject abilityObject;

            if (abilityCard.CardStage == 0)
            {
                // Adds a new ability if the picked ability wasn't active yet
                GameManager.GetInstance().AbilityManager.AddNewAbility(abilityCard);
            }
            else
            {
                // Upgrades a abilities stats if it was already active and player picked it again
                abilityObject = GameManager.GetInstance().AbilityManager.GetAbilityObject(abilityCard.AbilityName);
                abilityObject.GetComponent<IAbility>().SetConfig(abilityCard.Modifications[abilityCard.CardStage]);
            }

            abilityCard.CardStage += 1;

            if (abilityCard.CardStage >= abilityCard.Modifications.Count)
            {
                _cardManager.RemoveCardFromAvailableList(_cardType);
            }
        }

        Time.timeScale = 1;
        _cardManager.RemoveDisplayedCards();
    }
}
