using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    public float rayLength = 10;
    Ray raySetDirection, rayCurrentDirection;
    Vector3 inputVertical, inputHorizontal, kickedDirection;
    public GameObject scriptAObject;
    enemy_main scriptA;
    
    // Start is called before the first frame update
    void Start()
    {
        raySetDirection = new Ray(this.transform.position, Vector3.down);
    }

    // Update is called once per frame
    void Update()
    {
        inputVertical = new Vector3(0, 1, 0) * Input.GetAxis("Vertical");
        inputHorizontal = new Vector3(1, 0, 0) * Input.GetAxis("Horizontal");
        RaycastHit hit;
        if (Input.GetAxis("Horizontal") != 0)
        {
            raySetDirection = new Ray(this.transform.position, inputHorizontal);
            kickedDirection = inputHorizontal;
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            raySetDirection = new Ray(this.transform.position, inputVertical);
            kickedDirection = inputVertical;
        }
        rayCurrentDirection = raySetDirection;
        
        if (Physics.Raycast(rayCurrentDirection, out hit, rayLength))
        {
            if (hit.collider.tag == "Enemy" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Enemy Kicked");
                scriptAObject = hit.collider.gameObject;
                scriptA = scriptAObject.GetComponent<enemy_main>();
                scriptA.being_kicked(kickedDirection);
            }
        }
    }
}
