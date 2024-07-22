using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    bool hasExploded = false;
    public float radius = 5f;
    public float force = 700f;
    public GameObject explosionEffect;

    public float explosionEffect_endTime = 2f;

    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode ()
    {
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
			Destructable dest =  nearbyObject.GetComponent<Destructable>();
            if(dest != null)
            {
                dest.Destroy();
            }

        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
		
        foreach(Collider nearbyObject in collidersToMove)
        {
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //remove 
        Destroy(gameObject);

        GameObject instantiatedObj = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(instantiatedObj,explosionEffect_endTime);
    }
}
