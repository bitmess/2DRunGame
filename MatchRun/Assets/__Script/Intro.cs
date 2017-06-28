using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public GUISkin Skin;
    public Texture2D Tex;
    public Texture2D TipTex;
    private bool _isToPlay;
    private bool _isState;
    private bool _isTip;
    private Rect _full;
    private AsyncOperation _ao;
    private GUIStyle _s;

    // Use this for initialization
    void Start()
    {
        _s = new GUIStyle();
        _s.normal.textColor = Color.red;
        _s.fontSize = 20;
        _full = new Rect(0, 0, Screen.width, Screen.height);
    }

    void OnGUI()
    {
        if (Skin)
        {
            GUI.skin = Skin;
        }

        if (!_isToPlay && Tex)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Tex);
        }
        if (_isToPlay && TipTex)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TipTex);
            if (null != _ao && !_ao.isDone)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 20, 100, 40), "Loading...", _s);
            }
        }
        if (_isState)
        {
            var val = "BestTime : " + PlayerPrefs.GetFloat(GameLogic.BestTime) + " HighScore : " + PlayerPrefs.GetInt(GameLogic.BestScore);
            GUI.Box(new Rect(0, Screen.height / 2 - 25, Screen.width, 50), val, Skin.customStyles[0]);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30), "Back",
                                   Skin.customStyles[0]))
            {
                _isState = false;
            }
        }

        if (_isTip)
        {
            GUI.DrawTexture(_full, TipTex);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30), "Back",
                           Skin.customStyles[0]))
            {
                _isTip = false;
            }
        }

        //*************************

        if (!_isToPlay && !_isState && !_isTip && GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40, 100, 30), "Play", Skin.customStyles[0]))
        {
            _isToPlay = true;
            StartCoroutine("LoadNextLevel");
        }

        if (!_isToPlay && !_isState && !_isTip && GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "State", Skin.customStyles[0]))
        {
            _isState = true;
        }

        if (!_isToPlay && !_isState && !_isTip && GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 40, 100, 30), "Tip", Skin.customStyles[0]))
        {
            _isTip = true;
        }

        if (!_isToPlay && !_isState && !_isTip && GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 80, 100, 30), "Exit", Skin.customStyles[0]))
        {
            Application.Quit();
        }
    }

    IEnumerator LoadNextLevel()
    {
        _ao = Application.LoadLevelAsync(Application.loadedLevel + 1);
        yield return null;
    }

}
