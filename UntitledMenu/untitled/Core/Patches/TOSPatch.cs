using System;
using HarmonyLib;

namespace untitled.Core.Patches
{
	// Token: 0x02000028 RID: 40
	[HarmonyPatch(typeof(LegalAgreements))]
	[HarmonyPatch("PostUpdate", 0)]
	internal class TOSPatch
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C780
		private static bool Prefix(LegalAgreements __instance)
		{
			bool flag = TOSPatch.turnedthefuckon;
			bool result;
			if (flag)
			{
				Traverse.Create(__instance).Field("controllerBehaviour").Field("buttonDown").SetValue(true);
				Traverse.Create(__instance).Field("holdTime").SetValue(0.1f);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0400008D RID: 141
		public static bool turnedthefuckon;
	}
}
