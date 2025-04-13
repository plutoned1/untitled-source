using System;
using HarmonyLib;
using PlayFab;

namespace untitled.Core.Patches
{
	// Token: 0x0200001A RID: 26
	[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportDeviceInfo")]
	internal class NoDeviceInfo2
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C558
		private static bool Prefix()
		{
			return false;
		}
	}
}
