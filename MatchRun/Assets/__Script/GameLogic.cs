using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
    public GUISkin Skin;
    public Texture2D ResetTex;
    public Transform Spawnpoint;
    public Transform Resetpoint;
    public Transform Floor0;
    public Transform[] Floorpoints;
    public Transform[] Enemypoints;
    public Transform[] Scorepoints;
    public Transform[] Scoreitems;

    public Transform[] Tiles;
    public Transform[] Enemies;

    public float MinDelay = 1.2f;
    public float MaxDelay = 1.5f;
    [HideInInspector]
    public int Score;

    private GameObject _player;

    [HideInInspector]
    public bool IsGameOver;

    private float _t;
    private float _seconds;
    private GameControl _gc;

    private float _bestTime;
    private int _bestScore;

    [HideInInspector]
    public const string BestTime = "BestTime";
    [HideInInspector]
    public const string BestScore = "BestScore";

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _gc = GameObject.Find("All").GetComponent<GameControl>();

        _bestTime = PlayerPrefs.GetFloat(BestTime);
        _bestScore = PlayerPrefs.GetInt(BestScore);

        /////////////////////////////////
        StartCop();
    }

    private void StartCop()
    {
        StartCoroutine("GenTiles");
        StartCoroutine("ProcessScore");
        StartCoroutine("GenEnemy");
        StartCoroutine("GenItem");
    }

    IEnumerator GenTiles()
    {
        int last = Tiles.Length;
        int floorPLen = Floorpoints.Length;
        while (!IsGameOver)
        {
            _seconds = Random.Range(MinDelay, MaxDelay);
            Instantiate(Tiles[Random.Range(0, last)], Floorpoints[Random.Range(0,floorPLen)].position, Quaternion.identity);
            yield return new WaitForSeconds(_seconds);
        }
    }

    IEnumerator GenEnemy()
    {
        int last = Enemies.Length;
        int enemyPLen = Enemypoints.Length;
        while (!IsGameOver)
        {
            Instantiate(Enemies[Random.Range(0, last)], Enemypoints[Random.Range(0,enemyPLen)].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
        }
    }

    IEnumerator ProcessScore()
    {
        while (!IsGameOver)
        {
            _t++;
            yield return new WaitForSeconds(1.0f);
        }
    }


    IEnumerator GenItem()
    {
        var lenI = Scoreitems.Length;
        var lenP = Scorepoints.Length;
        Transform original = Scoreitems[Random.Range(0, lenI)];
        Vector3 position = Scorepoints[Random.Range(0, lenP)].position;
        while (!IsGameOver)
        {
            var UPPER = Random.Range(3, 5);
            for (int i = 0; i < UPPER; i++)
            {
                Instantiate(original, position, original.rotation);
                yield return new WaitForSeconds(0.2f);
            }
            original = Scoreitems[Random.Range(0, lenI)];
            position = Scorepoints[Random.Range(0, lenP)].position;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    void Update()
    {
        if (!IsGameOver && _player.transform.position.y < -2f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    void OnGUI()
    {
        if (Skin)
        {
            GUI.skin = Skin;
        }

        if (IsGameOver)
        {
            if (GUI.Button(new Rect(0, 80, 60, 60), ResetTex))
            {
                Reset();
            }
        }
        if (!_gc.IsPaused)
        {
            CheckBest();
            GUI.Box(new Rect(Screen.width / 2 - 100, 0, 200, 25), "Time : " + _t + " Score : " + Score);
        }
    }



    void Reset()
    {
        Score = 0;
        _t = 0;
        GameObject[] ts = GameObject.FindGameObjectsWithTag("Tile");
        GameObject[] ens = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] its = GameObject.FindGameObjectsWithTag("Item");
        foreach (var o in ts)
        {
            Destroy(o);
        }
        foreach (var o in ens)
        {
            Destroy(o);
        }
        foreach (var o in its)
        {
            Destroy(o);
        }
        Instantiate(Floor0, Resetpoint.position, Quaternion.identity);

        _player.transform.position = Spawnpoint.position;

        Time.timeScale = 1;
        IsGameOver = false;
        StartCop();
    }

    private void CheckBest()
    {
        if (Score > _bestScore)
            _bestScore = Score;
        if (_t > _bestTime)
            _bestTime = _t;

        PlayerPrefs.SetFloat(BestTime, _bestTime);
        PlayerPrefs.SetInt(BestScore, _bestScore);
    }

    public string GetState()
    {
        return "BestTime : " + _bestTime + " HighScore : " + _bestScore;
    }

}
