using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreManager : MonoBehaviour
{
	[SerializeField] GameObject _newScorePanel;
	[SerializeField] InputField _inputField;

	[SerializeField] Text[] _nameTexts;
	[SerializeField] Text[] _scoreTexts;
	[Space]
	[SerializeField] Text _newScoreText;

	HighScore[] _highScores;

	int _newScore = 0;
	string _newScoreName = "";

	void Start()
	{
		_highScores = HighscoreData.LoadScores();
		_newScore = HighscoreData.CurrentScore;
		DisplayHighScores();

		if (_newScore > _highScores[0].Score)
			OpenNewScorePanel();
	}

	void OpenNewScorePanel()
	{
		_newScorePanel.SetActive(true);
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
}
