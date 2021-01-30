using UnityEngine;

public enum Type { letter, cylinder, box };
public enum Color { Blue, Pink, Yellow, Gray };

public class MailItem : MonoBehaviour
{
	public int Points;
	public Type type; 
	public Color color;

	public int GetPoints()
	{
		return Points;
	}

	public Type GetType()
	{
		return type;
	}

	public Color GetColor()
	{
		return color;
	}

	public void Expire()
	{
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(true);
	}

	public void Unexpire()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}
}
