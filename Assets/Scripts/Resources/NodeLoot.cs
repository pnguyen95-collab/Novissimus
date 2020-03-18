using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLoot : MonoBehaviour
{
    [System.Serializable]
    public class ResourceTable
    {
        public Item.Type itemType;
        public int rarity;
        public int minQuantity;
        public int maxQuantity;
    }

    public List<ResourceTable> metalSpawns;
    public List<ResourceTable> syntheticSpawns;
    public List<ResourceTable> electronicSpawns;
    public List<ResourceTable> coreResource;
    public GameManager gmCode;

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

                //set the type and amount values for the player to add to inventory
                Item spawnedItem = new Item();
                spawnedItem.type = metalSpawns[i].itemType;
                spawnedItem.amount = quantityDrop;

                //give spawned resources to player and end loop
                gmCode.inventory.AddItem(spawnedItem);
                gmCode.AddMessage("You have gotten " + quantityDrop + " " + metalSpawns[i].itemType + "!", Color.yellow);
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

                //set the type and amount values for the player to add to inventory
                Item spawnedItem = new Item();
                spawnedItem.type = syntheticSpawns[i].itemType;
                spawnedItem.amount = quantityDrop;

                //give spawned resources to player and end loop
                gmCode.inventory.AddItem(spawnedItem);
                gmCode.AddMessage("You have gotten " + quantityDrop + " " + syntheticSpawns[i].itemType + "!", Color.yellow);
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

                //set the type and amount values for the player to add to inventory
                Item spawnedItem = new Item();
                spawnedItem.type = electronicSpawns[i].itemType;
                spawnedItem.amount = quantityDrop;

                //give spawned resources to player and end loop
                gmCode.inventory.AddItem(spawnedItem);
                gmCode.AddMessage("You have gotten " + quantityDrop + " " + electronicSpawns[i].itemType + "!", Color.yellow);
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

            //set the type and amount values for the player to add to inventory
            Item spawnedItem = new Item();
            spawnedItem.type = coreResource[i].itemType;
            spawnedItem.amount = quantityDrop;

            //give player core resources
            gmCode.inventory.AddItem(spawnedItem);
            gmCode.AddMessage("You have gotten " + quantityDrop + " " + coreResource[i].itemType + ".", Color.yellow);
        }

        return;
    }
}
