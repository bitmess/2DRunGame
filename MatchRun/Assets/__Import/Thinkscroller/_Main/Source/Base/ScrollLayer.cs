using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

#if !UNITY_3_5 && !UNITY_3_4 && !UNITY_3_3
namespace ThinksquirrelSoftware.Thinkscroller
{
#endif
	/// <summary>
	/// Scroll layer component.
	/// </summary>
	/// <remarks>
	/// Scroll layers represent each parallax layer, and can be both in the foreground and background.
	/// Scroll layers are sorted by their weight - this can be done automatically, or manually overriden.
	/// Layers can also be stretched across the screen or pixel-perfect.
	/// Note that scroll layers are independent of Unity's layer system.
	/// </remarks>
	/*! \page scroll_layer_a Scroll Layer (Auto-Billboard Layer)
	 *
	 * \section overview Overview
	 * The Auto-Billboard Scroll Layer automatically creates a billboard (camera-facing mesh) and scrolls the UVs of a texture placed on it. Only orthographic cameras are officially supported in this mode.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_a.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be enabled for this mode.
	 * 		- <b>X, Y</b> - The size of the billboard (in world units). This is disabled if the layer is in pixel-perfect mode.
	 * 		- <b>Stretch</b> - Stretches the billboard to fill the screen. This is disabled if the layer is in pixel-perfect mode.
	 * 		- <b>Pixel Perfect</b> - If enabled, the billboard's screen size will match the size of the texture, for pixel-perfect rendering.
	 * 		- <b>Texture</b> - The texture to scroll.
	 * 		- <b>Dimensions</b> - Displays the dimensions of the selected texture.
	 * - <b>Advanced</b>
	 * 		- <b>Alignment</b> - The screen alignment for the billboard.
	 * 		- <b>Offset X, Offset Y</b> - The offset for the billboard, in pixel space (unless overriden through script).
	 * 		- <b>UV Padding</b> - The inset for the layer UVs, in UV (0..1) space. If there are lines visible at the edges, set this to a low negative value.
	 * 		- <b>Tile X</b> - If enabled, repeat the billboard on the X axis. (Pixel-perfect only)
	 * 		- <b>Tile Y</b> - If enabled, repeat the billboard on the Y axis. (Pixel-perfect only)
	 * 		- <b>Border X, Border Y</b> - Additional texture correction, in pixel space. This should usually stay at 0. (Pixel-perfect only)
	 * 		- <b>Normals, Tangents</b> - Determines whether normal or tangent information should be calculated for the billboard.
	 * 		- <b>On Awake()...</b> - Determines if and how billboards are refreshed on Awake.
	 * 		- <b>Refresh Now</b> - Refresh the billboard now, in the editor. This is usually automatic.
	 * 	- <b>Texture Names</b>
	 * 		- <b>Texture Names Array</b> - Use this when using a custom shader. Texture Names correspond to the names in a shader. \htmlonly Thinkscroller\endhtmlonly will only scroll the listed texture names. Defaults to _MainTex.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	/*! \page scroll_layer_b Scroll Layer (UV Scrolling Layer)
	 *
	 * \section overview Overview
	 * The UV Scrolling Layer will scroll the textures on a mesh.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_b.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be disabled for this mode.
	 * 		- <b>Object Layer</b> - This must be disabled for this mode.
	 * 		- <b>Renderer</b> - The mesh renderer to scroll.
	 * 	- <b>Texture Names</b>
	 * 		- <b>Texture Names Array</b> - Use this when using a custom shader. Texture Names correspond to the names in a shader. \htmlonly Thinkscroller\endhtmlonly will only scroll the listed texture names. Defaults to _MainTex.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	/*! \page scroll_layer_c Scroll Layer (Object Layer)
	 *
	 * \section overview Overview
	 * The Object Layer will scroll the transform of a game object.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_c.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be disabled for this mode.
	 * 		- <b>Object Layer</b> - This must be enabled for this mode.
	 * 		- <b>Use Pixel Space</b> - If enabled, object layers will use pixel space to scroll. Use this to match scroll speeds with non-object layers.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	[AddComponentMenu("Thinkscroller/Scroll Layer")]
	public sealed class ScrollLayer : ThinkscrollerBase
#if !UNITY_FLASH
		, System.IComparable<ScrollLayer>
#endif	
	{
		[SerializeField] [HideInInspector] MeshFilter meshFilter;
		[SerializeField] [HideInInspector] MeshRenderer meshRenderer;
		[SerializeField] [HideInInspector] bool autoBillboard = true;
		[SerializeField] [HideInInspector] bool objectLayer = false;
		[SerializeField] [HideInInspector] bool _objectLayerPixelSpace = true;
		[SerializeField] [HideInInspector] bool auto = true;
		[SerializeField] [HideInInspector] float weight = 0;
		[SerializeField] [HideInInspector] bool stretch = false;
		[SerializeField] [HideInInspector] bool pixelPerfect = true;
		[SerializeField] [HideInInspector] bool _tileX = true;
		[SerializeField] [HideInInspector] bool _tileY = true;
		[SerializeField] [HideInInspector] Vector2 _offset = Vector2.zero;
		[SerializeField] [HideInInspector] Vector2 _pixelOffset = Vector2.zero;
		[SerializeField] [HideInInspector] ScrollLayerAlignment mAlignment = ScrollLayerAlignment.Center;
		[SerializeField] [HideInInspector] Vector2 _padding = Vector2.zero;
		[SerializeField] [HideInInspector] float xSize = 10;
		[SerializeField] [HideInInspector] float ySize = 10;
		[SerializeField] [HideInInspector] float scrollSpeed = 1;
		[SerializeField] [HideInInspector] float scrollMod = 1;
		[SerializeField] [HideInInspector] Texture2D texture;
		[SerializeField] [HideInInspector] string[] textureNames;
		[SerializeField] [HideInInspector] bool _refreshVertsOnAwake = true;
		[SerializeField] [HideInInspector] bool _refreshMaterialOnAwake = true;
		[SerializeField] [HideInInspector] bool _refreshTextureOnAwake = true;
		[SerializeField] [HideInInspector] bool _calculateNormals = false;
		[SerializeField] [HideInInspector] bool _calculateTangents = false;
		[SerializeField] [HideInInspector] Vector3 oldPosition = Vector3.zero;
		/*! \cond PRIVATE */
		[HideInInspector] public bool textureNamesFoldout_EDITOR = true;
		[HideInInspector] public int foldoutSize_EDITOR = 1;
		[HideInInspector] public bool advancedFoldout_EDITOR = false;
		/*! \endcond */
		
