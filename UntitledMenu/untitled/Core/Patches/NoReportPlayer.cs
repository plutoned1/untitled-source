using System;
using System.Collections.Generic;
using HarmonyLib;
using PlayFab;
using PlayFab.ClientModels;

namespace untitled.Core.Patches
{
	// Token: 0x0200001D RID: 29
	[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportPlayer", 0)]
	public class NoReportPlayer
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C5AC
		private static bool Prefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			return false;
		}
	}
}
