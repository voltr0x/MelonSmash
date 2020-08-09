using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    Ray rayDirection;
    Vector3 inputVertical = new Vector3(0, 1, 0), inputHorizontal = new Vector3(1, 0, 0);
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rayDirection = new Ray(this.transform.position, inputHorizontal * Input.GetAxis("Horizontal"));
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            rayDirection = new Ray(this.transform.position, inputVertical * Input.GetAxis("Vertical"));
        }
        
        Physics.Raycast(rayDirection, 10);
    }
}
