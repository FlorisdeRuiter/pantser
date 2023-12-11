using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> Cards;
    public List<CardScriptableDetails> CardDetailsList;

    private void Start()
    {
        // Loads all the cards from the resources file into a list
        CardDetailsList = Resources.LoadAll<CardScriptableDetails>("Cards").ToList();

        foreach (CardScriptableDetails card in CardDetailsList)
        {
            if (card.ActivateOnStart)
            {
                if (card.BaseCardStage <= 0)
                    card.CardStage = 1;
                else
                    card.CardStage = card.BaseCardStage;
            }
            if (card is AbilityCardDetails abilityCard)
            {
                abilityCard.AbilityObject.GetComponent<Ability>().Name = abilityCard.AbilityName;

                if (card.ActivateOnStart)
                {
                    GameManager.GetInstance().AbilityManager.AddNewAbility(abilityCard);
                }
            }
        }
    }

    /// <summary>
    /// Returns a random set of cards
    /// </summary>
    /// <param name="cardsNeeded">Amount of cards you want returned</param>
    /// <returns></returns>
    public List<CardScriptableDetails> GetRandomCards(int cardsNeeded)
    {
        List<CardScriptableDetails> selectedCards = new List<CardScriptableDetails>();

        int returnAmount;

        // Changes the amount of cards that are returned depending on of their are enough available
        if (CardDetailsList.Count < cardsNeeded)
            returnAmount = CardDetailsList.Count;
        else
            returnAmount = cardsNeeded;

        // Selects an amount equal to returnAmount of random cards to return
        for (int i = 0; i < returnAmount; i++)
        {
            CardScriptableDetails cardToAdd = CardDetailsList[Random.Range(0, CardDetailsList.Count)];

            if (selectedCards.Contains(cardToAdd))
                returnAmount += 1;
            else
                selectedCards.Add(cardToAdd);
        }
        return selectedCards;
    }

    /// <summary>
    /// Displays cards the player can choose from
    /// </summary>
    public void DisplayCards()
    {
        // Pauses the game
        Time.timeScale = 0;
        List<CardScriptableDetails> cardsToDisplay = GetRandomCards(Cards.Count);

        // Sets the details of the displayed card equal to those of the randomly selected card
        for (int i = 0; i < cardsToDisplay.Count; i++)
        {
            Cards[i].SetCardDetails(cardsToDisplay[i]);
            Cards[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Removes cards from the screen
    /// </summary>
    public void RemoveDisplayedCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].gameObject.SetActive(false);
        }
    }

    public void RemoveCardFromAvailableList(CardScriptableDetails card)
    {
        CardDetailsList.Remove(card);
    }
}
