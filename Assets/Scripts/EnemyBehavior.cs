using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject gm;
    public GameObject parent;
    public GridBehavior gridBehaviorCode;
    public CharacterStats enemyStats;
    public GameManager gmCode;

    public List<GameObject> tempList = new List<GameObject>();
    public List<GameObject> detectList = new List<GameObject>();
    public List<GameObject> attackAbleBlocks = new List<GameObject>();
    public List<GameObject> pathToPlayer = new List<GameObject>();

    public GameObject playerTarget;

    public Vector3[] positions;

    public bool mouseOver;
    public bool triggerEnemyToFunction;
    public bool triggerForLerp;

    private int detectableRange;
    private bool triggerSelectOnePositionToMove;
    private bool triggerShowMoveableBlocks;
    private bool attackNext;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
        gridBehaviorCode = gm.GetComponent<GridBehavior>();
        enemyStats = this.GetComponent<CharacterStats>();


        SetOutline("_FirstOutlineWidth", 0.0f);

        SetOutline("_SecondOutlineWidth", 0.0f);

        detectableRange = gmCode.GetComponent<WeaponStats>().GiveFarestRange(this.enemyStats.weaponNumber)+this.enemyStats.moveSpeed.GetValue();


        triggerEnemyToFunction = false;
        triggerForLerp = false;
        mouseOver = false;
        triggerSelectOnePositionToMove = false;
        triggerShowMoveableBlocks = false;
        attackNext = false;
    }
    void Update()
    {
        CheckOutline();
        parent = this.transform.parent.gameObject;
        if (triggerEnemyToFunction == true)
        {
            StartCoroutine(EnemyAction());

            triggerEnemyToFunction = false;
            
        }

        if (triggerForLerp == true)
        {
            StartCoroutine(MultipleLerp(positions,1f));
            triggerForLerp = false;
            gridBehaviorCode.resetVisit();
        }
    }
    public void OnMouseOver()
    {
        mouseOver = true;
        gmCode.currentEnemy = this.gameObject;
        
    }

    public void OnMouseExit()
    {
        mouseOver = false;
        gmCode.currentEnemy = null;
    }
    
    public void CheckOutline()
    {
        if (mouseOver == true)
        {

            SetOutline("_FirstOutlineWidth", 0.12f);
        }
        else
        {
            SetOutline("_FirstOutlineWidth", 0.0f);
        }
        if (CheckIfCanBeAttack())
        {
            SetOutline("_SecondOutlineWidth", 0.05f);
        }
        else
        {
           
            SetOutline("_SecondOutlineWidth", 0.0f);
        }
    }
    private bool CheckIfCanBeAttack()
    {
        if (this.transform.parent.tag == "Block")
        {
            if (this.transform.parent.GetComponent<GridStat>().inAttackRange == true)
            {
                return true;
            }
            else
                return false;
        }
        else {
            return false;
        }

    }

    IEnumerator EnemyAction()
    {
       GameObject p = CheckIfPlayerInDetectableRange();
        attackNext = false;

        gridBehaviorCode.resetVisit();
        pathToPlayer.Clear();
        //thats mean no player in range
        if (p == null)
        {
            yield return new WaitUntil(() => triggerShowMoveableBlocks == true);
            ShowMoveableBlocks();

            yield return new WaitUntil(() => triggerSelectOnePositionToMove == true);
            GameObject target = SelectOnePositionToMove();

            if (target != null)
            {
                FinalMove(target.GetComponent<GridStat>().x, target.GetComponent<GridStat>().y);
            }
            else
            {
                print("enemy skip moving");
            }
            print("end enemy action");
        }
        else
        {
            bool playerInRange;
            ShowAttackableBlocks(parent.GetComponent<GridStat>().x, parent.GetComponent<GridStat>().y);
            playerInRange = IsPlayerInRange();

            if (playerInRange == true)//attack
            {
                //before attack check
                GameObject toMoveBlock;
                toMoveBlock = MoveFurther();
                

                if (toMoveBlock == null)
                {
                    List<GameObject> o = new List<GameObject>();
                    o.Add(playerTarget);
                    Attack(o);
                    print("end enemy action");
                }
                else
                {
                    int currentX = parent.GetComponent<GridStat>().x;
                    int currentY = parent.GetComponent<GridStat>().y;
                    gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);
                    tempList.Clear();
                    tempList = gridBehaviorCode.tempOfInteractableBlocks;

                    foreach (GameObject t in tempList)
                    {
                        //now tempList has all the possible position to go
                        if (t.GetComponent<GridStat>().occupied == true && t.GetComponent<GridStat>().standable == false)
                        {
                            tempList.Remove(t);
                        }
                    }


                    print("location is " + toMoveBlock.GetComponent<GridStat>().x+","+ toMoveBlock.GetComponent<GridStat>().y);
                    FinalMove(toMoveBlock.GetComponent<GridStat>().x, toMoveBlock.GetComponent<GridStat>().y);
                    List<GameObject> o = new List<GameObject>();
                    o.Add(playerTarget);
                    Attack(o);
                    print("end enemy action");

                   

                }
            }
            else// find the block to be in attack range
            {
                int currentX = parent.GetComponent<GridStat>().x;
                int currentY = parent.GetComponent<GridStat>().y;
                gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);
                tempList.Clear();
                tempList = gridBehaviorCode.tempOfInteractableBlocks;

                foreach (GameObject t in tempList)
                {
                    //now tempList has all the possible position to go
                    if (t.GetComponent<GridStat>().occupied == true && t.GetComponent<GridStat>().standable == false)
                    {
                        tempList.Remove(t);
                    }
                }


                pathToPlayer = getPathTo(playerTarget,detectableRange);

               
                if (pathToPlayer == null)
                {
                    print("pathToPlayer = null");
                }
                print("going to "+playerTarget.name);
                GameObject toMoveBlock =  FindTheAttackableBlock(pathToPlayer);
                //move first then check attackNext

                if (toMoveBlock != null)
                {
                    print("this is to move block" + toMoveBlock.GetComponent<GridStat>().x + "," + toMoveBlock.GetComponent<GridStat>().y);
                    FinalMove(toMoveBlock.GetComponent<GridStat>().x, toMoveBlock.GetComponent<GridStat>().y);
                }

                if (attackNext==true) 
                {
                    List<GameObject> d = new List<GameObject>();
                    d.Add(playerTarget);
                    Attack(d);
                    print("end enemy action");
                }
                
                
            }
        }
    }
    

   

    IEnumerator MultipleLerp(Vector3[] _pos, float _speed)
    {
        for (int i = 0; i < _pos.Length; i++)
        {
            Vector3 startPos = transform.position;
            float timer = 0f;
            while (timer <= 1f)
            {
                timer += Time.deltaTime * _speed;
                Vector3 newPos = Vector3.Lerp(startPos, _pos[i], timer);
                transform.position = newPos;
                yield return new WaitForEndOfFrame();
            }
            transform.position = _pos[i];
            startPos = _pos[i];
        }

        yield return false;
    }

    public GameObject CheckIfPlayerInDetectableRange()
    {
        detectList.Clear();
        int x = parent.GetComponent<GridStat>().x;
        int y = parent.GetComponent<GridStat>().y;
        gridBehaviorCode.FindSelectableBlock(x, y, detectableRange, false, true);
        detectList = gridBehaviorCode.tempOfInteractableBlocks;
        List<GameObject> g = new List<GameObject>();

        foreach (GameObject t in detectList)
        {
            //now tempList has all the possible position to go
            if (t.GetComponent<GridStat>().occupied == true && t.transform.GetChild(0).tag=="Player" )
            {
                g.Add(t);
            }
        }

        playerTarget  = SetTarget(g);

        if (playerTarget != null)
        {
            print(playerTarget.name);
        }
        
        triggerShowMoveableBlocks = true;
        return playerTarget;
    }

    private GameObject SetTarget(List<GameObject> playerInRangeBlock)
    {
        
        int lowestIndex;
        bool foundOne=false;
        int tempHealth = 0;
        if (playerInRangeBlock.Count > 0)
        {
            tempHealth = playerInRangeBlock[0].transform.GetChild(0).GetComponent<CharacterStats>().currentHealth;
            lowestIndex = 0;

            for (int i = 0; i < playerInRangeBlock.Count; i++)
            {
                if (playerInRangeBlock[i].transform.GetChild(0).GetComponent<CharacterStats>().currentHealth < tempHealth)
                {
                    foundOne = true;
                    lowestIndex = i;

                    tempHealth = playerInRangeBlock[i].transform.GetChild(0).GetComponent<CharacterStats>().currentHealth;
                }

            }

            if (foundOne == false)
            {
                //compare first one to second one
                if (playerInRangeBlock.Count >= 2 && playerInRangeBlock[0].transform.GetChild(0).GetComponent<CharacterStats>().currentHealth < playerInRangeBlock[1].transform.GetChild(0).GetComponent<CharacterStats>().currentHealth)
                {
                    
                    lowestIndex = 0;
                    foundOne = true;
                    return playerInRangeBlock[lowestIndex].transform.GetChild(0).gameObject;
                   
                }
                else
                {
                    print("random");
                    //random
                    return playerInRangeBlock[Random.Range(0, playerInRangeBlock.Count)].transform.GetChild(0).gameObject;

                }
            }
            else
            {
                return playerInRangeBlock[lowestIndex].transform.GetChild(0).gameObject;
            }

        }
        else
        {
            return null;
        }
        
    }

    public void ShowMoveableBlocks()
    {
        tempList.Clear();
        int currentX = parent.GetComponent<GridStat>().x;
        int currentY = parent.GetComponent<GridStat>().y;

        /*
        if (playerTarget != null)
        {
            int endX = playerTarget.transform.parent.GetComponent<GridStat>().x;
            int endY = playerTarget.transform.parent.GetComponent<GridStat>().y;
            //calculate for the closer block
            gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);
         */   
        
            //random moving 
            gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);
            tempList = gridBehaviorCode.tempOfInteractableBlocks;

            foreach (GameObject t in tempList)
            {
                //now tempList has all the possible position to go
                if (t.GetComponent<GridStat>().occupied == true && t.GetComponent<GridStat>().standable == false)
                {
                    tempList.Remove(t);
                }
            }
        
        triggerSelectOnePositionToMove = true;
    }

    public GameObject SelectOnePositionToMove()
    {
        GameObject target;

        foreach (GameObject b in tempList)
        {
            if (b.GetComponent<GridStat>().standable == false && b.GetComponent<GridStat>().occupied == true)
            {
                tempList.Remove(b);
            }
        }

        //if = 0 thats mean no possible location to move
        if (tempList.Count > 0)
        {
            GameObject t = null;

            t = tempList[Random.Range(0, tempList.Count)];
            
            print(this.name + "-target to move X:" + t.GetComponent<GridStat>().x + " Y: " + t.GetComponent<GridStat>().y);
            target = t;
            return target;
           

        }
        else
        {
            print("list is empty");
            print(this.name + "cannot move anywhere");
            return target = null;
        }
        
    }

    public void FinalMove(int endX, int endY)
    {
        
       
        
        

        gridBehaviorCode.findDistance = true;
        int currentX;
        int currentY;

        currentX = parent.GetComponent<GridStat>().x;
        currentY = parent.GetComponent<GridStat>().y;

        gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);

        bool walkable = gridBehaviorCode.RunThePath(currentX, currentY, endX, endY, enemyStats.moveSpeed.GetValue());

        //check if it over block limit
        if (walkable == true)
        {
            // In TRUE, this will store points to the 'positions[]'
            int pathCount = gridBehaviorCode.path.Count;
            positions = new Vector3[pathCount];

            for (int i = pathCount - 1; i >= 0; i--)
            {
                Vector3 temp = gridBehaviorCode.path[i].transform.position;

                temp = new Vector3(temp.x, temp.y + 1, temp.z);

                for (int j = 0; j < pathCount; j++)
                {
                    positions[j] = temp;
                }

                this.transform.SetParent(gridBehaviorCode.path[i].transform);
                
                triggerForLerp = true;
            }
        }
        else
        {
            print("over block limit");
        }
        
    }

    public void ShowAttackableBlocks(int startX,int startY)
    {
        attackAbleBlocks.Clear();
        
        int attackRange = gmCode.GetComponent<WeaponStats>().GiveAttackRange(enemyStats.weaponNumber);
        List<int> x = gmCode.GetComponent<WeaponStats>().GiveXValue(startX, enemyStats.weaponNumber);
        List<int> y = gmCode.GetComponent<WeaponStats>().GiveYValue(startY, enemyStats.weaponNumber);

        for (int i = 0; i < x.Count; i++)
        {
            print(x.Count + " " + x[0]);

            if (x[i] != -1 && y[i] != -1)
                gridBehaviorCode.FindSelectableBlock(x[i], y[i], attackRange, true, false);
            attackAbleBlocks = gridBehaviorCode.tempOfInteractableBlocks;
        }

        /*
        if (enemyStats.weaponNumber == 3)
        {
            //gmCode.setOnOffMenu(gmCode.menuPanel3, true);
        }*/
    }
    

    private bool IsPlayerInRange()
    {
        if (attackAbleBlocks.Count > 0)
        {
            foreach (GameObject obj in attackAbleBlocks)
            {
                if (obj.transform.childCount > 0)
                {
                    if (obj.transform.GetChild(0).gameObject == playerTarget)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    private void Attack(List<GameObject> target)
    {
        for (int i = 0; i < target.Count; i++)
        {
            int damageValue, visitValue;

            visitValue = target[i].transform.parent.GetComponent<GridStat>().visit;
            damageValue = gmCode.GetComponent<WeaponStats>().GiveDamageValue(enemyStats.weaponNumber, visitValue);

            if (target[i].GetComponent<CharacterStats>() != null)
            {
                bool resetPlayers = false;
                //has to check if the target will be destoy or no
                if (target[i].GetComponent<CharacterStats>().currentHealth - damageValue <= 0)
                {
                    resetPlayers = true;
                    gmCode.playersList.Remove(target[i]);
                    gmCode.PopupText(target[i].name+" got killed!");
                }
                gmCode.AddMessage(this.name+" Attacked " + target[i].name + "for " + damageValue + " damage.", Color.white);
                target[i].GetComponent<CharacterStats>().TakeDamage(damageValue);

                if (resetPlayers == true)
                {

                    gmCode.ReCountPlayers();
                }
            }

            
            gridBehaviorCode.resetVisit();
        }

    }

    private GameObject MoveFurther()
    {
        int furthestRange; GameObject b;
        GameObject tempBlock;
        GameObject toMoveBlock = null;

        int currentX = parent.GetComponent<GridStat>().x;
        int currentY = parent.GetComponent<GridStat>().y;
        gridBehaviorCode.FindSelectableBlock(currentX, currentY, enemyStats.moveSpeed.GetValue(), false, true);
        tempList.Clear();
        tempList = gridBehaviorCode.tempOfInteractableBlocks;

        foreach (GameObject t in tempList)
        {
            //now tempList has all the possible position to go
            if (t.GetComponent<GridStat>().occupied == true && t.GetComponent<GridStat>().standable == false)
            {
                tempList.Remove(t);
            }
        }

        //pathToPlayer.Clear();
        pathToPlayer = getPathTo(playerTarget,this.enemyStats.moveSpeed.GetValue());
        furthestRange = gmCode.GetComponent<WeaponStats>().GiveFarestRange(this.enemyStats.weaponNumber);
        if (((pathToPlayer.Count) - 1) == furthestRange)
        {
            //no need to move
            return toMoveBlock;
        }
        else
        {
            int i = 0; int j = 0; int keepInLoop = 0; 
            bool pleaseCheckLeft = false;
            bool pleaseCheckRight = false;
            //need to move. check if to move back or forward
            if (this.parent.GetComponent<GridStat>().y > playerTarget.transform.parent.GetComponent<GridStat>().y)
            {
                while (keepInLoop == 0)
                {
                    b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i, this.parent.GetComponent<GridStat>().y + j + 1];
                    print(b.name);
                    if (CheckStandable(b) == true)
                    {
                        bool a;
                        tempBlock = b;
                        ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                        a = IsPlayerInRange();
                        if (a == true)
                        {
                            j++;
                            toMoveBlock = tempBlock;
                            
                        }
                        else
                        {
                            //a= false
                            return toMoveBlock;
                        }
                    }
                    else
                    {
                        pleaseCheckLeft = true;
                    }

                    if (pleaseCheckLeft == true)
                    {
                        

                        b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i - 1, this.parent.GetComponent<GridStat>().y + j];
                        if (CheckStandable(b) == true)
                        {
                            bool a;
                            tempBlock = b;
                            ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                            a = IsPlayerInRange();
                            if (a == true)
                            {
                                i--;
                                toMoveBlock = tempBlock;
                                
                            }
                            else
                            {
                                //a= false
                                return toMoveBlock;
                            }
                        }
                        else
                        {
                            pleaseCheckRight = true;
                        }

                        pleaseCheckLeft = false;
                    }

                    if (pleaseCheckRight == true)
                    {


                        b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i + 1, this.parent.GetComponent<GridStat>().y + j];
                        if (CheckStandable(b) == true)
                        {
                            bool a;
                            tempBlock = b;
                            ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                            a = IsPlayerInRange();
                            if (a == true)
                            {
                                i++;
                                toMoveBlock = tempBlock;
                                
                            }
                            else
                            {
                                //a= false
                                return toMoveBlock;
                            }
                        }
                        else
                        {
                            keepInLoop = 1;
                            return toMoveBlock;
                            
                        }

                        pleaseCheckRight = false;
                    }


                }
            }
            
            else
            {
                while (keepInLoop == 0)
                {
                    b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i, this.parent.GetComponent<GridStat>().y + j - 1];
                    if (CheckStandable(b) == true)
                    {
                        bool a;
                        tempBlock = b;
                        ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                        a = IsPlayerInRange();
                        if (a == true)
                        {
                            j--;
                            toMoveBlock = tempBlock;

                        }
                        else
                        {
                            //a= false
                            return toMoveBlock;
                        }
                    }
                    else
                    {
                        pleaseCheckLeft = true;
                    }

                    if (pleaseCheckLeft == true)
                    {


                        b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i - 1, this.parent.GetComponent<GridStat>().y + j];
                        if (CheckStandable(b) == true)
                        {
                            bool a;
                            tempBlock = b;
                            ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                            a = IsPlayerInRange();
                            if (a == true)
                            {
                                i--;
                                toMoveBlock = tempBlock;

                            }
                            else
                            {
                                //a= false
                                return toMoveBlock;
                            }
                        }
                        else
                        {
                            pleaseCheckRight = true;
                        }

                        pleaseCheckLeft = false;
                    }

                    if (pleaseCheckRight == true)
                    {


                        b = gridBehaviorCode.gridArray[this.parent.GetComponent<GridStat>().x + i + 1, this.parent.GetComponent<GridStat>().y + j];
                        if (CheckStandable(b) == true)
                        {
                            bool a;
                            tempBlock = b;
                            ShowAttackableBlocks(b.GetComponent<GridStat>().x, b.GetComponent<GridStat>().y);
                            a = IsPlayerInRange();
                            if (a == true)
                            {
                                i++;
                                toMoveBlock = tempBlock;

                            }
                            else
                            {
                                //a= false
                                return toMoveBlock;
                            }
                        }
                        else
                        {
                            keepInLoop = 1;
                            return toMoveBlock;

                        }

                        pleaseCheckRight = false;
                    }


                }
            }
           
        }

        return toMoveBlock;
    }

    private bool CheckStandable(GameObject d)
    {
        if (d.GetComponent<GridStat>().standable == true && d.GetComponent<GridStat>().occupied == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<GameObject> getPathTo(GameObject p,int limit)
    {
        List<GameObject> toBeReturn = new List<GameObject>();
        int startX = this.parent.GetComponent<GridStat>().x;
        int startY = this.parent.GetComponent<GridStat>().y;
        int endX = p.transform.parent.GetComponent<GridStat>().x;
        int endY = p.transform.parent.GetComponent<GridStat>().y;

        print("going from" + startX + "," + startY + " and to " + endX +","+endY+" with limit of "+limit);

        gridBehaviorCode.findDistance = true;


        bool a = gridBehaviorCode.RunThePath(startX, startY, endX, endY, limit);
        print("a= " + a);
        if (a == true)
        {
            return gridBehaviorCode.path;
        }
        else
        {
            return null;
        }

        
    }

    private GameObject FindTheAttackableBlock(List<GameObject> path)
    {
        // must check if over walk limit
        if (path == null)
        {
            print("path = null");
        }
        bool foundOne = false;
        List<GameObject> tempInteractableList = new List<GameObject>();
        int playerX = playerTarget.transform.parent.GetComponent<GridStat>().x;
        int playerY = playerTarget.transform.parent.GetComponent<GridStat>().y;
        int count = 1; int j=-1;

        for (int i = path.Count-2; i >0; i--)
        {
            print("count is " + count);
                j = i;
            
                if (i != 1)
                {
                    print("path" + path[i].GetComponent<GridStat>().x + ", " + path[i].GetComponent<GridStat>().y);

                    ShowAttackableBlocks(path[i].GetComponent<GridStat>().x, path[i].GetComponent<GridStat>().y);
                    tempInteractableList = attackAbleBlocks;
                    foreach (GameObject obj in tempInteractableList)
                    {
                        if (obj.GetComponent<GridStat>().x == playerX && obj.GetComponent<GridStat>().y == playerY)
                        {

                            foundOne = true;
                            attackNext = true;
                            return path[i];
                        }
                    }
                }
            if (count == this.enemyStats.moveSpeed.GetValue())
            {
                print("in here: count=" + count);
                break;
            }
            else
            {
                count++;
            }  
        }
        if (foundOne == false)
        {
            //return in closest block
            return path[j];
        }

        return null;
        
    }

    public void SetOutline(string o, float a)
    {
        this.GetComponent<Renderer>().material.SetFloat(o, a);
    }








}
