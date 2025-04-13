using System;
using UnityEngine;
using Untitled.Auth;

namespace Loading
{
	// Token: 0x02000003 RID: 3
	public class Loader : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002AF4
		public static void Load()
		{
			Loader.gameobject = new GameObject("untitledloaded");
			Loader.gameobject.AddComponent<Auth>();
			Object.DontDestroyOnLoad(Loader.gameobject);
		}

		// Token: 0x04000017 RID: 23
		private static GameObject gameobject;

		// Token: 0x04000018 RID: 24
		public static bool loaded;
	}
}
