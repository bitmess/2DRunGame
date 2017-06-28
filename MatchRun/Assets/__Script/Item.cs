using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{

    public int Score;

    private GameLogic _gl;

    void Start()
    {
        _gl = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.RotateAroundLocal(Vector3.up,5 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _gl.Score += Score;
            Destroy(gameObject);
        }
    }

}
