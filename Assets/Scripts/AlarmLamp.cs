using System;
using UnityEngine;

[Serializable]
public class AlarmLamp : MonoBehaviour
{
    [SerializeField]
    GameObject offVisual;

    [SerializeField]
    GameObject onVisual;

    public void TurnOn()
	{
        if (offVisual.activeSelf) offVisual.SetActive(false);
        if (!onVisual.activeSelf) onVisual.SetActive(true);
	}

    public void TurnOff()
	{
        if (onVisual.activeSelf) onVisual.SetActive(false);
        if (!offVisual.activeSelf) offVisual.SetActive(true);
    }
}
