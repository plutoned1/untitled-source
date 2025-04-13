using System;
using HarmonyLib;
using UnityEngine;

namespace untitled.Core.Patches
{
	// Token: 0x02000023 RID: 35
	[HarmonyPatch(typeof(Gorillanalytics), "Start", 5)]
	public class GorillanalyticsStart : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C654
		private static bool Prefix()
		{
			return false;
		}
	}
}
