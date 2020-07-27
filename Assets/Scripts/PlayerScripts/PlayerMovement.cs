using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float motionspeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        float moveHorizontal = inputHorizontal * motionspeed * Time.deltaTime;
        float moveVertical = inputVertical * motionspeed * Time.deltaTime;
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        transform.position += movement;
    }
}
