using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuAudio : MonoBehaviour
{
    [SerializeField] private bool enableMenuAudio;
    private static MenuAudio singleton;

    public static MenuAudio GetMenuAudio()
    {
        return singleton;
    }

    public static void Fade(float alpha)
    {
        singleton.GetComponent<AudioSource>().volume = Mathf.Clamp01(alpha);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (enableMenuAudio)
        {
            if (singleton != null)
            {
                Destroy(this);
            }
            else
            {
                singleton = this;
                DontDestroyOnLoad(this);
                this.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (singleton != null)
            {
                singleton.GetComponent<AudioSource>().Stop();
                Destroy(singleton);
                singleton = null;
            }
            Destroy(this); 
        }
    }
}
