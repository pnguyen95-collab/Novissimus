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
        public int minQuantity;
        public int maxQuantity;
    }

    public List<ResourceTable> metalSpawns;
    public List<ResourceTable> syntheticSpawns;
    public List<ResourceTable> electronicSpawns;
    public List<ResourceTable> coreResource;

    public void MetalSpawn()
    {
        int randomNumber = Random.Range(0,101);

        //loop to check spawns
        for (int i = 0; i < metalSpawns.Count; i++)
        {
            if (randomNumber <= metalSpawns[i].rarity)
            {
                //check how many of the resource spawns
                int quantityDrop = Random.Range(metalSpawns[i].minQuantity, metalSpawns[i].maxQuantity);

                //give spawned resources to play and end loop
                Debug.Log("You have gotten " + quantityDrop + " " + metalSpawns[i].name + "!");
                CoreResourceAdd();
                return;
            }

            //cycle through spawn weights
            randomNumber -= metalSpawns[i].rarity;

            //errordebug
            if (randomNumber <= 0)
            {
                Debug.Log("Error: Spawn Table doesn't add up to 100!");
                return;
            }
        }
    }

    public void SyntheticSpawn()
    {
        int randomNumber = Random.Range(0, 101);

        //loop to check spawns
        for (int i = 0; i < syntheticSpawns.Count; i++)
        {
            if (randomNumber <= syntheticSpawns[i].rarity)
            {
                //check how many of the resource spawns
                int quantityDrop = Random.Range(syntheticSpawns[i].minQuantity, syntheticSpawns[i].maxQuantity);

                //give spawned resources to play and end loop
                Debug.Log("You have gotten " + quantityDrop + " " + syntheticSpawns[i].name + "!");
                CoreResourceAdd();
                return;
            }

            //cycle through spawn weights
            randomNumber -= syntheticSpawns[i].rarity;

            //errordebug
            if (randomNumber <= 0)
            {
                Debug.Log("Error: Spawn Table doesn't add up to 100!");
                return;
            }
        }
    }

    public void ElectronicSpawn()
    {
        int randomNumber = Random.Range(0, 101);

        //loop to check spawns
        for (int i = 0; i < electronicSpawns.Count; i++)
        {
            if (randomNumber <= electronicSpawns[i].rarity)
            {
                //check how many of the resource spawns
                int quantityDrop = Random.Range(electronicSpawns[i].minQuantity, electronicSpawns[i].maxQuantity);

                //give spawned resources to play and end loop
                Debug.Log("You have gotten " + quantityDrop + " " + electronicSpawns[i].name + "!");
                CoreResourceAdd();
                return;
            }

            //cycle through spawn weights
            randomNumber -= electronicSpawns[i].rarity;

            //errordebug
            if (randomNumber <= 0)
            {
                Debug.Log("Error: Spawn Table doesn't add up to 100!");
                return;
            }
        }
    }

    public void CoreResourceAdd()
    {
        //loop to give players core resources
        for (int i = 0; i < coreResource.Count; i++)
        {
            //check quantity to give
            int quantityDrop = Random.Range(coreResource[i].minQuantity, coreResource[i].maxQuantity);

            //give players core resources
            Debug.Log("You have gotten " + quantityDrop + " " + coreResource[i].name + ".");
        }

        return;
    }
}
