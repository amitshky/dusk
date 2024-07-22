using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[SerializeField]private float maxHealth = 100f;
	private float health;
	

	private void Start()
	{
		health = maxHealth;
	}

	public void Hurt(float damage)
	{
		health -= damage;
		Debug.Log("Health: " + health);
		if (health <= 0f)
		{
			Debug.Log("You Died!!!");
			FindObjectOfType<GameManager>().EndGame();
		}
	}
	public float GetHealthPct()
	{
		return (float)health / maxHealth;
	}

	public void getHealth(float amount)
	{
		if ((health + amount) <= maxHealth)
			health += amount;
		else if ((health + amount) > maxHealth)
			health = maxHealth;
	}
}
