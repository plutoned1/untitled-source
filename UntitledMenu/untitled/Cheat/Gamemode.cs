using System;
using GorillaGameModes;
using GorillaLocomotion;
using GorillaTagScripts;
using Photon.Pun;
using UnityEngine;
using untitled.Core;
using untitled.Core.Scripts;
using untitled.Core.Utils;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x02000030 RID: 48
	public class Gamemode
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E6AC
		public static TappableGuardianIdol[] GetGuardianIdols()
		{
			bool flag = Time.time > Gamemode.cooldown;
			if (flag)
			{
				Gamemode.idols = null;
				Gamemode.cooldown = Time.time + 5f;
			}
			bool flag2 = Gamemode.idols == null;
			if (flag2)
			{
				Gamemode.idols = Object.FindObjectsOfType<TappableGuardianIdol>();
			}
			return Gamemode.idols;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000E704 File Offset: 0x0000C904
		public static bool IsTagged
		{
			get
			{
				GorillaGameManager gorillaGameManager;
				bool flag = Gamemode.TryGetGameMode(Gamemode.GameModes.Infection, out gorillaGameManager);
				bool result;
				if (flag)
				{
					GorillaTagManager gorillaTagManager = (GorillaTagManager)gorillaGameManager;
					result = gorillaTagManager.currentInfected.Contains(GorillaTagger.Instance.offlineVRRig.Creator);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E748
		public static bool TryGetGameMode(Gamemode.GameModes mode, out GorillaGameManager manager)
		{
			bool result;
			switch (mode)
			{
			case Gamemode.GameModes.Infection:
			{
				bool flag = GorillaGameManager.instance is GorillaTagManager;
				manager = (GorillaTagManager)GorillaGameManager.instance;
				result = (flag && manager != null);
				break;
			}
			case Gamemode.GameModes.Ghost:
			{
				bool flag2 = GorillaGameManager.instance is GorillaAmbushManager;
				manager = (GorillaAmbushManager)GorillaGameManager.instance;
				result = (flag2 && manager != null);
				break;
			}
			case Gamemode.GameModes.FreezeTag:
			{
				bool flag3 = GorillaGameManager.instance is GorillaFreezeTagManager;
				manager = (GorillaFreezeTagManager)GorillaGameManager.instance;
				result = (flag3 && manager != null);
				break;
			}
			case Gamemode.GameModes.Guardian:
			{
				bool flag4 = GorillaGameManager.instance is GorillaGuardianManager;
				manager = (GorillaGuardianManager)GorillaGameManager.instance;
				result = (flag4 && manager != null);
				break;
			}
			case Gamemode.GameModes.Casual:
			{
				bool flag5 = GorillaGameManager.instance is CasualGameMode;
				manager = (CasualGameMode)GorillaGameManager.instance;
				result = (flag5 && manager != null);
				break;
			}
			case Gamemode.GameModes.Ambush:
			{
				bool flag6 = GorillaGameManager.instance is GorillaAmbushManager;
				manager = (GorillaAmbushManager)GorillaGameManager.instance;
				result = (flag6 && manager != null);
				break;
			}
			case Gamemode.GameModes.Paintbrawl:
			{
				bool flag7 = GorillaGameManager.instance is GorillaPaintbrawlManager;
				manager = (GorillaPaintbrawlManager)GorillaGameManager.instance;
				result = (flag7 && manager != null);
				break;
			}
			case Gamemode.GameModes.Hunt:
			{
				bool flag8 = GorillaGameManager.instance is GorillaHuntManager;
				manager = (GorillaHuntManager)GorillaGameManager.instance;
				result = (flag8 && manager != null);
				break;
			}
			default:
				manager = GorillaGameManager.instance;
				result = false;
				break;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E910
		public static void TagPlayer(VRRig plr)
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				Master.TagPlayer(plr.Creator);
			}
			else
			{
				GorillaGameManager gorillaGameManager;
				bool flag = Gamemode.TryGetGameMode(Gamemode.GameModes.Infection, out gorillaGameManager);
				if (flag)
				{
					GorillaTagManager gorillaTagManager = (GorillaTagManager)gorillaGameManager;
					bool flag2 = !gorillaTagManager.currentInfected.Contains(plr.Creator) && gorillaTagManager.currentInfected.Contains(GorillaTagger.Instance.offlineVRRig.Creator);
					if (flag2)
					{
						GorillaTagger.Instance.offlineVRRig.enabled = false;
						GorillaTagger.Instance.offlineVRRig.transform.position = plr.transform.position + new Vector3(0f, -0.2f, 0f);
						Master.Serialize();
						PhotonView photonView = PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)"));
						photonView.RPC("RPC_ReportTag", 2, new object[]
						{
							plr.Creator.ActorNumber
						});
						GorillaTagger.Instance.offlineVRRig.enabled = true;
						Master.Serialize();
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000EA30
		public static void TagGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				Gamemode.TagPlayer(gunLibData.lockedPlayer);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000EA68
		public static void TagAll()
		{
			bool isTagged = Gamemode.IsTagged;
			if (isTagged)
			{
				foreach (VRRig plr in GorillaParent.instance.vrrigs)
				{
					Gamemode.TagPlayer(plr);
				}
				foreach (VRRig plr2 in GorillaParent.instance.vrrigs)
				{
					Gamemode.TagPlayer(plr2);
				}
			}
			else
			{
				Menu.GetButton("Tag All").Enabled = false;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000EB38
		public static void FlickTagAssist()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				GameObject gameObject = GameObject.CreatePrimitive(0);
				Object.Destroy(gameObject.GetComponent<Collider>());
				gameObject.GetComponent<Renderer>().material.color = Color.white;
				gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				Vector3 item = Global.TrueRightHand().Item1;
				Vector3 averageVelocity = GTPlayer.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(false, 0.15f, false);
				Vector3 vector = item;
				bool flag = averageVelocity.magnitude > 0.02f;
				if (flag)
				{
					float num = Vector3.Dot(averageVelocity.normalized, Global.TrueRightHand().Item4);
					float num2 = 0.7f + Mathf.Max(0f, num) * 2f;
					float num3 = Mathf.Clamp(averageVelocity.magnitude * num2, 0f, 2f);
					vector = item + averageVelocity.normalized * num3;
				}
				bool flag2 = Gamemode.smoothedBallPos == Vector3.zero;
				if (flag2)
				{
					Gamemode.smoothedBallPos = item;
				}
				Gamemode.smoothedBallPos = Vector3.Lerp(Gamemode.smoothedBallPos, vector, 0.5f);
				gameObject.transform.position = Gamemode.smoothedBallPos;
				Object.Destroy(gameObject, Time.deltaTime);
				foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
				{
					bool flag3 = Vector3.Distance(vrrig.transform.position, gameObject.transform.position) < 1f;
					if (flag3)
					{
						GorillaGameManager gorillaGameManager;
						bool flag4 = Gamemode.TryGetGameMode(Gamemode.GameModes.Infection, out gorillaGameManager);
						if (flag4)
						{
							GorillaTagManager gorillaTagManager = (GorillaTagManager)gorillaGameManager;
							bool flag5 = !gorillaTagManager.currentInfected.Contains(vrrig.Creator);
							if (flag5)
							{
								PhotonView photonView = PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)"));
								photonView.RPC("RPC_ReportTag", 2, new object[]
								{
									vrrig.Creator.ActorNumber
								});
							}
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000ED78
		public static void GrabBallAura()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				GameBall component = GameObject.Find("GameBall").GetComponent<GameBall>();
				bool flag = Vector3.Distance(component.transform.position, GorillaTagger.Instance.offlineVRRig.rightHandTransform.position) <= 5f;
				if (flag)
				{
					GamePlayer gamePlayer = (component.heldByActorNumber < 0) ? null : GamePlayer.GetGamePlayer(component.heldByActorNumber);
					bool flag2 = gamePlayer != null;
					if (flag2)
					{
						bool flag3 = gamePlayer.rig == GorillaTagger.Instance.offlineVRRig;
						if (flag3)
						{
							return;
						}
					}
					long num = BitPackUtils.PackHandPosRotForNetwork(Vector3.zero, Quaternion.identity);
					GameBallManager.Instance.photonView.RPC("RequestGrabBallRPC", 0, new object[]
					{
						component.id.index,
						false,
						num
					});
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000EE78
		public static void MatSelf()
		{
			bool flag = Time.time > Gamemode.cooldown;
			if (flag)
			{
				bool flag2 = GamePlayer.GetGamePlayer(PhotonNetwork.LocalPlayer.ActorNumber).teamId == 0 || GamePlayer.GetGamePlayer(PhotonNetwork.LocalPlayer.ActorNumber).teamId == -1;
				if (flag2)
				{
					MonkeBallGame.Instance.photonView.RPC("RequestSetTeamRPC", 2, new object[]
					{
						1
					});
					Color color = MonkeBallGame.Instance.team[1].color;
					GorillaTagger.Instance.offlineVRRig.InitializeNoobMaterialLocal(color.r, color.g, color.b);
				}
				else
				{
					bool flag3 = GamePlayer.GetGamePlayer(PhotonNetwork.LocalPlayer.ActorNumber).teamId == 1;
					if (flag3)
					{
						MonkeBallGame.Instance.photonView.RPC("RequestSetTeamRPC", 2, new object[]
						{
							0
						});
						Color color2 = MonkeBallGame.Instance.team[0].color;
						GorillaTagger.Instance.offlineVRRig.InitializeNoobMaterialLocal(color2.r, color2.g, color2.b);
					}
				}
				Gamemode.cooldown = Time.time + 0.05f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000EFC0
		public static void TagSelf()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				bool flag = !Gamemode.IsTagged;
				if (flag)
				{
					Master.TagPlayer(GorillaTagger.Instance.offlineVRRig.Creator);
				}
			}
			else
			{
				bool flag2 = !Gamemode.IsTagged;
				if (flag2)
				{
					foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
					{
						GorillaGameManager gorillaGameManager;
						bool flag3 = Gamemode.TryGetGameMode(Gamemode.GameModes.Infection, out gorillaGameManager);
						if (flag3)
						{
							bool flag4 = ((GorillaTagManager)gorillaGameManager).currentInfected.Contains(vrrig.Creator);
							if (flag4)
							{
								GorillaTagger.Instance.offlineVRRig.enabled = false;
								GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.rightHandTransform.position;
								Master.Serialize();
							}
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F0C8
		public static void AntiTag()
		{
			bool isMasterClient = Master.IsMasterClient;
			if (isMasterClient)
			{
				bool isTagged = Gamemode.IsTagged;
				if (isTagged)
				{
					Master.UntagPlayer(GorillaTagger.Instance.offlineVRRig.Creator);
				}
			}
			else
			{
				bool flag = !Gamemode.IsTagged;
				if (flag)
				{
					foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
					{
						GorillaGameManager gorillaGameManager;
						bool flag2 = Gamemode.TryGetGameMode(Gamemode.GameModes.Infection, out gorillaGameManager);
						if (flag2)
						{
							bool flag3 = ((GorillaTagManager)gorillaGameManager).currentInfected.Contains(vrrig.Creator);
							if (flag3)
							{
								bool flag4 = Vector3.Distance(vrrig.transform.position, GorillaTagger.Instance.offlineVRRig.transform.position) < 3f;
								if (flag4)
								{
									bool flag5 = Settings.AntiTagIndex == 0;
									if (flag5)
									{
										GorillaTagger.Instance.offlineVRRig.enabled = false;
										Master.Serialize();
									}
								}
							}
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F1F4
		public static void AlwaysGuardian()
		{
			GorillaGuardianManager gorillaGuardianManager = GameMode.ActiveGameMode as GorillaGuardianManager;
			bool flag = gorillaGuardianManager != null;
			if (flag)
			{
				bool isMasterClient = PhotonNetwork.IsMasterClient;
				if (isMasterClient)
				{
					GorillaGuardianManager component = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
					bool flag2 = !component.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer);
					if (flag2)
					{
						foreach (GorillaGuardianZoneManager gorillaGuardianZoneManager in GorillaGuardianZoneManager.zoneManagers)
						{
							bool enabled = gorillaGuardianZoneManager.enabled;
							if (enabled)
							{
								bool flag3 = gorillaGuardianZoneManager.CurrentGuardian != NetworkSystem.Instance.LocalPlayer;
								if (flag3)
								{
									gorillaGuardianZoneManager.SetGuardian(NetworkSystem.Instance.LocalPlayer);
								}
							}
						}
					}
				}
				else
				{
					foreach (TappableGuardianIdol tappableGuardianIdol in Gamemode.GetGuardianIdols())
					{
						bool flag4 = !tappableGuardianIdol.isChangingPositions;
						if (flag4)
						{
							GorillaGuardianManager component2 = GameObject.Find("GT Systems/GameModeSystem/Gorilla Guardian Manager").GetComponent<GorillaGuardianManager>();
							bool flag5 = !component2.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer);
							if (flag5)
							{
								GorillaTagger.Instance.offlineVRRig.enabled = false;
								GorillaTagger.Instance.offlineVRRig.transform.position = tappableGuardianIdol.transform.position;
								GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = tappableGuardianIdol.transform.position;
								GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = tappableGuardianIdol.transform.position;
								tappableGuardianIdol.manager.photonView.RPC("SendOnTapRPC", 0, new object[]
								{
									tappableGuardianIdol.tappableId,
									Random.Range(0.2f, 0.4f)
								});
							}
						}
						else
						{
							GorillaTagger.Instance.offlineVRRig.enabled = true;
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F430
		public static void MonkeBallFxSpammer()
		{
			Gamemode.counter++;
			MonkeBallGame.Instance.SendRPC("SetScoreRPC", 0, new object[]
			{
				Random.Range(0, 1),
				Gamemode.counter
			});
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F480
		public static void GuardianGrabAll()
		{
			GorillaGuardianManager gorillaGuardianManager = GameMode.ActiveGameMode as GorillaGuardianManager;
			bool flag = gorillaGuardianManager == null || !gorillaGuardianManager.IsPlayerGuardian(PhotonNetwork.LocalPlayer);
			if (!flag)
			{
				bool rightGrip = Input.RightGrip;
				if (rightGrip)
				{
					foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
					{
						bool flag2 = !vrrig.isOfflineVRRig;
						if (flag2)
						{
							Util.GetPhotonView(vrrig.Creator).RPC("GrabbedByPlayer", 0, new object[]
							{
								true,
								false,
								false
							});
						}
					}
				}
				else
				{
					foreach (VRRig vrrig2 in GorillaParent.instance.vrrigs)
					{
						bool flag3 = !vrrig2.isOfflineVRRig;
						if (flag3)
						{
							Util.GetPhotonView(vrrig2.Creator).RPC("DroppedByPlayer", 0, new object[]
							{
								new Vector3(0f, 10f, 0f)
							});
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F600
		public static void GuardianFlingAll()
		{
			GorillaGuardianManager gorillaGuardianManager = GameMode.ActiveGameMode as GorillaGuardianManager;
			bool flag = gorillaGuardianManager == null || !gorillaGuardianManager.IsPlayerGuardian(PhotonNetwork.LocalPlayer);
			if (!flag)
			{
				bool rightGrip = Input.RightGrip;
				if (rightGrip)
				{
					foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
					{
						bool flag2 = !vrrig.isOfflineVRRig;
						if (flag2)
						{
							GorillaTagger.Instance.offlineVRRig.enabled = false;
							GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(500f, 500f, 500f);
							bool flag3 = Gamemode.flingCooldown < Time.time;
							if (flag3)
							{
								Util.GetPhotonView(vrrig.Creator).RPC("GrabbedByPlayer", 0, new object[]
								{
									true,
									false,
									false
								});
								Gamemode.flingCooldown = Time.time + 0.1f;
							}
						}
					}
				}
				else
				{
					GorillaTagger.Instance.offlineVRRig.enabled = true;
				}
			}
		}

		// Token: 0x040000BA RID: 186
		public static float cooldown;

		// Token: 0x040000BB RID: 187
		public static float TagAuraDistance = 4f;

		// Token: 0x040000BC RID: 188
		public static int counter;

		// Token: 0x040000BD RID: 189
		public static Vector3 smoothedBallPos = Vector3.zero;

		// Token: 0x040000BE RID: 190
		public static TappableGuardianIdol[] idols;

		// Token: 0x040000BF RID: 191
		public static float flingCooldown = 0f;

		// Token: 0x0200004E RID: 78
		public enum GameModes
		{
			// Token: 0x04000198 RID: 408
			Infection,
			// Token: 0x04000199 RID: 409
			Ghost,
			// Token: 0x0400019A RID: 410
			FreezeTag,
			// Token: 0x0400019B RID: 411
			Guardian,
			// Token: 0x0400019C RID: 412
			Casual,
			// Token: 0x0400019D RID: 413
			Ambush,
			// Token: 0x0400019E RID: 414
			Paintbrawl,
			// Token: 0x0400019F RID: 415
			Hunt
		}
	}
}
