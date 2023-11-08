using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    private CardScriptableDetails cardType;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardInfo;
    [SerializeField] private RawImage cardSprite;

    private CardManager cardManager;

    private void Awake()
    {
        cardManager = CardManager.GetInstance();
    }

    public void SetCardDetails(CardScriptableDetails details)
    {
        cardType = details;
        cardName.text = details.cardName;
        cardInfo.text = details.cardInfo;
        cardSprite.texture = details.cardSprite;  
    }

    public void OnPickCard()
    {
        GameObject abilityObject = new GameObject();

        if (cardType.cardStage == 0)
        {
            AddNewAbility();
        }
        else
        {
            abilityObject = GameObject.Find(cardType.abilityName);
            abilityObject.GetComponent<IAbility>().SetConfig(cardType.Modifications[cardType.cardStage]);
        }

        cardType.cardStage += 1;
        Time.timeScale = 1;
        cardManager.Removecards();
    }

    private void AddNewAbility()
    {
        GameObject abilityObject;
        GameObject abilityParent = GameObject.Find(cardType.abilityType.ToString());

        if (abilityParent != null)
        {
            Player player = FindObjectOfType<Player>();
            abilityParent = new GameObject(cardType.abilityType.ToString());

            abilityParent.transform.parent = player.transform;
            abilityParent.transform.localPosition = Vector3.zero;
            abilityParent.transform.rotation = player.transform.rotation;
        }

        abilityObject = Instantiate(cardType.abilityObject, abilityParent.transform.position, Quaternion.identity);
        abilityObject.transform.parent = abilityParent.transform;
        abilityObject.name = cardType.abilityName;
    }
}
