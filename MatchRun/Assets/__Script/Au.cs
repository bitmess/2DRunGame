using UnityEngine;
using System.Collections;

public class Au : MonoBehaviour
{
    public GUISkin Skin;
    public Texture2D Sound;
    public Texture2D Mute;
    private Rect _pos;
    private const string Volumn = "Volumn";
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        int top = Screen.height / 3 - 25;
        int left = Screen.width - 50;
        _pos = new Rect(left, top, 50, 50);
        int mute = PlayerPrefs.GetInt(Volumn);
        GetComponent<AudioSource>().mute = mute == 1;
    }

    void OnGUI()
    {
        if (Skin)
        {
            GUI.skin = Skin;
        }
        if (GetComponent<AudioSource>().mute)
        {
            if (GUI.Button(_pos, Mute))
            {
                GetComponent<AudioSource>().mute = false;
                PlayerPrefs.SetInt(Volumn, 0);
            }
        }
        else
        {
            if (GUI.Button(_pos, Sound))
            {
                GetComponent<AudioSource>().mute = true;
                PlayerPrefs.SetInt(Volumn, 1);
            }
        }
    }
}
