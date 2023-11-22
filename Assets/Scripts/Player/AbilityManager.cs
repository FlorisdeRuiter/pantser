using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private static AbilityManager _instance;
    [SerializeField] private List<Ability> _abilities;
    
    public void AddNewAbility(AbilityCardDetails card)
    {
        // Adds the ability object to the parent object
        GameObject abilityObject = Instantiate(card.AbilityObject, transform);
        _abilities.Add(abilityObject.GetComponent<Ability>());

        if (card.CardStage >= card.Modifications.Count)
        {
            abilityObject.GetComponent<IAbility>().SetConfig(card.Modifications[card.CardStage]);
        }
    }

    public GameObject GetAbilityObject(string name)
    {
        foreach (Ability ability in _abilities)
        {
            if (ability.Name == name)
            {
                return ability.gameObject;
            }
        }
        return null;
    }

    #region Get Instance
    public static AbilityManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<AbilityManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("AbilityManager").AddComponent<AbilityManager>();
        }

        return _instance;
    }
    #endregion
}