using UnityEngine;

public class Target : MonoBehaviour
{
	public float health = 80f;
	public float deathTime = 3f;

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject,deathTime);
	}
}
