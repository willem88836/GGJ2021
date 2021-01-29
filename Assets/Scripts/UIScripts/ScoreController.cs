using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private Text currentScoreText;
	[SerializeField] private Text maxScoreText;
	[SerializeField] private float minSladerWidthAlpha; 

	public void UpdateScore(int current, int max)
	{
		slider.maxValue = max;
		slider.value = Mathf.Max(current, minSladerWidthAlpha * max);	
		currentScoreText.text = current.ToString();
		maxScoreText.text = max.ToString();
	}
}
