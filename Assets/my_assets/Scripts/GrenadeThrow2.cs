using UnityEngine;

public class GrenadeThrow2 : MonoBehaviour
{
	public float throwForce = 40f;
	public float coolDown = 3f;
	private float coolDownTimer = 0f;
	public GameObject grenadePrefab;


	// Update is called once per frame
	void Update()
	{

		if (coolDownTimer > 0f)
			coolDownTimer -= Time.deltaTime;
		if (coolDownTimer < 0f)
			coolDownTimer = 0f;
		if (Input.GetKeyDown("e") && coolDownTimer == 0f)
		{
			ThrowGrenade();
			coolDownTimer = coolDown;
		}
	}

	void ThrowGrenade()
	{
		GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
		Rigidbody rb = grenade.GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
	}
}