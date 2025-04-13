using System;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using untitled.Core.Scripts;

namespace untitled.Core.Patches
{
	// Token: 0x02000026 RID: 38
	[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
	public class JoinPatch
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C6E8
		public static void Prefix(Player newPlayer)
		{
			bool flag = newPlayer != JoinPatch.lastPlayerLeft;
			if (flag)
			{
				Notifications.SendNotification("<color=grey>[</color><color=green>JOIN</color><color=grey>] </color><color=white>Name: " + newPlayer.NickName + "</color>");
				JoinPatch.lastPlayerLeft = newPlayer;
			}
		}

		// Token: 0x0400008B RID: 139
		private static Player lastPlayerLeft;
	}
}
