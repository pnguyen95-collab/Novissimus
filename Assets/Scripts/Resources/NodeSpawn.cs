using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawn : MonoBehaviour
{
    public GridBehavior gridBehaviorCode;
    public GameObject metalPrefab;
    public GameObject syntheticPrefab;
    public GameObject electronicPrefab;
    public int resourceSpawn; //number of resources to spawn
    public Vector2Int resourceSpawnXY;

    // Start is called before the first frame update
    void Start()
    {
        gridBehaviorCode = this.GetComponent<GridBehavior>();

        GameObject[,] gridArray = gridBehaviorCode.gridArray;
        Vector3 leftBottomLocation = gridBehaviorCode.leftBottomLocation;
        float scale = gridBehaviorCode.scale;

        //for loop to spawn resources
        for (int i = 0; i < resourceSpawn; i++)
        {
            int spawnType = Random.Range(1, 4);

            //spawn Metal
            if (spawnType == 1)
            {
                GenerateRandom();

                int x = resourceSpawnXY.x;
                int y = resourceSpawnXY.y;

                //check to see if there's already a resource node there
                if (gridArray[x, y].GetComponent<GridStat>().resourceNode == false)
                {
                    GameObject player = Instantiate(metalPrefab, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.identity);
                    player.transform.SetParent(gridArray[x, y].transform);
                    gridArray[x, y].GetComponent<GridStat>().resourceNode = true;
                    Debug.Log("Spawned a Metal Node!");
                }
            }
            //spawn Synthetic Polymer
            if (spawnType == 2)
            {
                GenerateRandom();

                int x = resourceSpawnXY.x;
                int y = resourceSpawnXY.y;

                //check to see if there's already a resource node there
                if (gridArray[x, y].GetComponent<GridStat>().resourceNode == false)
                {
                    GameObject res = Instantiate(syntheticPrefab, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.identity);
                    res.transform.SetParent(gridArray[x, y].transform);
                    gridArray[x, y].GetComponent<GridStat>().resourceNode = true;
                    Debug.Log("Spawned a Synthetic Polymer Node!");
                }

            }
            //spawn Electronics
            if (spawnType == 3)
            {
                GenerateRandom();

                int x = resourceSpawnXY.x;
                int y = resourceSpawnXY.y;

                //check to see if there's already a resource node there
                if (gridArray[x, y].GetComponent<GridStat>().resourceNode == false)
                {
                    GameObject res = Instantiate(electronicPrefab, new Vector3(leftBottomLocation.x + scale * x, leftBottomLocation.y + scale, leftBottomLocation.z + scale * y), Quaternion.identity);
                    res.transform.SetParent(gridArray[x, y].transform);
                    gridArray[x, y].GetComponent<GridStat>().resourceNode = true;
                    Debug.Log("Spawned an Electronic Node!");
                }
            }
        }
    }

    //Random spawn generator
    void GenerateRandom()
    {
        Vector2Int spawnPoint = new Vector2Int(0, 0);

        spawnPoint.x = Random.Range(0, gridBehaviorCode.columns);
        spawnPoint.y = Random.Range(3, gridBehaviorCode.rows);
        //Debug.Log(spawnPoint);

        //check to see if spawn point is not on an obstacle
        if (gridBehaviorCode.gridArray[spawnPoint.x, spawnPoint.y].GetComponent<GridStat>().standable == true)
        {
            //check to see if there is not a player character there
            if (gridBehaviorCode.gridArray[spawnPoint.x, spawnPoint.y].GetComponent<GridStat>().occupied == false)
            {
                resourceSpawnXY = new Vector2Int(spawnPoint.x, spawnPoint.y);
            }
        }
    }

}
