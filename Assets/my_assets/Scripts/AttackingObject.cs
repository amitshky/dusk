using UnityEngine;

public class AttackingObject : MonoBehaviour
{
	public float speed = 2f;
	public float damage = 2f;
	public float elevation = 0f;
	
    void Update()
    {
		transform.Translate(0, elevation, speed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		PlayerStats player = other.GetComponent<PlayerStats>();
		if (player != null)
		{
			player.Hurt(damage);
			Destroy(gameObject);
		}
		else
			Destroy(gameObject, 3f);
	}
}
