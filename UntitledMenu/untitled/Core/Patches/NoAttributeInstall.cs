using System;
using HarmonyLib;
using PlayFab;

namespace untitled.Core.Patches
{
	// Token: 0x02000016 RID: 22
	[HarmonyPatch(typeof(PlayFabClientAPI), "AttributeInstall")]
	internal class NoAttributeInstall
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C4E8
		private static bool Prefix()
		{
			return false;
		}
	}
}
