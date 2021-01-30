using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private AnimationCurve levelTime;
	[SerializeField] private AnimationCurve levelScore;
	[SerializeField] private int maxStrikes = 3;
	[SerializeField] private int restartLevelDelay;

	[Header("References")]
	[SerializeField] private ScoreController scoreController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private StrikeController strikeController;

	[Space]
	[SerializeField] private Truck truck; 


	private bool timeIsRunning;

	private int totalScore;
	private int score;
	private int strikes;
	private int level;

	private int scoreGoal
	{
		get
		{
			return (int)(level > levelScore.length
			? levelScore.Evaluate(levelScore.length)
			: levelScore.Evaluate(level));
		}
	}


	private void Start()
	{
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

			int time = (int)(level > levelTime.length 
				? levelTime.Evaluate(levelTime.length) 
				: levelTime.Evaluate(level));

			for (int currentTime = 0; currentTime < time; currentTime++)
			{
				timeController.UpdateTimer(time - currentTime);
				yield return new WaitForSeconds(1);
			}

			LevelEnded();
			level++;

			yield return new WaitForSeconds(restartLevelDelay);
		}

		GameOver();
	}


	private void UpdateScore()
	{
		scoreController.UpdateScore(score, scoreGoal);
	}

	private void LevelStarted()
	{
		truck.StartNextRound();
	}

	private void LevelEnded()
	{
		if (score < scoreGoal) {
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
