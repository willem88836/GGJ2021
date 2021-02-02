using UnityEngine;

public static class Utilities
{
	public static int SafeEvaluate(this AnimationCurve curve,  int alpha)
	{
		Keyframe lastKey = curve.keys[curve.length - 1];
		if (alpha > lastKey.time)
		{
			return (int) lastKey.value;
		}
		return (int) curve.Evaluate(alpha);
	}
}
