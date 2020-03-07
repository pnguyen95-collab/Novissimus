using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLoot : MonoBehaviour
{
    [System.Serializable]
    public class ResourceTable
    {
        public string name;
        public int rarity;
        public int quantity;
    }

    public List<ResourceTable> metalSpawns;
    public List<ResourceTable> syntheticSpawns;
    public List<ResourceTable> electronicSpawns;

    public void MetalSpawn()
    {
        int randomNumber = Random.Range(0,101);
       
        //check for iron spawn
    }
}
