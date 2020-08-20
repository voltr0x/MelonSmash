using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float motionspeed = 1;
    [HideInInspector] public bool kickLeft = true;
    public Animator animator;
    [HideInInspector]public bool isMoving;
    public PlayerKick2 playerKick2;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerKick2.isKicking == false)
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");
            float moveHorizontal = inputHorizontal * motionspeed * Time.deltaTime;
            float moveVertical = inputVertical * motionspeed * Time.deltaTime;
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            transform.position += movement;
            animator.SetFloat("Horizontal", inputHorizontal);
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            animator.SetBool("Move", isMoving);
            spriteDirection();
        }
    }

    public void spriteDirection()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (Input.GetKey(KeyCode.A))
        {
            kickLeft = true;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            kickLeft = false;
        }
    }
}
