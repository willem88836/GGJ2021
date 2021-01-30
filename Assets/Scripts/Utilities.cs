using UnityEngine;

public static class Utilities
{
	public static int SafeEvaluate(this AnimationCurve curve,  int alpha)
	{
		if (alpha > curve.length)
		{
			return (int) curve.Evaluate(curve.length);
		}
		return (int) curve.Evaluate(alpha);
	}

}
