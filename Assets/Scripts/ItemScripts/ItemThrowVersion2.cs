using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowVersion2 : MonoBehaviour
{
    Transform playerLocation;
    [HideInInspector] public Vector3 newLocation;
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public float moveDistance;
    [HideInInspector] public bool moveCheck = false;
    [HideInInspector] public float currentDistance;
    public float maxVariance = 0.07f;
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
        if (currentDistance >= maxVariance)
        {
            this.transform.position += movementDirection * moveDistance * Time.deltaTime * moveSpeed;
            currentDistance = Vector3.Distance(newLocation, this.transform.position);
        }
        else if (this.tag == "Itemthrown")
        {
            {
                itemAliveTime += Time.deltaTime;
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
                currentDistance = Vector3.Distance(newLocation, this.transform.position);
                this.tag = "Itemthrown";
            }
        }
    }
    /*
        // deal damage
        ExplosionDamage(this.transform.position, 5.0f);
        Destroy(this.gameObject);
    */
    void ExplosionDamage(Vector3 center, float radius)
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
}