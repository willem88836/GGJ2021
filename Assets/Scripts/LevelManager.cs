using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private AnimationCurve levelTime;

	private bool timeIsRunning;

	private int level = 0;


	private void Start()
	{
		StartCoroutine(LevelProgression());
	}

	private IEnumerator<YieldInstruction> LevelProgression()
	{
		timeIsRunning = true;

		while (timeIsRunning)
		{
			float time = level > levelTime.length 
				? levelTime.Evaluate(levelTime.length) 
				: levelTime.Evaluate(level);

			for (int currentTime = 0; currentTime < time; currentTime++)
			{
				yield return new WaitForSeconds(1);
			}

			LevelEnded();
			level++;
		}

		GameOver();
	}


	private void LevelEnded()
	{
		// TODO: stuff. 
	}

	private void GameOver()
	{
		// TODO: stuff. 
	}
}
