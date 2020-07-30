using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    int itemsPickedup; 
    public float itemDeleteTime = 10;
    public float itemAliveTime = 0;
    GameObject currentItem;
    int countItemThrown;
    public int damage = 4;
    // Update is called once per frame
    void Update()
    {
        itemsPickedup = GameObject.FindGameObjectsWithTag("Item_pickedup").Length;
        
        if (Input.GetMouseButtonDown(1) && this.tag == "Item_pickedup")
        {
            Throw ();
            currentItem = GameObject.FindGameObjectWithTag(this.tag);
            countItemThrown = GameObject.FindGameObjectsWithTag(this.tag).Length;
        }
        if (countItemThrown >= 1)
        {
            itemAliveTime += Time.deltaTime;
            if (itemAliveTime >= itemDeleteTime)
            {
                // deal damage
                ExplosionDamage(this.transform.position, 5.0f);
                Destroy(currentItem);
                itemAliveTime = 0;
            }
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
                    this.tag = null;
                }
            }
    }

    void ExplosionDamage(Vector3 center, float radius) {
        Debug.Log("exploded");
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            hitColliders[i].SendMessage("take_damage", damage);
            i++;
        }
    }
}
/*public class ItemThrow : MonoBehaviour
{
    int itemsPickedup;
public float itemDeleteTime = 10;
public float itemAliveTime = 0;
GameObject currentItem;
int countItemThrown;
string tagItemThrown;
int i = 0;
//date is called once per frame
void Update()
{
    itemsPickedup = GameObject.FindGameObjectsWithTag("Item_pickedup").Length;
    currentItem = GameObject.FindGameObjectWithTag(tagItemThrown);
    countItemThrown = GameObject.FindGameObjectsWithTag("Itemthrown").Length;
    if (Input.GetMouseButtonDown(1))
    {
        Throw();
    }
    if (countItemThrown >= 1)
    {
        itemAliveTime += Time.deltaTime;
        if (itemAliveTime >= itemDeleteTime)
        {
            Destroy(currentItem);
            itemAliveTime = 0;
        }
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
            tagItemThrown = string.Concat("Itemthrown", i.ToString());
            this.tag = tagItemThrown;
            i++;
        }
    }
}
}*/