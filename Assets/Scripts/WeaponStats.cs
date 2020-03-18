using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public string Name;
    
    public int weaponNumber; 

    /*
    weapons
    number 1 = Normal bullet machine guns
    number 2 = Pivot Giant hammer
    */

    public int GiveXValue(int x,int itsWeaponNumber)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                return x;
            case 2:
                return x;
                
        }


        return 0;
    }
    public int GiveYValue(int y, int itsWeaponNumber)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                print("in here");
                return y + 2;
            case 2:
                return y;



        }
        return 0;
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
