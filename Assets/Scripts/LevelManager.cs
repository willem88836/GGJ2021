using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private AnimationCurve levelTime;
	[SerializeField] private AnimationCurve levelScore;
	[SerializeField] private int maxStrikes = 3;

	// TODO: change this to the proper UI items.
	[Header("References")]
	[SerializeField] private ScoreSlider scoreController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private StrikeController strikeController;


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
		}

		GameOver();
	}


	private void UpdateScore()
	{
		scoreController.UpdateScore(score, scoreGoal);
	}

	private void LevelEnded()
	{
		// TODO: level ending stuff. 

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
