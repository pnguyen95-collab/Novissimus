using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField]
    private int baseValue = 0;

    //Get the current value of stats after modifiers
    public int GetValue ()
    {
        int finalValue = baseValue;

        // Blah blah stat modifiers from Attachments

        return finalValue;
    }
}
