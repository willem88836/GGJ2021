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
	[SerializeField] private Text timeField;
	[SerializeField] private Text scoreField;
	[SerializeField] private Text levelField;
	[SerializeField] private Text strikeField;


	private bool timeIsRunning;

	private int totalScore;
	private int score;
	private int strikes;
	private int level;


	private void Start()
	{
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

		while (timeIsRunning)
		{
			float time = level > levelTime.length 
				? levelTime.Evaluate(levelTime.length) 
				: levelTime.Evaluate(level);

			for (int currentTime = 0; currentTime < time; currentTime++)
			{
				timeField.text = (time - currentTime).ToString();
				yield return new WaitForSeconds(1);
			}

			LevelEnded();
			level++;
		}

		GameOver();
	}


	private void LevelEnded()
	{
		// TODO: level ending stuff. 

		float scoreGoal = level > levelScore.length
			? levelScore.Evaluate(levelScore.length)
			: levelScore.Evaluate(level);

		if (score < scoreGoal) {
			DealStrike();
		}

		scoreField.text = totalScore.ToString();

		totalScore += score;
		score = 0;
	}

	private void DealStrike()
	{
		// TODO strike stuff. 
		Debug.Log("STRIKE!");


		strikes++;
		strikeField.text = strikes.ToString();
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
