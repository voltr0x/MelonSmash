using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.transform.childCount == 0 && collider.gameObject.tag == "Item")
        {
            collider.transform.SetParent(this.transform);
            collider.transform.position = this.transform.position + new Vector3(0, 0.51f, 0);
            collider.gameObject.tag = "Item_pickedup";
        }
    }
}
