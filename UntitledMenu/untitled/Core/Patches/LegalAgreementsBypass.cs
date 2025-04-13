using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000029 RID: 41
	[HarmonyPatch(typeof(LegalAgreements), "Awake", 0)]
	internal class LegalAgreementsBypass
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C7F0
		private static bool Prefix()
		{
			return false;
		}
	}
}
