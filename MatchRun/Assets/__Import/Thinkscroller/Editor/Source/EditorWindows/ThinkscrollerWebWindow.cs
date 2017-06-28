// Web window
// WebWindow.cs
// Thinksquirrel Software Common Libraries
//  
// Authors:
//		 Daniele Giardini <http://www.holoville.com>
//       Josh Montoute <josh@thinksquirrel.com>
// 
// Original code available at http://forum.unity3d.com/threads/67149-Browsing-the-web-within-Unity-Editor
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
// Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
// OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// This file is available at https://github.com/Thinksquirrel-Software/Thinksquirrel-Common
//
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

#if UNITY_4
namespace ThinksquirrelSoftware.Thinkscroller
{
#endif
	public class ThinkscrollerWebWindow : EditorWindow
	{
		static Rect windowRect = new Rect(100,100,Mathf.Max(Screen.currentResolution.width - 200, 800),Mathf.Max(Screen.currentResolution.height - 200, 600));
		static BindingFlags fullBinding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
		static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
		
		object webView;
		Type webViewType;
		MethodInfo doGUIMethod;
		MethodInfo loadURLMethod;
		MethodInfo focusMethod;
		MethodInfo unFocusMethod;
		
		MethodInfo dockedGetterMethod;
		
		string urlText = "http://thinksquirrel.com";
		bool initialized = false;
		
		public static void Load(string title, string url) {
			ThinkscrollerWebWindow window = ThinkscrollerWebWindow.GetWindow<ThinkscrollerWebWindow>(title);
			window.urlText = url;
			window.Init();
	    }
		
		void Init() {
			if (!initialized)
			{
				//Set window rect
				this.position = windowRect;
				initialized = true;
			}
			
			//Init web view
			InitWebView();
		}
		
		private void InitWebView() {
			//Get WebView type
			webViewType = GetTypeFromAllAssemblies("WebView");
			
			webView = ScriptableObject.CreateInstance(webViewType);
			webViewType.GetMethod("InitWebView").Invoke(webView, new object[] {(int)position.width,(int)position.height,true});
			webViewType.GetMethod("set_hideFlags").Invoke(webView, new object[] {13});
			
			loadURLMethod = webViewType.GetMethod("LoadURL");
			loadURLMethod.Invoke(webView, new object[] {urlText});
			webViewType.GetMethod("SetDelegateObject").Invoke(webView, new object[] {this});
			
			doGUIMethod = webViewType.GetMethod("DoGUI");
			focusMethod = webViewType.GetMethod("Focus");
			unFocusMethod = webViewType.GetMethod("UnFocus");
	
			this.wantsMouseMove = true;
		
			//Get docked property getter MethodInfo
			dockedGetterMethod = typeof(EditorWindow).GetProperty("docked", fullBinding).GetGetMethod(true);
		}
		
		void OnGUI() {
			
			if(GUI.GetNameOfFocusedControl().Equals("urlfield"))
				unFocusMethod.Invoke(webView, null);
			
			if (dockedGetterMethod == null)
			{
				InitWebView();
			}
			
			bool isDocked = (bool)(dockedGetterMethod.Invoke(this, null));
			Rect webViewRect = new Rect(0,0,position.width,position.height - ((isDocked) ? 0 : 20));
			
			//Hidden, disabled, button for taking focus away from urlfield
			GUI.enabled = false;
			GUI.SetNextControlName("hidden");
			GUI.Button(new Rect(-20,-20,5,5), string.Empty);
			GUI.enabled = true;
			
			if(Event.current.isMouse && Event.current.type == EventType.MouseDown && webViewRect.Contains(Event.current.mousePosition)) {
				GUI.FocusControl("hidden");
				focusMethod.Invoke(webView, null);
			}
			
			//Web view
			if(webView != null)
			{		
				doGUIMethod.Invoke(webView, new object[] {webViewRect});
			}
		}
		
		private void OnWebViewDirty() {
			this.Repaint();
		}
		
		public static Type GetTypeFromAllAssemblies(string typeName) {
			Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach(Assembly assembly in assemblies) {
				Type[] types = assembly.GetTypes();
				foreach(Type type in types) {
					if(type.Name.Equals(typeName, ignoreCase) || type.Name.Contains('+' + typeName)) //+ check for inline classes
						return type;
				}
			}
			return null;
		}
		
		void OnDestroy() {
			//Destroy web view
			if (webViewType != null && webView != null)
				webViewType.GetMethod("DestroyWebView", fullBinding).Invoke(webView, null);
		}
	}
#if UNITY_4
}
#endif