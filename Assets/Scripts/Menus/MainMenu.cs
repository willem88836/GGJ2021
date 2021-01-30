using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void SwitchScene(int nextScene)
	{
		SceneManager.LoadScene(nextScene);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
