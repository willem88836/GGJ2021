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

	[SerializeField]
	private RectTransform mainUI;

	[SerializeField]
	private RectTransform ControlUI;

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

	public void Controls()
	{
		StartCoroutine(SwitchToControls());
	}

	private IEnumerator SwitchToControls()
	{
		if (_isBusy)
			yield break;

		_isBusy = true;

		float timer = 0;

		Vector3 centralPos = mainUI.position;
		Vector3 nextUIpos = centralPos + Vector3.right * Screen.width;
		Vector3 prevControlUIpos = centralPos - Vector3.right * Screen.width;

		while (timer < switchTime)
		{
			timer += Time.deltaTime;
			mainUI.position = Vector3.Lerp(centralPos, nextUIpos, timer);
			ControlUI.position = Vector3.Lerp(prevControlUIpos, centralPos, timer);

			yield return null;
		}

		_isBusy = false;
	}

	public void MainUI()
	{
		StartCoroutine(SwitchToMainUI());
	}

	private IEnumerator SwitchToMainUI()
	{
		if (_isBusy)
			yield break;

		_isBusy = true;

		float timer = 0;

		Vector3 centralPos = ControlUI.position;
		Vector3 prevUIpos = centralPos + Vector3.right * Screen.width;
		Vector3 nextControlUIpos = centralPos - Vector3.right * Screen.width;

		while (timer < switchTime)
		{
			timer += Time.deltaTime;
			mainUI.position = Vector3.Lerp(prevUIpos, centralPos, timer);
			ControlUI.position = Vector3.Lerp(centralPos, nextControlUIpos, timer);

			yield return null;
		}

		_isBusy = false;
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
