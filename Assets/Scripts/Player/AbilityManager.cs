using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> _abilities;

    public void AddNewAbility(AbilityCardDetails card)
    {
        // Adds the ability object to the parent object
        GameObject abilityObject = Instantiate(card.AbilityObject, transform);
        _abilities.Add(abilityObject.GetComponent<Ability>());

        if (card.CardStage < card.Modifications.Count)
        {
            abilityObject.GetComponent<IAbility>().SetConfig(card.Modifications[card.CardStage-1]);
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
}