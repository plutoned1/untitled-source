using System;
using System.Collections.Generic;
using HarmonyLib;
using PlayFab;
using PlayFab.ClientModels;

namespace untitled.Core.Patches
{
	// Token: 0x0200001E RID: 30
	[HarmonyPatch(typeof(PlayFabClientAPI), "ReportPlayer", 0)]
	public class PlayFabReportPatch2
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C5C8
		private static bool Prefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			return false;
		}
	}
}
