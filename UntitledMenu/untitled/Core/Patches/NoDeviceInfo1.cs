using System;
using HarmonyLib;
using PlayFab;

namespace untitled.Core.Patches
{
	// Token: 0x0200001B RID: 27
	[HarmonyPatch(typeof(PlayFabClientAPI), "ReportDeviceInfo")]
	internal class NoDeviceInfo1
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C574
		private static bool Prefix()
		{
			return false;
		}
	}
}
