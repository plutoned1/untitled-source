using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using GorillaGameModes;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace untitled.Core.Utils
{
	// Token: 0x02000013 RID: 19
	public class Util
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C0BF
		public static PhotonView GetPhotonView(VRRig Player)
		{
			return Traverse.Create(Player).Field("netView").GetValue<NetworkView>().GetView;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C0DB
		public static PhotonView GetPhotonView(NetPlayer Player)
		{
			return Traverse.Create(Util.GetPlayerRig(Player)).Field("netView").GetValue<NetworkView>().GetView;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C0FC
		public static Color32 GetTransparentColor(Color32 color)
		{
			return new Color32(color.r, color.g, color.b, 100);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C117
		public static Color GetTransparentColor(Color color)
		{
			return new Color(color.r, color.g, color.b, 0.5f);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C135
		public static bool IsInfected(VRRig Player)
		{
			return Player.mainSkin.material.name.Contains("fected") || Player.mainSkin.material.name.Contains("hunted");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C170
		public static bool IsMyPlayer(VRRig Player)
		{
			return Player == GorillaTagger.Instance.offlineVRRig;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C184
		public static VRRig GetPlayerRig(NetPlayer netPlayer)
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig.Creator == netPlayer;
				if (flag)
				{
					return vrrig;
				}
			}
			return null;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C1F4
		public static void CreatePlatform(ref GameObject platform, Color32 color, Vector3 Position, Vector3 Scale, Quaternion Rotation)
		{
			bool flag = platform == null;
			if (flag)
			{
				platform = GameObject.CreatePrimitive(3);
				platform.transform.position = Position - new Vector3(0f, 0.034f, 0f);
				platform.transform.rotation = Rotation;
				platform.transform.localScale = Scale;
			}
			platform.GetComponent<Renderer>().material.color = color;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C274
		[return: TupleElementNames(new string[]
		{
			"position",
			"rotation",
			"up",
			"forward",
			"right"
		})]
		public static ValueTuple<Vector3, Quaternion, Vector3, Vector3, Vector3> TrueLeftHand()
		{
			Quaternion quaternion = GorillaTagger.Instance.leftHandTransform.rotation * GTPlayer.Instance.leftHandRotOffset;
			return new ValueTuple<Vector3, Quaternion, Vector3, Vector3, Vector3>(GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.rotation * GTPlayer.Instance.leftHandOffset, quaternion, quaternion * Vector3.up, quaternion * Vector3.forward, quaternion * Vector3.right);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C300
		[return: TupleElementNames(new string[]
		{
			"position",
			"rotation",
			"up",
			"forward",
			"right"
		})]
		public static ValueTuple<Vector3, Quaternion, Vector3, Vector3, Vector3> TrueRightHand()
		{
			Quaternion quaternion = GorillaTagger.Instance.rightHandTransform.rotation * GTPlayer.Instance.rightHandRotOffset;
			return new ValueTuple<Vector3, Quaternion, Vector3, Vector3, Vector3>(GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.rightHandTransform.rotation * GTPlayer.Instance.rightHandOffset, quaternion, quaternion * Vector3.up, quaternion * Vector3.forward, quaternion * Vector3.right);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C38C
		public static void SendRoomEvent(byte code, object[] data, ReceiverGroup group)
		{
			object[] array = new object[]
			{
				PhotonNetwork.ServerTimestamp,
				code,
				data
			};
			PhotonNetwork.RaiseEvent(3, array, new RaiseEventOptions
			{
				Receivers = group
			}, SendOptions.SendUnreliable);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C3D4
		public static void CallGameModeSerializerRPC(string rpc, Player target, object[] args)
		{
			NetworkView networkView = (NetworkView)Traverse.Create(typeof(GameMode)).Field("activeNetworkHandler").Field("netView").GetValue();
			networkView.GetView.RPC(rpc, target, args);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C420
		public static void CallGameModeSerializerRPC(string rpc, RpcTarget target, object[] args)
		{
			NetworkView networkView = (NetworkView)Traverse.Create(typeof(GameMode)).Field("activeNetworkHandler").Field("netView").GetValue();
			networkView.GetView.RPC(rpc, target, args);
		}

		// Token: 0x04000087 RID: 135
		private static float _cooldown;

		// Token: 0x04000088 RID: 136
		private static float shootDelay;

		// Token: 0x04000089 RID: 137
		private static bool canShoot;
	}
}
