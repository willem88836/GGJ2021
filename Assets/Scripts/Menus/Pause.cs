using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	[SerializeField] GameObject _menu;
	[SerializeField] CanvasGroup _fadePanel;

	bool _isBusy;


	void Update()
    {
		if (_isBusy)
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
				PauseGame();
			else
				UnPause();
		}
    }

	void PauseGame()
	{
		Time.timeScale = 0;
		_menu.SetActive(true);
	}

	public void UnPause()
	{
		if (_isBusy)
			return;

		Time.timeScale = 1;
		_menu.SetActive(false);
	}

	public void BackToMenu()
	{
		StartCoroutine(SwitchToSceneSequence(0));
	}

	private IEnumerator SwitchToSceneSequence(int nextScene)
	{
		if (_isBusy)
		{
			yield break;
		}

		_isBusy = true;

		var loadAction = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
		loadAction.allowSceneActivation = false;

		float timer = 0;

		while (timer < 1)
		{
			var percent = timer / 1;
			_fadePanel.alpha = percent;
			timer += Time.unscaledDeltaTime;
			yield return null;
		}

		_fadePanel.alpha = 1;

		loadAction.allowSceneActivation = true;
	}
}
