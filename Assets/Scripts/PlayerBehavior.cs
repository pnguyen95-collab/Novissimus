using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    GameObject gm;
    GridBehavior gridBehaviorCode;
    GameObject parent;

    private int limitNum;
    
    public Vector3[] positions;
    public bool triggerMoving;
    public float speed;
    


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        gridBehaviorCode = gm.GetComponent<GridBehavior>();

        //set limit num
        limitNum = 5;

        triggerMoving = false;
        speed = 1;

    }

    // Update is called once per frame
    void Update()
    {
        parent = this.transform.parent.gameObject;
        parent.GetComponent<GridStat>().status = 2;

        if(triggerMoving==true)
        {
            StartCoroutine(MultipleLerp(positions, speed));
        }
    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject.name);
    }

    //playerMove receive input from player
    public void PlayerMove(int endX, int endY)
    {
        gridBehaviorCode.findDistance = true;
        int currentX;
        int currentY;
        //findcurrent
        currentX = parent.GetComponent<GridStat>().x;

        currentY = parent.GetComponent<GridStat>().y;

        bool walkable = gridBehaviorCode.RunThePath(currentX, currentY, endX, endY, limitNum);


        //player move here
        if (walkable == true)
        {
            //find loop number
            int pathCount = gridBehaviorCode.path.Count;
            positions = new Vector3[pathCount];
            


          

            for (int i = pathCount-1; i >= 0; i--)
            {
                Vector3 temp = gridBehaviorCode.path[i].transform.position;
                
                temp = new Vector3(temp.x, temp.y + 1, temp.z);

                for (int j = 0; j < pathCount; j++)
                {
                    positions[j] = temp;

                }

                this.transform.SetParent(gridBehaviorCode.path[i].transform);

                triggerMoving = true;
            }
        }
        else
        {
            print("over speed limit");

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
        triggerMoving = false;
        yield return false;
    }







}
