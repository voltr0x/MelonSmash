using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_main : MonoBehaviour
{
	public  float          enemy_speed;
	public  enemy_target   where_to_go;
	private Vector3        enemy_move_direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // find the current target dir
        where_to_go = where_to_go.update_target(this);
        if (where_to_go) {
            enemy_move_direction     =   Vector3.Normalize( where_to_go.transform.position - this.transform.position );
            // move toward that direction
            this.transform.position +=  enemy_speed * enemy_move_direction * Time.deltaTime;
        }
    }
}
