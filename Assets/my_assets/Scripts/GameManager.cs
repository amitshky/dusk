using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool gameOver = false;

	public float restartDelay = 1f;

	public GameObject gameOverUI;
	public GameObject youWonUI;
	public GameObject FPC;  // Assign the First Person Controller to this in the Editor.

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha9))
			Restart();	
	}

	public void EndGame()
	{
		Time.timeScale = 0f;
		gameOverUI.SetActive(true);
		if (!gameOver)
		{
			Time.timeScale = 0f;
			FPC.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_cursorIsLocked = false;
			Debug.Log("Game Over");
			Invoke("Restart",restartDelay);

			gameOver = true;
		}
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Victory()
	{
		Time.timeScale = 0f;
		youWonUI.SetActive(true);
	}
}
