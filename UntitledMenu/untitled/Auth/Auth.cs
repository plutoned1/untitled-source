using System;
using UnityEngine;
using untitled;

namespace Untitled.Auth
{
	// Token: 0x0200003F RID: 63
	public class Auth : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A3C4
		private void Start()
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<Initializer>();
			gameObject.AddComponent<Plugin>();
			Debug.Log("[AUTH - DEBUG]: Force authenticated");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A3F3
		private void OnGUI()
		{
			GUILayout.Label("WARNING THIS VERSION HAS AUTHENTICATION BLOCKED! REMOVE RETURN STATEMENTS FOR RELEASE!", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x0400014E RID: 334
		public static bool keyvalid = false;

		// Token: 0x0400014F RID: 335
		public static string keyinput = "";
	}
}
