using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_target : MonoBehaviour
{
	public  enemy_target 	next_target;
	public 	float 			arrival_radius;

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ask a node for new target
    public enemy_target update_target( enemy_main curr_enemy )
    {
        // determine distance between an enemy and the target
    	float distance = Vector3.Distance(curr_enemy.transform.position, this.transform.position);
    	// If the enemy has arrived, guide to the next target
    	if(distance > arrival_radius || !next_target)
    	{
    		return this;
    	}
    	return next_target;
    }
}
