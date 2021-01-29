using UnityEngine;

public class StrikeController : MonoBehaviour
{
	[SerializeField] private float spacing;

	private GameObject[] children;

	public void SetStrikeCount(int count)
	{
		GameObject child = transform.GetChild(0).gameObject;
		children = new GameObject[count];

		for (int i = 0; i < count; i++)
		{
			GameObject newChild = Instantiate(child, transform);
			newChild.transform.localPosition = new Vector3(((float)i - count / 2f) * spacing, 0, 0);
			newChild.SetActive(true);
			children[i] = newChild;
		}
	}

	public void UpdateStrikes(int count)
	{
		for (int i = 0; i < children.Length; i++)
		{
			// TODO update this according to UI design.
			children[i].SetActive(count <= i);
		}
	}
}
