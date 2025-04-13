using System;
using HarmonyLib;
using UnityEngine;

namespace untitled.Core.Patches
{
	// Token: 0x02000024 RID: 36
	[HarmonyPatch(typeof(Gorillanalytics), "UploadGorillanalytics", 0)]
	public class AnalyticsPatch : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C670
		private static bool Prefix()
		{
			return false;
		}
	}
}
