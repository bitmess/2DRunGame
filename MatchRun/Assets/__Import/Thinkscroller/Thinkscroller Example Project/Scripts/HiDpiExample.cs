using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

/// <summary>
/// Example script - instantiates a prefab based on display density.
/// A more advanced version of this script would swap textures instead.
/// </summary>
[AddComponentMenu("Thinkscroller Example Project/HiDPI Example")]
public class HiDpiExample : MonoBehaviour {
	
	public bool forceEditorHiDpi = false;
	public float dpiThreshold = 200f;
	public float fallbackDpi = 160f;
	public GameObject lowDpiPrefab;
	public GameObject hiDpiPrefab;
	
	void Awake()
	{
		float dpi = Screen.dpi == 0 ? fallbackDpi : Screen.dpi;
		
		if (((Application.isEditor && forceEditorHiDpi) || dpi >= dpiThreshold) && hiDpiPrefab)
			Object.Instantiate(hiDpiPrefab);
		else if (lowDpiPrefab)
			Object.Instantiate(lowDpiPrefab);
	}
}
