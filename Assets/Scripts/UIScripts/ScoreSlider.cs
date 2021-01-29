using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private Text currentScoreText;
	[SerializeField] private Text maxScoreText; 

	public void UpdateScore(int current, int max)
	{
		slider.maxValue = max;
		currentScoreText.text = current.ToString();
		maxScoreText.text = max.ToString();
	}
}
