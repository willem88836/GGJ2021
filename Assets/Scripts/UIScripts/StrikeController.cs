using System.Collections.Generic;
using UnityEngine;

public class StrikeController : MonoBehaviour
{
	[SerializeField]
	private List<AlarmLamp> firstStrikes;

	[SerializeField]
	private List<AlarmLamp> secondStrikes;

	[SerializeField]
	private List<AlarmLamp> thirdStrikes;

	public void SetStrikeCount(int count)
	{
		UpdateStrikes(count);
	}

	public void UpdateStrikes(int count)
	{
		switch(count)
		{
			case 0:
				ToggleLamps(firstStrikes, false);
				ToggleLamps(secondStrikes, false);
				ToggleLamps(thirdStrikes, false);
				break;
			case 1:
				ToggleLamps(firstStrikes, true);
				ToggleLamps(secondStrikes, false);
				ToggleLamps(thirdStrikes, false);
				break;
			case 2:
				ToggleLamps(firstStrikes, true);
				ToggleLamps(secondStrikes, true);
				ToggleLamps(thirdStrikes, false);
				break;
			case 3:
			default:
				ToggleLamps(firstStrikes, true);
				ToggleLamps(secondStrikes, true);
				ToggleLamps(thirdStrikes, true);
				break;
		}
	}

	private void ToggleLamps(List<AlarmLamp> lamps, bool active)
	{
		if (active)
		{
			lamps.ForEach(i => i.TurnOn());
		} else
		{
			lamps.ForEach(i => i.TurnOff());
		}
	}
}
