using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private Text currentScoreText;
	[SerializeField] private Text maxScoreText;
	[SerializeField] private float minSladerWidthAlpha;
	[SerializeField] private int[] textScale;

	public void UpdateScore(int current, int max, float ratio)
	{
		//slider.maxValue = max;
		slider.value = Mathf.Max(ratio, minSladerWidthAlpha);	
		currentScoreText.text = current.ToString();
		maxScoreText.text = max.ToString();

		string c = current.ToString();
		int ci = c.Length - 1;
		int sSize = c.Length >= textScale.Length
			? textScale[textScale.Length - 1]
			: textScale[ci];
		currentScoreText.fontSize = sSize;


		string m = max.ToString();
		int mi = m.Length - 1;
		int mSize = m.Length >= textScale.Length
			? textScale[textScale.Length - 1]
			: textScale[mi];
		maxScoreText.fontSize = mSize;
	}
}
