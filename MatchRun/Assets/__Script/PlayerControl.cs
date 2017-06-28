using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public GUISkin Skin;
    public float Gravity = 10f;
    public Texture2D JumpTex;
    public Texture2D DJumpTex;
    public Texture2D NJumpTex;
    //***********************************
    private float _jumpf = -1.0f;
    private Vector3 _jumpDistance;
    private const string Run = "run";
    private const string Jump = "jump";
    private CharacterController _cc;
    private int _step = 2;
    private GameLogic _gl;
    private GameControl _gc;
    // Use this for initialization
    void Start()
    {
        GetComponent<Animation>()[Jump].layer = 1;
        _jumpDistance = Vector3.up * Gravity;
        _cc = GetComponent<CharacterController>();
        _gl = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        _gc = GameObject.Find("All").GetComponent<GameControl>();
    }


    void Update()
    {
        if (_jumpf <= -1.0f)
        {
            GetComponent<Animation>().Play(Run);
            _cc.Move(-Gravity * Vector3.up * Time.deltaTime);
        }
        if (_jumpf > -1.0f)
        {
            _jumpf -= 9 * Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (!_gl.IsGameOver && !_gc.IsPaused)
        {
            if (Skin)
            {
                GUI.skin = Skin;
            }
            var tex = JumpTex;
            switch (_step)
            {
                case 1:
                    tex = DJumpTex;
                    break;
                case 0:
                    tex = NJumpTex;
                    break;
            }
            if (GUI.RepeatButton(new Rect(0, 0, 80, 80), tex))
            {
                if (_step > 0 && transform.position.y < 0.5f)
                {
                    _jumpf = 1.0f;
                    GetComponent<Animation>().Play(Jump);
                    transform.Translate(_jumpDistance * Time.deltaTime);
                }
                if (Event.current.type == EventType.Used)
                {
                    _step--;
                }
                if (transform.position.y >= 0.5f)
                {
                    _step = 0;
                }
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Tile")
        {
            _step = 2;
        }
    }

}
