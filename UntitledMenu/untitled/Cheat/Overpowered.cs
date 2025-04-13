using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using GorillaNetworking;
using GorillaTagScripts;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using untitled.Libs;
using untitled.Utilities;

namespace untitled.Cheat
{
	// Token: 0x02000034 RID: 52
	public class Overpowered
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00014330 File Offset: 0x00012530
		public static string CurrentQueue
		{
			get
			{
				string text = (string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"];
				bool flag = text.Contains("DEFAULT");
				string result;
				if (flag)
				{
					result = "DEFAULT";
				}
				else
				{
					bool flag2 = text.Contains("MINIGAMES");
					if (flag2)
					{
						result = "MINIGAMES";
					}
					else
					{
						bool flag3 = text.Contains("COMPTITIVE");
						if (flag3)
						{
							result = "COMPTITIVE";
						}
						else
						{
							result = "DEFAULT";
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000143B8 File Offset: 0x000125B8
		public static string CurrentMode
		{
			get
			{
				string text = (string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"];
				bool flag = text.Contains("Infection");
				string result;
				if (flag)
				{
					result = "Infection";
				}
				else
				{
					bool flag2 = text.Contains("Guardian");
					if (flag2)
					{
						result = "Guardian";
					}
					else
					{
						bool flag3 = text.Contains("Casual");
						if (flag3)
						{
							result = "Casual";
						}
						else
						{
							bool flag4 = text.Contains("Freezetag");
							if (flag4)
							{
								result = "Freezetag";
							}
							else
							{
								result = "Freezetag";
							}
						}
					}
				}
				return result;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001445D
		public static IEnumerator FreezeServerC()
		{
			int num;
			for (int i = 0; i < 100; i = num + 1)
			{
				try
				{
					CosmeticsController.instance.ProcessExternalUnlock(null, true, false);
				}
				catch
				{
				}
				yield return new WaitForSeconds(0.02f);
				num = i;
			}
			Dictionary<byte, object> dick = new Dictionary<byte, object>();
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOperation(byte.MaxValue, dick, SendOptions.SendReliable);
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014465
		public static IEnumerator KickPlayer(Player Target)
		{
			Traverse.Create(GameObject.Find("PhotonMono").GetComponent<PhotonHandler>()).Field("nextSendTickCountOnSerialize").SetValue((int)(Time.realtimeSinceStartup * 9999f));
			yield return new WaitForSeconds(0.5f);
			int num;
			for (int i = 0; i < 3960; i = num + 1)
			{
				PhotonView pv = FriendshipGroupDetection.Instance.photonView;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, pv.ViewID);
				hashtable.Add(2, PhotonNetwork.ServerTimestamp + -2147483647);
				hashtable.Add(3, "VerifyPartyMember");
				hashtable.Add(5, 70);
				Hashtable rpcHash = hashtable;
				LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
				byte b = 200;
				object obj = rpcHash;
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						Target.ActorNumber
					},
					InterestGroup = pv.Group
				};
				SendOptions sendOptions = default(SendOptions);
				sendOptions.Reliability = true;
				sendOptions.DeliveryMode = 3;
				sendOptions.Encrypt = false;
				loadBalancingPeer.OpRaiseEvent(b, obj, raiseEventOptions, sendOptions);
				pv = null;
				rpcHash = null;
				num = i;
				pv = null;
				rpcHash = null;
			}
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014474
		public static IEnumerator CrashPlayer(Player Target)
		{
			Traverse.Create(GameObject.Find("PhotonMono").GetComponent<PhotonHandler>()).Field("nextSendTickCountOnSerialize").SetValue((int)(Time.realtimeSinceStartup * 9999f));
			yield return new WaitForSeconds(0.5f);
			int num;
			for (int i = 0; i < 150; i = num + 1)
			{
				PhotonView pv = FriendshipGroupDetection.Instance.photonView;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, pv.ViewID);
				hashtable.Add(2, PhotonNetwork.ServerTimestamp + -2147483647);
				hashtable.Add(3, "VerifyPartyMember");
				hashtable.Add(5, float.NaN);
				Hashtable rpcHash = hashtable;
				LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
				byte b = 200;
				object obj = rpcHash;
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						Target.ActorNumber
					},
					InterestGroup = pv.Group
				};
				SendOptions sendOptions = default(SendOptions);
				sendOptions.Reliability = true;
				sendOptions.DeliveryMode = 3;
				sendOptions.Encrypt = false;
				loadBalancingPeer.OpRaiseEvent(b, obj, raiseEventOptions, sendOptions);
				pv = null;
				rpcHash = null;
				num = i;
				pv = null;
				rpcHash = null;
			}
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014483
		public static IEnumerator ForceReconnectAndCreateRoom(string roomName)
		{
			bool isConnected = PhotonNetwork.IsConnected;
			if (isConnected)
			{
				PhotonNetwork.Disconnect();
			}
			while (PhotonNetwork.NetworkClientState != 14)
			{
				yield return null;
			}
			PhotonNetwork.ConnectUsingSettings();
			while (PhotonNetwork.NetworkClientState != 15)
			{
				yield return null;
			}
			Debug.Log((string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetworkController.Instance));
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.PlayerTtl = 1;
			roomOptions.EmptyRoomTtl = 0;
			roomOptions.IsVisible = false;
			roomOptions.IsOpen = true;
			roomOptions.PublishUserId = true;
			roomOptions.MaxPlayers = 10;
			RoomOptions roomOptions2 = roomOptions;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameMode", "Ghost");
			hashtable.Add("platform", (string)typeof(PhotonNetworkController).GetField("platformTag", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetworkController.Instance));
			hashtable.Add("queueName", GorillaComputer.instance.currentQueue);
			roomOptions2.CustomRoomProperties = hashtable;
			RoomOptions opts = roomOptions;
			PhotonNetwork.CreateRoom(roomName, opts, null, null);
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014494
		public static PhotonView GetPv(VRRig rig)
		{
			return Traverse.Create(rig).Field("netView").GetValue<NetworkView>().GetView;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000144C0
		public static PhotonView GetPv(Player rig)
		{
			return RigShit.GetViewFromPlayer(rig);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000144D8
		public static void CreatePub()
		{
			BuilderTableNetworking.instance.StartCoroutine(Overpowered.ForceReconnectAndCreateRoom("PBBV"));
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000144F0
		public static void SetTick(float tick)
		{
			Traverse.Create(GameObject.Find("PhotonMono").GetComponent<PhotonHandler>()).Field("nextSendTickCountOnSerialize").SetValue((int)(Time.realtimeSinceStartup * tick));
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014524
		public static void test()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				Global.done = !Global.done;
				Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
				Dictionary<byte, object> dictionary2 = dictionary;
				byte key = 251;
				Hashtable hashtable = new Hashtable();
				hashtable.Add("didTutorial", float.NaN);
				dictionary2.Add(key, hashtable);
				dictionary.Add(250, true);
				dictionary.Add(254, gunLibData.lockedPlayer.Creator.ActorNumber);
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOperation(252, dictionary, SendOptions.SendReliable);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000145DC
		public static void KickStump()
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			Dictionary<byte, object> dictionary2 = dictionary;
			byte key = 251;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameMode", true);
			dictionary2.Add(key, hashtable);
			dictionary.Add(250, true);
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOperation(252, dictionary, SendOptions.SendReliable);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014640
		public static void FreezeServer()
		{
			BuilderTableNetworking.instance.StartCoroutine(Overpowered.FreezeServerC());
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014654
		public static void FreezeRigGun(bool all)
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && Time.time > Overpowered.cooldown;
			if (flag)
			{
				List<int> list = new List<int>();
				foreach (Player player in PhotonNetwork.PlayerListOthers)
				{
					bool flag2 = player != gunLibData.lockedPlayer.Creator.GetPlayerRef();
					if (flag2)
					{
						list.Add(player.ActorNumber);
					}
				}
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				byte b = 202;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, "Player Network Controller");
				hashtable.Add(6, PhotonNetwork.ServerTimestamp);
				hashtable.Add(4, new int[]
				{
					Overpowered.GetPv(gunLibData.lockedPlayer).ViewID,
					Overpowered.GetPv(gunLibData.lockedPlayer).ViewID
				});
				hashtable.Add(7, Overpowered.GetPv(gunLibData.lockedPlayer).ViewID);
				networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
				{
					TargetActors = list.ToArray()
				}, SendOptions.SendReliable);
				if (all)
				{
					PhotonNetwork.RemoveInstantiatedGO(Overpowered.GetPv(gunLibData.lockedPlayer).gameObject, true);
				}
				Overpowered.cooldown = Time.time + 0.5f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000147B8
		public static void GhostTouch()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig != null && !vrrig.isOfflineVRRig && Vector3.Distance(vrrig.transform.position, Global.TrueRightHand().Item1) < 0.13f;
				if (flag)
				{
					List<int> list = new List<int>();
					foreach (Player player in PhotonNetwork.PlayerListOthers)
					{
						bool flag2 = player != vrrig.Creator.GetPlayerRef();
						if (flag2)
						{
							list.Add(player.ActorNumber);
						}
					}
					LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
					byte b = 202;
					Hashtable hashtable = new Hashtable();
					hashtable.Add(0, "Player Network Controller");
					hashtable.Add(6, PhotonNetwork.ServerTimestamp);
					hashtable.Add(4, new int[]
					{
						Overpowered.GetPv(vrrig).ViewID,
						Overpowered.GetPv(vrrig).ViewID
					});
					hashtable.Add(7, Overpowered.GetPv(vrrig).ViewID);
					networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
					{
						TargetActors = list.ToArray()
					}, SendOptions.SendReliable);
					PhotonNetwork.RemoveInstantiatedGO(Overpowered.GetPv(vrrig).gameObject, true);
					Overpowered.cooldown = Time.time + 0.5f;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014968
		public static void GhostPlayer(bool all, Player p)
		{
			bool flag = Time.time > Overpowered.cooldown;
			if (flag)
			{
				List<int> list = new List<int>();
				foreach (Player player in PhotonNetwork.PlayerListOthers)
				{
					bool flag2 = p != player;
					if (flag2)
					{
						list.Add(p.ActorNumber);
					}
				}
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				byte b = 202;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, "Player Network Controller");
				hashtable.Add(6, PhotonNetwork.ServerTimestamp);
				hashtable.Add(4, new int[]
				{
					Overpowered.GetPv(p).ViewID,
					Overpowered.GetPv(p).ViewID
				});
				hashtable.Add(7, Overpowered.GetPv(p).ViewID);
				networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
				{
					TargetActors = list.ToArray()
				}, SendOptions.SendReliable);
				if (all)
				{
					PhotonNetwork.RemoveInstantiatedGO(Overpowered.GetPv(p).gameObject, true);
				}
				Overpowered.cooldown = Time.time + 0.5f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014A8C
		public static void FreezeAllRigs()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig != null && !vrrig.isOfflineVRRig;
				if (flag)
				{
					List<int> list = new List<int>();
					foreach (Player player in PhotonNetwork.PlayerListOthers)
					{
						bool flag2 = player != vrrig.Creator.GetPlayerRef();
						if (flag2)
						{
							list.Add(player.ActorNumber);
						}
					}
					LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
					byte b = 202;
					Hashtable hashtable = new Hashtable();
					hashtable.Add(0, "Player Network Controller");
					hashtable.Add(6, PhotonNetwork.ServerTimestamp);
					hashtable.Add(4, new int[]
					{
						Overpowered.GetPv(vrrig).ViewID,
						Overpowered.GetPv(vrrig).ViewID
					});
					hashtable.Add(7, Overpowered.GetPv(vrrig).ViewID);
					networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
					{
						TargetActors = list.ToArray()
					}, SendOptions.SendReliable);
					PhotonNetwork.RemoveInstantiatedGO(Overpowered.GetPv(vrrig).gameObject, true);
					Overpowered.cooldown = Time.time + 0.5f;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014C1C
		public static void TimeStopGun(bool includeSelf)
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && Time.time > Overpowered.cooldown;
			if (flag)
			{
				foreach (Player player in PhotonNetwork.PlayerList)
				{
					bool flag2 = (includeSelf || player != PhotonNetwork.LocalPlayer) && player != gunLibData.lockedPlayer.Creator.GetPlayerRef();
					if (flag2)
					{
						LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
						byte b = 202;
						Hashtable hashtable = new Hashtable();
						hashtable.Add(0, "Player Network Controller");
						hashtable.Add(6, PhotonNetwork.ServerTimestamp);
						hashtable.Add(4, new int[]
						{
							Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID,
							Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID
						});
						hashtable.Add(7, Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID);
						networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
						{
							TargetActors = new int[]
							{
								gunLibData.lockedPlayer.Creator.ActorNumber
							}
						}, SendOptions.SendReliable);
					}
				}
				Overpowered.cooldown = Time.time + 1f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014DA0
		public static void IsolatePlayer(bool includeSelf, Player p)
		{
			bool flag = Time.time > Overpowered.cooldown;
			if (flag)
			{
				foreach (Player player in PhotonNetwork.PlayerList)
				{
					bool flag2 = (includeSelf || player != PhotonNetwork.LocalPlayer) && player != p;
					if (flag2)
					{
						LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
						byte b = 202;
						Hashtable hashtable = new Hashtable();
						hashtable.Add(0, "Player Network Controller");
						hashtable.Add(6, PhotonNetwork.ServerTimestamp);
						hashtable.Add(4, new int[]
						{
							Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID,
							Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID
						});
						hashtable.Add(7, Overpowered.GetPv(GorillaGameManager.instance.FindPlayerVRRig(NetPlayer.Get(player))).ViewID);
						networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
						{
							TargetActors = new int[]
							{
								p.ActorNumber
							}
						}, SendOptions.SendReliable);
					}
				}
				Overpowered.cooldown = Time.time + 1f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014EEC
		public static void AntiLeaveGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && Time.time > Overpowered.cooldown;
			if (flag)
			{
				int viewID = GameObject.Find("Networking Scripts/FreeHoverboardManager").GetComponent<PhotonView>().ViewID;
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				byte b = 202;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, "Player Network Controller");
				hashtable.Add(6, PhotonNetwork.ServerTimestamp);
				hashtable.Add(4, new int[]
				{
					viewID,
					viewID
				});
				hashtable.Add(7, viewID);
				networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						gunLibData.lockedPlayer.Creator.ActorNumber
					}
				}, SendOptions.SendReliable);
				Overpowered.cooldown = Time.time + 0.5f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014FD8
		public static void AntiLeavePlayer(Player p)
		{
			bool flag = Time.time > Overpowered.cooldown;
			if (flag)
			{
				int viewID = GameObject.Find("Networking Scripts/FreeHoverboardManager").GetComponent<PhotonView>().ViewID;
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				byte b = 202;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, "Player Network Controller");
				hashtable.Add(6, PhotonNetwork.ServerTimestamp);
				hashtable.Add(4, new int[]
				{
					viewID,
					viewID
				});
				hashtable.Add(7, viewID);
				networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						p.ActorNumber
					}
				}, SendOptions.SendReliable);
				Overpowered.cooldown = Time.time + 0.5f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001509C
		public static void AntiLeaveAll()
		{
			int viewID = GameObject.Find("Networking Scripts/FreeHoverboardManager").GetComponent<PhotonView>().ViewID;
			foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
			{
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				byte b = 202;
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, "Player Network Controller");
				hashtable.Add(6, PhotonNetwork.ServerTimestamp);
				hashtable.Add(4, new int[]
				{
					viewID,
					viewID
				});
				hashtable.Add(7, viewID);
				networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
				{
					TargetActors = new int[]
					{
						netPlayer.ActorNumber
					}
				}, SendOptions.SendReliable);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001516C
		public static void LagGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && Time.time > Overpowered.cooldown;
			if (flag)
			{
				Overpowered.ForceDestroy(Overpowered.GetPv(gunLibData.lockedPlayer).gameObject, gunLibData.lockedPlayer.Creator);
				Overpowered.cooldown = Time.time + 0.05f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000151D8
		public static void LagAll()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					Overpowered.ForceDestroy(Overpowered.GetPv(vrrig).gameObject, gunLibData.lockedPlayer.Creator);
					Overpowered.cooldown = Time.time + 0.07f;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015270
		public static void LagPlayer(VRRig rig)
		{
			bool flag = Time.time > Overpowered.cooldown;
			if (flag)
			{
				Overpowered.ForceDestroy(Overpowered.GetPv(rig).gameObject, rig.Creator);
				Overpowered.cooldown = Time.time + 0.07f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000152B8
		public static void KickGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				Overpowered.SetTick(9999f);
				bool flag2 = Overpowered.kick == null;
				if (flag2)
				{
					Overpowered.kick = BuilderTableNetworking.instance.StartCoroutine(Overpowered.KickPlayer(gunLibData.lockedPlayer.Creator.GetPlayerRef()));
					PhotonNetwork.SendAllOutgoingCommands();
				}
			}
			else
			{
				bool flag3 = Overpowered.kick != null;
				if (flag3)
				{
					Overpowered.SetTick(1000f);
					BuilderTableNetworking.instance.StopCoroutine(Overpowered.kick);
					Overpowered.kick = null;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015358
		public static void CrashGun(float delay, int foramount)
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				bool flag2 = Time.time > Overpowered.cooldown;
				if (flag2)
				{
					Overpowered.cooldown = Time.time + delay;
					for (int i = 0; i < foramount; i++)
					{
						PhotonNetwork.NetworkingClient.OpRaiseEvent(204, new object[]
						{
							"Untitled official"
						}, new RaiseEventOptions
						{
							CachingOption = 0,
							TargetActors = new int[]
							{
								gunLibData.lockedPlayer.Creator.ActorNumber
							}
						}, SendOptions.SendReliable);
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001540C
		public static void CrashAll(float delay, int foramount)
		{
			bool flag = Time.time > Overpowered.cooldown;
			if (flag)
			{
				Overpowered.cooldown = Time.time + delay;
				for (int i = 0; i < foramount; i++)
				{
					PhotonNetwork.NetworkingClient.OpRaiseEvent(204, new object[]
					{
						"Untitled official"
					}, new RaiseEventOptions
					{
						CachingOption = 0,
						Receivers = 0
					}, SendOptions.SendReliable);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015480
		public static void ForceDestroy(GameObject view, NetPlayer player)
		{
			int viewID = view.GetComponent<PhotonView>().ViewID;
			LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
			byte b = 202;
			Hashtable hashtable = new Hashtable();
			hashtable.Add(0, "Player Network Controller");
			hashtable.Add(6, PhotonNetwork.ServerTimestamp);
			hashtable.Add(4, new int[]
			{
				GorillaTagger.Instance.myVRRig.ViewID,
				viewID
			});
			hashtable.Add(7, GorillaTagger.Instance.myVRRig.ViewID);
			networkingClient.OpRaiseEvent(b, hashtable, new RaiseEventOptions
			{
				TargetActors = new int[]
				{
					player.ActorNumber
				}
			}, SendOptions.SendReliable);
		}

		// Token: 0x040000E6 RID: 230
		public static GameObject ray;

		// Token: 0x040000E7 RID: 231
		public static bool timeout;

		// Token: 0x040000E8 RID: 232
		public static float cooldown;

		// Token: 0x040000E9 RID: 233
		public static float cooldown2;

		// Token: 0x040000EA RID: 234
		public static float flint;

		// Token: 0x040000EB RID: 235
		public static int counter;

		// Token: 0x040000EC RID: 236
		public static int countercams;

		// Token: 0x040000ED RID: 237
		public static int countercamsdih;

		// Token: 0x040000EE RID: 238
		public static List<GameObject> cams = new List<GameObject>();

		// Token: 0x040000EF RID: 239
		public static List<GameObject> camsdih = new List<GameObject>();

		// Token: 0x040000F0 RID: 240
		public static Coroutine kick = null;

		// Token: 0x040000F1 RID: 241
		public static Coroutine crash = null;
	}
}
