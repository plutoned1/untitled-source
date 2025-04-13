using System;
using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;
using untitled.Core;
using Untitled.Core.Scripts;
using untitled.Core.Scripts;
using untitled.Core.Utilities;

namespace untitled
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
	public class Initializer : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002B28
		private static void Postfix()
		{
			bool flag = Initializer.menuObject != null;
			if (!flag)
			{
				try
				{
					Debug.Log("Initializing menu EARLY");
					Initializer.menuObject = new GameObject("untitled's Cheat Panel");
					Initializer.menuObject.AddComponent<Menu>();
					Initializer.menuObject.AddComponent<PCGUI>();
					Initializer.menuObject.AddComponent<Input>();
					Initializer.menuObject.AddComponent<Notifications>();
					Initializer.menuObject.AddComponent<Boards>();
					Initializer.menuObject.AddComponent<Ghost>();
					Initializer.menuObject.AddComponent<Coroutines>();
					Initializer.menuObject.AddComponent<Plugin>();
					Initializer.menuObject.AddComponent<PluginManager>();
					Initializer.menuObject.AddComponent<UpdateBasedElfsManager>();
					Object.DontDestroyOnLoad(Initializer.menuObject);
					Initializer.menuObject.hideFlags = 61;
				}
				catch (Exception ex)
				{
					Debug.LogError("Failed to initialize menu: " + ex.Message);
				}
			}
		}

		// Token: 0x04000019 RID: 25
		private static GameObject menuObject;
	}
}
