using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum AbilityInputNamesEnum { MeleeAbility, RangedAbility }

public class AttackInputMappings : MonoBehaviour
{
    [SerializeField] private AbilityInputNamesEnum[] inputNames;
    [SerializeField] private Ability[] defaultAbilities;
    
    private Dictionary<AbilityInputNamesEnum, Ability> mappings;

    private void Start()
    {
        mappings = new Dictionary<AbilityInputNamesEnum, Ability>();

        Assert.AreEqual(inputNames.Length, defaultAbilities.Length, 
            "Ability input mapping lists are not the same size, cannot make dictionary");
        
        for (int i = 0; i < inputNames.Length; i++)
        {
            mappings.Add(inputNames[i], defaultAbilities[i]);
        }
    }

    public Ability GetCurAssociatedAttack(AbilityInputNamesEnum inputInputName)
    {
        if (mappings.ContainsKey(inputInputName))
        {
            return mappings[inputInputName];
        }

        return null;
    }
    
    // TODO: check if correct mapping allowed (ie Firebolt isnt a MeleeAttack)
    public bool TryChangeMapping(AbilityInputNamesEnum inputInputName, Ability newAbility)
    {
        if (mappings.ContainsKey(inputInputName))
        {
            mappings[inputInputName] = newAbility;
            return true;
        }

        return false;
    }
}