		/// <summary>
		/// If true, refresh materials on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		public bool refreshMaterialOnAwake
		{
			get
			{
				return _refreshMaterialOnAwake;
			}
			set
			{
				_refreshMaterialOnAwake = value;
			}
		}
		
		/// <summary>
		/// If true, refresh the texture on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		public bool refreshTextureOnAwake
		{
			get
			{
				return _refreshTextureOnAwake;
			}
			set
			{
				_refreshTextureOnAwake = value;
			}
		}
		
		/// <summary>
		/// If true, refresh vertices on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		public bool refreshVertsOnAwake
		{
			get
			{
				return _refreshVertsOnAwake;
			}
			set
			{
				_refreshVertsOnAwake = value;
			}
		}
		
		/// <summary>
		/// If true, calculate normals on the billboard mesh.
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		public bool calculateNormals
		{
			get
			{
				return _calculateNormals;
			}
			set
			{
				if (value == false)
				{
					_calculateTangents = false;
				}
				_calculateNormals = value;
			}
		}
		
		/// <summary>
		/// If true, calculate tangents on the billboard mesh.
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		public bool calculateTangents
		{
			get
			{
				return _calculateTangents;
			}
			set
			{
				if (value == true)
				{
					_calculateNormals = true;
				}
				_calculateTangents = value;
			}
		}
		
		public void SetWeight(float weight)
		{
			SetWeight(weight, false);
		}
		
