using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField]
    private int baseValue;

    //Get the current value of stats after modifiers
    public int GetValue ()
    {
        int finalValue = baseValue;

        // Blah blah stat modifiers from Attachments

        return finalValue;
    }
}
