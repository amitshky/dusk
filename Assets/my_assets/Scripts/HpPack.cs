using UnityEngine;

public class HpPack : MonoBehaviour
{
	[SerializeField]private float hpIncrease = 30f;

	public PlayerStats playerHeatlth;
	public GameObject pickupEffect;
	private bool healed = false;

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0, 3, 0);
    }

	void pickup()
	{
		Instantiate(pickupEffect,transform.position, transform.rotation);
		playerHeatlth.getHealth(hpIncrease);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			pickup();
		}
	}
}
