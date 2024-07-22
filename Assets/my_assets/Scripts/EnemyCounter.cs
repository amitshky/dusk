using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
	int enemyCounter = 0;
	public Text scoreText;

	// Update is called once per frame
	void Update()
    {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyCounter = enemies.Length;
		Debug.Log("Enemies Left:" + enemyCounter);
		scoreText.text = "Enemies Left: " + enemyCounter;
		if (enemyCounter == 0)
			FindObjectOfType<GameManager>().Victory();
    }
}
