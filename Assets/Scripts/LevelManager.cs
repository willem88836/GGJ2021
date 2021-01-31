using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private AnimationCurve levelTime;
	[SerializeField] private AnimationCurve levelScore;
	[SerializeField] private AnimationCurve mailCurve;
	[SerializeField] private int maxStrikes = 3;
	[SerializeField] private float restartLevelDelay;
	[SerializeField] private float pointCaptureDelay;

	[Header("References")]
	[SerializeField] private ScoreController scoreController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private StrikeController strikeController;
	[SerializeField] private LeverController levelController;
	[SerializeField] private Hatch[] hatches;
	[SerializeField] private Truck truck;

	[Header("Debug")]
	[SerializeField] private bool debugMode;
	[SerializeField] private KeyCode skipTimeKey;


	private bool timeIsRunning;

	private int totalScore;
	private int score;
	private int strikes;
	private int level;


	private void Start()
	{
		foreach(Hatch hatch in hatches)
		{
			hatch.setLevelManager(this);
		}

		//BeginGame();
	}

	public void BeginGame()
	{
		StartCoroutine(LevelProgression());
	}

	private IEnumerator<YieldInstruction> LevelProgression()
	{
		Debug.Log("Game Started");

		level = 0;
		strikes = 0;
		score = 0;
		totalScore = 0;
		timeIsRunning = true;

		UpdateScore();
		strikeController.UpdateStrikes(strikes);

		while (timeIsRunning)
		{
			LevelStarted();
			levelController.UpdateLevel(level + 2);

			int time = levelTime.SafeEvaluate(level);

			for (int currentTime = 0; currentTime < time; currentTime++)
			{
				timeController.UpdateTimer(time - currentTime);
				UpdateScore();
				yield return new WaitForSeconds(1);

				if(debugMode && Input.GetKey(skipTimeKey)) break;
			}

			timeController.UpdateTimer(0);

			foreach(Hatch hatch in hatches)
			{
				hatch.StartHatches();
			}

			yield return new WaitForSeconds(pointCaptureDelay);

			LevelEnded();
			level++;

			yield return new WaitForSeconds(restartLevelDelay);
		}

		GameOver();
	}


	public void AddScore(int increment)
	{
		score += increment;
		UpdateScore();
	}

	private void UpdateScore()
	{
		int goal = levelScore.SafeEvaluate(level);
		float ratio = (float)score / goal;
		scoreController.UpdateScore(totalScore + score, totalScore + goal, ratio);
	}

	private void LevelStarted()
	{
		int mailCount = mailCurve.SafeEvaluate(level);
		truck.StartNextRound(mailCount);

	}

	private void LevelEnded()
	{
		if (score < levelScore.SafeEvaluate(level)) {
			DealStrike();
		}

		UpdateScore();
		totalScore += score;
		score = 0;
	}


	private void DealStrike()
	{
		// TODO strike stuff. 
		Debug.Log("STRIKE!");


		strikes++;
		strikeController.UpdateStrikes(strikes);

		if (strikes >= maxStrikes)
		{
			timeIsRunning = false;
		}
	}

	private void GameOver()
	{
		// TODO: game over stuff. 
		Debug.Log("U DED");
		Debug.Log("Score: " + totalScore);

		SwitchToHighscores();
	}

	private void SwitchToHighscores()
	{
		HighscoreData.CurrentScore = totalScore;
		SceneManager.LoadScene(2);
	}
}
