using UnityEngine;
using UnityEngine.UI;

public class LeverController : MonoBehaviour
{
    [SerializeField] private string fillerText;
    [SerializeField] private Text textField;
    [SerializeField] private string[] extentions;

    public void UpdateLevel(int level)
    {
        string sLevel = level.ToString();
        int q = int.Parse(sLevel[sLevel.Length - 1].ToString());
        string ext = extentions[q];
        string o = level + ext + fillerText;
        textField.text = o;
    }
}
