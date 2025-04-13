using System;
using HarmonyLib;
using PlayFab.Internal;

namespace untitled.Core.Patches
{
	// Token: 0x02000018 RID: 24
	[HarmonyPatch(typeof(PlayFabDeviceUtil), "GetAdvertIdFromUnity")]
	internal class NoGetAdvertIdFromUnity
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C520
		private static bool Prefix()
		{
			return false;
		}
	}
}
