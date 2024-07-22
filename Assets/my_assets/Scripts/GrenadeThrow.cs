﻿using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
      GameObject grenade =   Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
