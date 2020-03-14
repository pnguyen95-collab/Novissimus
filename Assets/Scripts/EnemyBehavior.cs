using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //should add one more particle for mouseOVer
    public GameObject particleChildAttackable;
    public GameObject particleChildMouseOver;
    public GameObject gm;
    public GameObject parent;
    public GridBehavior gridBehaviorCode;
    public CharacterStats enemyStats;
    public GameManager gmCode;

    private List<GameObject> tempList = new List<GameObject>();
    

    public Vector3[] positions;

    public bool mouseOver;
    public bool triggerMoving;
    public bool triggerForLerp;

    private bool trigger;
    
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
        gridBehaviorCode = gm.GetComponent<GridBehavior>();
        enemyStats = this.GetComponent<CharacterStats>();

        if (this.transform.childCount > 0)
        {
            particleChildMouseOver = this.transform.GetChild(1).gameObject;
            particleChildAttackable = this.transform.GetChild(0).gameObject;
        }

        
        triggerMoving = false;
        triggerForLerp = false;
        mouseOver = false;
        trigger = false;
    }
    void Update()
    {
       
        CheckParticle();

        parent = this.transform.parent.gameObject;
        if (triggerMoving == true)
        {
            StartCoroutine(EnemyMove());
            
            triggerMoving = false;
            
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
    public void CheckParticle()
    {
        if (mouseOver == true)
        {
            particleChildMouseOver.SetActive(true);
        }
        else if (mouseOver == false)
        {
            particleChildMouseOver.SetActive(false);

        }

        //if true and attackable (need to figure out the enemy is attackable)
        if (CheckIfCanBeAttack())
        {
            particleChildAttackable.SetActive(true);
        }
        else
        {
            particleChildAttackable.SetActive(false);
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

    IEnumerator EnemyMove()
    {
       ShowMoveableBlcoks();
        
       yield return new WaitUntil(() => trigger == true);
       GameObject target = SelectOnePositionToMove();

       FinalMove(target.GetComponent<GridStat>().x, target.GetComponent<GridStat>().y);
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

    public void ShowMoveableBlcoks()
    {
        int x = parent.GetComponent<GridStat>().x;
        int y = parent.GetComponent<GridStat>().y;
        //get stat from enemy code
        gridBehaviorCode.FindSelectableBlock(x, y, enemyStats.moveSpeed.GetValue());

        trigger = true;
    }

    public GameObject SelectOnePositionToMove()
    {
        GameObject target;
        if (gridBehaviorCode.tempOfInteractableBlocks.Count > 0)
        {
            //check stanable and occupied
            
            int i = 0;
            bool foundOne = false;
            GameObject t=null;

            while (i< gridBehaviorCode.tempOfInteractableBlocks.Count)
            {
                
                t = gridBehaviorCode.tempOfInteractableBlocks[Random.Range(0, gridBehaviorCode.tempOfInteractableBlocks.Count)];
                if (t.GetComponent<GridStat>().occupied == false && t.GetComponent<GridStat>().standable == true)
                {
                    
                    print("target to move X:" + t.GetComponent<GridStat>().x + " Y: " + t.GetComponent<GridStat>().y);
                    foundOne = true;

                    break;
                    

                }
                else { i++; }


            }

            if (foundOne == false)
            {
                print("the enemy cannot move anywhere");
                return target = null;
            }
            else
            {
                target = t;
                return target;
            }
        }
        else
        {
            print("list is empty");
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
        
        bool walkable = gridBehaviorCode.RunThePath(currentX, currentY, endX, endY, 4);

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

   








}
