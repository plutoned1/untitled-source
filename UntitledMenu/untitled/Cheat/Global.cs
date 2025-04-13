using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BepInEx;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using untitled.Core.Patches;
using untitled.Core.Scripts;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x02000031 RID: 49
	public class Global
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F790
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

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F81C
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

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F8A8
		public static void NetworkSplash(Vector3 position, Quaternion rotation, float size, bool bigSplash, float timeCooldown)
		{
			bool flag = Global.SplashCooldown < Time.time;
			if (flag)
			{
				Global.SplashCooldown = Time.time + timeCooldown;
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", 0, new object[]
				{
					position,
					rotation,
					size,
					size,
					bigSplash,
					bigSplash
				});
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F927
		public static void SetQuestNetworkedQuestScore(int score)
		{
			GorillaTagger.Instance.myVRRig.SendRPC("RPC_UpdateQuestScore", 0, new object[]
			{
				score
			});
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F950
		public static void SendEffect(int timestamp, int affected, RaiseEventOptions options)
		{
			object[] array = new object[]
			{
				affected,
				0
			};
			object[] array2 = new object[]
			{
				timestamp,
				6,
				array
			};
			PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(3, array2, options, SendOptions.SendReliable);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000F9B0
		public static void SpawnHoldableThrowable(Vector3 pos, Vector3 vel, int type, string path, float delay)
		{
			bool flag = Time.time > delay;
			if (flag)
			{
				GorillaTagger.Instance.offlineVRRig.SetActiveTransferrableObjectIndex(1, type);
				GameObject gameObject = GorillaTagger.Instance.offlineVRRig.myBodyDockPositions.allObjects[type].gameObject;
				gameObject.SetActive(true);
				GorillaTagger.Instance.offlineVRRig.myBodyDockPositions.allObjects[type].storedZone = 2;
				GorillaTagger.Instance.offlineVRRig.myBodyDockPositions.allObjects[type].currentState = 2;
				object[] source = new object[]
				{
					pos,
					Quaternion.identity,
					vel,
					1f
				};
				RubberDuckEvents component = GameObject.Find(path).GetComponent<RubberDuckEvents>();
				int num = StaticHash.Compute(string.Format("{0}.{1}", component.PlayerId, "Activate"));
				object[] array = source.Prepend(num).ToArray<object>();
				PhotonNetwork.RaiseEvent(176, array, new RaiseEventOptions
				{
					Receivers = 1
				}, SendOptions.SendReliable);
				delay = Time.time + 0.3f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FAE0
		public static void WhoopeeCusionSpam()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnHoldableThrowable(Global.TrueRightHand().Item1, Vector3.zero, 626, "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/Right Arm Item Anchor/DropZoneAnchor/WhoopeeCushion_Anchor Variant(Clone)/LMAPM.", Global.cooldown3);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FB30
		public static void FirecrackerSpam()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnHoldableThrowable(Global.TrueRightHand().Item1, Vector3.zero, 587, "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/Right Arm Item Anchor/DropZoneAnchor/FireCrackersAnchor(Clone)/LMANZ.", Global.cooldown2);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FB80
		public static void BombSpam()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnHoldableThrowable(Global.TrueRightHand().Item1, Vector3.zero, 600, "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/Right Arm Item Anchor/DropZoneAnchor/SmokeBomb_Anchor Variant(Clone)/LMAOM.", Global.cooldown);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FBD0
		public static void AcceptTos()
		{
			bool flag = GameObject.Find("MetaReporting") != null;
			if (flag)
			{
				GameObject.Find("MetaReporting").SetActive(false);
			}
			bool flag2 = GameObject.Find("Miscellaneous Scripts/LegalAgreementCheck") != null;
			if (flag2)
			{
				LegalAgreements component = GameObject.Find("Miscellaneous Scripts/LegalAgreementCheck").GetComponent<LegalAgreements>();
				component.TurnPage(999);
				TOSPatch.turnedthefuckon = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FC3C
		public static void SpawnSnowball(Vector3 pos, Vector3 vel, float size, bool target = false, int[] targets = null, bool disable = false)
		{
			bool flag = !disable;
			if (flag)
			{
				bool flag2 = Time.time > Global.cooldown4;
				if (flag2)
				{
					GorillaTagger.Instance.offlineVRRig.RightThrowableProjectileIndex = 0;
					GorillaTagger.Instance.offlineVRRig.LeftThrowableProjectileIndex = 0;
					foreach (SnowballThrowable snowballThrowable in Object.FindObjectsOfType<SnowballThrowable>(true))
					{
						bool flag3 = snowballThrowable.name.Contains("LMACF. RIGHT.") || snowballThrowable.name.Contains("LMACE. LEFT.");
						if (flag3)
						{
							snowballThrowable.SetSnowballActiveLocal(true);
							snowballThrowable.gameObject.SetActive(true);
						}
					}
					Global.cooldown4 = Time.time + 5f;
				}
				RaiseEventOptions raiseEventOptions;
				if (!target)
				{
					(raiseEventOptions = new RaiseEventOptions()).Receivers = 1;
				}
				else
				{
					(raiseEventOptions = new RaiseEventOptions()).TargetActors = targets;
				}
				RaiseEventOptions raiseEventOptions2 = raiseEventOptions;
				Global.rightSnowball = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.");
				Global.leftSnowball = GameObject.Find("Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/GrowingSnowballLeftAnchor(Clone)/LMACE. LEFT.");
				bool flag4 = Time.time > Global.cooldown;
				if (flag4)
				{
					Global.cooldown = Time.time + 0.04f;
					Global.done = !Global.done;
					bool flag5 = Global.done;
					if (flag5)
					{
						PhotonEvent value = Traverse.Create(Global.rightSnowball.GetComponent<GrowingSnowballThrowable>()).Field("changeSizeEvent").GetValue<PhotonEvent>();
						PhotonEvent value2 = Traverse.Create(Global.rightSnowball.GetComponent<GrowingSnowballThrowable>()).Field("snowballThrowEvent").GetValue<PhotonEvent>();
						PhotonNetwork.RaiseEvent(176, new object[]
						{
							(int)Traverse.Create(value2).Field("_eventId").GetValue(),
							pos,
							vel,
							5f
						}, raiseEventOptions2, SendOptions.SendReliable);
						PhotonNetwork.RaiseEvent(176, new object[]
						{
							(int)Traverse.Create(value).Field("_eventId").GetValue(),
							(int)size
						}, raiseEventOptions2, SendOptions.SendReliable);
					}
					else
					{
						PhotonEvent value3 = Traverse.Create(Global.leftSnowball.GetComponent<GrowingSnowballThrowable>()).Field("changeSizeEvent").GetValue<PhotonEvent>();
						PhotonEvent value4 = Traverse.Create(Global.leftSnowball.GetComponent<GrowingSnowballThrowable>()).Field("snowballThrowEvent").GetValue<PhotonEvent>();
						PhotonNetwork.RaiseEvent(176, new object[]
						{
							(int)Traverse.Create(value4).Field("_eventId").GetValue(),
							pos,
							vel,
							5f
						}, raiseEventOptions2, SendOptions.SendReliable);
						PhotonNetwork.RaiseEvent(176, new object[]
						{
							(int)Traverse.Create(value3).Field("_eventId").GetValue(),
							(int)size
						}, raiseEventOptions2, SendOptions.SendReliable);
					}
				}
			}
			else
			{
				bool flag6 = Global.rightSnowball || Global.leftSnowball;
				if (flag6)
				{
					Global.rightSnowball.GetComponent<GrowingSnowballThrowable>().SetSnowballActiveLocal(false);
					Global.leftSnowball.GetComponent<GrowingSnowballThrowable>().SetSnowballActiveLocal(false);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FF90
		public static void SnowballSpam()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, false);
			}
			else
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000FFF8
		public static void SnowballMinigun()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Global.TrueRightHand().Item4 * 900f, 2f, false, null, false);
			}
			else
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010070
		public static void BigSnowballSpam()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 5f, false, null, false);
			}
			else
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000100D8
		public static void BigSnowballMinigun()
		{
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Global.TrueRightHand().Item4 * 900f, 5f, false, null, false);
			}
			else
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010150
		public static void SnowballPunchmod()
		{
			bool flag = Input.RightGrip && Input.RightTrigger;
			if (flag)
			{
				foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
				{
					bool flag2 = vrrig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, vrrig.headMesh.transform.position) < 0.25f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, vrrig.headMesh.transform.position) < 0.25f);
					if (flag2)
					{
						Vector3 vector = GorillaTagger.Instance.headCollider.transform.position - vrrig.headMesh.transform.position;
						Master.Serialize();
						for (int i = 0; i < 4; i++)
						{
							Global.SpawnSnowball(GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 0.5f, 0f) + new Vector3(vector.x, 0f, vector.z).normalized / 1.7f, new Vector3(0f, -500f, 0f), 5f, true, new int[]
							{
								vrrig.Creator.ActorNumber
							}, false);
						}
					}
				}
			}
			else
			{
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001034C
		public static void SnowballFlingGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isLocked;
			if (flag)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position = gunLibData.hitPosition + new Vector3(0f, -2f, -1.2f);
				Master.Serialize();
				Global.SpawnSnowball(gunLibData.hitPosition + new Vector3(0f, -2f, -0.2f), new Vector3(0f, 50f, 0f), 5f, true, new int[]
				{
					gunLibData.lockedPlayer.Creator.ActorNumber
				}, false);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
				Global.SpawnSnowball(Global.TrueRightHand().Item1, Vector3.zero, 2f, false, null, true);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010450
		public static void WatersplashGun()
		{
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				Global.NetworkSplash(gunLibData.hitPosition, Quaternion.identity, 1f, true, 0.1f);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010498
		public static void WatersplashHands()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				Global.NetworkSplash(Global.TrueRightHand().Item1, Global.TrueLeftHand().Item2, 1f, true, 0.1f);
			}
			bool leftGrip = Input.LeftGrip;
			if (leftGrip)
			{
				Global.NetworkSplash(Global.TrueLeftHand().Item1, Global.TrueLeftHand().Item2, 1f, true, 0.1f);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010508
		public static void EnableSnowballsRight()
		{
			GorillaTagger.Instance.offlineVRRig.RightThrowableProjectileIndex = 0;
			GameObject gameObject = GameObject.Find("Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.");
			GrowingSnowballThrowable growingSnowballThrowable = null;
			bool flag = !gameObject;
			if (flag)
			{
				gameObject = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.");
				growingSnowballThrowable.IncreaseSize(5);
				gameObject.gameObject.SetActive(true);
			}
			else
			{
				growingSnowballThrowable = gameObject.GetComponent<GrowingSnowballThrowable>();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010570
		public static void EnableSnowballsLeft()
		{
			GorillaTagger.Instance.offlineVRRig.LeftThrowableProjectileIndex = 0;
			GameObject gameObject = GameObject.Find("Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/GrowingSnowballLeftAnchor(Clone)/LMACE. LEFT.");
			GrowingSnowballThrowable growingSnowballThrowable = null;
			bool flag = !gameObject;
			if (flag)
			{
				gameObject = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballLeftAnchor(Clone)/LMACE. LEFT.");
				growingSnowballThrowable.IncreaseSize(5);
				gameObject.gameObject.SetActive(true);
			}
			else
			{
				growingSnowballThrowable = gameObject.GetComponent<GrowingSnowballThrowable>();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000105D8
		public static void EffectPlayerGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				bool flag2 = Global.EffectCooldown < Time.time;
				if (flag2)
				{
					Global.EffectCooldown = Time.time + 0.05f;
					int actorNumber = gunLibData.lockedPlayer.Creator.ActorNumber;
					Global.SendEffect(PhotonNetwork.ServerTimestamp, actorNumber, new RaiseEventOptions
					{
						Receivers = 1
					});
					bool flag3 = Global.stage < 5;
					if (flag3)
					{
						Global.stage++;
						Global.SendEffect(PhotonNetwork.ServerTimestamp - 1750, actorNumber, new RaiseEventOptions
						{
							Receivers = 1
						});
					}
					else
					{
						bool flag4 = Global.stage < 10;
						if (flag4)
						{
							Global.stage++;
							Global.SendEffect(PhotonNetwork.ServerTimestamp - 1501, actorNumber, new RaiseEventOptions
							{
								Receivers = 1
							});
						}
						else
						{
							bool flag5 = Global.stage < 15;
							if (flag5)
							{
								Global.stage++;
								Global.SendEffect(PhotonNetwork.ServerTimestamp - 1252, actorNumber, new RaiseEventOptions
								{
									Receivers = 1
								});
							}
							else
							{
								bool flag6 = Global.stage < 20;
								if (flag6)
								{
									Global.stage++;
									Global.SendEffect(PhotonNetwork.ServerTimestamp - 1003, actorNumber, new RaiseEventOptions
									{
										Receivers = 1
									});
								}
								else
								{
									bool flag7 = Global.stage < 25;
									if (flag7)
									{
										Global.stage++;
										Global.SendEffect(PhotonNetwork.ServerTimestamp - 754, actorNumber, new RaiseEventOptions
										{
											Receivers = 1
										});
									}
									else
									{
										bool flag8 = Global.stage < 30;
										if (flag8)
										{
											Global.stage++;
											Global.SendEffect(PhotonNetwork.ServerTimestamp - 505, actorNumber, new RaiseEventOptions
											{
												Receivers = 1
											});
										}
										else
										{
											bool flag9 = Global.stage < 35;
											if (flag9)
											{
												Global.stage++;
												Global.SendEffect(PhotonNetwork.ServerTimestamp - 256, actorNumber, new RaiseEventOptions
												{
													Receivers = 1
												});
											}
											else
											{
												bool flag10 = Global.stage < 40;
												if (flag10)
												{
													Global.stage++;
													Global.SendEffect(PhotonNetwork.ServerTimestamp - 7, actorNumber, new RaiseEventOptions
													{
														Receivers = 1
													});
												}
												else
												{
													bool flag11 = Global.stage < 45;
													if (flag11)
													{
														Global.stage++;
														Global.SendEffect(PhotonNetwork.ServerTimestamp + 242, actorNumber, new RaiseEventOptions
														{
															Receivers = 1
														});
													}
													else
													{
														bool flag12 = Global.stage < 50;
														if (flag12)
														{
															Global.stage++;
															Global.SendEffect(PhotonNetwork.ServerTimestamp + 491, actorNumber, new RaiseEventOptions
															{
																Receivers = 1
															});
														}
														else
														{
															bool flag13 = Global.stage < 55;
															if (flag13)
															{
																Global.stage++;
																Global.SendEffect(PhotonNetwork.ServerTimestamp + 740, actorNumber, new RaiseEventOptions
																{
																	Receivers = 1
																});
															}
															else
															{
																bool flag14 = Global.stage < 65;
																if (flag14)
																{
																	Global.stage++;
																	Global.SendEffect(PhotonNetwork.ServerTimestamp + 989, actorNumber, new RaiseEventOptions
																	{
																		Receivers = 1
																	});
																}
																else
																{
																	bool flag15 = Global.stage < 70;
																	if (flag15)
																	{
																		Global.stage++;
																		Global.SendEffect(PhotonNetwork.ServerTimestamp + 1238, actorNumber, new RaiseEventOptions
																		{
																			Receivers = 1
																		});
																	}
																	else
																	{
																		bool flag16 = Global.stage < 75;
																		if (flag16)
																		{
																			Global.stage++;
																			Global.SendEffect(PhotonNetwork.ServerTimestamp + 1487, actorNumber, new RaiseEventOptions
																			{
																				Receivers = 1
																			});
																		}
																		else
																		{
																			bool flag17 = Global.stage < 80;
																			if (flag17)
																			{
																				Global.stage++;
																				Global.SendEffect(PhotonNetwork.ServerTimestamp + 1736, actorNumber, new RaiseEventOptions
																				{
																					Receivers = 1
																				});
																			}
																			else
																			{
																				bool flag18 = Global.stage < 85;
																				if (flag18)
																				{
																					Global.stage++;
																					Global.SendEffect(PhotonNetwork.ServerTimestamp + -1985, actorNumber, new RaiseEventOptions
																					{
																						Receivers = 1
																					});
																				}
																				else
																				{
																					bool flag19 = Global.stage >= 85;
																					if (flag19)
																					{
																						Global.stage = 0;
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010A64
		public static void EffectAll()
		{
			bool flag = Global.EffectCooldown < Time.time;
			if (flag)
			{
				Global.EffectCooldown = Time.time + 0.4f;
				foreach (Player player in PhotonNetwork.PlayerList)
				{
					Global.SendEffect(PhotonNetwork.ServerTimestamp, player.ActorNumber, new RaiseEventOptions
					{
						Receivers = 1
					});
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010ACC
		public static void GetHoverboard()
		{
			GorillaTagger.Instance.offlineVRRig.hoverboardVisual.SetIsHeld(false, Vector3.zero, Quaternion.identity, Color.red);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010AF4
		public static void SpawnHoverboard()
		{
			FreeHoverboardManager.instance.SendDropBoardRPC(Global.TrueRightHand().Item1, Quaternion.identity, Vector3.zero, Vector3.zero, Color.black);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010B20
		public static void MaxQuestScore()
		{
			Global.SetQuestNetworkedQuestScore(99999);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010B30
		public static void OwnershipOfCritters()
		{
			bool flag = Time.time > Global.cooldown && !CrittersManager.instance.guard.photonView.IsMine;
			if (flag)
			{
				CrittersManager.instance.guard.photonView.RPC("OwnershipRequested", 0, new object[]
				{
					CrittersManager.instance.guard.ownershipRequestNonce + "1"
				});
				Global.cooldown = Time.time + 0.5f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010BC0
		public static void EnermousCritterSpammer()
		{
			Global.OwnershipOfCritters();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && CrittersManager.instance.guard.photonView.IsMine;
			if (flag)
			{
				CrittersPawn crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
				crittersPawn.MoveActor(gunLibData.hitPosition, Quaternion.identity, false, true, true);
				CritterVisuals visuals = crittersPawn.visuals;
				CritterAppearance appearance = default(CritterAppearance);
				appearance.size = 650f;
				visuals.SetAppearance(appearance);
				CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
				{
					crittersPawn.actorId,
					crittersPawn.regionIndex,
					crittersPawn.visuals.Appearance.WriteToRPCData()
				});
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010CAC
		public static void GiantCritterSpammer()
		{
			Global.OwnershipOfCritters();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && CrittersManager.instance.guard.photonView.IsMine;
			if (flag)
			{
				CrittersPawn crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
				crittersPawn.MoveActor(gunLibData.hitPosition, Quaternion.identity, false, true, true);
				CritterVisuals visuals = crittersPawn.visuals;
				CritterAppearance appearance = default(CritterAppearance);
				appearance.size = 250f;
				visuals.SetAppearance(appearance);
				CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
				{
					crittersPawn.actorId,
					crittersPawn.regionIndex,
					crittersPawn.visuals.Appearance.WriteToRPCData()
				});
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010D98
		public static void HugeCritterSpammer()
		{
			Global.OwnershipOfCritters();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && CrittersManager.instance.guard.photonView.IsMine;
			if (flag)
			{
				CrittersPawn crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
				crittersPawn.MoveActor(gunLibData.hitPosition, Quaternion.identity, false, true, true);
				CritterVisuals visuals = crittersPawn.visuals;
				CritterAppearance appearance = default(CritterAppearance);
				appearance.size = 100f;
				visuals.SetAppearance(appearance);
				CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
				{
					crittersPawn.actorId,
					crittersPawn.regionIndex,
					crittersPawn.visuals.Appearance.WriteToRPCData()
				});
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010E84
		public static void BigCritterSpammer()
		{
			Global.OwnershipOfCritters();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered && CrittersManager.instance.guard.photonView.IsMine;
			if (flag)
			{
				CrittersPawn crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
				crittersPawn.MoveActor(gunLibData.hitPosition, Quaternion.identity, false, true, true);
				CritterVisuals visuals = crittersPawn.visuals;
				CritterAppearance appearance = default(CritterAppearance);
				appearance.size = 10f;
				visuals.SetAppearance(appearance);
				CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
				{
					crittersPawn.actorId,
					crittersPawn.regionIndex,
					crittersPawn.visuals.Appearance.WriteToRPCData()
				});
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010F70
		public static void StickyGooGun()
		{
			Global.OwnershipOfCritters();
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				bool isMine = CrittersManager.instance.guard.photonView.IsMine;
				if (isMine)
				{
					CrittersStickyGoo crittersStickyGoo = (CrittersStickyGoo)CrittersManager.instance.SpawnActor(18, -1);
					crittersStickyGoo.MoveActor(gunLibData.hitPosition, Quaternion.identity, false, true, true);
					Master.Serialize();
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00010FF0
		public static void FxSpammerWhite()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && Time.time > Global.cooldown2) || (UnityInput.Current.GetMouseButton(1) && Time.time > Global.cooldown4);
			if (flag)
			{
				Vector3 position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
				CrittersStunBomb crittersStunBomb = (CrittersStunBomb)CrittersManager.instance.SpawnActor(13, -1);
				Master.Serialize();
				CrittersManager.instance.TriggerEvent(0, crittersStunBomb.actorId, position, Quaternion.Euler(Random.insideUnitSphere.normalized));
				Global.cooldown2 = Time.time + 0.035f;
			}
			bool flag2 = Input.LeftGrip && Time.time > Global.cooldown2;
			if (flag2)
			{
				Vector3 position2 = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
				CrittersStunBomb crittersStunBomb2 = (CrittersStunBomb)CrittersManager.instance.SpawnActor(13, -1);
				Master.Serialize();
				CrittersManager.instance.TriggerEvent(0, crittersStunBomb2.actorId, position2, Quaternion.Euler(Random.insideUnitSphere.normalized));
				Global.cooldown2 = Time.time + 0.035f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001112C
		public static void FxSpammerYellow()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && Time.time > Global.cooldown) || (UnityInput.Current.GetMouseButton(1) && Time.time > Global.cooldown4);
			if (flag)
			{
				Vector3 position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
				CrittersLoudNoise crittersLoudNoise = (CrittersLoudNoise)CrittersManager.instance.SpawnActor(2, -1);
				crittersLoudNoise.soundDuration = 1000f;
				crittersLoudNoise.soundVolume = 1000f;
				CrittersManager.instance.TriggerEvent(1, crittersLoudNoise.actorId, position, Quaternion.Euler(Random.insideUnitSphere.normalized));
				Master.Serialize();
				Global.cooldown = Time.time + 0.03f;
			}
			bool flag2 = Input.LeftGrip && Time.time > Global.cooldown;
			if (flag2)
			{
				Vector3 position2 = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
				CrittersLoudNoise crittersLoudNoise2 = (CrittersLoudNoise)CrittersManager.instance.SpawnActor(2, -1);
				crittersLoudNoise2.soundDuration = 1000f;
				crittersLoudNoise2.soundVolume = 1000f;
				CrittersManager.instance.TriggerEvent(1, crittersLoudNoise2.actorId, position2, Quaternion.Euler(Random.insideUnitSphere.normalized));
				Master.Serialize();
				Global.cooldown = Time.time + 0.03f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00011298
		public static void FxSpammerGray()
		{
			Global.OwnershipOfCritters();
			bool flag = Input.RightGrip || (UnityInput.Current.GetMouseButton(1) && Time.time > Global.cooldown4);
			if (flag)
			{
				CrittersStickyGoo crittersStickyGoo = (CrittersStickyGoo)CrittersManager.instance.SpawnActor(18, -1);
				CrittersManager.instance.TriggerEvent(3, crittersStickyGoo.actorId, Global.TrueRightHand().Item1, Global.TrueRightHand().Item2);
				Master.Serialize();
				Global.cooldown3 = Time.time + 0.03f;
			}
			bool flag2 = Input.LeftGrip && Time.time > Global.cooldown3;
			if (flag2)
			{
				CrittersStickyGoo crittersStickyGoo2 = (CrittersStickyGoo)CrittersManager.instance.SpawnActor(18, -1);
				CrittersManager.instance.TriggerEvent(3, crittersStickyGoo2.actorId, Global.TrueRightHand().Item1, Global.TrueRightHand().Item2);
				Master.Serialize();
				Global.cooldown3 = Time.time + 0.03f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001139C
		public static void FxSpammerOrange()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && Time.time > Global.cooldown4) || (UnityInput.Current.GetMouseButton(1) && Time.time > Global.cooldown4);
			if (flag)
			{
				CrittersStickyTrap crittersStickyTrap = (CrittersStickyTrap)CrittersManager.instance.SpawnActor(17, -1);
				CrittersManager.instance.TriggerEvent(2, crittersStickyTrap.actorId, Global.TrueRightHand().Item1, Global.TrueRightHand().Item2);
				Master.Serialize();
				Global.cooldown4 = Time.time + 0.03f;
			}
			bool flag2 = Input.LeftGrip && Time.time > Global.cooldown4;
			if (flag2)
			{
				CrittersStickyTrap crittersStickyTrap2 = (CrittersStickyTrap)CrittersManager.instance.SpawnActor(17, -1);
				CrittersManager.instance.TriggerEvent(2, crittersStickyTrap2.actorId, Global.TrueRightHand().Item1, Global.TrueRightHand().Item2);
				Master.Serialize();
				Global.cooldown4 = Time.time + 0.03f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000114AC
		public static void GrabAllItems()
		{
			Global.OwnershipOfCritters();
			bool flag = Input.RightGrip || UnityInput.Current.GetMouseButton(1);
			if (flag)
			{
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000114DC
		public static void CritterSpammer()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveCritters.Count < 40;
				CrittersPawn crittersPawn;
				if (flag2)
				{
					crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
					CritterVisuals visuals = crittersPawn.visuals;
					CritterAppearance appearance = default(CritterAppearance);
					appearance.size = 1f;
					appearance.hatName = "PaperCrown";
					visuals.SetAppearance(appearance);
					crittersPawn.SetTemplate(0);
					crittersPawn.currentState = 2;
					CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
					{
						crittersPawn.actorId,
						crittersPawn.regionIndex,
						crittersPawn.visuals.Appearance.WriteToRPCData()
					});
					bool flag3 = !Global.ActiveCritters.Contains(crittersPawn);
					if (flag3)
					{
						Global.ActiveCritters.Add(crittersPawn);
					}
				}
				else
				{
					crittersPawn = Global.ActiveCritters[Global.reuseIndex];
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 40;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersPawn.MoveActor(item, Quaternion.identity, false, true, true);
				crittersPawn.rb.velocity = Vector3.zero;
				crittersPawn.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001169C
		public static void CritterMinigun()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveCritters.Count < 40;
				CrittersPawn crittersPawn;
				if (flag2)
				{
					crittersPawn = (CrittersPawn)CrittersManager.instance.SpawnActor(0, -1);
					CritterVisuals visuals = crittersPawn.visuals;
					CritterAppearance appearance = default(CritterAppearance);
					appearance.size = 1f;
					appearance.hatName = "PaperCrown";
					visuals.SetAppearance(appearance);
					crittersPawn.SetTemplate(0);
					crittersPawn.currentState = 2;
					CrittersManager.instance.GetView.RPC("RemoteSpawnCreature", 0, new object[]
					{
						crittersPawn.actorId,
						crittersPawn.regionIndex,
						crittersPawn.visuals.Appearance.WriteToRPCData()
					});
					bool flag3 = !Global.ActiveCritters.Contains(crittersPawn);
					if (flag3)
					{
						Global.ActiveCritters.Add(crittersPawn);
					}
				}
				else
				{
					crittersPawn = Global.ActiveCritters[Global.reuseIndex];
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 40;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersPawn.MoveActor(item, Quaternion.identity, false, true, true);
				crittersPawn.rb.velocity = Global.TrueRightHand().Item4 * Time.deltaTime * 1000f;
				crittersPawn.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00011874
		public static void StunBombSpammer()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveBalls.Count < 90;
				CrittersStunBomb crittersStunBomb;
				if (flag2)
				{
					crittersStunBomb = (CrittersStunBomb)CrittersManager.instance.SpawnActor(13, -1);
					bool flag3 = !Global.ActiveBalls.Contains(crittersStunBomb);
					if (flag3)
					{
						Global.ActiveBalls.Add(crittersStunBomb);
					}
				}
				else
				{
					crittersStunBomb = Global.ActiveBalls[Global.reuseIndex];
					crittersStunBomb.MoveActor(item, Quaternion.identity, false, true, true);
					crittersStunBomb.lastImpulseVelocity = Vector3.zero;
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 90;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersStunBomb.MoveActor(item, Quaternion.identity, false, true, true);
				crittersStunBomb.rb.velocity = Vector3.zero;
				crittersStunBomb.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000119BC
		public static void StunBombMinigun()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveBalls.Count < 90;
				CrittersStunBomb crittersStunBomb;
				if (flag2)
				{
					crittersStunBomb = (CrittersStunBomb)CrittersManager.instance.SpawnActor(13, -1);
					bool flag3 = !Global.ActiveBalls.Contains(crittersStunBomb);
					if (flag3)
					{
						Global.ActiveBalls.Add(crittersStunBomb);
					}
				}
				else
				{
					crittersStunBomb = Global.ActiveBalls[Global.reuseIndex];
					crittersStunBomb.MoveActor(item, Quaternion.identity, false, true, true);
					crittersStunBomb.rb.velocity = Global.TrueRightHand().Item4 * Time.deltaTime * 1000f;
					crittersStunBomb.UpdateImpulseVelocity();
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 90;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersStunBomb.MoveActor(item, Quaternion.identity, false, true, true);
				crittersStunBomb.rb.velocity = Global.TrueRightHand().Item4 * Time.deltaTime * 1000f;
				crittersStunBomb.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00011B44
		public static void StickyTrapSpammer()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveOrangeBalls.Count < 90;
				CrittersStickyTrap crittersStickyTrap;
				if (flag2)
				{
					crittersStickyTrap = (CrittersStickyTrap)CrittersManager.instance.SpawnActor(17, -1);
					bool flag3 = !Global.ActiveOrangeBalls.Contains(crittersStickyTrap);
					if (flag3)
					{
						Global.ActiveOrangeBalls.Add(crittersStickyTrap);
					}
				}
				else
				{
					crittersStickyTrap = Global.ActiveOrangeBalls[Global.reuseIndex];
					crittersStickyTrap.MoveActor(item, Quaternion.identity, false, true, true);
					crittersStickyTrap.rb.velocity = Vector3.zero;
					crittersStickyTrap.UpdateImpulseVelocity();
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 90;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersStickyTrap.MoveActor(item, Quaternion.identity, false, true, true);
				crittersStickyTrap.rb.velocity = Vector3.zero;
				crittersStickyTrap.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00011C9C
		public static void StickyTrapMinigun()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 item = Global.TrueRightHand().Item1;
				bool flag2 = Global.ActiveOrangeBalls.Count < 90;
				CrittersStickyTrap crittersStickyTrap;
				if (flag2)
				{
					crittersStickyTrap = (CrittersStickyTrap)CrittersManager.instance.SpawnActor(17, -1);
					bool flag3 = !Global.ActiveOrangeBalls.Contains(crittersStickyTrap);
					if (flag3)
					{
						Global.ActiveOrangeBalls.Add(crittersStickyTrap);
					}
				}
				else
				{
					crittersStickyTrap = Global.ActiveOrangeBalls[Global.reuseIndex];
					crittersStickyTrap.MoveActor(item, Quaternion.identity, false, true, true);
					crittersStickyTrap.rb.velocity = Global.TrueRightHand().Item4 * Time.deltaTime * 1000f;
					crittersStickyTrap.UpdateImpulseVelocity();
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 90;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersStickyTrap.MoveActor(item, Quaternion.identity, false, true, true);
				crittersStickyTrap.rb.velocity = Global.TrueRightHand().Item4 * Time.deltaTime * 1000f;
				crittersStickyTrap.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00011E24
		public static void StickyTrapPiss()
		{
			Global.OwnershipOfCritters();
			bool flag = (Input.RightGrip && CrittersManager.instance.guard.photonView.IsMine) || (UnityInput.Current.GetMouseButton(1) && CrittersManager.instance.guard.photonView.IsMine);
			if (flag)
			{
				Master.Serialize();
				Vector3 vector = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, -0.15f, 0f);
				Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;
				bool flag2 = Global.ActiveOrangeBalls.Count < 90;
				CrittersStickyTrap crittersStickyTrap;
				if (flag2)
				{
					crittersStickyTrap = (CrittersStickyTrap)CrittersManager.instance.SpawnActor(17, -1);
					bool flag3 = !Global.ActiveOrangeBalls.Contains(crittersStickyTrap);
					if (flag3)
					{
						Global.ActiveOrangeBalls.Add(crittersStickyTrap);
					}
				}
				else
				{
					crittersStickyTrap = Global.ActiveOrangeBalls[Global.reuseIndex];
					crittersStickyTrap.MoveActor(vector, Quaternion.identity, false, true, true);
					crittersStickyTrap.rb.velocity = velocity;
					crittersStickyTrap.UpdateImpulseVelocity();
					Global.reuseIndex++;
					bool flag4 = Global.reuseIndex >= 90;
					if (flag4)
					{
						Global.reuseIndex = 0;
					}
				}
				Master.Serialize();
				crittersStickyTrap.MoveActor(vector, Quaternion.identity, false, true, true);
				crittersStickyTrap.rb.velocity = velocity;
				crittersStickyTrap.UpdateImpulseVelocity();
				Master.Serialize();
			}
		}

		// Token: 0x040000C0 RID: 192
		public static float cooldown;

		// Token: 0x040000C1 RID: 193
		public static float cooldown2;

		// Token: 0x040000C2 RID: 194
		public static float cooldown3;

		// Token: 0x040000C3 RID: 195
		public static float cooldown4;

		// Token: 0x040000C4 RID: 196
		public static float SplashCooldown;

		// Token: 0x040000C5 RID: 197
		public static bool done;

		// Token: 0x040000C6 RID: 198
		public static bool timeout;

		// Token: 0x040000C7 RID: 199
		public static int stage;

		// Token: 0x040000C8 RID: 200
		public static float EffectCooldown;

		// Token: 0x040000C9 RID: 201
		public static int reuseIndex = 0;

		// Token: 0x040000CA RID: 202
		public const int MaxCritters = 100;

		// Token: 0x040000CB RID: 203
		public static List<CrittersPawn> ActiveCritters = new List<CrittersPawn>();

		// Token: 0x040000CC RID: 204
		public static List<CrittersPawn> skeletonCritters = new List<CrittersPawn>();

		// Token: 0x040000CD RID: 205
		public static List<CrittersStunBomb> ActiveBalls = new List<CrittersStunBomb>();

		// Token: 0x040000CE RID: 206
		public static List<CrittersStickyTrap> ActiveOrangeBalls = new List<CrittersStickyTrap>();

		// Token: 0x040000CF RID: 207
		public static bool skeletonInitialized;

		// Token: 0x040000D0 RID: 208
		public static GameObject rightSnowball;

		// Token: 0x040000D1 RID: 209
		public static GameObject leftSnowball;
	}
}
