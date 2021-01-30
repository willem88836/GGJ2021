using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private AnimationCurve levelTime;
	[SerializeField] private AnimationCurve levelScore;
	[SerializeField] private AnimationCurve packages;
	[SerializeField] private AnimationCurve letters;
	[SerializeField] private int maxStrikes = 3;
	[SerializeField] private int restartLevelDelay;
	[SerializeField] private int pointCaptureDelay;

	[Header("References")]
	[SerializeField] private ScoreController scoreController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private StrikeController strikeController;
	[SerializeField] private Hatch[] hatches;

	[Space]
	[SerializeField] private Truck truck; 


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
		strikeController.SetStrikeCount(maxStrikes);
		BeginGame();
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

			int time = levelTime.SafeEvaluate(level);

			for (int currentTime = 0; currentTime < time; currentTime++)
			{
				timeController.UpdateTimer(time - currentTime);
				yield return new WaitForSeconds(1);
			}

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
		scoreController.UpdateScore(score, levelScore.SafeEvaluate(level));
	}

	private void LevelStarted()
	{
		SpawnConfig spawnConfig = new SpawnConfig()
		{
			letterCount = letters.SafeEvaluate(level),
			packageCount = packages.SafeEvaluate(level)
		};

		truck.StartNextRound(spawnConfig);
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
	}
}
