using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000022 RID: 34
	[HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2))]
	[HarmonyPatch("LateUpdate", 0)]
	public class NoGracePeriod2
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C638
		public static bool Prefix()
		{
			return false;
		}
	}
}
