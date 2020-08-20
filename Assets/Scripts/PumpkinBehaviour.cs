using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    public SpriteRenderer sprite_renderer;
    public Sprite[] sprites;
    private int bites;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bites = 0;
        sprite_renderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameManager.PumpkinEaten(1);
            
            bites++;
            sprite_renderer.sprite = sprites[bites];
        }
    }
}
