using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_main : MonoBehaviour
{
    public  float          enemy_speed;
    public  enemy_target   where_to_go;
    private Vector3        enemy_move_direction;
    private Vector3        enemy_attracted_pos;
    public  int            health = 10;
<<<<<<< HEAD
    [HideInInspector] public GameObject scriptAObject;
    ItemThrowVersion2 scriptA;
=======

    
    public  const int      ENEMY_NORMAL     = 1;
    public  const int      ENEMY_KICKED     = 2;
    public  const int      ENEMY_STUNNED    = 3;
    public  const int      ENEMY_ATTRACTED  = 4;

    private int            stunned_effect_second   = 3;
    private int            attracted_effect_second = 3;

    private int            state;
    private float          stunned_range = 0.3f;

    private bool           effect_started = false;
    private bool           effect_ended   = false;

>>>>>>> b295b9409fd0136d8133519f9d1c18a6e43b1526
    // Start is called before the first frame update
    void Start()
    {  
        state = ENEMY_NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        // for test only
        
        if(Input.GetKeyDown(KeyCode.N))      { state = ENEMY_NORMAL; }
        else if(Input.GetKeyDown(KeyCode.K)) { state = ENEMY_KICKED; }
        else if(Input.GetKeyDown(KeyCode.S)) { being_stunned();}
        else if(Input.GetKeyDown(KeyCode.A)) { state = ENEMY_ATTRACTED; }
        
        switch(state)
        {
            case ENEMY_NORMAL:
                // find the current target dir
                where_to_go = where_to_go.update_target(this);
                enemy_move_direction     =   Vector3.Normalize( where_to_go.transform.position - this.transform.position );
                // move toward that direction
                this.transform.position +=  enemy_speed * enemy_move_direction * Time.deltaTime;
                break;
            case ENEMY_KICKED:
                /* TODO
                 * Get kicked direction from character script, fly to that direction
                 * then self-destruction
                 * note that if an enemy is in "kicked" state
                 * it can never be in another state (ie. cannot be stunned when flying)
                 */
                break;
            case ENEMY_STUNNED:
                this.transform.position = this.transform.position + (stunned_range * new Vector3(1, 0, 0));
                stunned_range = -stunned_range;
                break;
            case ENEMY_ATTRACTED:
                // change the current target to the melon
                enemy_move_direction     =   Vector3.Normalize( enemy_attracted_pos - this.transform.position );
                // move toward that direction
                this.transform.position +=  enemy_speed * enemy_move_direction * Time.deltaTime;
                break;
            default:
                state = ENEMY_NORMAL;
                break;

            
        }
        // if effect ended, back to normal
        if(effect_ended) {
            effect_ended   = false;
            state          = ENEMY_NORMAL;
            Debug.Log("back to normal");
        }
        
        // if died or reached the end point, self-destruction
        if(health <= 0 || where_to_go.tag == "objectDestroyer")
        {
            Destroy(this.gameObject);
<<<<<<< HEAD
        }
        else
        {
            enemy_move_direction     =   Vector3.Normalize( where_to_go.transform.position - this.transform.position );
            // move toward that direction
            this.transform.position +=  enemy_speed * enemy_move_direction * Time.deltaTime;
        }
    }

    void take_damage(int damage)
    {
=======
        }
    }

    // after x seconds, a effect_ended signal will be released
    // then enemy will back to normal
    IEnumerator wait_for_seconds(int seconds)
    {
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(seconds);
        // then set the flag
        effect_ended = true;
    }

    void being_attracted(Vector3 melon_pos) {
        state = ENEMY_ATTRACTED;
        enemy_attracted_pos = melon_pos;
        StartCoroutine(wait_for_seconds(attracted_effect_second));
    }
    void being_stunned () {
        state = ENEMY_STUNNED;
        // make the enemy back to normal after x seconds
        StartCoroutine(wait_for_seconds(stunned_effect_second));
    }
    // get kicked direction from character script
    void being_kicked(Vector3 kicked_direction) {
        state = ENEMY_KICKED;
        enemy_move_direction = Vector3.Normalize(kicked_direction);
    }
    
    void take_damage(int damage) {
>>>>>>> b295b9409fd0136d8133519f9d1c18a6e43b1526
        health -= damage;
        Debug.Log("Uhhhhh " + health);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Itemthrown")
        {
            scriptAObject = collider.gameObject;
            scriptA = scriptAObject.GetComponent<ItemThrowVersion2>();
            scriptA.itemExploded2 = true;
            if (scriptA.itemExploded2 == true && scriptA.itemAliveTime <= 0.5)
            {
                take_damage(scriptA.damage);
                scriptA.itemExploded2 = false;
            }
        }
    }
}
