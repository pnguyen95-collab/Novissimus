using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //should add one more particle for mouseOVer
    public GameObject particleChildAttackable;
    public GameObject particleChildMouseOver;
    public GridBehavior gridBehaviorCode;
    public bool mouseOver;
    public bool canBeAttacked;
    public GameObject gm;
    public GameManager gmCode;
    private int upDown; // if 0 = go front , 1 = go back 
    public bool triggerMoving;
    public bool triggerForLerp;
    public GameObject parent;
    public GameObject targetForMoving;
    public List<GameObject> tempList = new List<GameObject>();
    private bool trigger;
    public Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        triggerMoving = false;
        triggerForLerp = false;
        upDown = 0;
        gm = GameObject.FindGameObjectWithTag("GameController");
        gmCode = gm.GetComponent<GameManager>();
        gridBehaviorCode = gm.GetComponent<GridBehavior>();

        if (this.transform.childCount > 0)
        {
            particleChildMouseOver = this.transform.GetChild(1).gameObject;
            particleChildAttackable = this.transform.GetChild(0).gameObject;
        }

        canBeAttacked = false;
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
    }

    public void OnMouseExit()
    {
        mouseOver = false;
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
        //targetForMoving = SelectOnePositionToMove();
        

        
        

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
        gridBehaviorCode.FindSelectableBlock(x, y, 4);


        trigger = true;
        

    }

    public GameObject SelectOnePositionToMove()
    {
        

        foreach (GameObject obj in gridBehaviorCode.gridArray)
        {
            


            if (obj.GetComponent<GridStat>().interactable == true&& obj.GetComponent<GridStat>().occupied == false )
            {
               
                tempList.Add(obj);
            }

            
        }
        

        GameObject target = tempList[Random.Range(0, tempList.Count)];
        print("target to move X:"+ target.GetComponent<GridStat>().x+" Y: "+ target.GetComponent<GridStat>().y);
        return target;

    }

    public void FinalMove(int endX, int endY)
    {
        gridBehaviorCode.findDistance = true;
        int currentX;
        int currentY;
        //findcurrent
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
