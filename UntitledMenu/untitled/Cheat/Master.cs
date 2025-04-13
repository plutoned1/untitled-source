using System;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaTag;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x02000032 RID: 50
	public class Master
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00011FF0 File Offset: 0x000101F0
		public static bool IsMasterClient
		{
			get
			{
				return PhotonNetwork.IsMasterClient;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012007
		public static void Serialize()
		{
			typeof(PhotonNetwork).GetMethod("RunViewUpdate", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), Array.Empty<object>());
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012038
		public static void UntagPlayer(NetPlayer player)
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				GorillaTagManager gorillaTagManager = (GorillaTagManager)GorillaGameManager.instance;
				bool flag = gorillaTagManager != null;
				if (flag)
				{
					gorillaTagManager.currentInfected.Remove(player);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012078
		public static void TagPlayer(NetPlayer player)
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				GorillaTagManager gorillaTagManager = (GorillaTagManager)GorillaGameManager.instance;
				bool flag = gorillaTagManager != null;
				if (flag)
				{
					gorillaTagManager.currentInfected.Add(player);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000120B8
		public static void UntagAll()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				Master.UntagPlayer(vrrig.Creator);
			}
			Master.Serialize();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012124
		public static void UntagGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
			if (flag)
			{
				Master.UntagPlayer(gunLibData.lockedPlayer.Creator);
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012170
		public static void MatGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
			if (flag)
			{
				bool flag2 = Time.time > Master.MasterClientTimedAction;
				if (flag2)
				{
					Master.MasterClientTimedAction = Time.time + 0.05f;
					bool materialSwap = Master.MaterialSwap;
					if (materialSwap)
					{
						Master.TagPlayer(gunLibData.lockedPlayer.Creator);
						Master.MaterialSwap = true;
					}
					else
					{
						Master.UntagPlayer(gunLibData.lockedPlayer.Creator);
						Master.MaterialSwap = false;
					}
					Master.Serialize();
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012208
		public static void MatAll()
		{
			bool flag = Time.time > Master.MasterClientTimedAction;
			if (flag)
			{
				Master.MasterClientTimedAction = Time.time + 0.05f;
				bool materialSwap = Master.MaterialSwap;
				if (materialSwap)
				{
					foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
					{
						Master.TagPlayer(vrrig.Creator);
					}
					Master.MaterialSwap = false;
				}
				else
				{
					foreach (VRRig vrrig2 in GorillaParent.instance.vrrigs)
					{
						Master.UntagPlayer(vrrig2.Creator);
					}
					Master.MaterialSwap = true;
				}
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012308
		public static void TagSoundSpam()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				object[] array = new object[]
				{
					0,
					0.25f,
					false,
					PhotonNetwork.LocalPlayer.ActorNumber
				};
				byte b = 3;
				object obj = array;
				Master.SendEvent(b, obj, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012368
		public static void ViberateGun()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				GunLib.GunLibData gunLibData = GunLib.ShootLock();
				bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
				if (flag)
				{
					bool flag2 = Master.MasterClientTimedAction < Time.time;
					if (flag2)
					{
						Master.MasterClientTimedAction = Time.time + 0.2f;
						Master.SetNetStatus(1, new RaiseEventOptions
						{
							TargetActors = new int[]
							{
								gunLibData.lockedPlayer.Creator.ActorNumber
							}
						});
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000123F8
		public static void ViberateAll()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				bool flag = Master.MasterClientTimedAction < Time.time;
				if (flag)
				{
					Master.MasterClientTimedAction = Time.time + 0.2f;
					Master.SetNetStatus(1, new RaiseEventOptions
					{
						Receivers = 1
					});
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012448
		public static void SlowGun()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				GunLib.GunLibData gunLibData = GunLib.ShootLock();
				bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
				if (flag)
				{
					bool flag2 = Master.MasterClientTimedAction < Time.time;
					if (flag2)
					{
						Master.MasterClientTimedAction = Time.time + 0.2f;
						Master.SetNetStatus(0, new RaiseEventOptions
						{
							TargetActors = new int[]
							{
								gunLibData.lockedPlayer.Creator.ActorNumber
							}
						});
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000124D8
		public static void SlowAll()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				bool flag = Master.MasterClientTimedAction < Time.time;
				if (flag)
				{
					Master.MasterClientTimedAction = Time.time + 0.2f;
					Master.SetNetStatus(0, new RaiseEventOptions
					{
						Receivers = 1
					});
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012528
		public static void RedGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
			if (flag)
			{
				bool flag2 = Master.MasterClientTimedAction < Time.time;
				if (flag2)
				{
					Master.MasterClientTimedAction = Time.time + 0.1f;
					Master.SetPlayerColor(gunLibData.lockedPlayer.Creator, true);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012590
		public static void BlueGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
			if (flag)
			{
				bool flag2 = Master.MasterClientTimedAction < Time.time;
				if (flag2)
				{
					Master.MasterClientTimedAction = Time.time + 0.1f;
					Master.SetPlayerColor(gunLibData.lockedPlayer.Creator, false);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000125F8
		public static void LucyGun()
		{
			SecondLookSkeleton component = GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton").GetComponent<SecondLookSkeleton>();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				bool flag2 = !PhotonNetwork.IsMasterClient;
				if (!flag2)
				{
					Master.EnableLucy();
					GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton/Offset/SpookySkeletonParent").transform.rotation = GTPlayer.Instance.rightControllerTransform.rotation;
					GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton/Offset/SpookySkeletonParent").transform.position = gunLibData.hitPosition;
					component.enabled = false;
				}
			}
			else
			{
				component.enabled = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001269C
		public static void NoclipGun()
		{
			SecondLookSkeleton component = GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton").GetComponent<SecondLookSkeleton>();
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && gunLibData.isLocked;
			if (flag)
			{
				Master.EnableLucy();
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton/Offset/SpookySkeletonParent").transform.rotation = GTPlayer.Instance.rightControllerTransform.rotation;
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton/Offset/SpookySkeletonParent").transform.position = gunLibData.lockedPlayer.transform.position;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012730
		public static void TargetHitSpam()
		{
			bool flag = Master.TargetNetworkStates.Count == 0;
			if (flag)
			{
				foreach (HitTargetNetworkState item in Resources.FindObjectsOfTypeAll<HitTargetNetworkState>())
				{
					Master.TargetNetworkStates.Add(item);
				}
			}
			foreach (HitTargetNetworkState hitTargetNetworkState in Master.TargetNetworkStates)
			{
				Traverse.Create(hitTargetNetworkState).Field("hitCooldownTime").SetValue(0);
				hitTargetNetworkState.TargetHit(Vector3.zero, Vector3.zero);
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000127F4
		public static void TargetWinSpam()
		{
			bool flag = Master.TargetNetworkStates.Count == 0;
			if (flag)
			{
				foreach (HitTargetNetworkState hitTargetNetworkState in Resources.FindObjectsOfTypeAll<HitTargetNetworkState>())
				{
					Traverse.Create(hitTargetNetworkState).Field("hitCooldownTime").SetValue(0);
					Master.TargetNetworkStates.Add(hitTargetNetworkState);
				}
			}
			foreach (HitTargetNetworkState hitTargetNetworkState2 in Master.TargetNetworkStates)
			{
				Traverse traverse = Traverse.Create(hitTargetNetworkState2).Field("networkedScore");
				WatchableIntSO value = traverse.GetValue<WatchableIntSO>();
				value.Value = 999;
				traverse.SetValue(value);
				Master.Serialize();
				hitTargetNetworkState2.TargetHit(Vector3.zero, Vector3.zero);
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000128F0
		public static void EnableLucy()
		{
			bool flag = !GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton/Offset/SpookySkeletonParent").activeSelf;
			if (flag)
			{
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/MinesSecondLookSkeleton").GetComponent<SecondLookSkeletonSynchValues>().SendRPC("RemoteActivateGhost", 2, Array.Empty<object>());
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012938
		public static void SendEvent(in byte code, in object evData, bool reliable)
		{
			object[] array = new object[0];
			array[0] = NetworkSystem.Instance.ServerTimestamp;
			array[1] = code;
			array[2] = evData;
			PhotonNetwork.RaiseEvent(3, array, new RaiseEventOptions
			{
				Receivers = 1
			}, reliable ? SendOptions.SendReliable : SendOptions.SendUnreliable);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012994
		public static void SetNetStatus(int state, RaiseEventOptions balls)
		{
			object[] array = new object[]
			{
				state
			};
			PhotonNetwork.RaiseEvent(3, new object[]
			{
				PhotonNetwork.ServerTimestamp,
				2,
				array
			}, balls, SendOptions.SendUnreliable);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000129E4
		public static void SetPlayerColor(NetPlayer player, bool red)
		{
			int num = red ? 1 : 0;
			MonkeBallGame instance = MonkeBallGame.Instance;
			int num2 = 2;
			double num3 = PhotonNetwork.Time + (double)instance.gameDuration;
			NetPlayer[] array = new NetPlayer[]
			{
				player
			};
			int[] array2 = new int[array.Length];
			int[] array3 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].ActorNumber;
				array3[i] = num;
			}
			int[] array4 = new int[instance.team.Count];
			for (int j = 0; j < array4.Length; j++)
			{
				array4[j] = 0;
			}
			int count = instance.startingBalls.Count;
			long[] array5 = new long[count];
			long[] array6 = new long[count];
			for (int k = 0; k < count; k++)
			{
				MonkeBall monkeBall = instance.startingBalls[k];
				array5[k] = BitPackUtils.PackHandPosRotForNetwork(monkeBall.transform.position, monkeBall.transform.rotation);
				array6[k] = BitPackUtils.PackWorldPosForNetwork(monkeBall.gameBall.GetVelocity());
			}
			instance.photonView.RPC("RequestSetGameStateRPC", 0, new object[]
			{
				num2,
				num3,
				array2,
				array3,
				array4,
				array5,
				array6
			});
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012B58
		public static void SetAllPlayersColor(bool red)
		{
			int num = red ? 1 : 0;
			MonkeBallGame instance = MonkeBallGame.Instance;
			int num2 = 2;
			double num3 = PhotonNetwork.Time + (double)instance.gameDuration;
			NetPlayer[] allNetPlayers = NetworkSystem.Instance.AllNetPlayers;
			int[] array = new int[allNetPlayers.Length];
			int[] array2 = new int[allNetPlayers.Length];
			for (int i = 0; i < allNetPlayers.Length; i++)
			{
				array[i] = allNetPlayers[i].ActorNumber;
				array2[i] = num;
			}
			int[] array3 = new int[instance.team.Count];
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] = 0;
			}
			int count = instance.startingBalls.Count;
			long[] array4 = new long[count];
			long[] array5 = new long[count];
			for (int k = 0; k < count; k++)
			{
				MonkeBall monkeBall = instance.startingBalls[k];
				array4[k] = BitPackUtils.PackHandPosRotForNetwork(monkeBall.transform.position, monkeBall.transform.rotation);
				array5[k] = BitPackUtils.PackWorldPosForNetwork(monkeBall.gameBall.GetVelocity());
			}
			instance.photonView.RPC("RequestSetGameStateRPC", 0, new object[]
			{
				num2,
				num3,
				array,
				array2,
				array3,
				array4,
				array5
			});
		}

		// Token: 0x040000D2 RID: 210
		public static float MasterClientTimedAction;

		// Token: 0x040000D3 RID: 211
		public static bool MaterialSwap;

		// Token: 0x040000D4 RID: 212
		public static List<HitTargetNetworkState> TargetNetworkStates = new List<HitTargetNetworkState>();
	}
}
