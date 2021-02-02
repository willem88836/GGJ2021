using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HighscoreManager : MonoBehaviour
{
	[SerializeField] GameObject _scorepanel;

	[Space]
	[SerializeField] GameObject _newScorePanel;
	[SerializeField] Text _scoreText;
	[SerializeField] Text _date;
	[SerializeField] InputField _inputField;

	[SerializeField] Text[] _nameTexts;
	[SerializeField] Text[] _scoreTexts;
	[Space]
	[SerializeField] Text _newScoreText;

	[Space]
	[SerializeField] float _sceneSwitchTime;
	[SerializeField] float _fadeTime;
	[SerializeField] CanvasGroup _fadePanel;

	[Space]
	[SerializeField] ObjectSpawner[] _objectSpawners;

	HighScore[] _highScores;

	int _newScore = 0;
	string _newScoreName = "";

	bool _isBusy;

	void Start()
	{
		_scorepanel.SetActive(false);

		foreach (ObjectSpawner os in _objectSpawners)
			StartCoroutine(os.StartSpawnSequence());

		_highScores = HighscoreData.LoadScores();
		_newScore = HighscoreData.CurrentScore;

		if (_newScore > _highScores[0].Score)
			OpenNewScorePanel();
		else
			DisplayHighScores();
	}

	void OpenNewScorePanel()
	{
		_newScorePanel.SetActive(true);

		_scoreText.text = _newScore.ToString();
		string date = System.DateTime.Now.ToString();
		string dateNoTime = date.Split(' ')[0];
		_date.text = dateNoTime;
	}

	public void CloseNewScorePanel()
	{
		_newScorePanel.SetActive(false);

		_newScoreName = _inputField.text;
		SetNewHighscore();
		DisplayHighScores();
	}

	HighScore[] SortHighscores()
	{
		HighScore[] sortedScores = _highScores.OrderBy(x => x.Score).ToArray();
		return sortedScores;
	}

	void DisplayCurrentScore()
	{
		_newScoreText.text = _newScore.ToString();
	}

	void DisplayHighScores()
	{
		_scorepanel.SetActive(true);

		DisplayCurrentScore();

		for (int i = 0; i < 5; i++)
		{
			_nameTexts[i].text = _highScores[i].Name;
			_scoreTexts[i].text = _highScores[i].Score.ToString();
		}
	}

	public void SetNewHighscore()
	{
		_highScores[0].Name = _newScoreName;
		_highScores[0].Score= _newScore;

		_highScores = SortHighscores();

		HighscoreData.SaveHighscores(_highScores);
	}

	public void SwitchScene(int nextScene)
	{
		HighscoreData.CurrentScore = 0;
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

		float timer = 0;

		while (timer < _sceneSwitchTime)
		{
			timer += Time.deltaTime;
			yield return null;
		}

		timer = 0;

		while (timer < _fadeTime)
		{
			var percent = timer / _fadeTime;
			_fadePanel.alpha = percent;
			timer += Time.deltaTime;
			yield return null;
		}

		_fadePanel.alpha = 1;

		loadAction.allowSceneActivation = true;
	}
}
