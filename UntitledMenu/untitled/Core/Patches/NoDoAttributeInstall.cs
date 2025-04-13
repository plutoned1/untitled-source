using System;
using HarmonyLib;
using PlayFab.Internal;

namespace untitled.Core.Patches
{
	// Token: 0x02000019 RID: 25
	[HarmonyPatch(typeof(PlayFabDeviceUtil), "DoAttributeInstall")]
	internal class NoDoAttributeInstall
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C53C
		private static bool Prefix()
		{
			return false;
		}
	}
}
