using System;
using HarmonyLib;
using PlayFab.Internal;

namespace untitled.Core.Patches
{
	// Token: 0x02000017 RID: 23
	[HarmonyPatch(typeof(PlayFabHttp), "InitializeScreenTimeTracker")]
	internal class NoInitializeScreenTimeTracker
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C504
		private static bool Prefix()
		{
			return false;
		}
	}
}
