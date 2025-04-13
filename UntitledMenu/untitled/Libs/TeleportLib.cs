using System;
using GorillaLocomotion;
using UnityEngine;
using untitled.Core;

namespace untitled.Libs
{
	// Token: 0x0200000A RID: 10
	public class TeleportLib
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00004644
		public static void TeleportTo(Vector3 position)
		{
			GTPlayer.Instance.TeleportTo(position - Menu.__instance.bodyCollider.transform.position + Menu.__instance.transform.position, GTPlayer.Instance.transform.rotation);
		}
	}
}