		/// <summary>
		/// Sets the weight of the scroll layer. This will force a speed change if the speed was previously overwritten, even if the weight is the same as the current weight.
		/// <remarks>
		/// Lighter weights scroll faster than heavier weights.
		/// </remarks>
		/// </summary>
		public void SetWeight(float weight, bool force)
		{
			if (force || this.weight != weight)
			{
				this.weight = weight;
				this.scrollSpeed = 1f / (weight + .00001f);
			}
		}
		
		/// <summary>
		/// Gets the weight of the scroll layer.
		/// </summary>
		/// <remarks>
		/// Lighter weights scroll faster than heavier weights.
		/// </remarks>
		public float GetWeight()
		{
			return weight;
		}
		
		/// <summary>
		/// Gets the scrolling speed of the scroll layer.
		/// </summary>
		public float GetScrollSpeed()
		{
			return scrollSpeed;
		}
		
		/// <summary>
		/// Sets the scrolling speed of the scroll layer directly, overriding the weight.
		/// </summary>
		public void SetScrollSpeed(float scrollSpeed)
		{
			if (this.scrollSpeed != scrollSpeed)
			{
				this.scrollSpeed = scrollSpeed;
			}
		}
		
		/// <summary>
		/// Gets the scrolling speed modifier of the scroll layer.
		/// </summary>
		public float GetScrollMod()
		{
			return scrollMod;
		}
		
		/// <summary>
		/// Sets the scrolling speed modifier of the scroll layer.
		/// </summary>
		public void SetScrollMod(float scrollMod)
		{
			if (this.scrollMod != scrollMod)
			{
				this.scrollMod = scrollMod;
			}
		}
		
		/// <summary>
		/// Determines whether the scroll layer is automatically configured.
		/// </summary>
		public bool isAutoConfigured
		{
			get
			{
				return auto;
			}
			set
			{
				if (value == true && auto == false)
				{
					auto = value;
					if (Parallax.instance)
					{
						Parallax.instance.RefreshLayers();
					}
				}
				else
				{
					auto = value;
				}
			}
		}
		
		/// <summary>
		/// Determines whether the scroll layer has an automatic billboard.
		/// </summary>
		public bool isAutoBillboard
		{
			get
			{
				return autoBillboard;
			}
			set
			{
				if (value == true && autoBillboard == false)
				{
					objectLayer = false;
					autoBillboard = value;
					RefreshBillboard();
				}
				else if (value == false && autoBillboard == true)
				{
					if (meshRenderer)
					{
						DestroyImmediate(meshRenderer);
					}
					if (meshFilter)
					{
						DestroyImmediate(meshFilter);
					}
					autoBillboard = value;
				}
					
			}
		}
		
		/// <summary>
		/// Determines whether the scroll layer is an object layer.
		/// </summary>
		/// <remarks>
		/// Object layers scroll the transform and ignore any textures on the object. It can also scroll groups of objects. This offers compatibility with sprite/2D management systems.
		/// </remarks>
		public bool isObjectLayer
		{
			get
			{
				return objectLayer;
			}
			set
			{
				if (value == true && objectLayer == false)
				{
					isAutoBillboard = false;
				}
				objectLayer = value;
			}
		}
		
		/// <summary>
		/// If true, object layers scroll in pixel space (similar to auto-billboards), as opposed to world space.
		/// </summary>
		/// <remarks>
		/// Note that object layers do not always align perfectly with auto-billboards over long distances, due to floating point calculation.
		/// </remarks>
		public bool objectLayerPixelSpace
		{
			get
			{
				return _objectLayerPixelSpace;
			}
			set
			{
				_objectLayerPixelSpace = value;
			}
		}
		
