using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    private List<Modifiers> modifiers = new List<Modifiers>();

    [SerializeField]
    private int baseValue = 0;

    //Add modifier
    public void AddModifier(Modifiers mod)
    {
        modifiers.Add(mod);
        modifiers.Sort(CompareModifiers);
    }

    //Resets modifier list
    public void ResetModifier()
    {
        modifiers.Clear();
    }

    //Compare order of modifiers and sort order they get added
    private int CompareModifiers(Modifiers a, Modifiers b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    //Get the current value of stats after modifiers
    public int GetValue ()
    {
        float finalValue = baseValue;

        //applying modifiers
        for (int i = 0; i < modifiers.Count; i++)
        {
            //checks whether or not the modifier is a flat increase or percentage and adds accordingly (boosters always calculated last)
            Modifiers mod = modifiers[i];

            if (mod.Type == ModifierType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == ModifierType.Percent)
            {
                finalValue *= 1 + mod.Value;
            }

        }

        //rounds it to an integer
        return Mathf.RoundToInt(finalValue);
    }
}
