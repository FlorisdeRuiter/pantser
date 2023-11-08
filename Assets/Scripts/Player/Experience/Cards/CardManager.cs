using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards;

    private static CardManager _instance;

    public List<CardScriptableDetails> cardDetailsList;

    private void Start()
    {
        cardDetailsList = Resources.LoadAll<CardScriptableDetails>("Cards").ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayCards();
        }
    }

    public List<CardScriptableDetails> GetRandomCards(int cardsNeeded)
    {
        List<CardScriptableDetails> selectedCards = new List<CardScriptableDetails>();

        int returnAmount;

        if (cardDetailsList.Count < cardsNeeded)
            returnAmount = cardDetailsList.Count;
        else
            returnAmount = cardsNeeded;

        for (int i = 0; i < returnAmount; i++)
        {
            CardScriptableDetails cardToAdd = cardDetailsList[Random.Range(0, cardDetailsList.Count)];

            if (selectedCards.Contains(cardToAdd))
                cardsNeeded += 1;
            else
                selectedCards.Add(cardToAdd);
        }
        return selectedCards;
    }

    public void DisplayCards()
    {
        Time.timeScale = 0;
        List<CardScriptableDetails> cardsToDisplay = GetRandomCards(cards.Count);
        for (int i = 0; i < cardsToDisplay.Count; i++)
        {
            cards[i].SetCardDetails(cardsToDisplay[i]);
            cards[i].gameObject.SetActive(true);
        }
    }

    public void Removecards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }

    #region Get Instance
    public static CardManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<CardManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("CardManager").AddComponent<CardManager>();
        }

        return _instance;
    }
    #endregion
}
