using UnityEngine;

public class Gun : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 80f;
	public float coolDown = 3f;
	public int rapidFireAmmo = 25;
	public float shootDelay = 0.4f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	public Animator anime;
	public AudioClip shootAudio;

	private AudioSource audioSource;

	private float nextTimeToFire = 0f;
	private float coolDownTimer = 0f;
	private int ammo = 0;
	private bool bFire = false;
	private bool rapidFire = false;
	private float shootDelayTimer = 0;

	private void Start()
	{
		anime = GetComponent<Animator>();
		ammo = rapidFireAmmo;
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
    {
		if (coolDownTimer > 0f)
			coolDownTimer -= Time.deltaTime;
		if (coolDownTimer < 0f)
			coolDownTimer = 0f;

		if(shootDelayTimer > 0f)
			shootDelayTimer -= Time.deltaTime;
		if (shootDelayTimer < 0f)
			shootDelayTimer = 0f;

		if (Input.GetButtonDown("Fire1") && shootDelayTimer == 0f)   //normal fire
		{
			bFire = true;
			Shoot();
			shootDelayTimer = shootDelay;
		}
		else if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire && ammo > 0) //rapid fire
		{
			rapidFire = true;
			ammo--;
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
			coolDownTimer = coolDown;
		}
		else
		{
			bFire = false;
			rapidFire = false;
		}
			
		if(coolDownTimer == 0)
			if (ammo == 0)
				ammo = rapidFireAmmo;

		updateAnimator();
    }

	void Shoot()
	{
		audioSource.PlayOneShot(shootAudio,1f);
		muzzleFlash.Play();
		RaycastHit hitInfo;
		// Bit shift the index of the layer 8 to get a bit mask
		int layerMask = 1 << 8;
		layerMask = ~layerMask; //every layer except layer 8

		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range, layerMask))
		{
			Debug.Log(hitInfo.transform.name);

			Target target = hitInfo.transform.GetComponent<Target>();
			if (target != null)
			{
				target.TakeDamage(damage);
			}

			if (hitInfo.rigidbody != null)
			{
				hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
			}

			GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
			Destroy(impactGO,1f); //impactGO is created everytime it is called, so we destroy it
		}
	}

	void updateAnimator()
	{
		anime.SetBool("bFire",bFire);
		anime.SetBool("RapidFire",rapidFire);
	}

	public int GetAmmo()
	{
		return ammo;
	}
}
