using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public bool findDistance = false;
    public int rows = 20;
    public int columns = 20;
    public int scale = 1;
    public GameObject gridPrefab;
    public GameObject enemyPrefeb;
    public GameObject playerPrefab;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 0;
    public int endY = 3;

    public List<GameObject> path = new List<GameObject>();

    public List<GameObject> AdjacencyList = new List<GameObject>();

    public Material one;

    [System.Serializable]
    public class SpawnXY
    {
        public Vector2Int spawnXY;
    }

    //Player, enemy and obstacle spawn locations
    public List<SpawnXY> playerSpawn;
    public List<SpawnXY> enemySpawn;
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

    // Update is called once per frame
    void Update()
    {

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

            if (path.Count > limitNum)
            {
                return false;
            }
            else
                return true;
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


        //Generates players/enemies/obstacles according to list
        for (int i = 0; i < playerSpawn.Count; i++)
        {
           GeneratePlayer(playerSpawn[i].spawnXY.x, playerSpawn[i].spawnXY.y);
        }

        for (int i = 0; i < enemySpawn.Count; i++)
        {
            GenerateEnemy(enemySpawn[i].spawnXY.x, enemySpawn[i].spawnXY.y);
        }

        for (int i = 0; i < obstacleSpawn.Count; i++)
        {
            GenerateObstacle(obstacleSpawn[i].spawnXY.x, obstacleSpawn[i].spawnXY.y);
        }

        //GenerateObstacle(2, 0); old system
        //GenerateObstacle(2, 2);
        //GeneratePlayer(0, 0);
        //GeneratePlayer(4, 0);
        //GeneratePlayer(6, 0);
        //GenerateEnemy(5, 5);
        //GenerateEnemy(5, 6);

    }

    void GenerateObstacle(int x, int y)
    {
        gridArray[x, y].GetComponent<GridStat>().standable = false;
    }

    void GeneratePlayer(int x, int y)
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.identity);
        player.transform.SetParent(gridArray[x, y].transform);
    }

    void GenerateEnemy(int x, int y)
    {
        GameObject player = Instantiate(enemyPrefeb, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.identity);
        player.transform.SetParent(gridArray[x, y].transform);
    }

    
    void SetDistance(int pointX,int pointY,int limitNum)
    {
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
                    TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
            }
        }
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
            step = gridArray[x, y].GetComponent<GridStat>().visit - 1;
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

            GameObject tempObj = FindClosest(gridArray[endX, endY].transform, tempList);
            path.Add(tempObj);
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
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().standable && gridArray[x, y - 1].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;

            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<GridStat>().standable && gridArray[x - 1, y].GetComponent<GridStat>().visit == step)
                    return true;
                else
                    return false;
        }
        return false;
    }

    void TestFourDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -1, 1))
            SetVisited(x, y + 1, step);

        if (TestDirection(x, y, -1, 2))
            SetVisited(x + 1, y, step);

        if (TestDirection(x, y, -1, 3))
            SetVisited(x, y - 1, step);

        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);
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

   

    public void FindSelectableBlock(int x,int y,int limitNum)
    {
        
        GameObject current = gridArray[x, y];
        SetDistance(x,y,limitNum);

     }

    public void resetVisit()
    {
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<GridStat>().visit = -1;
            obj.GetComponent<GridStat>().pathActive=false;
        }
    }

}
