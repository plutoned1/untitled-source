using System;
using HarmonyLib;
using PlayFab.Internal;

namespace untitled.Core.Patches
{
	// Token: 0x0200001C RID: 28
	[HarmonyPatch(typeof(PlayFabDeviceUtil), "SendDeviceInfoToPlayFab")]
	internal class NoDeviceInfo
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C590
		private static bool Prefix()
		{
			return false;
		}
	}
}
