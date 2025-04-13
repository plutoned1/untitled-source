using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000021 RID: 33
	[HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2))]
	[HarmonyPatch("GracePeriod", 5)]
	public class NoGracePeriod3
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C61C
		public static bool Prefix()
		{
			return false;
		}
	}
}
