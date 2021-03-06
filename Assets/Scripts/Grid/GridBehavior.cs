﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    
    public GameObject gridPrefab;
    public List<GameObject> enemyPrefabs;

    
    public GameObject playerVehicle1;
    public GameObject playerVehicle2;
    public GameObject playerVehicle3;
    public GameObject playerVehicle4;
    public GameObject playerVehicle5;

    public Sprite vehicleMachineGun;
    public Sprite vehiclePivotHammer;
    public Sprite vehicleBlade;
    public Sprite vehicleSniper;


    public GameObject[,] gridArray;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    public List<GameObject> path = new List<GameObject>();
    public List<GameObject> AdjacencyList = new List<GameObject>();
    public List<GameObject> tempOfInteractableBlocks = new List<GameObject>();

    public bool findDistance = false;
    public int rows = 30;
    public int columns = 30;
    public int startX = 0;
    public int startY = 0;
    public int endX = 0;
    public int endY = 3;
    private int level;
    public float scale = 2f;

    private int count=0;
    private bool isWeapon;

   

    [System.Serializable]
    public class SpawnXY
    {
        public Vector2Int spawnXY;
    }

    //Player, enemy and obstacle spawn locations
    public List<SpawnXY> playerSpawn;
    private List<SpawnXY> enemySpawn;
    
     
    //private List<> enemyTry;
    public List<SpawnXY> obstacleSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        
        gridArray = new GameObject[columns, rows];
        if (gridPrefab)
        {
            GenerateGrid();
        }
        else
        {
            print("Missing prefab of grid");
        }

    }
    
    

    //execute the finding path
    public bool RunThePath(int sX, int sY, int eX, int eY, int limitNum)
    {
        startX = sX;
        startY = sY;
        endX = eX;
        endY = eY;
        if (findDistance)
        {
            SetDistance(sX,sY,limitNum);

            setPath();
            findDistance = false;
            //print(path.Count + " " + limitNum);

            if (path.Count > (limitNum + 1))
            {
                print("in here");
                return false;
            }
            else if (path.Count <= (limitNum + 1))
            {
                return true;
            }

            else
            { return false;}
        }
        else { return false; }
    }

    //generate grid size of grid do here, positions of player's car do here, positions of obstacles do here
    public void GenerateGrid()
    {

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);

                obj.GetComponent<GridStat>().x = i;

                obj.GetComponent<GridStat>().y = j;

                gridArray[i, j] = obj;

            }
        }

        if (playerSpawn.Count > 0)
        {
            for (int i = 0; i < obstacleSpawn.Count; i++)
            {

                GenerateObstacle(obstacleSpawn[i].spawnXY.x, obstacleSpawn[i].spawnXY.y);
            }



            //Generates players/enemies/obstacles according to list
            for (int i = 0; i < playerSpawn.Count; i++)
            {
                GameObject obj;
                //force assign weapon number here
                if (i == 0)
                {
                    obj = GeneratePlayer(playerVehicle1, playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);

                }
                else if (i == 1)
                {
                    obj = GeneratePlayer(playerVehicle2, playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);

                }
                else if (i == 2)
                {
                    obj = GeneratePlayer(playerVehicle3, playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);

                }
                else if (i == 3)
                {
                    obj = GeneratePlayer(playerVehicle4, playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);

                }
                else if (i == 4)
                {
                    obj = GeneratePlayer(playerVehicle5, playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);

                }
                else
                {
                    obj = null; print("obj is null");
                }

                int weaponNum = obj.GetComponent<CharacterStats>().weaponNumber;

                switch (weaponNum)
                {
                    case 1:
                        obj.GetComponent<SpriteRenderer>().sprite = vehicleMachineGun;
                        break;
                    case 2:
                        obj.GetComponent<SpriteRenderer>().sprite = vehiclePivotHammer;
                        break;
                    case 3:
                        obj.GetComponent<SpriteRenderer>().sprite = vehicleBlade;
                        break;
                    case 4:
                        obj.GetComponent<SpriteRenderer>().sprite = vehicleSniper;
                        break;

                }
            }
           
            int a = this.GetComponent<GameManager>().playerDataObject.GetComponent<PlayerData>().levelNum;
            //for testing
           
           
            //different depend on level num
            switch (a)
            {
                case 0:
                    print("level num is missing");
                    break;
                case 1:
                    //level 1 of desert
                    //all runner
                     GenerateEnemy(enemyPrefabs[0], 10, 14);
                     GenerateEnemy(enemyPrefabs[0], 16, 11);
                     GenerateEnemy(enemyPrefabs[0], 24, 14);
                    
                    break;
                case 2:
                    //level 2 of desert
                    //2 runner. 1 hunter
                    GenerateEnemy(enemyPrefabs[0], 19, 11);
                    GenerateEnemy(enemyPrefabs[0], 6, 13);
                    GenerateEnemy(enemyPrefabs[1], 10, 18);
                  
                    break;
                case 3:
                    //level 3 of desert
                    //1 runner. 1 roadpounder
                    GenerateEnemy(enemyPrefabs[0], 9, 11);
                    GenerateEnemy(enemyPrefabs[2], 23, 9);
                   
                    break;
                case 4:
                    //level 4 of desert
                    //1 hunter 1 roadpounder
                    GenerateEnemy(enemyPrefabs[2], 16, 12);
                    GenerateEnemy(enemyPrefabs[1], 10, 15);
                    
                    break;
                case 5:
                    //level 5 of desert story level
                    // 2 hunter. 2 chopper 
                    GenerateEnemy(enemyPrefabs[3], 7, 12);
                    GenerateEnemy(enemyPrefabs[3], 23, 9);
                    GenerateEnemy(enemyPrefabs[1], 2, 23);
                    GenerateEnemy(enemyPrefabs[1], 23, 16);
                    
                    break;
                case 6:
                    //resource map no enemy
                    break;

            }
            

            

        }
        else {
            print("no spawn value");
        }

    }

    void GenerateObstacle(int x, int y)
    {
        
        gridArray[x, y].GetComponent<GridStat>().standable = false;
    }

    GameObject GeneratePlayer(GameObject p,int x, int y)
    {
        GameObject player = Instantiate(p, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.Euler(90, 0, 0));
        player.transform.SetParent(gridArray[x, y].transform);
        
        return player;
    }

   GameObject GenerateEnemy(GameObject e,int x, int y)
    {
        GameObject enemy = Instantiate(e, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.Euler(90, 180, 0));
        enemy.transform.SetParent(gridArray[x, y].transform);
       
        return enemy;
    }

    
    void SetDistance(int pointX,int pointY,int limitNum)
    {
        //tempOfInteractableBlocks.Clear();
         startX = pointX;
         startY = pointY;

        InitialSetUp();
         
        int[] testArray = new int[rows * columns];
        for (int step = 1; step < rows * columns; step++)
        {

            foreach (GameObject obj in gridArray)
            {
                obj.GetComponent<GridStat>().stepLimit = limitNum;

                if (obj && obj.GetComponent<GridStat>().visit == step - 1)
                {
                    bool a;

                    if (isWeapon == false)
                    {
                        a = TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                    }
                    else
                    {
                        a = TestEightDirection(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y,step);
                    }
                    if (a == true)
                    {
                        if (step <= limitNum+1 && step > 0)
                        {
                            
                            tempOfInteractableBlocks.Add(obj);

                        }
                       
                    }
                }
            }

            if (step > limitNum+1)
            {
                break;
            }
        }
        //done getting tempList
    }

    void InitialSetUp()
    {
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<GridStat>().visit = -1;

        }
        gridArray[startX, startY].GetComponent<GridStat>().visit = 0;
    }

    //you got the path array here
    public void setPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<GridStat>().visit > 0)
        {
            path.Add(gridArray[x, y]);
            print("path add:" + "x=" + x +" y="+y);
            step = gridArray[x, y].GetComponent<GridStat>().visit - 1;
            print("step:"+step);
        }
        else {
            print("cant reach because of block's standable = false"); return;
        }
        for (int i = step; step > -1; step--)
        {

            if (TestDirection(x, y, step, 1))
                tempList.Add(gridArray[x, y + 1]);

            if (TestDirection(x, y, step, 2))
                tempList.Add(gridArray[x + 1, y]);

            if (TestDirection(x, y, step, 3))
                tempList.Add(gridArray[x, y - 1]);

            if (TestDirection(x, y, step, 4))
                tempList.Add(gridArray[x - 1, y]);

            print("tempList .count:" + tempList.Count);

            GameObject tempObj = FindClosest(gridArray[endX, endY].transform, tempList);
            path.Add(tempObj);
            print("path add:" + "x=" + x + " y=" + y);
            tempObj.GetComponent<GridStat>().pathActive = true;
            x = tempObj.GetComponent<GridStat>().x;
            y = tempObj.GetComponent<GridStat>().y;
            tempList.Clear();

        }

    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<GridStat>().standable && gridArray[x, y + 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;

            case 2:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().standable && gridArray[x + 1, y].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;

            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().standable &&  gridArray[x, y - 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().standable &&  gridArray[x - 1, y].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;

            case 5:
                if (x - 1 > -1 && y+1<rows && gridArray[x - 1, y+1] && gridArray[x - 1, y+1].GetComponent<GridStat>().standable&& gridArray[x - 1, y+1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;
            case 6:
                if (x + 1 < columns && y + 1 < rows && gridArray[x + 1, y + 1] && gridArray[x + 1, y + 1].GetComponent<GridStat>().standable &&  gridArray[x + 1, y + 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;
            case 7:
                if (x + 1 < columns && y - 1 > -1 && gridArray[x + 1, y - 1] && gridArray[x + 1, y - 1].GetComponent<GridStat>().standable&& gridArray[x + 1, y - 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;
            case 8:
                if (x - 1 > -1 && y - 1 > -1 && gridArray[x - 1, y - 1] && gridArray[x - 1, y - 1].GetComponent<GridStat>().standable  && gridArray[x - 1, y - 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;
                
        }
        return false;
    }

    bool TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -1, 1))
            SetVisited(x, y + 1, step);

        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);

        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);

        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);

        if (step > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TestEightDirection(int x,int y, int step)
    {
        if (TestDirection(x, y, -1, 1))
            SetVisited(x, y + 1, step);

        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);

        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);

        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);

        if (TestDirection(x, y, -1, 5))
            SetVisited(x-1, y + 1, step);

        if (TestDirection(x, y, -1, 6))
            SetVisited(x + 1, y+1, step);

        if (TestDirection(x, y, -1, 7))
            SetVisited(x+1, y - 1, step);

        if (TestDirection(x, y, -1, 8))
            SetVisited(x - 1, y-1, step);

        if (step > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetVisited(int x, int y, int step)
    {
        if (gridArray[x, y])
        {
            gridArray[x, y].GetComponent<GridStat>().visit = step;
        }
    }
    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        print("this is list count:"+list.Count);
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }

        

        return list[indexNumber];
    }

   

    public void FindSelectableBlock(int x,int y,int limitNum,bool isWeapons,bool isNeedReset)
    {
        this.isWeapon = isWeapons;
        if (isNeedReset == true)
        {
            resetVisit();
        }
        if (x < 0 || x>=30 || y < 0||y>=30)
        {
            print("it is out of grid range");
        }
        else
        {
            //GameObject current = gridArray[x, y];
            SetDistance(x, y, limitNum);
        }
     }

    public void resetVisit()
    {
        
        foreach (GameObject obj in tempOfInteractableBlocks)
        {
            obj.GetComponent<GridStat>().visit = -1;
            obj.GetComponent<GridStat>().pathActive = false;

            obj.GetComponent<GridStat>().inAttackRange = false;

            obj.GetComponent<GridStat>().interactable = false;

        }
        tempOfInteractableBlocks.Clear();
        /*
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<GridStat>().visit = -1;
            obj.GetComponent<GridStat>().pathActive=false;

            obj.GetComponent<GridStat>().inAttackRange = false;

            obj.GetComponent<GridStat>().interactable = false;
        }*/
    }

   
    

}
