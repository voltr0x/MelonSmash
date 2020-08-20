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
    [HideInInspector] public GameObject scriptAObject;
    ItemThrowVersion2 scriptA;
    public float timeForDamage;
    
    public  const int      ENEMY_NORMAL     = 1;
    public  const int      ENEMY_KICKED     = 2;
    public  const int      ENEMY_STUNNED    = 3;
    public  const int      ENEMY_ATTRACTED  = 4;
    public  const int      ENEMY_DIED       = 5;

    private int            stunned_effect_second   = 3;
    private int            attracted_effect_second = 3;
    [HideInInspector] public int             kicked_effect_second    = 3;

    /*For animations*/
    public  SpriteRenderer sprite_renderer;
    public  Sprite         old_sprite;
    public  Sprite         new_sprite;
    public  Sprite         dead_sprite;
    private float          sprite_change_interval;
    private int            sprite_change_counter = 0;
    private bool           sprite_switcher = true;
    private bool           moving_right = false;
    private bool           facing_right = false;
    private float          rotate_angle = 10;
    
    //Size of the enemy
    private float enemyScale = 0.08f;

    /*For special effects*/
    private int            state;
    private float          stunned_range = 0.3f;
    private bool           effect_started = false;
    private bool           effect_ended   = false;

    /*For sound effect*/
    public AudioSource enemy_sound;
    public AudioClip eating_sound;
    private bool       sound_played = false;
    // Start is called before the first frame update
    void Start()
    {  
        state = ENEMY_NORMAL;
        sprite_change_interval = enemy_speed * 20;
        enemy_sound.clip = eating_sound;
    }

    // Update is called once per frame
    void Update()
    {
        // for test only
        if(Input.GetKeyDown(KeyCode.N))      { state = ENEMY_NORMAL; }
        else if(Input.GetKeyDown(KeyCode.K)) { state = ENEMY_KICKED; }
        else if(Input.GetKeyDown(KeyCode.J)) { being_stunned();}
        else if(Input.GetKeyDown(KeyCode.L)) { state = ENEMY_ATTRACTED; }

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
                // enemy_speed ^ 2 means flying faster than walking, also represent the
                // enemies' weight (walk slower -> heavier -> fly slower)
                this.transform.position += enemy_speed * enemy_speed * enemy_move_direction * Time.deltaTime;
                this.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.z + rotate_angle);
                rotate_angle += 10;
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

        if(state != ENEMY_DIED){
            /***************************** Moving Animation *****************************/
            // change sprite for animation
            sprite_change_counter++;
            if(sprite_change_counter >= sprite_change_interval) 
            {
                if(sprite_switcher) sprite_renderer.sprite = new_sprite;
                else sprite_renderer.sprite = old_sprite;
                // reset counter
                sprite_switcher = !sprite_switcher;
                sprite_change_counter = 0;
            }

            // detrmine moving direction
            if(enemy_move_direction.x > 0) {
                moving_right = true;
            } else moving_right = false;

            // determine if flip is needed
            // if moving right but not facing right, flip sprite
            if(moving_right && !facing_right) {
                this.transform.localScale = new Vector3(-enemyScale, enemyScale, 1);
                facing_right = true; // now it is facing right
            }
            // if moving left but facing right, flip
            else if(!moving_right && facing_right) {
                this.transform.localScale = new Vector3(enemyScale, enemyScale, 1);
                facing_right = false;
            }


            // if effect ended, back to normal
            if(effect_ended) {
                effect_ended   = false;
                state          = ENEMY_NORMAL;
                Debug.Log("back to normal");
            }
            
            // if died or reached the end point, self-destruction
            if(health <= 0)
            {
                state = ENEMY_DIED;
                sprite_renderer.sprite = dead_sprite;
                if(facing_right) this.transform.localScale = new Vector3(-enemyScale, enemyScale, 1);
                else this.transform.localScale = new Vector3(enemyScale, enemyScale, 1);
                StartCoroutine(die_animation(1));
            }

            if(where_to_go.tag == "objectDestroyer") 
            {
                if(!sound_played) StartCoroutine(die_sound(2));
                sound_played = true;
            }
        }
    }

    IEnumerator die_sound(int seconds)
    {
        enemy_sound.Play();
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
    
    IEnumerator die_animation(int seconds)
    {
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
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

    // after being kicked for x seconds (estimated time for enemy fly out
    // of the screen), destroy the object
    IEnumerator kicked_for_seconds(int seconds)
    {
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(seconds);
        // then self destruction
        Destroy(this.gameObject);
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
    public void being_kicked(Vector3 kicked_direction) {
        state = ENEMY_KICKED;
        enemy_move_direction = Vector3.Normalize(kicked_direction);
        StartCoroutine(kicked_for_seconds(kicked_effect_second));
    }
    
    void take_damage(int damage) {
        health -= damage;
        Debug.Log("Uhhhhh " + health);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Itemthrown")
        {
            scriptAObject = collider.gameObject;
            scriptA = scriptAObject.GetComponent<ItemThrowVersion2>();
            scriptA.itemExploded2 = true;
            if (scriptA.itemExploded2 == true && scriptA.itemAliveTime <= timeForDamage && scriptA.itemAliveTime > 0)
            {
                scriptA.itemExploded2 = false;
                take_damage(scriptA.damage);
            }
        }
    }
}
