using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemThrow : MonoBehaviour
{
    Transform playerLocation;
    [HideInInspector] public Vector3 newLocation;
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public float moveDistance;
    [HideInInspector] public bool moveCheck = false;
    [HideInInspector] public float currentDistance;
    public float moveSpeed;
    void Update()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        if (Input.GetMouseButtonDown(1))
        {
            Throw();
        }
        if (currentDistance > 0.05 && moveCheck == true)//if (newLocation.x > this.transform.position.x || newLocation.y > this.transform.position.y || newLocation.z > this.transform.position.z && moveCheck)
        {
            this.transform.position += movementDirection * moveDistance * Time.deltaTime * moveSpeed;
            currentDistance = Vector3.Distance(newLocation, this.transform.position);
        }
        /*else if (currentDistance == 0)
        {
            moveCheck = false;
            moveDistance = 0;
            moveSpeed = 0;
        }*/
    }
    void Throw()
    {
        if (playerLocation.childCount == 1)
        {
            {
                this.transform.SetParent(null);
                var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                screenPoint.z = 10.0f;
                newLocation = Camera.main.ScreenToWorldPoint(screenPoint); //get target(mouse) location
                movementDirection = Vector3.Normalize(newLocation - this.transform.position);
                moveDistance = Vector3.Distance(newLocation, this.transform.position);
                currentDistance = Vector3.Distance(newLocation, this.transform.position);
                moveCheck = true;
            }
        }
    }
}