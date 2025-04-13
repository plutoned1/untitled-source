using System;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace untitled.Core.Extensions
{
	// Token: 0x0200002E RID: 46
	public static class PhotonManager
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D498
		public static bool SendUnlimmitedRPC(this PhotonView photonView, string method, Player player, object[] parameters)
		{
			return PhotonManager.SendUnlimmitedRPC(photonView, method, 3, player, parameters);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D4B4
		public static bool SendUnlimmitedRPC(this PhotonView photonView, string method, RpcTarget player, object[] parameters)
		{
			return PhotonManager.SendUnlimmitedRPC(photonView, method, player, null, parameters);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D4D0
		private static bool SendUnlimmitedRPC(PhotonView photonView, string method, RpcTarget target, Player player, object[] parameters)
		{
			bool flag = photonView != null && parameters != null && !string.IsNullOrEmpty(method);
			if (flag)
			{
				Hashtable hashtable = new Hashtable();
				hashtable.Add(0, photonView.ViewID);
				hashtable.Add(2, PhotonNetwork.ServerTimestamp + -2147483647);
				hashtable.Add(3, method);
				hashtable.Add(4, parameters);
				Hashtable hashtable2 = hashtable;
				bool flag2 = photonView.Prefix > 0;
				if (flag2)
				{
					hashtable2[1] = (short)photonView.Prefix;
				}
				bool flag3 = PhotonNetwork.PhotonServerSettings.RpcList.Contains(method);
				if (flag3)
				{
					hashtable2[5] = (byte)PhotonNetwork.PhotonServerSettings.RpcList.IndexOf(method);
				}
				bool flag4 = player == null;
				if (flag4)
				{
					switch (target)
					{
					case 0:
					{
						LoadBalancingPeer loadBalancingPeer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b = 200;
						object obj = hashtable2;
						RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
						raiseEventOptions.Receivers = 1;
						raiseEventOptions.InterestGroup = photonView.Group;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer.OpRaiseEvent(b, obj, raiseEventOptions, sendOptions);
						typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
						{
							hashtable2,
							PhotonNetwork.LocalPlayer
						});
						break;
					}
					case 1:
					{
						LoadBalancingPeer loadBalancingPeer2 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b2 = 200;
						object obj2 = hashtable2;
						RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions();
						raiseEventOptions2.Receivers = 0;
						raiseEventOptions2.InterestGroup = photonView.Group;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer2.OpRaiseEvent(b2, obj2, raiseEventOptions2, sendOptions);
						break;
					}
					case 3:
					{
						LoadBalancingPeer loadBalancingPeer3 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b3 = 200;
						object obj3 = hashtable2;
						RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions();
						raiseEventOptions3.Receivers = 1;
						raiseEventOptions3.InterestGroup = photonView.Group;
						raiseEventOptions3.CachingOption = 4;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer3.OpRaiseEvent(b3, obj3, raiseEventOptions3, sendOptions);
						typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
						{
							hashtable2,
							PhotonNetwork.LocalPlayer
						});
						break;
					}
					case 4:
					{
						LoadBalancingPeer loadBalancingPeer4 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b4 = 200;
						object obj4 = hashtable2;
						RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions();
						raiseEventOptions4.Receivers = 0;
						raiseEventOptions4.InterestGroup = photonView.Group;
						raiseEventOptions4.CachingOption = 4;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer4.OpRaiseEvent(b4, obj4, raiseEventOptions4, sendOptions);
						break;
					}
					case 5:
					{
						LoadBalancingPeer loadBalancingPeer5 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b5 = 200;
						object obj5 = hashtable2;
						RaiseEventOptions raiseEventOptions5 = new RaiseEventOptions();
						raiseEventOptions5.Receivers = 1;
						raiseEventOptions5.InterestGroup = photonView.Group;
						raiseEventOptions5.CachingOption = 4;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer5.OpRaiseEvent(b5, obj5, raiseEventOptions5, sendOptions);
						bool offlineMode = PhotonNetwork.OfflineMode;
						if (offlineMode)
						{
							typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
							{
								hashtable2,
								PhotonNetwork.LocalPlayer
							});
						}
						break;
					}
					case 6:
					{
						LoadBalancingPeer loadBalancingPeer6 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b6 = 200;
						object obj6 = hashtable2;
						RaiseEventOptions raiseEventOptions6 = new RaiseEventOptions();
						raiseEventOptions6.Receivers = 1;
						raiseEventOptions6.InterestGroup = photonView.Group;
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer6.OpRaiseEvent(b6, obj6, raiseEventOptions6, sendOptions);
						bool offlineMode2 = PhotonNetwork.OfflineMode;
						if (offlineMode2)
						{
							typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
							{
								hashtable2,
								PhotonNetwork.LocalPlayer
							});
						}
						break;
					}
					}
				}
				else
				{
					bool flag5 = PhotonNetwork.NetworkingClient.LocalPlayer.ActorNumber == player.ActorNumber;
					if (flag5)
					{
						typeof(PhotonNetwork).GetMethod("ExecuteRpc", BindingFlags.Static | BindingFlags.NonPublic).Invoke(typeof(PhotonNetwork), new object[]
						{
							hashtable2,
							PhotonNetwork.LocalPlayer
						});
					}
					else
					{
						LoadBalancingPeer loadBalancingPeer7 = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
						byte b7 = 200;
						object obj7 = hashtable2;
						RaiseEventOptions raiseEventOptions7 = new RaiseEventOptions
						{
							TargetActors = new int[]
							{
								player.ActorNumber
							}
						};
						SendOptions sendOptions = default(SendOptions);
						sendOptions.Reliability = true;
						sendOptions.DeliveryMode = 3;
						sendOptions.Encrypt = false;
						loadBalancingPeer7.OpRaiseEvent(b7, obj7, raiseEventOptions7, sendOptions);
					}
				}
			}
			return false;
		}
	}
}
