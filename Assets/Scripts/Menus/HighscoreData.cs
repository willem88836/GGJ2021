using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreData 
{
	public static int CurrentScore;

	public static HighScore[] LoadScores()
	{
		HighScore[] scores = new HighScore[5];

		for (int i = 0; i < 5; i++)
		{
			scores[i] = new HighScore();

			string score = "score" + i;
			scores[i].Score = PlayerPrefs.GetInt(score);

			string name = "name" + i;
			scores[i].Name = PlayerPrefs.GetString(name);
		}

		return scores;
	}

    public static void SaveHighscores(HighScore[] highScores)
	{
		for (int i = 0; i < 5; i++)
		{
			string score = "score" + i;
			PlayerPrefs.SetInt(score, highScores[i].Score);

			string name = "name" + i;
			PlayerPrefs.SetString(name, highScores[i].Name);
		}

		PlayerPrefs.Save();

		Debug.Log("saving!");
	}
}
