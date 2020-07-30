using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_effects : MonoBehaviour
{
	public int damage = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplosionDamage(Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length) {
            hitColliders[i].SendMessage("take_damage", damage);
            i++;
        }
    }
}
