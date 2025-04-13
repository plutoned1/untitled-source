using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000015 RID: 21
	[HarmonyPatch(typeof(VRRig), "OnDisable", 0)]
	public class RigPatch
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C4B8
		public static bool Prefix(VRRig __instance)
		{
			return !(__instance == GorillaTagger.Instance.offlineVRRig);
		}
	}
}
