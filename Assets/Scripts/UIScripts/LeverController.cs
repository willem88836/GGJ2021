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
        string ext; 
        if (q < extentions.Length)
        {
            ext = extentions[q];
        }
        else
        {
            ext = "th";
        }
        string o = level + ext + fillerText;
        textField.text = o;
    }
}
