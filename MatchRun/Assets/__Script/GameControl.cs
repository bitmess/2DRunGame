using UnityEngine;

public class GameControl : MonoBehaviour
{

    public GUISkin Skin;
    public Texture2D PlayTex;
    public Texture2D PauseTex;
    public Texture2D TipTex;
    [HideInInspector]
    public bool IsPaused;
    private float _process;
    private float _left;
    private int _width = 50;
    private Rect _position;
    private Rect _full;
    private GameLogic _gl;
    private bool _isTip;
    private bool _isState;
    // Use this for initialization
    void Start()
    {
        _process = Time.timeScale;
        _left = Screen.width - _width;
        _position = new Rect(_left, 0, _width, _width);
        _gl = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        _full = new Rect(0, 0, Screen.width, Screen.height);
    }


    void OnGUI()
    {
        if (!_gl.IsGameOver)
        {
            if (Skin)
            {
                GUI.skin = Skin;
            }
            if (!IsPaused)
            {
                if (GUI.Button(_position, PauseTex))
                {
                    IsPaused = true;
                    Time.timeScale = 0;
                }
            }
            if (IsPaused)
            {
                //Tip
                if (!_isTip && !_isState &&
                    GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40, 100, 30), "Tip", Skin.customStyles[0]))
                {
                    _isTip = true;
                }
                //State
                if (!_isTip && !_isState &&
                    GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 30), "State", Skin.customStyles[0]))
                {
                    _isState = true;
                }
                //Exit
                if (!_isTip && !_isState &&
                    GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 40, 100, 30), "Exit",
                               Skin.customStyles[0]))
                {
                    Application.Quit();
                }

                if (GUI.Button(_position, PlayTex))
                {
                    IsPaused = false;
                    Time.timeScale = _process;
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

                if (_isState)
                {
                    GUI.Box(new Rect(0, Screen.height / 2 - 25, Screen.width, 50), _gl.GetState(), Skin.customStyles[0]);
                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30), "Back",
                                   Skin.customStyles[0]))
                    {
                        _isState = false;
                    }
                }
            }
            else
            {
                _isState = false;
                _isTip = false;
            }
        }
    }
}
