using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x0200001F RID: 31
	[HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin))]
	[HarmonyPatch("GracePeriod", 5)]
	public class NoGracePeriod
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C5E4
		public static bool Prefix()
		{
			return false;
		}
	}
}
