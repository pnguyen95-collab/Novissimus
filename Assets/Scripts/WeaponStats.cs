using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public string Name;
    
    public int weaponNumber;

    public List<int> xValues;
    public List<int> yValues;
    public int damageValue;
    private GridBehavior gridBehaviorCode;

    /*
    weapons
    number 1 = Normal bullet machine guns
    number 2 = Pivot Giant hammer
    number 3 = Blade
    */
    private void Start()
    {
        gridBehaviorCode = this.GetComponent<GridBehavior>();
    }

    public List<int> GiveXValue(int x,int itsWeaponNumber)
    {
        xValues.Clear();

        switch (itsWeaponNumber)
        {
            case 1:
                
                xValues.Add(x);
                if (x - 2 < 0)
                {
                    if (x - 1 < 0)
                    {
                        //should skip this direction
                        xValues.Add(-1);
                    }
                    else
                    {
                        xValues.Add(x - 1);

                    }
                }
                else
                {
                    xValues.Add(x - 2);
                }

                if (x + 2 > gridBehaviorCode.columns)
                {
                    if (x + 1 > gridBehaviorCode.columns)
                    {
                        //should skip this direction
                        xValues.Add(-1);
                    }
                    else
                    {
                        xValues.Add(x + 1);

                    }
                }
                else
                {
                    xValues.Add(x + 2);
                }
                xValues.Add(x);
                return xValues;
            case 2:
                xValues.Add(x);
                return xValues;
            case 3:
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

                if (y + 2 > gridBehaviorCode.rows)
                {
                    if (y + 1 > gridBehaviorCode.rows)
                    {
                        //should skip this direction
                        yValues.Add(-1);
                    }
                    else
                    {
                        yValues.Add(y + 1);

                    }
                }
                else
                {
                    yValues.Add(y + 2);
                }
                yValues.Add(y);
                yValues.Add(y);
                if (y - 2 < 0)
                {
                    if (y - 1 < 0)
                    {
                        //should skip this direction
                        yValues.Add(-1);
                    }
                    else
                    {
                        yValues.Add(y - 1);

                    }
                }
                else
                {
                    yValues.Add(y - 2);
                }
                return yValues;
            case 2:
                yValues.Add(y);
                return yValues;
            case 3:
                yValues.Add(y);
                return yValues;



        }
        return null;
    }

    public int GiveFarestRange(int itsWeaponNumber)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                return 3;
            case 2:
                return 1;
            case 3:
                return 1; 
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
            case 3:
                return 1;



        }
        return 0;
    }

    public int GiveDamageValue(int itsWeaponNumber,int step)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                return 3;
            case 2:
                if (step == 1)
                    return 3;
                else
                    return 5;
            case 3:
                return 5;



        }
        return 0;
    }

}
