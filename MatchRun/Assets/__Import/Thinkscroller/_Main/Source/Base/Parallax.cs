using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ThinksquirrelSoftware.Thinkscroller;

#if !UNITY_3_5 && !UNITY_3_4 && !UNITY_3_3
namespace ThinksquirrelSoftware.Thinkscroller
{
#endif
	/// <summary>
	/// Parallax manager component.
	/// </summary>
	/// <remarks>
	/// The parallax manager manages all of the scroll layers in the scene.
	/// Only one parallax manager can be in a scene at a time.
	/// Parallax managers are responsible for the global properties and the global update cycle.
	/// </remarks>
	/*! \page parallaxmanager Parallax Manager
	 *
	 * \section overview Overview
	 * The Parallax Manager component manages all of the scroll layers in a scene. Only one parallax manager can be in a scene at a time.
	 * \see ThinksquirrelSoftware.Thinkscroller.Parallax
	 * 
	 * \image html parallaxmanager.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Camera</b> - The camera to use for auto-billboards, and to determine scroll weights.
	 * 		- <b>Scroll Axes</b> - The axes to scroll (in camera space).
	 * 		- <b>Base Speed</b> - The base speed for all calculations. Modify this value to speed up and slow down the entire scene.
	 * 		- <b>Automatic</b> - If enabled, pixel-perfect layers will resize based on the camera's pixel height.
	 * 		- <b>Min/Max Size</b> - If automatic is disabled, pixel-perfect layers will clamp to the minimum and maximum screen sizes. This is useful for keeping a constant scale with multiple-resolution targets.
	 * 		- <b>Auto-Refresh Meshes</b> - If enabled, meshes will automatically refresh. It is recommended to disable this when targetting a platform with a single, set resolution.
	 * 		- <b>Set All Layers to Auto</b> - This will make all layers use the automatic weight system.
	 * 		- <b>About \htmlonly Thinkscroller\endhtmlonly</b> - Contains version information, and provides a link to rate \htmlonly Thinkscroller\endhtmlonly on the Asset Store.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	[ExecuteInEditMode]
	[AddComponentMenu("Thinkscroller/Parallax Manager")]
	public sealed class Parallax : ThinkscrollerBase
	{
		/* Begin singleton */
		
		[SerializeField] static Parallax _instance = null;
		[SerializeField] static readonly object padlock = new object();
		
