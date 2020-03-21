using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public string Name;
    
    public int weaponNumber;

    public List<int> xValues;
    public List<int> yValues;

    /*
    weapons
    number 1 = Normal bullet machine guns
    number 2 = Pivot Giant hammer
    */

    public List<int> GiveXValue(int x,int itsWeaponNumber)
    {
        xValues.Clear();

        switch (itsWeaponNumber)
        {
            case 1:
                xValues.Add(x);
                xValues.Add(x - 2);
                xValues.Add(x + 2);
                xValues.Add(x);
                return xValues;
            case 2:
                xValues.Add(x);
                return xValues;
                
        }


        return null;
    }
    public List<int> GiveYValue(int y, int itsWeaponNumber)
    {
        yValues.Clear();
        switch (itsWeaponNumber)
        {
            case 1:
                yValues.Add(y + 2);
                yValues.Add(y);
                yValues.Add(y);
                yValues.Add(y - 2);
                return yValues;
            case 2:
                return yValues;



        }
        return null;
    }

    public int GiveAttackRange(int itsWeaponNumber)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                return 1;
            case 2:
                return 2;



        }
        return 0;
    }

}
