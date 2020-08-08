using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    Transform playerLocation;
    int maxChildCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnMouseDown()
    {
        if (playerLocation.childCount < maxChildCount && this.tag == "Item")
        {
            transform.SetParent(playerLocation);
            transform.position = playerLocation.position + new Vector3(0.3f, 0.6f, 0);
            this.tag = "Item_pickedup";
        }
    }
}
