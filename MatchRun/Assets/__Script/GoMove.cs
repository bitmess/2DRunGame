using UnityEngine;
using System.Collections;

public class GoMove : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Translate(-Vector3.right * Time.deltaTime * 3, Space.World);
	}
}
