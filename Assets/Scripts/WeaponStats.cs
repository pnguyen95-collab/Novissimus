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
    number 4 = sniper
    number 5 = Runner weapon
    number 6 = Hunter 
    number 7 = Roadpounder
    number 8 = Chopper
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
            case 4:
                xValues.Add(x);

                if (x - 6 < 0)
                {
                    if (x - 5 < 0)
                    {
                        
                                    //shouldskip this direction
                                    xValues.Add(-1);
                                
                        
                    }
                    else
                    {
                        xValues.Add(x - 5);

                    }
                }
                else
                {
                    xValues.Add(x - 6);
                }

                if (x + 6> gridBehaviorCode.columns)
                {
                    if (x + 5 > gridBehaviorCode.columns)
                    {
                          
                                
                                    //should skip this direction
                                    xValues.Add(-1);
                                
                                
                            
                            
                        
                    }
                    else
                    {
                        xValues.Add(x + 5);

                    }
                }
                else
                {
                    xValues.Add(x + 6);
                }
                xValues.Add(x);
                return xValues;
            case 5:
                xValues.Add(x);
                return xValues;
            case 6:

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
            case 7:
                xValues.Add(x);
                return xValues;
            case 8:
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
            case 4:
                
                if (y + 6 > gridBehaviorCode.rows)
                {
                    if (y + 5 > gridBehaviorCode.rows)
                    {
                        
                            
                                    //should skip this direction
                                    yValues.Add(-1);
                               
                        
                    }
                    else
                    {
                        yValues.Add(y + 5);

                    }
                }
                else
                {
                    yValues.Add(y + 6);
                }
                yValues.Add(y);
                yValues.Add(y);
                if (y - 6 < 0)
                {
                    if (y - 5 < 0)
                    {
                         
                                    //should skip this direction
                                    yValues.Add(-1);
                              
                        
                    }
                    else
                    {
                        yValues.Add(y - 5);

                    }
                }
                else
                {
                    yValues.Add(y - 6);
                }
                return yValues;
            case 5:
                yValues.Add(y);
                return yValues;
            case 6:
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
            case 7:
                yValues.Add(y);
                return yValues;
            case 8:
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
            case 4:
                return 7;
            case 5:
                return 1;
            case 6:
                return 3;
            case 7:
                return 1;
            case 8:
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
            case 4:
                return 1;
            case 5:
                return 1;
            case 6:
                return 1;
            case 7:
                return 2;
            case 8:
                return 2;



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
            case 4:
                return 8;
            case 5:
                return 2;
            case 6:
                return 3;
            case 7:
                return 5;
            case 8:
                return 4;



        }
        return 0;
    }

    public string GiveNameOfTheWeapon(int itsWeaponNumber)
    {
        switch (itsWeaponNumber)
        {
            case 1:
                return "Auto Machine Gun";
            case 2:
                return "Pivot Giant Hammer";
            case 3:
                return "Eater Blade";
            case 4:
                return "Sniper";
            case 5:
                return "Some Blade";
            case 6:
                return "Machine Gun";
            case 7:
                return "God Hammer";
            case 8:
                return "Cutlass";

        }

        return null;
    }

}
