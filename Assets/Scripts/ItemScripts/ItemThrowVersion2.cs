using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowVersion2 : MonoBehaviour
{
    Transform playerLocation;
    [HideInInspector] public Vector3 newLocation;
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public float moveDistance;
    [HideInInspector] public float itemMoveTime = 0, itemStopTime;
    public float damageRadius;
    [HideInInspector] public bool itemExploded = false, itemExploded2;
    public float moveSpeed = 1;
    public int damage = 4;
    public float itemAliveTime = 0;
    public float itemDeleteTime = 10;

    void Update()
    {

        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        if (Input.GetMouseButtonDown(1))
        {
            Throw();
        }
        if (itemMoveTime < itemStopTime && this.tag == "Itemthrown")
        {
            this.transform.position += movementDirection * Time.deltaTime * moveSpeed;
            itemMoveTime += Time.deltaTime;
        }
        else if (itemMoveTime >= itemStopTime && this.tag == "Itemthrown")
        {
            {
                /*if (!itemExploded)
                {
                    ExplosionDamage(this.transform.position, damageRadius);
                    itemExploded = true;
                    itemExploded2 = true;
                }*/
                itemAliveTime += Time.deltaTime;
                /*if (this.tag == "Itemthrown")
                {
                    ExplosionDamage(this.transform.position, damageRadius);
                }*/
                if (itemAliveTime >= itemDeleteTime)
                {
                    Destroy(this.gameObject);
                    itemAliveTime = 0;
                }

            }
        }
    }
    void Throw()
    {
        if (playerLocation.childCount == 1 && playerLocation.GetChild(0) == this.transform && this.tag == "Item_pickedup")
        {
            {
                this.transform.SetParent(null);
                var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                screenPoint.z = 10.0f;
                newLocation = Camera.main.ScreenToWorldPoint(screenPoint); //get target(mouse) location
                movementDirection = Vector3.Normalize(newLocation - this.transform.position);
                moveDistance = Vector3.Distance(newLocation, this.transform.position);
                itemStopTime = moveDistance / moveSpeed;
                this.tag = "Itemthrown";
            }
        }
    }
}
    /*
        // deal damage
        ExplosionDamage(this.transform.position, 5.0f);
        Destroy(this.gameObject);
    */
    /*void ExplosionDamage(Vector3 center, float radius)
    {
        Debug.Log("exploded");
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            hitColliders[i].SendMessage("take_damage", damage);
            i++;
        }
    }
}*/