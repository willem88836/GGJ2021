using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
	[SerializeField] private Text timeField;

	public void UpdateTimer(int time)
	{
		int min = time / 60;
		int sec = time % 60;

		string m = min.ToString().Length < 2
			? $"0{min}"
			: min.ToString();

		string s = sec.ToString().Length < 2
			? $"0{sec}"
			: sec.ToString();


		timeField.text = $"{m}:{s}";
	}
}
