using System;
using HarmonyLib;
using UnityEngine;

namespace untitled.Core.Patches
{
	// Token: 0x02000025 RID: 37
	[HarmonyPatch(typeof(GameObject), "CreatePrimitive", 0)]
	public class GameObjectPatch
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C68C
		public static void Postfix(GameObject __result)
		{
			bool flag = __result.GetComponent<Renderer>() != null;
			if (flag)
			{
				__result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
				__result.GetComponent<Renderer>().material.color = Color.black;
			}
		}
	}
}
