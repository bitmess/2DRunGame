using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private GameObject _gl;

    void Start()
    {
        _gl = GameObject.Find("GameLogic");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _gl.SendMessage("GameOver");
        }
    }

}
