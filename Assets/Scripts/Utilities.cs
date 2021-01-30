using UnityEngine;

public static class Utilities
{
	public static int SafeEvaluate(this AnimationCurve curve,  int alpha)
	{
		if (alpha > curve.keys[curve.length - 1].time)
		{
			return (int) curve.Evaluate(curve.length);
		}
		return (int) curve.Evaluate(alpha);
	}
}
