using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupCollision : MonoBehaviour
{
    public PlayerMovement scriptA;
    [HideInInspector] public GameObject publicCollider;
    [HideInInspector] public Collider2D colliderActive;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount != 0)
        {
            if (scriptA.kickLeft == true)
            {
                publicCollider.transform.position = this.transform.position + new Vector3(-0.18f, -0.121f, -0.01f);
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                {
                    colliderActive.enabled = true;
                }
            }
            else if (scriptA.kickLeft == false)
            {
                publicCollider.transform.position = this.transform.position + new Vector3(0.142f, -0.101f, -0.01f);
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                {
                    colliderActive.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.transform.childCount == 0 && collider.gameObject.tag == "Item")
        {
            publicCollider = collider.gameObject;
            colliderActive = publicCollider.GetComponent<CircleCollider2D>();
            collider.transform.SetParent(this.transform);
            collider.gameObject.tag = "Item_pickedup";
            if (scriptA.kickLeft == true)
            {
                collider.transform.position = this.transform.position + new Vector3(-0.18f, -0.121f, -0.01f);
                colliderActive.enabled = false;
            }
            else if (scriptA.kickLeft == false)
            {
                collider.transform.position = this.transform.position + new Vector3(0.142f, -0.101f, -0.01f);
                colliderActive.enabled = false;
            }

        }
    }
}
