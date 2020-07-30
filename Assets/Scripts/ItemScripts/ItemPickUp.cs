using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    Transform playerLocation;
    int itemCheck;
    public void Update()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        itemCheck = GameObject.FindGameObjectsWithTag("Item_pickedup").Length;
    }

    void OnMouseDown()
    {
        if (this.tag == "Item" && itemCheck < 1)
        {
            this.transform.SetParent(playerLocation);
            this.transform.position = playerLocation.position + new Vector3(0.3f, 0.6f, 0);
            this.tag = "Item_pickedup";
        }
    }
}