		/// <summary>
		/// Determines whether the auto billboard is pixel-perfect.
		/// </summary>
		public bool isPixelPerfect
		{
			get
			{
				return pixelPerfect;
			}
			set
			{
				if (value == true && pixelPerfect == false)
				{
					pixelPerfect = value;
					SetBillboardStretch(false);
					RefreshBillboard(true, false, false);
				}
				else if (value == false && pixelPerfect == true)
				{
					pixelPerfect = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		/// <summary>
		/// Determines whether to tile on the X axis.
		/// </summary>
		public bool tileX
		{
			get
			{
				return _tileX;
			}
			set
			{
				if (value != _tileX)
				{
					_tileX = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		/// <summary>
		/// Determines whether to tile on the Y axis
		/// </summary>
		public bool tileY
		{
			get
			{
				return _tileY;
			}
			set
			{
				if (value != _tileY)
				{
					_tileY = value;
					RefreshBillboard(true, false, false);
				
				}
			}
		}
		
		/// <summary>
		/// The offset (in pixels) of the parallax billboard.
		/// </summary>
		public Vector2 offset
		{
			get
			{
				return _offset;
			}
			set
			{
				if (value != _offset)
				{
					_offset = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		/// <summary>
		/// The offset (in pixels) of the texture size. Used for fixing border issues.
		/// </summary>
		public Vector2 pixelOffset
		{
			get
			{
				return _pixelOffset;
			}
			set
			{
				if (value != _pixelOffset)
				{
					_pixelOffset = value;
					RefreshBillboard (true, false, false);
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the scroll layer's alignment.
		/// </summary>
		public ScrollLayerAlignment alignment
		{
			get
			{
				return mAlignment;
			}
			set
			{
				if (value != mAlignment)
				{
					mAlignment = value;
					RefreshBillboard (true, false, false);
				}
			}
		}
		
		/// <summary>
		/// UV padding. Used for fixing border issues.
		/// </summary>
		public Vector2 padding
		{
			get
			{
				return _padding;
			}
			set
			{
				if (value != _padding)
				{
					_padding = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		/// <summary>
		/// Gets the auto billboard scale, in world units.
		/// </summary>
		public Vector2 GetBillboardScale()
		{
			return new Vector2(xSize, ySize);
		}
		
		/// <summary>
		/// Sets the auto billboard scale, in world units.
		/// </summary>
		public void SetBillboardScale(Vector2 scale)
		{
			SetBillboardScale(scale.x, scale.y);
		}
		
		/// <summary>
		/// Sets the auto billboard scale, in world units.
		/// </summary>
		public void SetBillboardScale(float xSize, float ySize)
		{
			bool changed = false;
			
			if (this.xSize != xSize)
			{
				this.xSize = xSize;
				changed = true;
			}
			if (this.ySize != ySize)
			{
				this.ySize = ySize;
				changed = true;
			}
			if (changed)
				RefreshBillboard(true, false, false);
		}
		
		/// <summary>
		/// If true, the billboard will stretch to fit the camera viewport.
		/// </summary>
		/// <remarks>
		/// Only works while in play mode.
		/// </remarks>
		public bool GetBillboardStretch()
		{
			return stretch;
		}
		
		/// <summary>
		/// Sets whether the billboard will stretch.
		/// </summary>
		public void SetBillboardStretch(bool stretch)
		{
			this.stretch = stretch;
		}
		
		/// <summary>
		/// Gets the mesh renderer associated with the scroll layer.
		/// </summary> 
		public MeshRenderer GetRenderer()
		{
			return meshRenderer;
		}
		
		/// <summary>
		/// Sets the mesh renderer associated with the scroll layer.
		/// </summary>
		public void SetRenderer(MeshRenderer renderer)
		{
			meshRenderer = renderer;
		}
		
		/// <summary>
		/// Gets the material associated with the scroll layer.
		/// </summary>
		public Material GetMaterial()
		{
			if (autoBillboard)
			{
				if (!meshRenderer)
				{
					meshRenderer = gameObject.AddComponent<MeshRenderer>();	
					ResetMaterial_INTERNAL();
				}
				else if (meshRenderer.sharedMaterial == null)
				{
					ResetMaterial_INTERNAL();
				}
				
				return meshRenderer.sharedMaterial;
			}
			else if (meshRenderer)
			{
				if (meshRenderer.sharedMaterial != null)
				{
					return meshRenderer.sharedMaterial;
				}
			}
			return null;
		}
		
		/// <summary>
		/// Sets the material associated with the scroll layer.
		/// </summary>
		public void SetMaterial(Material material)
		{
			if (meshRenderer)
			{
				meshRenderer.sharedMaterial = material;
			}
		}
		
		public Texture2D GetTexture()
		{
			return texture;
		}
		
		public void SetTexture(Texture2D texture)
		{
			if (this.texture != texture)
			{
				this.texture = texture;
			}
		}
		
		/// <summary>
		/// Gets the texture names associated with a scroll layer.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		public string[] GetTextureNames()
		{
			return textureNames;
		}
		
		/// <summary>
		/// Gets the texture name associated with a scroll layer at the specified index.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		public string GetTextureName(int index)
		{
			if (textureNames.Length > index)
			{
				return textureNames[index];
			}
			return "";
		}
		
		/// <summary>
		/// Sets the texture names associated with a scroll layer.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		public void SetTextureNames(params string[] textureNames)
		{
			this.textureNames = textureNames;
		}
		
		/// <summary>
		/// Sets the texture name associated with a scroll layer at the specified index.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		public void SetTextureName(int index, string textureName)
		{
			if (textureNames.Length > index)
			{
				textureNames[index] = textureName;
			}
		}

#if !UNITY_FLASH
		/// <summary>
		/// Compares a scroll layer to another by weight.
		/// </summary>
		/// <remarks>
		/// Implements System.IComparable<ScrollLayer>.
		/// </remarks>
		public int CompareTo(ScrollLayer other)
		{
	        // If other is not a valid object reference, this instance is greater.
			if (other == null) return 1;

			// Compare the underlying weights.
			return GetWeight().CompareTo(other.GetWeight());
		}	
#endif
		internal static int Comparison(ScrollLayer layer, ScrollLayer other)
		{
			// If other is not a valid object reference, this instance is greater.
			if (other == null) return 1;
			
			// Compare the underlying weights.
			float weight = layer.GetWeight();
			float otherWeight = other.GetWeight();
			return weight < otherWeight ? -1 : (weight > otherWeight ? 1 : 0 );	
		}
		/* Behaviours */
		
		private void RefreshVertices_INTERNAL()
		{
			if (!Parallax.instance)
				return;
			
			if (!Parallax.instance.GetParallaxCamera())
				return;
			
			Camera parallaxCam = Parallax.instance.GetParallaxCamera();
			float distance = Vector3.Distance(parallaxCam.transform.position, transform.position);
			Vector3 topLeft = Vector3.zero;
			Vector3 topRight = Vector3.zero;
			Vector3 bottomLeft = Vector3.zero;
			Vector3 bottomRight = Vector3.zero;
			Vector3 ppxy0 = Vector3.zero;
			Vector3 ppxy1 = Vector3.zero;
			
			Matrix4x4 m = parallaxCam.cameraToWorldMatrix;
			Matrix4x4 mm = parallaxCam.projectionMatrix.inverse;
			float x = parallaxCam.orthographicSize * Parallax.instance.GetCameraAspectRatio();
			float width = 0;
			float height = 0;
			
			float pixel = Parallax.GetPixelWidth(parallaxCam, this.cachedTransform.position);
			Vector2 offset = this.offset * pixel;
			
			switch(alignment)
			{
			case ScrollLayerAlignment.TopLeft:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel,
								Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Top:
					offset -= new Vector2(0, Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.TopRight:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel,
								Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Left:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel, 0);
					break;
			
			case ScrollLayerAlignment.Center:
					offset -= Vector2.zero;
					break;
			
			case ScrollLayerAlignment.Right:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel, 0);
					break;
			
			case ScrollLayerAlignment.BottomLeft:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel,
								-Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Bottom:
					offset -= new Vector2(0, -Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.BottomRight:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel,
								-Parallax.instance.GetCameraHeightRaw() * .5f * pixel);
					break;
			}
			
			if (texture)
			{
				width = (texture.width + pixelOffset.x);
				height = (texture.height + pixelOffset.y);
			
				if (!stretch && pixelPerfect && tileX && !tileY)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
				}
				else if (!stretch && pixelPerfect && tileY && !tileX)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					float xS = -(ppxy1.y - ppxy0.y) * (width / height);
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
				}
				else if (!stretch && pixelPerfect && !tileY && !tileX)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					float xS = -(ppxy1.y - ppxy0.y) * (width / height);
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
	
				}
				else
				{
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
				}
			}
			
			
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[4]{
				new Vector3(topLeft.x, topLeft.y, 0),
				new Vector3(topRight.x, topRight.y, 0),
				new Vector3(bottomLeft.x, bottomLeft.y, 0),
				new Vector3(bottomRight.x, bottomRight.y, 0)
			};
			mesh.triangles = new int[6]{0,1,2,1,3,2};
			
			if (calculateNormals)
			{
				mesh.RecalculateNormals();
			}
			
			if (!stretch)
			{
				float xx = -(x / xSize);
				float yy = -(parallaxCam.orthographicSize / ySize);
				
				if (pixelPerfect && texture)
				{
					if (tileX && tileY)
					{
						ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
						ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					}
					
					ySize = -(ppxy1.y - ppxy0.y);
					
					xSize = ySize * width / height;
					
					if (!tileX && !tileY)
					{
						xx = -1 - padding.x;
						yy = -1 - padding.y;
					}
					else
					{
						if (tileX)
						{
							xx = -(x / xSize) - padding.x;
						}
						else
						{
							xx = -1 - padding.x;
						}
						
						if (tileY)
						{
							yy = -(parallaxCam.orthographicSize / ySize) - padding.y;
						}
						else
						{
							yy = -1 - padding.y;
						}
					}
					
				}
				
				mesh.uv = new Vector2[4]{
					new Vector2(padding.x, yy),
					new Vector2(xx, yy),
					new Vector2(padding.x, padding.y),
					new Vector2(xx, padding.y),
				};
			}
			else
			{
				mesh.uv = new Vector2[4]{
					new Vector3(padding.x, - padding.y),
					new Vector3(-1 - padding.x, - padding.y),
					new Vector3(padding.x, 1 - padding.y),
					new Vector3(-1 - padding.x, 1 - padding.y)
				};
			}
			
			if (calculateNormals && calculateTangents)
			{
				RecalculateTangents(mesh);
			}
			
			;
			
			if (meshFilter.sharedMesh)
			{
				DestroyImmediate(meshFilter.sharedMesh);
			}
			meshFilter.sharedMesh = mesh;
		}
		
		private void ResetMaterial_INTERNAL()
		{
			if (meshRenderer.sharedMaterial)
			{
				DestroyImmediate(meshRenderer.sharedMaterial);
			}
			meshRenderer.sharedMaterial = new Material(Shader.Find("Thinkscroller/Parallax Default"));
			textureNames = new string[1]{ "_MainTex" };
		}
		
		private void RefreshTexture_INTERNAL()
		{
			if (texture)
			{
				meshRenderer.sharedMaterial.mainTexture = texture;
			}
		}
		
		/// <summary>
		/// Refreshes the auto billboard mesh.
		/// </summary>
		public void RefreshBillboard ()
		{
			RefreshBillboard (true, true, true);
		}
		
		/// <summary>
		/// Refreshes the auto billboard mesh.
		/// </summary>
		/// <param name='refreshVertices'>
		/// If true, refresh vertices.
		/// </param>
		/// <param name='resetMaterial'>
		/// If true, reset material.
		/// </param>
		/// <param name='refreshTexture'>
		/// If true, refresh texture.
		/// </param>
		public void RefreshBillboard (bool refreshVertices, bool resetMaterial, bool refreshTexture)
		{
			if (!meshFilter && gameObject.GetComponent<MeshFilter>())
			{
				meshFilter = gameObject.GetComponent<MeshFilter>();
			}
			else if (!meshFilter)
			{
				meshFilter = gameObject.AddComponent<MeshFilter>();
			}
			
			if (gameObject.GetComponent<MeshRenderer>())
			{
				meshRenderer = gameObject.GetComponent<MeshRenderer>();
				meshRenderer.castShadows = false;
				meshRenderer.receiveShadows = false;
			}
			else if (gameObject.GetComponent<MeshRenderer>() == null)
			{
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
				meshRenderer.castShadows = false;
				meshRenderer.receiveShadows = false;
			}
			
			if (refreshVertices)
				RefreshVertices_INTERNAL();
			if (resetMaterial)
				ResetMaterial_INTERNAL();
			if (refreshTexture)
				RefreshTexture_INTERNAL();
		}
		
		/// <summary>
		/// Reset the scroll layer.
		/// </summary>
		public void Reset()
		{	
			if (!Parallax.instance)
			{
				return;
			}
			if (autoBillboard)
			{
				RefreshBillboard();
			}
			Parallax.instance.RefreshLayers();
		}
		
		/// <summary>
		/// Reset the scroll layer's position (Object layers only).
		/// </summary>
		public void ResetPosition()
		{
			if (isObjectLayer)
			{
				transform.position = oldPosition;
			}
		}
		
		/// <summary>
		/// Reset the scroll layer's position (Object layers only).
		/// </summary>
		public void ResetPosition(Vector3 resetPosition)
		{
			if (isObjectLayer)
			{
				transform.position = resetPosition;
				oldPosition = transform.position;
			}
		}
		
		IEnumerator ResetDelay_INTERNAL()
		{
			yield return new WaitForEndOfFrame();
			
			if (Parallax.instance)
			{
				DoReset();
				Parallax.instance.RefreshLayers();
			}
		}
		
		internal void DoReset()
		{
			if (autoBillboard && (_refreshMaterialOnAwake || _refreshTextureOnAwake || _refreshVertsOnAwake))
			{
				RefreshBillboard(_refreshVertsOnAwake, _refreshMaterialOnAwake, _refreshTextureOnAwake);
			}
		}
		
		/* MonoBehaviours */
		
		void Awake()
		{
			oldPosition = transform.position;
			StartCoroutine(ResetDelay_INTERNAL());
		}
		
		void OnDisable()
		{
			if (Parallax.instance)
			{
				Parallax.instance.RefreshLayers();
			}
		}
		
		void OnEnable()
		{
			if (Parallax.instance)
			{
				Parallax.instance.RefreshLayers();
			}
		}
	
		/// <summary>
		/// Recalculates the tangents of a mesh.
		/// </summary>
		static void RecalculateTangents(Mesh mesh)
		{
			int triangleCount = mesh.triangles.Length / 3;
			int vertexCount = mesh.vertices.Length;
	
			Vector3[] tan1 = new Vector3[vertexCount];
			Vector3[] tan2 = new Vector3[vertexCount];
	
			Vector4[] tangents = new Vector4[vertexCount];
	
			for (long a = 0; a < triangleCount; a+=3)
			{
				long i1 = mesh.triangles[a + 0];
				long i2 = mesh.triangles[a + 1];
				long i3 = mesh.triangles[a + 2];
	
				Vector3 v1 = mesh.vertices[i1];
				Vector3 v2 = mesh.vertices[i2];
				Vector3 v3 = mesh.vertices[i3];
	
				Vector2 w1 = mesh.uv[i1];
				Vector2 w2 = mesh.uv[i2];
				Vector2 w3 = mesh.uv[i3];
	
				float x1 = v2.x - v1.x;
				float x2 = v3.x - v1.x;
				float y1 = v2.y - v1.y;
				float y2 = v3.y - v1.y;
				float z1 = v2.z - v1.z;
				float z2 = v3.z - v1.z;
	
				float s1 = w2.x - w1.x;
				float s2 = w3.x - w1.x;
				float t1 = w2.y - w1.y;
				float t2 = w3.y - w1.y;
	
				float r = 1.0f / (s1 * t2 - s2 * t1);
	
				Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
				Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
	
				tan1[i1] += sdir;
				tan1[i2] += sdir;
				tan1[i3] += sdir;
	
				tan2[i1] += tdir;
				tan2[i2] += tdir;
				tan2[i3] += tdir;
			}
	
			for (long a = 0; a < vertexCount; ++a)
			{
				Vector3 n = mesh.normals[a];
				Vector3 t = tan1[a];
	
				Vector3 tmp = (t - n * Vector3.Dot(n, t)).normalized;
				tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
	
				tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
			}
	
			mesh.tangents = tangents;
		}
	}
#if !UNITY_3_5 && !UNITY_3_4 && !UNITY_3_3
}
#endif