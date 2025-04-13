using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000020 RID: 32
	[HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin))]
	[HarmonyPatch("LateUpdate", 0)]
	public class NoGracePeriod4
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C600
		public static bool Prefix()
		{
			return false;
		}
	}
}
