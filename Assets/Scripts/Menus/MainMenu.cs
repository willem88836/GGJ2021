using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private ObjectSpawner objectSpawner;

	[Space]
	[SerializeField]
	private GameObject floor;

	[SerializeField]
	private float switchTime;

	[Space]
	[SerializeField]
	private CanvasGroup fadePanel;

	[SerializeField]
	private float fadeTime;


	private bool _isBusy;

	private void Start()
	{
		StartCoroutine(objectSpawner.StartSpawnSequence());
	}

	public void SwitchScene(int nextScene)
	{
		StartCoroutine(SwitchToSceneSequence(nextScene));
	}

	public void Exit()
	{
		Application.Quit();
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

		floor.SetActive(false);

		float timer = 0;

		while (timer < switchTime)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		timer = 0;

		while (timer < fadeTime)
		{
			var percent = timer / fadeTime;
			fadePanel.alpha = percent;
			timer += Time.deltaTime;
			yield return null;
		}

		fadePanel.alpha = 1;

		loadAction.allowSceneActivation = true;
	}
}
