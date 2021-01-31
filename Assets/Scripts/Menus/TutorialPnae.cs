using UnityEngine;

public class TutorialPnae : MonoBehaviour
{
	[SerializeField] private LevelManager levelManager;

	public void Start()
	{
		Time.timeScale = 0;
	}

	public void Stop()
	{
		Time.timeScale = 1;
		gameObject.SetActive(false);
		levelManager.BeginGame();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Stop();
		}
	}
}
