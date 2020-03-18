using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //all collectable items & resources put in here
    public enum Type
    {
        Fuel,
        Scrap,
        Iron,
        Steel,
        Brass,
        Plastic,
        Rubber,
        Nylon,
        Wires,
        CircuitBoard,
        ElectronicChip,
    }

    public Type type;
    public int amount;
}