		/// <summary>
		/// The singleton instance of the parallax manager.
		/// </summary>
		public static Parallax instance
		{
			get
			{
				lock (padlock)
				{
					if (!_instance)
					{
						_instance = ThinkscrollerBase.FindObjectOfType(typeof(Parallax)) as Parallax;
					}
				
					return _instance;
				}
			}
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public static void Scroll(float xScrollValue, float yScrollValue)
		{
			instance.DoScroll(xScrollValue, yScrollValue);
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public static void Scroll(Vector2 scrollValue)
		{
			instance.DoScroll(scrollValue);
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public static void ResetAllObjectLayers()
		{
			instance.DoResetPosition();
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public static void ResetAllObjectLayers(Vector3 resetPosition)
		{
			instance.DoResetPosition(resetPosition);
		}
		
		/// <summary>
		/// Refreshes all billboard meshes. Note that this only updates vertices.
		/// </summary>
		public static void RefreshAllBillboards()
		{
			if (instance)
			{
				foreach(var scrollLayer in instance.scrollLayers)
				{
					if (!scrollLayer)
						continue;
					
					if (scrollLayer.isAutoBillboard)
					{
						scrollLayer.RefreshBillboard(true, false, false);
					}
				}
			}
		}
		
		/// <summary>
		/// Gets a layer's raw scroll vector from an input value.
		/// </summary>
		/// <remarks>
		/// Useful for figuring out how much a layer will scroll without moving it.
		/// </remarks>
		public static Vector2 GetRawScrollVector(ScrollLayer layer, Vector2 scrollValue)
		{
			if (!instance)
			{
				Debug.LogError("Error: Trying to get raw scroll vector without a parallax manager!");
				return Vector2.zero;
			}
			
			if (layer.isObjectLayer && !layer.objectLayerPixelSpace)
			{	
				if (instance.GetScrollConstraints() == ScrollConstraints.X)
				{
					return instance.GetParallaxCamera().transform.right * -scrollValue.x * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
				}
				else if (instance.GetScrollConstraints() == ScrollConstraints.Y)
				{
					return instance.GetParallaxCamera().transform.up * -scrollValue.y * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
				}
				else
				{
					Vector3 sV = (instance.GetParallaxCamera().transform.right * -scrollValue.x) + (instance.GetParallaxCamera().transform.up * -scrollValue.y);
					return sV * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();								
				}
			}
			else
			{
				return scrollValue * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
			}
		}
			
		void Awake()
		{
			Init();
			
			if (!Application.isPlaying)
			{
				foreach(var scrollLayer in scrollLayers)
				{
					scrollLayer.DoReset();
				}
			}
		}
		
		/// <summary>
		/// Creates a parallax manager if no current manager exists.
		/// </summary>
		public static void CreateManager()
		{
			if (_instance)
				return;
			
			_instance = new GameObject("Parallax Manager").AddComponent<Parallax>() as Parallax;
		}
		bool CheckForDuplicate()
		{
			if (instance != this)
			{
				Debug.LogError("Parallax Manager: There can only be one parallax manager per scene.", this);
				return true;
			}
			return false;
		}
		
		/* End singleton */
		
		[SerializeField] [HideInInspector] Camera parallaxCamera;
		[SerializeField] [HideInInspector] float cameraWidth;
		[SerializeField] [HideInInspector] float cameraHeight;
		[SerializeField] [HideInInspector] float cameraWidthRaw;
		[SerializeField] [HideInInspector] float cameraHeightRaw;
		[SerializeField] [HideInInspector] float cameraAspectRatio;
		[SerializeField] [HideInInspector] float cameraAspectRatioRaw;
		[SerializeField] [HideInInspector] bool _automatic = true;
		[SerializeField] [HideInInspector] bool _autoRefreshBillboards = false;
		[SerializeField] [HideInInspector] Vector2 _minSize;
		[SerializeField] [HideInInspector] Vector2 _maxSize;
		[SerializeField] [HideInInspector] float _pixelDensity = 1;
		[SerializeField] [HideInInspector] ScrollConstraints scrollConstraints = ScrollConstraints.X;
		[SerializeField] [HideInInspector] Vector2 scrollValue;
		[SerializeField] [HideInInspector] float baseSpeed = 1;
		[SerializeField] [HideInInspector] ScrollLayer[] scrollLayers;
		
		/// <summary>
		/// The list of active scroll layers in the scene, ordered by weight.
		/// </summary>
		public ScrollLayer this[int i]
		{
			get
			{
				return scrollLayers[i];
			}
		}
		
		/// <summary>
		/// The number of active scroll layers in the scene.
		/// </summary>
		public int Length
		{
			get
			{
				return scrollLayers.Length;
			}
		}
		
		/// <summary>
		/// If true, the camera's viewport is set automatically.
		/// </summary>
		public bool automatic
		{
			get
			{
				return _automatic;
			}
			set
			{
				_automatic = value;
			}
		}
		
		/// <summary>
		/// If true, billboard meshes are automatically regenerated every LateUpdate().
		/// </summary>
		public bool autoRefreshBillboards
		{
		
			get
			{
				return _autoRefreshBillboards;
			}
			set
			{
				_autoRefreshBillboards = value;
			}
		}
		
		/// <summary>
		/// If the camera viewport is not automatic, the minimum viewport size.
		/// </summary>
		public Vector2 minSize
		{
			get
			{
				return _minSize;
			}
			set
			{
				_minSize = value;
			}
		}
		
		/// <summary>
		/// If the camera viewport is not automatic, the maximum viewport size.
		/// </summary>
		public Vector2 maxSize
		{
			get
			{
				return _maxSize;
			}
			set
			{
				_maxSize = value;
			}
		}
		
		/// <summary>
		/// The scale of a pixel for Thinkscroller calculations. Use this setting when targetting displays with different densities.
		/// </summary>
		public float pixelDensity
		{
			get
			{
				return _pixelDensity;
			}
			set
			{
				_pixelDensity = value;
			}
		}
		/// <summary>
		/// Gets the parallax camera.
		/// </summary>
		public Camera GetParallaxCamera()
		{
			if (!parallaxCamera)
				SetParallaxCamera(Camera.main);
			
			return parallaxCamera;
		}
		
		/// <summary>
		/// Gets the width of the parallax camera viewport.
		/// </summary>
		public float GetCameraWidth()
		{
			if (cameraWidth == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraWidth * _pixelDensity;
		}
		
		internal float GetCameraWidthRaw()
		{
			if (cameraWidthRaw == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraWidthRaw;
		}
		
		/// <summary>
		/// Gets the height of the parallax camera viewport.
		/// </summary>
		public float GetCameraHeight()
		{
			if (cameraHeight == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraHeight * _pixelDensity;
		}
		
		public float GetCameraHeightRaw()
		{
			if (cameraHeightRaw == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraHeightRaw;
		}
		
		/// <summary>
		/// Gets the aspect ratio of the parallax camera viewport.
		/// </summary>
		public float GetCameraAspectRatio()
		{
			if (cameraAspectRatio == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraAspectRatio;
		}
		
		internal float GetCameraAspectRatioRaw()
		{
			if (cameraAspectRatioRaw == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraAspectRatioRaw;
		}
		
		/// <summary>
		/// Sets the parallax camera.
		/// </summary>
		public void SetParallaxCamera(Camera parallaxCamera)
		{
			this.parallaxCamera = parallaxCamera;
		}
		
		/// <summary>
		/// Gets the current scroll movement constriants.
		/// </summary>
		public ScrollConstraints GetScrollConstraints()
		{
			return scrollConstraints;
		}
		
		/// <summary>
		/// Sets the current scroll movement constraints.
		/// </summary>
		public void SetScrollConstraints(ScrollConstraints scrollConstraints)
		{
			this.scrollConstraints = scrollConstraints;
		}
		
		/// <summary>
		/// Gets the global base speed for parallax scrolling.
		/// </summary>
		public float GetBaseSpeed()
		{
			return baseSpeed;
		}
		
		/// <summary>
		/// Sets the global base speed for parallax scrolling.
		/// </summary>
		public void SetBaseSpeed(float baseSpeed)
		{
			this.baseSpeed = baseSpeed;
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// Can also be accessed statically (Parallax.Scroll()).
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public void DoScroll(Vector2 scrollValue)
		{
			this.scrollValue = scrollValue;
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// Can also be accessed statically (Parallax.Scroll()).
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public void DoScroll(float xScrollValue, float yScrollValue)
		{
			Scroll(new Vector2(xScrollValue, yScrollValue));
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public void DoResetPosition()
		{
			foreach (ScrollLayer s in scrollLayers)
			{
				s.ResetPosition();
			}
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public void DoResetPosition(Vector3 resetPosition)
		{
			foreach (ScrollLayer s in scrollLayers)
			{
				s.ResetPosition(resetPosition);
			}
		}
		
		/* Behaviours */
		
		/// <summary>
		/// Refreshes the scroll layers.
		/// </summary>
		public void RefreshLayers()
		{
			List<ScrollLayer> layerList = new List<ScrollLayer>(ThinkscrollerBase.FindObjectsOfType(typeof(ScrollLayer)) as ScrollLayer[]);
			
			// If auto-configured, assign weights
			foreach (ScrollLayer layer in layerList)
			{
				if (layer.isAutoConfigured && GetParallaxCamera())
				{
					layer.SetWeight(Vector3.Distance(GetParallaxCamera().transform.position, layer.transform.position));
				}
			}
			
			// Sort scroll layers by weight
#if UNITY_FLASH
			layerList.Sort(ScrollLayer.Comparison);
#else
			layerList.Sort();
#endif
			scrollLayers = layerList.ToArray();
		}
		
		void Init()
		{
			if (CheckForDuplicate())
				return;
			
			RefreshLayers();
		}
		
		void GetCameraSize_INTERNAL()
		{
			cameraWidthRaw = GetParallaxCamera().pixelWidth;
			cameraHeightRaw = GetParallaxCamera().pixelHeight;
			
			if (automatic)
			{
				minSize = new Vector2(cameraWidthRaw, cameraHeightRaw);
				maxSize = new Vector2(cameraWidthRaw, cameraHeightRaw);
			}
			
			cameraWidth = Mathf.Clamp(cameraWidthRaw, minSize.x, maxSize.x);
			cameraHeight = Mathf.Clamp(cameraHeightRaw, minSize.y, maxSize.y);
			
			cameraAspectRatio = cameraWidth / cameraHeight;
			cameraAspectRatioRaw = cameraWidthRaw / cameraHeightRaw;
		}
		
		void LateUpdate()
		{
			if (CheckForDuplicate())
			{
				scrollValue = Vector2.zero;
				return;
			}
			
			if (!GetParallaxCamera())
			{
				scrollValue = Vector2.zero;
				return;
			}
			
			GetCameraSize_INTERNAL();
			
			if (cameraWidth == 0 || cameraHeight == 0)
			{
				scrollValue = Vector2.zero;
				return;
			}
			
#if UNITY_EDITOR
				// Force automatic refreshing in the editor
				if (!Application.isPlaying || autoRefreshBillboards)
				{
					RefreshAllBillboards();
				}
#else		
			if (autoRefreshBillboards)
			{
				RefreshAllBillboards();
			}
#endif			
			foreach (ScrollLayer scrollLayer in scrollLayers)
			{
					
				if (!scrollLayer)
					continue;
				
				if (scrollLayer.enabled)
				{
					if (scrollLayer.isObjectLayer)
					{
						Camera cam = GetParallaxCamera();
						
						float xx = 1;
						float yy = 1;
						float ww = cameraWidth / 2;
						float hh = cameraHeight / 2;
						
						if (scrollLayer.objectLayerPixelSpace)
						{	
							xx = GetPixelWidth(cam, scrollLayer.cachedTransform.position) * ww;
							yy = GetPixelWidth(cam, scrollLayer.cachedTransform.position) * hh;
						}
						
						if (scrollConstraints == ScrollConstraints.X)
						{
							scrollLayer.cachedTransform.position += cam.transform.right * xx * -scrollValue.x * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();
						}
						else if (scrollConstraints == ScrollConstraints.Y)
						{
							scrollLayer.cachedTransform.position += cam.transform.up * yy * -scrollValue.y * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();				
						}
						else
						{
							Vector3 sV = (cam.transform.right * xx * -scrollValue.x) + (cam.transform.up * yy * -scrollValue.y);
							scrollLayer.cachedTransform.position += sV * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();								
						}
					}
					else if (scrollLayer.GetMaterial())
					{
						foreach (string textureName in scrollLayer.GetTextureNames())
						{
							if (string.IsNullOrEmpty(textureName))
								continue;
							
							if (scrollLayer.GetMaterial().HasProperty(textureName))
							{
								if (scrollConstraints == ScrollConstraints.X)
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + new Vector2(scrollValue.x, 0) * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));
								}
								else if (scrollConstraints == ScrollConstraints.Y)
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + new Vector2(0, scrollValue.y) * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));
								}
								else
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + scrollValue * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));				
								}
							}
						}
					}
				}
			}
			
			scrollValue = Vector2.zero;
		}
	
		Vector2 WrapVector(Vector2 input)
		{
			return new Vector2(input.x - (int)input.x, input.y - (int)input.y);	
		}
		
		
		/// <summary>
		/// Gets the width of a pixel at a world space position, relative to a camera.
		/// </summary>
		internal static float GetPixelWidth(Camera camera, Vector3 position)
		{
			//Get the screen coordinate of some point
			var screenPos = camera.WorldToScreenPoint(position);
			var offset = Vector3.zero;

			//Offset by 1 pixel
			if (screenPos.x > 0)
				offset = screenPos - Vector3.right;
			else
				offset = screenPos + Vector3.right;

			if (screenPos.y > 0)
				offset = screenPos - Vector3.up;
			else
				offset = screenPos + Vector3.up;

			//Get the world coordinate once offset.
			offset = camera.ScreenToWorldPoint(offset);

			return (camera.transform.InverseTransformPoint(position) - camera.transform.InverseTransformPoint(offset)).magnitude;	
		}
	}
#if !UNITY_3_5 && !UNITY_3_4 && !UNITY_3_3
}
#endif