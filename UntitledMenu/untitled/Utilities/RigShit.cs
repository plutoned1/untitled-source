using System;
using GorillaLocomotion.Gameplay;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace untitled.Utilities
{
	// Token: 0x02000008 RID: 8
	internal class RigShit : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002E20
		public static PhotonView rig2view(VRRig p)
		{
			return Traverse.Create(p).Field("netView").GetValue<NetworkView>().GetView;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002E4C
		public static Player NetPlayerToPlayer(NetPlayer np)
		{
			return np.GetPlayerRef();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002E64
		public static NetPlayer PlayerToNetPlayer(Player np)
		{
			foreach (NetPlayer netPlayer in NetworkSystem.Instance.AllNetPlayers)
			{
				bool flag = np.UserId == netPlayer.UserId;
				if (flag)
				{
					return netPlayer;
				}
			}
			return null;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002EB4
		public static NetworkView GetNetViewFromVRRig(VRRig p)
		{
			return (NetworkView)Traverse.Create(p).Field("netView").GetValue();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002EE0
		public static VRRig GetRigFromPlayer(Player p)
		{
			foreach (NetPlayer netPlayer in NetworkSystem.Instance.AllNetPlayers)
			{
				bool flag = netPlayer.UserId == p.UserId;
				if (flag)
				{
					return RigShit.GetRigFromNetPlayer(netPlayer);
				}
			}
			return null;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002F38
		public static VRRig GetRigFromNetPlayer(NetPlayer p)
		{
			return GorillaGameManager.instance.FindPlayerVRRig(p);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002F58
		public static PhotonView GetViewFromPlayer(Player p)
		{
			return RigShit.rig2view(GorillaGameManager.instance.FindPlayerVRRig(p));
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002F80
		public static VRRig GetOwnVRRig()
		{
			return GorillaTagger.Instance.offlineVRRig;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002F9C
		public static PhotonView GetViewFromRig(VRRig rig)
		{
			return RigShit.rig2view(rig);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002FB4
		public static NetworkView GetNetViewFromRig(VRRig rig)
		{
			return RigShit.rig2netview(rig);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002FCC
		public static NetPlayer GetPlayerFromID(string id)
		{
			NetPlayer result = null;
			foreach (Player player in PhotonNetwork.PlayerList)
			{
				bool flag = player.UserId == id;
				if (flag)
				{
					result = player;
					break;
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000301C
		public static NetworkView rig2netview(VRRig p)
		{
			return Traverse.Create(p).Field("netView").GetValue<NetworkView>();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00003044
		public static Player GetPlayerFromRig(VRRig rig)
		{
			return rig.OwningNetPlayer.GetPlayerRef();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00003064
		public static NetPlayer GetNetPlayerFromRig(VRRig rig)
		{
			return rig.OwningNetPlayer;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000307C
		public static GorillaRopeSwing GetPlayersRope(VRRig rig)
		{
			return (GorillaRopeSwing)Traverse.Create(rig).Field("currentRopeSwing").GetValue();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000030A8
		private float Distance2D(Vector3 a, Vector3 b)
		{
			Vector2 vector;
			vector..ctor(a.x, a.z);
			Vector2 vector2;
			vector2..ctor(b.x, b.z);
			return Vector2.Distance(vector, vector2);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000030E8
		private bool PlayerNear(VRRig rig, float dist, out float playerDist)
		{
			bool flag = rig == null;
			bool result;
			if (flag)
			{
				playerDist = float.PositiveInfinity;
				result = false;
			}
			else
			{
				playerDist = this.Distance2D(rig.transform.position, base.transform.position);
				result = (playerDist < dist && Physics.RaycastNonAlloc(new Ray(base.transform.position, rig.transform.position - base.transform.position), this.rayResults, playerDist, this.layerMask) <= 0);
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00003180
		private bool ClosestPlayer(in Vector3 myPos, out VRRig outRig)
		{
			this.layerMask = (UnityLayerExtensions.ToLayerMask(0) | UnityLayerExtensions.ToLayerMask(9));
			float num = float.MaxValue;
			outRig = null;
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				float num2 = 0f;
				bool flag = this.PlayerNear(vrrig, GorillaGameManager.instance.tagDistanceThreshold, out num2) && num2 < num;
				if (flag)
				{
					num = num2;
					outRig = vrrig;
				}
			}
			return num != float.MaxValue;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000323C
		public static bool battleIsOnCooldown(VRRig rig)
		{
			return rig.mainSkin.material.name.Contains("hit");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00003268
		public static Player GetRandomPlayer(bool includeSelf)
		{
			Player result;
			if (includeSelf)
			{
				Player player = PhotonNetwork.PlayerList[Random.Range(0, 11)];
				bool flag = player != null;
				if (flag)
				{
					result = player;
				}
				else
				{
					result = RigShit.GetRandomPlayer(includeSelf);
				}
			}
			else
			{
				Player player2 = PhotonNetwork.PlayerListOthers[Random.Range(0, 10)];
				bool flag2 = player2 != null;
				if (flag2)
				{
					result = player2;
				}
				else
				{
					result = RigShit.GetRandomPlayer(includeSelf);
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000032D0
		public static NetPlayer GetRandomNetPlayer(bool includeSelf)
		{
			NetPlayer result;
			if (includeSelf)
			{
				NetPlayer netPlayer = NetworkSystem.Instance.PlayerListOthers[Random.Range(0, NetworkSystem.Instance.PlayerListOthers.Length)];
				bool flag = netPlayer != null;
				if (flag)
				{
					result = netPlayer;
				}
				else
				{
					result = RigShit.GetRandomNetPlayer(includeSelf);
				}
			}
			else
			{
				NetPlayer netPlayer2 = NetworkSystem.Instance.PlayerListOthers[Random.Range(0, NetworkSystem.Instance.PlayerListOthers.Length)];
				bool flag2 = netPlayer2 != null;
				if (flag2)
				{
					result = netPlayer2;
				}
				else
				{
					result = RigShit.GetRandomNetPlayer(includeSelf);
				}
			}
			return result;
		}

		// Token: 0x04000020 RID: 32
		private RaycastHit[] rayResults = new RaycastHit[1];

		// Token: 0x04000021 RID: 33
		private LayerMask layerMask;
	}
}
