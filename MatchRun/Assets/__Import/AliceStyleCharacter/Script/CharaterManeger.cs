using UnityEngine;
using System.Collections;

public class CharaterManeger : MonoBehaviour {
	
	public int ibtnNum = 10;
	public int ibtnInitPosY = 50;
	
	public float GravityForce = -1.3f;
	public float JumpHeight = 2.0f;
	public bool CanJump = false;
	public bool Jump = false;
	public float hSliderValue = 0.0F;
	
	
	private int ibtnPositionX = 0;
	private int ibtnPositionY = 50;
	private int ibtnPositionSizeX = 100;
	private int ibtnPositionSizeY = 30;
	
	
	
	
	// Use this for initialization
	void Start () {
		
		
		
		CanJump = false;
		
		ibtnPositionX = (int)Screen.width - 120;
			
		GetComponent<Animation>().wrapMode = WrapMode.Loop; 
		GetComponent<Animation>().Play("idle01");
			
	}
	
	// Update is called once per frame
	void Update () {
		
		Physics.gravity = new Vector3(0, GravityForce, 0);
		Debug.Log(GetComponent<Rigidbody>().velocity.y);
		
		transform.eulerAngles = new Vector3(0, hSliderValue, 0);
		
		
		if (CanJump == true && Jump == true)
        {
			JumpLogic();
			
			CanJump = false;
			GetComponent<Animation>().CrossFade("jump", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
            
        }
		
	}
	
	void OnCollisionEnter(Collision Wall)
    {
        if (Wall.gameObject.tag == "Wall") 
        {
            CanJump = true;
			GetComponent<Animation>().wrapMode = WrapMode.Loop;
            GetComponent<Animation>().CrossFade("idle01",0.1f);
        }
    }
	
	
	void OnGUI()
	{
		
		 
		
		if (GUI.Button(new Rect(Screen.width / 2 - (ibtnPositionSizeX * 2), Screen.height - 70, ibtnPositionSizeX, ibtnPositionSizeY), "RIGHT"))
		{
			hSliderValue += 45;
			
		}
		if (GUI.Button(new Rect(Screen.width / 2 + ibtnPositionSizeX, Screen.height - 70, ibtnPositionSizeX, ibtnPositionSizeY), "LEFT"))
		{
			
			hSliderValue -=45;
		}
		
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (0 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Idle"))
		{
			GetComponent<Animation>().CrossFade("idle01", 0.1f);
			
		}
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (1 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Walk"))
		{
			GetComponent<Animation>().CrossFade("walk", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Loop;
	    	
		}
	
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (2 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Jump"))
		{
			Jump = true;
		}
		else
			Jump = false;
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (3 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Run"))
		{
	    	GetComponent<Animation>().CrossFade("run", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Loop;
		}
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (4 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Run2"))
		{
	    	GetComponent<Animation>().CrossFade("run2", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Loop;
		}
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (5 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Touch Arm"))
		{
	    	GetComponent<Animation>().CrossFade("touchArm", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
		}
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (6 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Touch Breast"))
		{
	    	GetComponent<Animation>().CrossFade("touchBreast", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
		}
		
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (7 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Touch Foot"))
		{
	    	GetComponent<Animation>().CrossFade("touchFoot", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
		}
		
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (8 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Touch Head"))
		{
	    	GetComponent<Animation>().CrossFade("touchHead", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
		}
		
		
		if (GUI.Button(new Rect(ibtnPositionX, ibtnInitPosY + (9 * ibtnPositionY), ibtnPositionSizeX, ibtnPositionSizeY), "Touch Hip"))
		{
	    	GetComponent<Animation>().CrossFade("touchHip", 0.1f);
			GetComponent<Animation>().wrapMode = WrapMode.Default;
		}
		
		
		
		
	}
	
	  void JumpLogic()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight);
    }
	
	
}
