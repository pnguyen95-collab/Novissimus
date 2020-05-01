using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    Flat,
    Percent,
}

public class Modifiers
{
    public float Value;
    public ModifierType Type;
    public int Order;

    public Modifiers(float value, ModifierType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }
}
