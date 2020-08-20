using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* This script is referenced from Unity forum
	https://forum.unity.com/threads/health-bar-over-enemy.26014/
*/
public class health_bar : MonoBehaviour
{
	public enemy_main enemy_main_script;
	public int maxHealth;
    public int curHealth;
   	private int testCounter;
    public float healthBarLength;
   
    // Use this for initialization
    void Start () {
    	maxHealth = enemy_main_script.health;
    	curHealth = maxHealth;
        healthBarLength = Screen.width / 6;
        testCounter = 100;
    }
   
    // Update is called once per frame
    void Update () {
        AddjustCurrentHealth(0);
    }
   
	void OnGUI()
    {
        Vector2 targetPos;
	    targetPos = Camera.main.WorldToScreenPoint (transform.position);
	    GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y + 20, 60, 20), curHealth + "/" + maxHealth);
       
    }

    public void AddjustCurrentHealth(int adj) {
        curHealth += adj;
       
        if (curHealth < 0)
            curHealth = 0;
       
        if (curHealth > maxHealth)
            curHealth = maxHealth;
       
        if(maxHealth < 1)
            maxHealth = 1;
       
        healthBarLength = (Screen.width / 6) * (curHealth /(float)maxHealth);
    }
}