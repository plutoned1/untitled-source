using System;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using untitled.Core.Scripts;

namespace untitled.Core.Patches
{
	// Token: 0x02000027 RID: 39
	[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
	public class LeavePatch
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C734
		public static void Prefix(Player newPlayer)
		{
			bool flag = newPlayer != LeavePatch.lastPlayerLeft;
			if (flag)
			{
				Notifications.SendNotification("<color=grey>[</color><color=red>LEFT</color><color=grey>] </color><color=white>Name: " + newPlayer.NickName + "</color>");
				LeavePatch.lastPlayerLeft = newPlayer;
			}
		}

		// Token: 0x0400008C RID: 140
		private static Player lastPlayerLeft;
	}
}
