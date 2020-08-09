using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick2 : MonoBehaviour
{
    public float rayLength = 10;
    Ray rayCurrentDirection;
    public GameObject scriptAObject;
    enemy_main scriptA;
    [HideInInspector] public Vector3 newLocation, kickedDirection;
    public float kickCooldown, cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        kickCooldown = cooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer();
        if (Input.GetMouseButtonDown(0))
        {            
            Debug.Log("MouseDown");
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            screenPoint.z = 10.0f;
            newLocation = Camera.main.ScreenToWorldPoint(screenPoint);
            kickedDirection = Vector3.Normalize(newLocation - this.transform.position);
            Debug.Log(kickedDirection);
            Debug.DrawRay(this.transform.position, kickedDirection * rayLength, Color.black);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, kickedDirection, rayLength);
            if (hit.collider.tag == "Enemy")
            {
                scriptAObject = hit.collider.gameObject;
                scriptA = scriptAObject.GetComponent<enemy_main>();
                if (kickCooldown > cooldownTimer)
                {
                    Debug.Log("Kicking");
                    scriptA.being_kicked(kickedDirection);
                    kickCooldown = 0;
                }
            }
        }
    }

    void timer()
    {
        if (kickCooldown <= cooldownTimer)
        {
            kickCooldown += Time.deltaTime;
        }
    }
}
