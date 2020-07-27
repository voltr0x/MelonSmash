using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemThrow : MonoBehaviour
{
        int itemsPickedup;
        GameObject currentItem;
        int countItemThrown;
        // Update is called once per frame
        void Update()
        {
            itemsPickedup = GameObject.FindGameObjectsWithTag("Item_pickedup").Length;
            currentItem = GameObject.FindGameObjectWithTag("Itemthrown");
            countItemThrown = GameObject.FindGameObjectsWithTag("Itemthrown").Length;

        if (Input.GetMouseButtonDown(1))
            {
                Throw();
            }
        }
        void Throw()
        {
            if (itemsPickedup >= 1)
            {
                {
                    this.transform.SetParent(null);
                    var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    screenPoint.z = 10.0f;
                    transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
                    this.tag = "Itemthrown";
                }
            }
        }
}