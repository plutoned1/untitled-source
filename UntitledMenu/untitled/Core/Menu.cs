using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using GorillaExtensions;
using GorillaLocomotion;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using untitled.Assets;
using untitled.Cheat;
using untitled.Core.Scripts;
using untitled.Utilities;

namespace untitled.Core
{
	// Token: 0x0200000D RID: 13
	public class Menu : MonoBehaviour
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000653D File Offset: 0x0000473D
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00006544 File Offset: 0x00004744
		public static Menu instance { get; private set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000654C
		private void Awake()
		{
			Menu.instance = this;
			Menu.__instance = GTPlayer.Instance;
			Menu.__self = GorillaTagger.Instance.offlineVRRig;
			Menu.StaticCameraObject = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera");
			Menu.StaticVCam = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1");
			Debug.Log("Captured Player Instances and Started Menu!");
			GorillaTagger.OnPlayerSpawned(new Action(this.PlayerSpawned));
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000065B8
		public void Start()
		{
			bool flag = Menu.cachedData == null;
			if (flag)
			{
				Menu.cachedData = Settings.ReadJson("Untitled\\config.json");
			}
			bool flag2 = !Directory.Exists("Untitled");
			if (flag2)
			{
				Directory.CreateDirectory("Untitled");
			}
			bool flag3 = !Directory.Exists("Untitled\\Plugins");
			if (flag3)
			{
				Directory.CreateDirectory("Untitled\\Plugins");
			}
			File.WriteAllText("Untitled\\Plugins\\readme.txt", "How to use!\n\n- For Users - \nIf you have a plugin (should be a .dll file) just drag it in this folder and it will load in a tab called Plugin Mods\n\n= WARNING =\n\n You will not be able to just add any dll/mod menu here, it has to be made with the plugin libary.\n\n- For Developers - \nIf you are wanting to make plugins then you will have to download the untitled plugin libary. Then after that you will need visual studio to make the mods if you dont know how to code dont ask the developers because we WONT HELP!\n\nIn the discord there will be examples on how to use it!\n\n- Untitled Team, last update: 4/7/25 18:50 PM");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000662F
		public void PlayerSpawned()
		{
			Settings.LoadConfig();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00006638
		public void LateUpdate()
		{
			try
			{
				bool flag = !Menu.__instance || !Menu.__self;
				if (flag)
				{
					Debug.LogError("Failed to capture player instance, the menu will not function.");
					Object.Destroy(base.gameObject);
				}
				else
				{
					bool flag2 = !PhotonNetwork.InRoom;
					if (flag2)
					{
						foreach (PhotonView photonView in PhotonNetwork.PhotonViewCollection)
						{
							bool flag3 = (photonView.IsRoomView && photonView.gameObject.name.Contains("Player Network")) || (photonView.IsRoomView && photonView.gameObject.name.Contains("LCK"));
							if (flag3)
							{
								Object.Destroy(photonView.gameObject);
							}
						}
					}
					bool keyDown = UnityInput.Current.GetKeyDown(Menu.PCMenuKey);
					if (keyDown)
					{
						Menu.PCMenuOpen = !Menu.PCMenuOpen;
					}
					bool flag4 = Menu.PCMenuOpen && !Menu.InMenuCondition && !Menu.menuOpen;
					if (flag4)
					{
						Menu.InPcCondition = true;
						bool flag5 = Menu.StaticVCam != null;
						if (flag5)
						{
							Menu.StaticVCam.SetActive(false);
						}
						bool flag6 = Menu.selectedPlayerCate != null;
						if (flag6)
						{
							VRRig rigFromPlayer = RigShit.GetRigFromPlayer(Menu.selectedPlayerCate);
							bool flag7 = rigFromPlayer != null;
							if (flag7)
							{
								bool flag8 = Menu.TitleView == null;
								if (flag8)
								{
									Menu.RefreshMenu();
								}
								bool flag9 = Menu.CamView != null;
								if (flag9)
								{
									bool flag10 = Settings.CamIndex == 0;
									if (flag10)
									{
										Vector3 position = rigFromPlayer.transform.position - new Vector3(0f, 0f, 2f);
										Menu.CamView.transform.position = position;
										Menu.CamView.transform.rotation = Quaternion.identity;
									}
									else
									{
										Menu.CamView.transform.position = rigFromPlayer.head.rigTarget.position;
										Menu.CamView.transform.rotation = rigFromPlayer.head.rigTarget.rotation;
									}
								}
							}
							else
							{
								Debug.LogError("No rig found for the selected player category.");
							}
						}
						bool flag11 = Menu.selectedPlayerCate != null && !PhotonNetwork.InRoom;
						if (flag11)
						{
							Buttons.List[13].Clear();
							Menu.selectedPlayerCate = null;
							Menu.RefreshMenu();
							Buttons.List[13].Add(new Button("Back", false, delegate()
							{
								Menu.ChangePage(Menu.Category.Base);
							}, null, null, false));
							Menu.ChangePage(Menu.Category.Base);
						}
						bool flag12 = Menu.MenuObj == null;
						if (flag12)
						{
							Menu.DrawFrame();
							bool animations = Settings.Animations;
							if (animations)
							{
								base.StartCoroutine(Menu.GrowCoroutine());
							}
							Menu.CreateClicker(ref Menu.PointerObject, Menu.StaticCameraObject.transform);
						}
						else
						{
							bool flag13 = Menu.fps != null;
							if (flag13)
							{
								Menu.fps.text = string.Format("FPS : {0}", Mathf.Round(1f / Time.unscaledDeltaTime));
							}
							Menu.CreateClicker(ref Menu.PointerObject, Menu.StaticCameraObject.transform);
							bool flag14 = Menu.StaticCameraObject != null;
							if (flag14)
							{
								Menu.PositionMenu(Menu.PositionType.Computer);
								bool isPressed = Mouse.current.leftButton.isPressed;
								if (isPressed)
								{
									Ray ray = Menu.StaticCameraObject.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
									RaycastHit raycastHit;
									bool flag15 = Physics.Raycast(ray, ref raycastHit);
									if (flag15)
									{
										ButtonCollider buttonCollider = (raycastHit.collider != null) ? raycastHit.collider.GetComponent<ButtonCollider>() : null;
										bool flag16 = buttonCollider != null && Menu.PointerObject != null;
										if (flag16)
										{
											buttonCollider.OnTriggerEnter(Menu.PointerObject.GetComponent<Collider>());
										}
									}
								}
								else
								{
									bool flag17 = Menu.PointerObject != null;
									if (flag17)
									{
										Object.Destroy(Menu.PointerObject);
										Menu.PointerObject = null;
									}
								}
							}
						}
					}
					bool flag18 = Menu.MenuObj != null && !Menu.PCMenuOpen && !Menu.InMenuCondition && Menu.InPcCondition;
					if (flag18)
					{
						bool animations2 = Settings.Animations;
						if (animations2)
						{
							base.StartCoroutine(Menu.ShrinkCoroutine());
						}
						Menu.DestroyMenu();
						Menu.InPcCondition = false;
						bool flag19 = Menu.StaticVCam != null;
						if (flag19)
						{
							Menu.StaticVCam.SetActive(true);
						}
					}
					Menu.openMenu = ControllerInputPoller.instance.leftControllerSecondaryButton;
					bool flag20 = Menu.openMenu && !Menu.InPcCondition;
					if (flag20)
					{
						Menu.InMenuCondition = true;
						bool flag21 = Menu.MenuObj == null;
						if (flag21)
						{
							Menu.DrawFrame();
							bool animations3 = Settings.Animations;
							if (animations3)
							{
								base.StartCoroutine(Menu.GrowCoroutine());
							}
							bool flag22 = !Settings.Animations || Settings.DropMenu;
							if (flag22)
							{
								Menu.AddRigidbodyToMenu();
							}
							bool flag23 = !Menu.firstopen;
							if (flag23)
							{
								Settings.LoadConfig();
								Menu.firstopen = true;
							}
							bool flag24 = Menu.PointerObject == null;
							if (flag24)
							{
								Menu.CreateClicker(ref Menu.PointerObject, Menu.__instance.rightControllerTransform);
							}
						}
						else
						{
							bool flag25 = Menu.selectedPlayerCate != null;
							if (flag25)
							{
								bool flag26 = Menu.TitleView == null;
								if (flag26)
								{
									Menu.RefreshMenu();
								}
								VRRig rigFromPlayer2 = RigShit.GetRigFromPlayer(Menu.selectedPlayerCate);
								bool flag27 = rigFromPlayer2 != null;
								if (flag27)
								{
									bool flag28 = Settings.CamIndex == 0;
									if (flag28)
									{
										Vector3 position2 = rigFromPlayer2.transform.position - new Vector3(0f, 0f, 2f);
										Menu.CamView.transform.position = position2;
										Menu.CamView.transform.rotation = Quaternion.identity;
									}
									else
									{
										Menu.CamView.transform.position = rigFromPlayer2.head.rigTarget.position;
										Menu.CamView.transform.rotation = rigFromPlayer2.head.rigTarget.rotation;
									}
								}
							}
							bool flag29 = Menu.fps != null;
							if (flag29)
							{
								Menu.fps.text = string.Format("FPS : {0}", Mathf.RoundToInt(1f / Time.deltaTime));
							}
							Menu.PositionMenu(Menu.PositionType.Hand);
							bool flag30 = !Settings.Pad && !Settings.PageButtons;
							if (flag30)
							{
								bool flag31 = Input.RightTrigger && !this.lastRightTrigger;
								if (flag31)
								{
									Menu.ToggleButton(new Button("PreviousPage", false, delegate()
									{
										Menu.PageIndex++;
									}, null, null, false));
									Menu.RefreshMenu();
									bool audio = Settings.Audio;
									if (audio)
									{
										AssetLoader.PlayClick(Settings.AudioName);
									}
									else
									{
										GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Settings.AudioID, false, 0.8f);
									}
									Menu.ButtonCooldown = Time.frameCount;
								}
								this.lastRightTrigger = Input.RightTrigger;
								bool flag32 = Input.LeftTrigger && !this.lastLeftTrigger;
								if (flag32)
								{
									Menu.ToggleButton(new Button("NextPage", false, delegate()
									{
										Menu.PageIndex--;
									}, null, null, false));
									Menu.RefreshMenu();
									bool audio2 = Settings.Audio;
									if (audio2)
									{
										AssetLoader.PlayClick(Settings.AudioName);
									}
									else
									{
										GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Settings.AudioID, true, 0.8f);
									}
									Menu.ButtonCooldown = Time.frameCount;
								}
								this.lastLeftTrigger = Input.LeftTrigger;
							}
						}
					}
					else
					{
						bool flag33 = Menu.MenuObj != null && Menu.InMenuCondition;
						if (flag33)
						{
							Menu.InMenuCondition = false;
							bool flag34 = !Settings.Animations || Settings.DropMenu;
							if (flag34)
							{
								Menu.AddRigidbodyToMenu();
								Menu.currentMenuRigidbody.velocity = GTPlayer.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0f, false);
								Menu.currentMenuRigidbody.angularVelocity = GTExt.GetOrAddComponent<GorillaVelocityEstimator>(GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/RightHand Controller")).angularVelocity;
							}
							bool animations4 = Settings.Animations;
							if (animations4)
							{
								base.StartCoroutine(Menu.ShrinkCoroutine());
							}
							Menu.DestroyMenu();
						}
					}
					bool flag35 = Menu.PageIndex != Menu.GetCategoryIndex();
					if (flag35)
					{
						Menu.PageIndex = Menu.GetCategoryIndex();
					}
					foreach (List<Button> list in Buttons.List)
					{
						foreach (Button button in list)
						{
							bool flag36 = button.OnEnable != null && button.Enabled;
							if (flag36)
							{
								button.OnEnable();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007040
		public static void DisableMod(string name)
		{
			using (List<List<Button>>.Enumerator enumerator = Buttons.List.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					List<Button> list = enumerator.Current;
					foreach (Button button in list)
					{
						bool flag = button.Name.Contains(name);
						if (flag)
						{
							button.Enabled = false;
							Menu.RefreshMenu();
							break;
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000070F0
		public static int GetCategoryIndex()
		{
			bool flag = Menu.CurrentCategory == Menu.Category.Base;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = Menu.CurrentCategory == Menu.Category.Settings;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = Menu.CurrentCategory == Menu.Category.Players;
					if (flag3)
					{
						result = 13;
					}
					else
					{
						bool flag4 = Menu.CurrentCategory == Menu.Category.Vrrig;
						if (flag4)
						{
							result = 2;
						}
						else
						{
							bool flag5 = Menu.CurrentCategory == Menu.Category.Movement;
							if (flag5)
							{
								result = 3;
							}
							else
							{
								bool flag6 = Menu.CurrentCategory == Menu.Category.Visuals;
								if (flag6)
								{
									result = 4;
								}
								else
								{
									bool flag7 = Menu.CurrentCategory == Menu.Category.OP;
									if (flag7)
									{
										result = 5;
									}
									else
									{
										bool flag8 = Menu.CurrentCategory == Menu.Category.Master;
										if (flag8)
										{
											result = 6;
										}
										else
										{
											bool flag9 = Menu.CurrentCategory == Menu.Category.Global;
											if (flag9)
											{
												result = 7;
											}
											else
											{
												bool flag10 = Menu.CurrentCategory == Menu.Category.Mode;
												if (flag10)
												{
													result = 8;
												}
												else
												{
													bool flag11 = Menu.CurrentCategory == Menu.Category.MenuSettings;
													if (flag11)
													{
														result = 9;
													}
													else
													{
														bool flag12 = Menu.CurrentCategory == Menu.Category.VRRigSettings;
														if (flag12)
														{
															result = 10;
														}
														else
														{
															bool flag13 = Menu.CurrentCategory == Menu.Category.MovementSettings;
															if (flag13)
															{
																result = 11;
															}
															else
															{
																bool flag14 = Menu.CurrentCategory == Menu.Category.VisualSettings;
																if (flag14)
																{
																	result = 12;
																}
																else
																{
																	bool flag15 = Menu.CurrentCategory == Menu.Category.Plugin;
																	if (flag15)
																	{
																		result = 13;
																	}
																	else
																	{
																		result = Menu.PageIndex;
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
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007234
		public static Button GetButton(string name)
		{
			foreach (Button button in Menu.GetButtonInfoByPage(Menu.CurrentCategory))
			{
				bool flag = button.Name.Contains(name) || button.Name == name;
				if (flag)
				{
					return button;
				}
			}
			return null;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000072B4
		public static Button GetButton(string name, Menu.Category cat)
		{
			foreach (Button button in Menu.GetButtonInfoByPage(cat))
			{
				bool flag = button.Name.Contains(name) || button.Name == name;
				if (flag)
				{
					return button;
				}
			}
			return null;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007330
		public static void PositionMenu(Menu.PositionType type)
		{
			if (type != Menu.PositionType.Hand)
			{
				if (type == Menu.PositionType.Computer)
				{
					bool flag = Menu.StaticCameraObject != null;
					if (flag)
					{
						Menu.StaticCameraObject.transform.position = Menu.backgroundPos;
						Menu.StaticCameraObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
						Menu.MenuObj.transform.parent = Menu.StaticCameraObject.transform;
						Vector3 position = Menu.StaticCameraObject.transform.position;
						Quaternion rotation = Menu.StaticCameraObject.transform.rotation;
						float num = 0.65f;
						Vector3 position2 = position + rotation * Vector3.forward * num;
						Menu.MenuObj.transform.position = position2;
						Vector3 vector = position - Menu.MenuObj.transform.position;
						Menu.MenuObj.transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
						Menu.MenuObj.transform.Rotate(Vector3.up, -90f);
						Menu.MenuObj.transform.Rotate(Vector3.right, -90f);
					}
				}
			}
			else
			{
				Menu.MenuObj.transform.position = Menu.__instance.leftControllerTransform.position;
				Menu.MenuObj.transform.rotation = Menu.__instance.leftControllerTransform.rotation;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000074BC
		public static void CreateClicker(ref GameObject clickerObj, Transform parentTransform)
		{
			bool flag = clickerObj == null;
			if (flag)
			{
				clickerObj = GameObject.CreatePrimitive(0);
				clickerObj.transform.parent = parentTransform;
				clickerObj.GetComponent<Renderer>().material.color = Menu.BgColor1;
				clickerObj.transform.localPosition = new Vector3(0f, -0.1f, 0f) * Menu.__instance.scale;
				clickerObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
				clickerObj.GetComponent<SphereCollider>().isTrigger = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000756C
		public static void AddRigidbodyToMenu()
		{
			bool flag = Menu.currentMenuRigidbody == null && Menu.MenuObj != null;
			if (flag)
			{
				Menu.currentMenuRigidbody = Menu.MenuObj.GetComponent<Rigidbody>();
				bool flag2 = Menu.currentMenuRigidbody == null;
				if (flag2)
				{
					Menu.currentMenuRigidbody = Menu.MenuObj.AddComponent<Rigidbody>();
				}
				Menu.currentMenuRigidbody.useGravity = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000075D8
		public static Vector3 GetControllerVelocity(bool leftHand)
		{
			Vector3 averageVelocity;
			if (leftHand)
			{
				averageVelocity = Menu.__instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0f, false);
			}
			else
			{
				averageVelocity = Menu.__instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0f, false);
			}
			return averageVelocity;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007620
		public static void RefreshMenu()
		{
			bool flag = Menu.MenuObj != null;
			if (flag)
			{
				Object.Destroy(Menu.MenuObj);
				Menu.MenuObj = null;
				Menu.DrawFrame();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007658
		public static List<Button> GetButtonInfoByPage(Menu.Category page)
		{
			return Buttons.List[Menu.GetCategoryIndex()];
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000767C
		private static int GetTotalPages(Menu.Category page)
		{
			return (Menu.GetButtonInfoByPage(page).Count + Menu.ButtonsPerPage - 1) / Menu.ButtonsPerPage;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000076A7
		public static void ReturnBackHome()
		{
			Menu.CurrentCategory = Menu.Category.Base;
			Menu.CurrentPage = 0;
			Menu.PageIndex = 0;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000076C4
		public static void ChangePage(Menu.Category category)
		{
			Menu.CurrentPage = 0;
			bool flag = category == Menu.Category.Settings;
			if (flag)
			{
				Settings.SaveConfig();
			}
			bool flag2 = category == Menu.Category.Players;
			if (flag2)
			{
				Menu.StartPlayers();
			}
			Menu.CurrentCategory = category;
			Menu.PageIndex = Menu.GetCategoryIndex();
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000770C
		public static void StartPlayers()
		{
			Menu.CurrentPage = 0;
			Buttons.List[13].Clear();
			Buttons.List[13].Add(new Button("Back", false, delegate()
			{
				Menu.ChangePage(Menu.Category.Base);
			}, null, null, false));
			Player[] playerListOthers = PhotonNetwork.PlayerListOthers;
			for (int i = 0; i < playerListOthers.Length; i++)
			{
				Player p = playerListOthers[i];
				Buttons.List[13].Add(new Button(p.NickName, false, delegate()
				{
					Menu.OpenPlayer(p);
				}, null, null, false));
			}
			Menu.selectedPlayerCate = null;
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig && vrrig != null;
				if (flag)
				{
					vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007850
		public static void OpenPlayer(Player p)
		{
			Menu.CurrentPage = 0;
			Buttons.List[13].Clear();
			Buttons.List[13].Add(new Button("Back", true, delegate()
			{
				Menu.StartPlayers();
			}, null, null, false));
			Menu.selectedPlayerCate = p;
			VRRig rigFromPlayer = RigShit.GetRigFromPlayer(p);
			bool flag = !rigFromPlayer.isOfflineVRRig && rigFromPlayer != null;
			if (flag)
			{
				rigFromPlayer.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
				rigFromPlayer.mainSkin.material.color = Color.red;
			}
			Menu.AddPlayerButtons(p);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007914
		public static void AddPlayerButtons(Player p)
		{
			Buttons.List[13].Add(new Button("Lag " + p.NickName, true, delegate()
			{
				Overpowered.LagPlayer(RigShit.GetRigFromPlayer(p));
			}, null, null, false));
			Buttons.List[13].Add(new Button("Anti Leave " + p.NickName, false, delegate()
			{
				Overpowered.AntiLeavePlayer(p);
			}, null, null, false));
			Buttons.List[13].Add(new Button("Isolate " + p.NickName, false, delegate()
			{
				Overpowered.IsolatePlayer(true, p);
			}, null, null, false));
			Buttons.List[13].Add(new Button("Isolate Others " + p.NickName, false, delegate()
			{
				Overpowered.IsolatePlayer(false, p);
			}, null, null, false));
			Buttons.List[13].Add(new Button("Ghost " + p.NickName, false, delegate()
			{
				Overpowered.GhostPlayer(true, p);
			}, null, null, false));
			Buttons.List[13].Add(new Button("Crash " + p.NickName, true, delegate()
			{
				Overpowered.GhostPlayer(false, p);
			}, null, null, false));
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007A98
		public static void DestroyMenu()
		{
			bool flag = !Menu.InPcCondition;
			if (flag)
			{
				bool flag2 = Menu.MenuObj != null;
				if (flag2)
				{
					bool dropMenu = Settings.DropMenu;
					if (dropMenu)
					{
						Object.Destroy(Menu.MenuObj, 3f);
					}
					else
					{
						Object.Destroy(Menu.MenuObj);
					}
					Menu.MenuObj = null;
				}
				bool flag3 = Menu.PointerObject != null;
				if (flag3)
				{
					Object.Destroy(Menu.PointerObject);
					Menu.PointerObject = null;
				}
				Menu.currentMenuRigidbody = null;
			}
			else
			{
				bool flag4 = Menu.MenuObj != null;
				if (flag4)
				{
					Object.Destroy(Menu.MenuObj);
					Menu.MenuObj = null;
				}
				Menu.currentMenuRigidbody = null;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007B48
		private static void NavigatePage(bool forward)
		{
			int totalPages = Menu.GetTotalPages(Menu.CurrentCategory);
			int num = totalPages - 1;
			Menu.CurrentPage += (forward ? 1 : -1);
			bool flag = Menu.CurrentPage < 0;
			if (flag)
			{
				Menu.CurrentPage = num;
			}
			else
			{
				bool flag2 = Menu.CurrentPage > num;
				if (flag2)
				{
					Menu.CurrentPage = 0;
				}
			}
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007BA3
		public static IEnumerator GrowCoroutine()
		{
			Vector3 target = Vector3.zero;
			bool pad = Settings.Pad;
			if (pad)
			{
				bool stretchy = Settings.stretchy;
				if (stretchy)
				{
					target = new Vector3(0.1f, 0.35f, 0.35f);
				}
				else
				{
					target = new Vector3(0.1f, 0.3f, 0.4f);
				}
			}
			else
			{
				bool flag = !Settings.stretchy;
				if (flag)
				{
					target = new Vector3(0.1f, 0.35f, 0.45f);
				}
				else
				{
					target = new Vector3(0.1f, 0.35f, 0.37f);
				}
			}
			float factor = 1.3f;
			Vector3 ot = target * factor;
			float time = 0f;
			float factor1duration = 0.07f;
			while (time < factor1duration)
			{
				Menu.MenuObj.transform.localScale = Vector3.Lerp(Vector3.zero, ot, time / factor1duration);
				time += Time.deltaTime;
				yield return null;
			}
			Menu.MenuObj.transform.localScale = ot;
			time = 0f;
			float factor2duration = 0.04f;
			while (time < factor2duration)
			{
				Menu.MenuObj.transform.localScale = Vector3.Lerp(ot, target, time / factor2duration);
				time += Time.deltaTime;
				yield return null;
			}
			Menu.MenuObj.transform.localScale = target;
			AssetLoader.PlayClick("menuopen.wav");
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007BAB
		public static IEnumerator ShrinkCoroutine()
		{
			Transform menuTransform = Menu.MenuObj.transform;
			Menu.MenuObj = null;
			float pFactor = 1.2f;
			Vector3 currentScale = menuTransform.localScale;
			Vector3 nextScale = currentScale * pFactor;
			float time = 0f;
			float factor1duration = 0.05f;
			while (time < factor1duration)
			{
				menuTransform.localScale = Vector3.Lerp(currentScale, nextScale, time / factor1duration);
				time += Time.deltaTime;
				yield return null;
			}
			menuTransform.localScale = nextScale;
			time = 0f;
			float factor2duration = 0.07f;
			while (time < factor2duration)
			{
				menuTransform.localScale = Vector3.Lerp(nextScale, Vector3.zero, time / factor2duration);
				time += Time.deltaTime;
				yield return null;
			}
			menuTransform.localScale = Vector3.zero;
			Object.Destroy(menuTransform.gameObject);
			AssetLoader.PlayClick("menuclose.wav");
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007BB4
		public static void OutlineObj(GameObject toOut, bool force = false)
		{
			bool flag = !Settings.Outline && !force;
			if (!flag)
			{
				GameObject gameObject = GameObject.CreatePrimitive(3);
				Object.Destroy(gameObject.GetComponent<Rigidbody>());
				Object.Destroy(gameObject.GetComponent<BoxCollider>());
				gameObject.transform.parent = Menu.MenuObj.transform;
				gameObject.transform.rotation = toOut.transform.rotation;
				gameObject.transform.localRotation = toOut.transform.localRotation;
				gameObject.transform.localPosition = toOut.transform.localPosition;
				gameObject.transform.localScale = toOut.transform.localScale + new Vector3(-0.01f, 0.01f, 0.0075f);
				gameObject.GetComponent<Renderer>().material.color = Menu.OutlineColor;
				bool rounding = Settings.Rounding;
				if (rounding)
				{
					Menu.RoundObj(gameObject);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00007CB4
		public static void RoundObj(GameObject toRound)
		{
			bool flag = !Settings.Rounding;
			if (!flag)
			{
				float num = 0.02f;
				Renderer component = toRound.GetComponent<Renderer>();
				GameObject gameObject = GameObject.CreatePrimitive(3);
				gameObject.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject.GetComponent<Collider>());
				gameObject.transform.parent = Menu.MenuObj.transform;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = toRound.transform.localPosition;
				gameObject.transform.localScale = toRound.transform.localScale + new Vector3(0f, num * -2.55f, 0f);
				GameObject gameObject2 = GameObject.CreatePrimitive(3);
				gameObject2.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject2.GetComponent<Collider>());
				gameObject2.transform.parent = Menu.MenuObj.transform;
				gameObject2.transform.localRotation = Quaternion.identity;
				gameObject2.transform.localPosition = toRound.transform.localPosition;
				gameObject2.transform.localScale = toRound.transform.localScale + new Vector3(0f, 0f, -num * 2f);
				GameObject gameObject3 = GameObject.CreatePrimitive(2);
				gameObject3.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject3.GetComponent<Collider>());
				gameObject3.transform.parent = Menu.MenuObj.transform;
				gameObject3.transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
				gameObject3.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, toRound.transform.localScale.y / 2f - num * 1.275f, toRound.transform.localScale.z / 2f - num);
				gameObject3.transform.localScale = new Vector3(num * 2.55f, toRound.transform.localScale.x / 2f, num * 2f);
				GameObject gameObject4 = GameObject.CreatePrimitive(2);
				gameObject4.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject4.GetComponent<Collider>());
				gameObject4.transform.parent = Menu.MenuObj.transform;
				gameObject4.transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
				gameObject4.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, -(toRound.transform.localScale.y / 2f) + num * 1.275f, toRound.transform.localScale.z / 2f - num);
				gameObject4.transform.localScale = new Vector3(num * 2.55f, toRound.transform.localScale.x / 2f, num * 2f);
				GameObject gameObject5 = GameObject.CreatePrimitive(2);
				gameObject5.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject5.GetComponent<Collider>());
				gameObject5.transform.parent = Menu.MenuObj.transform;
				gameObject5.transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
				gameObject5.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, toRound.transform.localScale.y / 2f - num * 1.275f, -(toRound.transform.localScale.z / 2f) + num);
				gameObject5.transform.localScale = new Vector3(num * 2.55f, toRound.transform.localScale.x / 2f, num * 2f);
				GameObject gameObject6 = GameObject.CreatePrimitive(2);
				gameObject6.GetComponent<Renderer>().enabled = component.enabled;
				Object.Destroy(gameObject6.GetComponent<Collider>());
				gameObject6.transform.parent = Menu.MenuObj.transform;
				gameObject6.transform.localRotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);
				gameObject6.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, -(toRound.transform.localScale.y / 2f) + num * 1.275f, -(toRound.transform.localScale.z / 2f) + num);
				gameObject6.transform.localScale = new Vector3(num * 2.55f, toRound.transform.localScale.x / 2f, num * 2f);
				GameObject[] array = new GameObject[]
				{
					gameObject,
					gameObject2,
					gameObject3,
					gameObject4,
					gameObject5,
					gameObject6
				};
				foreach (GameObject gameObject7 in array)
				{
					ClampColor clampColor = gameObject7.AddComponent<ClampColor>();
					clampColor.targetRenderer = component;
					clampColor.Start();
				}
				component.enabled = false;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000826C
		public static void CreateButton(Button identifier, float offset)
		{
			bool flag = identifier == null;
			if (!flag)
			{
				bool flag2 = !Settings.Pad;
				if (flag2)
				{
					Menu.ButtonObject = GameObject.CreatePrimitive(3);
					Menu.ButtonObject.transform.parent = Menu.MenuObj.transform;
					Menu.ButtonObject.transform.rotation = Quaternion.identity;
					Menu.ButtonObject.transform.localScale = new Vector3(0.09f, 0.76f, 0.08f);
					Menu.ButtonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.43f - offset);
					Object.Destroy(Menu.ButtonObject.GetComponent<Rigidbody>());
					Menu.ButtonObject.GetComponent<BoxCollider>().isTrigger = true;
					Menu.ButtonObject.AddComponent<ButtonCollider>().ButtonIdentifier = identifier;
					bool enabled = identifier.Enabled;
					if (enabled)
					{
						int num = (int)(Menu.BgColor1.r / 2);
						int num2 = (int)(Menu.BgColor1.g / 2);
						int num3 = (int)(Menu.BgColor1.b / 2);
						Menu.ButtonObject.GetComponent<Renderer>().material.color = new Color32((byte)num, (byte)num2, (byte)num3, byte.MaxValue);
					}
					else
					{
						Menu.ButtonObject.GetComponent<Renderer>().material.color = Menu.BgColor2;
					}
					bool beachTheme = Settings.BeachTheme;
					if (beachTheme)
					{
						GradientColorKey[] array = new GradientColorKey[3];
						array[0].color = new Color32(105, 211, 216, byte.MaxValue);
						array[0].time = 0f;
						array[1].color = new Color32(211, 133, 233, byte.MaxValue);
						array[1].time = 0.5f;
						array[2].color = new Color32(105, 211, 216, byte.MaxValue);
						array[2].time = 1f;
						ColorChanger colorChanger = Menu.ButtonObject.AddComponent<ColorChanger>();
						colorChanger.colors = new Gradient
						{
							colorKeys = array
						};
						colorChanger.Start();
					}
					bool rgbtheme = Settings.RGBTheme;
					if (rgbtheme)
					{
						GradientColorKey[] array2 = new GradientColorKey[5];
						array2[0].color = new Color32(203, 49, 49, byte.MaxValue);
						array2[0].time = 0f;
						array2[1].color = new Color32(121, 203, 49, byte.MaxValue);
						array2[1].time = 0.25f;
						array2[2].color = new Color32(49, 108, 203, byte.MaxValue);
						array2[2].time = 0.5f;
						array2[3].color = new Color32(121, 203, 49, byte.MaxValue);
						array2[3].time = 0.75f;
						array2[4].color = new Color32(203, 49, 49, byte.MaxValue);
						array2[4].time = 1f;
						ColorChanger colorChanger2 = Menu.ButtonObject.AddComponent<ColorChanger>();
						colorChanger2.colors = new Gradient
						{
							colorKeys = array2
						};
						colorChanger2.cycleDuration = 7f;
						colorChanger2.Start();
					}
					Menu.RoundObj(Menu.ButtonObject);
					Menu.OutlineObj(Menu.ButtonObject, false);
					Text text = new GameObject
					{
						transform = 
						{
							parent = Menu.CanvasObject.transform
						}
					}.AddComponent<Text>();
					text.font = Settings.currentFont;
					text.fontSize = 1;
					text.alignment = 4;
					text.resizeTextForBestFit = true;
					text.resizeTextMinSize = 0;
					text.text = identifier.Name;
					text.color = Menu.TextColor;
					RectTransform component = text.GetComponent<RectTransform>();
					component.localPosition = Vector3.zero;
					component.sizeDelta = new Vector2(0.2f, 0.03f);
					component.localPosition = new Vector3(0.064f, 0f, 0.196f - offset / 2.23f);
					component.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
				}
				else
				{
					Menu.ButtonObject = GameObject.CreatePrimitive(3);
					Menu.ButtonObject.transform.parent = Menu.MenuObj.transform;
					Menu.ButtonObject.transform.rotation = Quaternion.identity;
					Menu.ButtonObject.transform.localScale = new Vector3(0.09f, 0.76f, 0.08f);
					Menu.ButtonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.24f - offset);
					Object.Destroy(Menu.ButtonObject.GetComponent<Rigidbody>());
					Menu.ButtonObject.GetComponent<BoxCollider>().isTrigger = true;
					Menu.ButtonObject.AddComponent<ButtonCollider>().ButtonIdentifier = identifier;
					bool enabled2 = identifier.Enabled;
					if (enabled2)
					{
						int num4 = (int)(Menu.BgColor1.r / 2);
						int num5 = (int)(Menu.BgColor1.g / 2);
						int num6 = (int)(Menu.BgColor1.b / 2);
						Menu.ButtonObject.GetComponent<Renderer>().material.color = new Color32((byte)num4, (byte)num5, (byte)num6, byte.MaxValue);
					}
					else
					{
						Menu.ButtonObject.GetComponent<Renderer>().material.color = Menu.BgColor2;
					}
					bool beachTheme2 = Settings.BeachTheme;
					if (beachTheme2)
					{
						GradientColorKey[] array3 = new GradientColorKey[3];
						array3[0].color = new Color32(105, 211, 216, byte.MaxValue);
						array3[0].time = 0f;
						array3[1].color = new Color32(211, 133, 233, byte.MaxValue);
						array3[1].time = 0.5f;
						array3[2].color = new Color32(105, 211, 216, byte.MaxValue);
						array3[2].time = 1f;
						ColorChanger colorChanger3 = Menu.ButtonObject.AddComponent<ColorChanger>();
						colorChanger3.colors = new Gradient
						{
							colorKeys = array3
						};
						colorChanger3.Start();
					}
					bool rgbtheme2 = Settings.RGBTheme;
					if (rgbtheme2)
					{
						GradientColorKey[] array4 = new GradientColorKey[5];
						array4[0].color = new Color32(203, 49, 49, byte.MaxValue);
						array4[0].time = 0f;
						array4[1].color = new Color32(121, 203, 49, byte.MaxValue);
						array4[1].time = 0.25f;
						array4[2].color = new Color32(49, 108, 203, byte.MaxValue);
						array4[2].time = 0.5f;
						array4[3].color = new Color32(121, 203, 49, byte.MaxValue);
						array4[3].time = 0.75f;
						array4[4].color = new Color32(203, 49, 49, byte.MaxValue);
						array4[4].time = 1f;
						ColorChanger colorChanger4 = Menu.ButtonObject.AddComponent<ColorChanger>();
						colorChanger4.colors = new Gradient
						{
							colorKeys = array4
						};
						colorChanger4.cycleDuration = 7f;
						colorChanger4.Start();
					}
					Menu.RoundObj(Menu.ButtonObject);
					Menu.OutlineObj(Menu.ButtonObject, false);
					Text text2 = new GameObject
					{
						transform = 
						{
							parent = Menu.CanvasObject.transform
						}
					}.AddComponent<Text>();
					text2.font = Settings.currentFont;
					text2.fontSize = 1;
					text2.alignment = 4;
					text2.resizeTextForBestFit = true;
					text2.resizeTextMinSize = 0;
					text2.text = identifier.Name;
					text2.color = Menu.TextColor;
					RectTransform component2 = text2.GetComponent<RectTransform>();
					component2.localPosition = Vector3.zero;
					component2.sizeDelta = new Vector2(0.2f, 0.03f);
					component2.localPosition = new Vector3(0.064f, 0f, 0.097f - offset / 2.5f);
					component2.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00008BB4
		public static void AddDisconnectButton()
		{
			bool flag = !Settings.Pad;
			if (flag)
			{
				Menu.ButtonObject = GameObject.CreatePrimitive(3);
				Menu.ButtonObject.transform.parent = Menu.MenuObj.transform;
				Menu.ButtonObject.transform.rotation = Quaternion.identity;
				Menu.ButtonObject.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
				Menu.ButtonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.47f);
				Menu.ButtonObject.name = "Disconnect b";
				Object.Destroy(Menu.ButtonObject.GetComponent<Rigidbody>());
				Menu.ButtonObject.GetComponent<BoxCollider>().isTrigger = true;
				Menu.ButtonObject.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("Disconnect", false, delegate()
				{
					PhotonNetwork.Disconnect();
				}, null, null, false);
				Menu.ButtonObject.GetComponent<Renderer>().material.color = Menu.BgColor2;
				bool beachTheme = Settings.BeachTheme;
				if (beachTheme)
				{
					GradientColorKey[] array = new GradientColorKey[3];
					array[0].color = new Color32(105, 211, 216, byte.MaxValue);
					array[0].time = 0f;
					array[1].color = new Color32(211, 133, 233, byte.MaxValue);
					array[1].time = 0.5f;
					array[2].color = new Color32(105, 211, 216, byte.MaxValue);
					array[2].time = 1f;
					ColorChanger colorChanger = Menu.ButtonObject.AddComponent<ColorChanger>();
					colorChanger.colors = new Gradient
					{
						colorKeys = array
					};
					colorChanger.Start();
				}
				bool rgbtheme = Settings.RGBTheme;
				if (rgbtheme)
				{
					GradientColorKey[] array2 = new GradientColorKey[5];
					array2[0].color = new Color32(203, 49, 49, byte.MaxValue);
					array2[0].time = 0f;
					array2[1].color = new Color32(121, 203, 49, byte.MaxValue);
					array2[1].time = 0.25f;
					array2[2].color = new Color32(49, 108, 203, byte.MaxValue);
					array2[2].time = 0.5f;
					array2[3].color = new Color32(121, 203, 49, byte.MaxValue);
					array2[3].time = 0.75f;
					array2[4].color = new Color32(203, 49, 49, byte.MaxValue);
					array2[4].time = 1f;
					ColorChanger colorChanger2 = Menu.ButtonObject.AddComponent<ColorChanger>();
					colorChanger2.colors = new Gradient
					{
						colorKeys = array2
					};
					colorChanger2.cycleDuration = 7f;
					colorChanger2.Start();
				}
				Menu.RoundObj(Menu.ButtonObject);
				Menu.OutlineObj(Menu.ButtonObject, false);
				Text text = new GameObject
				{
					transform = 
					{
						parent = Menu.CanvasObject.transform
					}
				}.AddComponent<Text>();
				text.font = Settings.currentFont;
				text.fontSize = 1;
				text.alignment = 4;
				text.resizeTextForBestFit = true;
				text.resizeTextMinSize = 0;
				text.text = "Disconnect";
				text.color = Color.white;
				RectTransform component = text.GetComponent<RectTransform>();
				component.localPosition = Vector3.zero;
				component.sizeDelta = new Vector2(0.2f, 0.03f);
				component.localPosition = new Vector3(0.064f, 0f, 0.215f);
				component.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			}
			else
			{
				Menu.ButtonObject = GameObject.CreatePrimitive(3);
				Menu.ButtonObject.transform.parent = Menu.MenuObj.transform;
				Menu.ButtonObject.transform.rotation = Quaternion.identity;
				Menu.ButtonObject.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
				Menu.ButtonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.5f);
				Menu.ButtonObject.name = "Disconnect b";
				Object.Destroy(Menu.ButtonObject.GetComponent<Rigidbody>());
				Menu.ButtonObject.GetComponent<BoxCollider>().isTrigger = true;
				Menu.ButtonObject.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("Disconnect", false, delegate()
				{
					PhotonNetwork.Disconnect();
				}, null, null, false);
				Menu.ButtonObject.GetComponent<Renderer>().material.color = Menu.BgColor2;
				bool beachTheme2 = Settings.BeachTheme;
				if (beachTheme2)
				{
					GradientColorKey[] array3 = new GradientColorKey[3];
					array3[0].color = new Color32(105, 211, 216, byte.MaxValue);
					array3[0].time = 0f;
					array3[1].color = new Color32(211, 133, 233, byte.MaxValue);
					array3[1].time = 0.5f;
					array3[2].color = new Color32(105, 211, 216, byte.MaxValue);
					array3[2].time = 1f;
					ColorChanger colorChanger3 = Menu.ButtonObject.AddComponent<ColorChanger>();
					colorChanger3.colors = new Gradient
					{
						colorKeys = array3
					};
					colorChanger3.Start();
				}
				bool rgbtheme2 = Settings.RGBTheme;
				if (rgbtheme2)
				{
					GradientColorKey[] array4 = new GradientColorKey[5];
					array4[0].color = new Color32(203, 49, 49, byte.MaxValue);
					array4[0].time = 0f;
					array4[1].color = new Color32(121, 203, 49, byte.MaxValue);
					array4[1].time = 0.25f;
					array4[2].color = new Color32(49, 108, 203, byte.MaxValue);
					array4[2].time = 0.5f;
					array4[3].color = new Color32(121, 203, 49, byte.MaxValue);
					array4[3].time = 0.75f;
					array4[4].color = new Color32(203, 49, 49, byte.MaxValue);
					array4[4].time = 1f;
					ColorChanger colorChanger4 = Menu.ButtonObject.AddComponent<ColorChanger>();
					colorChanger4.colors = new Gradient
					{
						colorKeys = array4
					};
					colorChanger4.cycleDuration = 7f;
					colorChanger4.Start();
				}
				Menu.RoundObj(Menu.ButtonObject);
				Menu.OutlineObj(Menu.ButtonObject, false);
				Text text2 = new GameObject
				{
					transform = 
					{
						parent = Menu.CanvasObject.transform
					}
				}.AddComponent<Text>();
				text2.font = Settings.currentFont;
				text2.fontSize = 1;
				text2.alignment = 4;
				text2.resizeTextForBestFit = true;
				text2.resizeTextMinSize = 0;
				text2.text = "Disconnect";
				text2.color = Color.white;
				RectTransform component2 = text2.GetComponent<RectTransform>();
				component2.localPosition = Vector3.zero;
				component2.sizeDelta = new Vector2(0.2f, 0.03f);
				component2.localPosition = new Vector3(0.064f, 0f, 0.2f);
				component2.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00009470
		public static void AddPageButtons()
		{
			float num = 0f;
			bool flag = !Settings.Pad;
			if (flag)
			{
				bool flag2 = Settings.PageButtons || Menu.PCMenuOpen;
				if (flag2)
				{
					string text = "<";
					string text2 = ">";
					GameObject gameObject = GameObject.CreatePrimitive(3);
					Object.Destroy(gameObject.GetComponent<Rigidbody>());
					gameObject.GetComponent<BoxCollider>().isTrigger = true;
					gameObject.transform.SetParent(Menu.MenuObj.transform, false);
					gameObject.transform.rotation = Quaternion.identity;
					gameObject.transform.localScale = Menu.pageSize;
					gameObject.transform.localPosition = Menu.pagePos;
					gameObject.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("PreviousPage", false, null, null, null, false);
					gameObject.GetComponent<Renderer>().material.color = Menu.BgColor2;
					bool beachTheme = Settings.BeachTheme;
					if (beachTheme)
					{
						GradientColorKey[] array = new GradientColorKey[3];
						array[0].color = new Color32(105, 211, 216, byte.MaxValue);
						array[0].time = 0f;
						array[1].color = new Color32(211, 133, 233, byte.MaxValue);
						array[1].time = 0.5f;
						array[2].color = new Color32(105, 211, 216, byte.MaxValue);
						array[2].time = 1f;
						ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
						colorChanger.colors = new Gradient
						{
							colorKeys = array
						};
						colorChanger.Start();
					}
					bool rgbtheme = Settings.RGBTheme;
					if (rgbtheme)
					{
						GradientColorKey[] array2 = new GradientColorKey[5];
						array2[0].color = new Color32(203, 49, 49, byte.MaxValue);
						array2[0].time = 0f;
						array2[1].color = new Color32(121, 203, 49, byte.MaxValue);
						array2[1].time = 0.25f;
						array2[2].color = new Color32(49, 108, 203, byte.MaxValue);
						array2[2].time = 0.5f;
						array2[3].color = new Color32(121, 203, 49, byte.MaxValue);
						array2[3].time = 0.75f;
						array2[4].color = new Color32(203, 49, 49, byte.MaxValue);
						array2[4].time = 1f;
						ColorChanger colorChanger2 = gameObject.AddComponent<ColorChanger>();
						colorChanger2.colors = new Gradient
						{
							colorKeys = array2
						};
						colorChanger2.cycleDuration = 7f;
						colorChanger2.Start();
					}
					Text text3 = new GameObject().AddComponent<Text>();
					text3.transform.SetParent(Menu.CanvasObject.transform);
					text3.font = Settings.currentFont;
					text3.text = text;
					text3.fontSize = 1;
					text3.alignment = 4;
					text3.resizeTextForBestFit = true;
					text3.resizeTextMinSize = 0;
					RectTransform component = text3.GetComponent<RectTransform>();
					component.localPosition = Vector3.zero;
					component.sizeDelta = new Vector2(0.2f, 0.03f);
					component.localPosition = Menu.pageTextNPos;
					component.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					num = 0.13f;
					Menu.OutlineObj(gameObject, false);
					Menu.RoundObj(gameObject);
					GameObject gameObject2 = GameObject.CreatePrimitive(3);
					Object.Destroy(gameObject2.GetComponent<Rigidbody>());
					gameObject2.GetComponent<BoxCollider>().isTrigger = true;
					gameObject2.transform.parent = Menu.MenuObj.transform;
					gameObject2.transform.rotation = Quaternion.identity;
					gameObject2.transform.localScale = Menu.pageSize;
					gameObject2.transform.localPosition = Menu.pageNextPos;
					gameObject2.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("NextPage", false, null, null, null, false);
					gameObject2.GetComponent<Renderer>().material.color = Menu.BgColor2;
					bool beachTheme2 = Settings.BeachTheme;
					if (beachTheme2)
					{
						GradientColorKey[] array3 = new GradientColorKey[3];
						array3[0].color = new Color32(105, 211, 216, byte.MaxValue);
						array3[0].time = 0f;
						array3[1].color = new Color32(211, 133, 233, byte.MaxValue);
						array3[1].time = 0.5f;
						array3[2].color = new Color32(105, 211, 216, byte.MaxValue);
						array3[2].time = 1f;
						ColorChanger colorChanger3 = gameObject2.AddComponent<ColorChanger>();
						colorChanger3.colors = new Gradient
						{
							colorKeys = array3
						};
						colorChanger3.Start();
					}
					bool rgbtheme2 = Settings.RGBTheme;
					if (rgbtheme2)
					{
						GradientColorKey[] array4 = new GradientColorKey[5];
						array4[0].color = new Color32(203, 49, 49, byte.MaxValue);
						array4[0].time = 0f;
						array4[1].color = new Color32(121, 203, 49, byte.MaxValue);
						array4[1].time = 0.25f;
						array4[2].color = new Color32(49, 108, 203, byte.MaxValue);
						array4[2].time = 0.5f;
						array4[3].color = new Color32(121, 203, 49, byte.MaxValue);
						array4[3].time = 0.75f;
						array4[4].color = new Color32(203, 49, 49, byte.MaxValue);
						array4[4].time = 1f;
						ColorChanger colorChanger4 = gameObject2.AddComponent<ColorChanger>();
						colorChanger4.colors = new Gradient
						{
							colorKeys = array4
						};
						colorChanger4.cycleDuration = 7f;
						colorChanger4.Start();
					}
					Text text4 = new GameObject().AddComponent<Text>();
					text4.transform.SetParent(Menu.CanvasObject.transform);
					text4.font = Settings.currentFont;
					text4.text = text2;
					text4.fontSize = 1;
					text4.alignment = 4;
					text4.resizeTextForBestFit = true;
					text4.resizeTextMinSize = 0;
					RectTransform component2 = text4.GetComponent<RectTransform>();
					component2.localPosition = Vector3.zero;
					component2.sizeDelta = new Vector2(0.2f, 0.03f);
					component2.localPosition = Menu.pageTextpos;
					component2.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					Menu.RoundObj(gameObject2);
					Menu.OutlineObj(gameObject2, false);
					bool homeButtons = Settings.HomeButtons;
					if (homeButtons)
					{
						GameObject gameObject3 = GameObject.CreatePrimitive(3);
						Object.Destroy(gameObject3.GetComponent<Rigidbody>());
						gameObject3.GetComponent<BoxCollider>().isTrigger = true;
						gameObject3.transform.parent = Menu.MenuObj.transform;
						gameObject3.transform.rotation = Quaternion.identity;
						gameObject3.transform.localScale = new Vector3(0.09f, 0.297f, 0.1026f);
						gameObject3.transform.localPosition = new Vector3(0.56f, -0.0045f, -0.6065f);
						gameObject3.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("HomeButton", false, null, null, null, false);
						gameObject3.GetComponent<Renderer>().material.color = Menu.BgColor2;
						bool beachTheme3 = Settings.BeachTheme;
						if (beachTheme3)
						{
							GradientColorKey[] array5 = new GradientColorKey[3];
							array5[0].color = new Color32(105, 211, 216, byte.MaxValue);
							array5[0].time = 0f;
							array5[1].color = new Color32(211, 133, 233, byte.MaxValue);
							array5[1].time = 0.5f;
							array5[2].color = new Color32(105, 211, 216, byte.MaxValue);
							array5[2].time = 1f;
							ColorChanger colorChanger5 = gameObject3.AddComponent<ColorChanger>();
							colorChanger5.colors = new Gradient
							{
								colorKeys = array5
							};
							colorChanger5.Start();
						}
						bool rgbtheme3 = Settings.RGBTheme;
						if (rgbtheme3)
						{
							GradientColorKey[] array6 = new GradientColorKey[5];
							array6[0].color = new Color32(203, 49, 49, byte.MaxValue);
							array6[0].time = 0f;
							array6[1].color = new Color32(121, 203, 49, byte.MaxValue);
							array6[1].time = 0.25f;
							array6[2].color = new Color32(49, 108, 203, byte.MaxValue);
							array6[2].time = 0.5f;
							array6[3].color = new Color32(121, 203, 49, byte.MaxValue);
							array6[3].time = 0.75f;
							array6[4].color = new Color32(203, 49, 49, byte.MaxValue);
							array6[4].time = 1f;
							ColorChanger colorChanger6 = gameObject3.AddComponent<ColorChanger>();
							colorChanger6.colors = new Gradient
							{
								colorKeys = array6
							};
							colorChanger6.cycleDuration = 7f;
							colorChanger6.Start();
						}
						Menu.RoundObj(gameObject3);
						Menu.OutlineObj(gameObject3, false);
					}
				}
			}
			bool pad = Settings.Pad;
			if (pad)
			{
				GameObject gameObject4 = GameObject.CreatePrimitive(3);
				Object.Destroy(gameObject4.GetComponent<Rigidbody>());
				gameObject4.GetComponent<BoxCollider>().isTrigger = true;
				gameObject4.transform.parent = Menu.MenuObj.transform;
				gameObject4.transform.rotation = Quaternion.identity;
				gameObject4.transform.localScale = new Vector3(0.09f, 0.76f, 0.08f);
				gameObject4.transform.localPosition = new Vector3(0.56f, 0f, 0.26f - num);
				gameObject4.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("NextPage", false, null, null, null, false);
				gameObject4.GetComponent<Renderer>().material.color = Menu.BgColor2;
				bool beachTheme4 = Settings.BeachTheme;
				if (beachTheme4)
				{
					GradientColorKey[] array7 = new GradientColorKey[3];
					array7[0].color = new Color32(105, 211, 216, byte.MaxValue);
					array7[0].time = 0f;
					array7[1].color = new Color32(211, 133, 233, byte.MaxValue);
					array7[1].time = 0.5f;
					array7[2].color = new Color32(105, 211, 216, byte.MaxValue);
					array7[2].time = 1f;
					ColorChanger colorChanger7 = gameObject4.AddComponent<ColorChanger>();
					colorChanger7.colors = new Gradient
					{
						colorKeys = array7
					};
					colorChanger7.Start();
				}
				bool rgbtheme4 = Settings.RGBTheme;
				if (rgbtheme4)
				{
					GradientColorKey[] array8 = new GradientColorKey[5];
					array8[0].color = new Color32(203, 49, 49, byte.MaxValue);
					array8[0].time = 0f;
					array8[1].color = new Color32(121, 203, 49, byte.MaxValue);
					array8[1].time = 0.25f;
					array8[2].color = new Color32(49, 108, 203, byte.MaxValue);
					array8[2].time = 0.5f;
					array8[3].color = new Color32(121, 203, 49, byte.MaxValue);
					array8[3].time = 0.75f;
					array8[4].color = new Color32(203, 49, 49, byte.MaxValue);
					array8[4].time = 1f;
					ColorChanger colorChanger8 = gameObject4.AddComponent<ColorChanger>();
					colorChanger8.colors = new Gradient
					{
						colorKeys = array8
					};
					colorChanger8.cycleDuration = 7f;
					colorChanger8.Start();
				}
				Text text5 = new GameObject().AddComponent<Text>();
				text5.transform.parent = Menu.CanvasObject.transform;
				text5.font = Settings.currentFont;
				text5.text = " << Prev";
				text5.fontSize = 1;
				text5.alignment = 4;
				text5.resizeTextForBestFit = true;
				text5.resizeTextMinSize = 0;
				text5.color = Menu.TextColor;
				RectTransform component3 = text5.GetComponent<RectTransform>();
				component3.localPosition = Vector3.zero;
				component3.sizeDelta = new Vector2(0.2f, 0.03f);
				component3.localPosition = new Vector3(0.064f, 0f, 0.111f - num / 2.25f);
				component3.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
				num = 0.13f;
				Menu.OutlineObj(gameObject4, false);
				Menu.RoundObj(gameObject4);
				GameObject gameObject5 = GameObject.CreatePrimitive(3);
				Object.Destroy(gameObject5.GetComponent<Rigidbody>());
				gameObject5.GetComponent<BoxCollider>().isTrigger = true;
				gameObject5.transform.parent = Menu.MenuObj.transform;
				gameObject5.transform.rotation = Quaternion.identity;
				gameObject5.transform.localScale = new Vector3(0.09f, 0.76f, 0.08f);
				gameObject5.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num);
				gameObject5.AddComponent<ButtonCollider>().ButtonIdentifier = new Button("PreviousPage", false, null, null, null, false);
				gameObject5.GetComponent<Renderer>().material.color = Menu.BgColor2;
				bool beachTheme5 = Settings.BeachTheme;
				if (beachTheme5)
				{
					GradientColorKey[] array9 = new GradientColorKey[3];
					array9[0].color = new Color32(105, 211, 216, byte.MaxValue);
					array9[0].time = 0f;
					array9[1].color = new Color32(211, 133, 233, byte.MaxValue);
					array9[1].time = 0.5f;
					array9[2].color = new Color32(105, 211, 216, byte.MaxValue);
					array9[2].time = 1f;
					ColorChanger colorChanger9 = gameObject5.AddComponent<ColorChanger>();
					colorChanger9.colors = new Gradient
					{
						colorKeys = array9
					};
					colorChanger9.Start();
				}
				bool rgbtheme5 = Settings.RGBTheme;
				if (rgbtheme5)
				{
					GradientColorKey[] array10 = new GradientColorKey[5];
					array10[0].color = new Color32(203, 49, 49, byte.MaxValue);
					array10[0].time = 0f;
					array10[1].color = new Color32(121, 203, 49, byte.MaxValue);
					array10[1].time = 0.25f;
					array10[2].color = new Color32(49, 108, 203, byte.MaxValue);
					array10[2].time = 0.5f;
					array10[3].color = new Color32(121, 203, 49, byte.MaxValue);
					array10[3].time = 0.75f;
					array10[4].color = new Color32(203, 49, 49, byte.MaxValue);
					array10[4].time = 1f;
					ColorChanger colorChanger10 = gameObject5.AddComponent<ColorChanger>();
					colorChanger10.colors = new Gradient
					{
						colorKeys = array10
					};
					colorChanger10.cycleDuration = 7f;
					colorChanger10.Start();
				}
				Text text6 = new GameObject().AddComponent<Text>();
				text6.transform.parent = Menu.CanvasObject.transform;
				text6.font = Settings.currentFont;
				text6.text = "Next >>";
				text6.fontSize = 1;
				text6.alignment = 4;
				text6.resizeTextForBestFit = true;
				text6.resizeTextMinSize = 0;
				text6.color = Menu.TextColor;
				RectTransform component4 = text6.GetComponent<RectTransform>();
				component4.localPosition = Vector3.zero;
				component4.sizeDelta = new Vector2(0.2f, 0.03f);
				component4.localPosition = new Vector3(0.064f, 0f, 0.111f - num / 2.55f);
				component4.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
				Menu.RoundObj(gameObject5);
				Menu.OutlineObj(gameObject5, false);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000A794
		public static void DrawFrame()
		{
			try
			{
				bool flag = !Settings.Pad;
				if (flag)
				{
					Menu.ButtonsPerPage = 8;
					Menu.MenuObj = GameObject.CreatePrimitive(3);
					Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.35f, 0.45f);
					Object.Destroy(Menu.MenuObj.GetComponent<Collider>());
					Object.Destroy(Menu.MenuObj.GetComponent<Renderer>());
					Object.Destroy(Menu.MenuObj.GetComponent<Rigidbody>());
					GameObject gameObject = GameObject.CreatePrimitive(3);
					gameObject.transform.parent = Menu.MenuObj.transform;
					gameObject.transform.rotation = Quaternion.identity;
					gameObject.transform.position = new Vector3(0.05f, 0f, 0f);
					gameObject.transform.localScale = new Vector3(0.1f, 0.86f, 0.9335f);
					gameObject.transform.localPosition = new Vector3(0.5f, 0f, -0.0465f);
					Object.Destroy(gameObject.GetComponent<BoxCollider>());
					Object.Destroy(gameObject.GetComponent<Rigidbody>());
					Menu.RoundObj(gameObject);
					gameObject.GetComponent<Renderer>().material.color = Menu.BgColor1;
					bool beachTheme = Settings.BeachTheme;
					if (beachTheme)
					{
						GradientColorKey[] array = new GradientColorKey[3];
						array[0].color = new Color32(95, 201, 206, byte.MaxValue);
						array[0].time = 0f;
						array[1].color = new Color32(201, 123, 223, byte.MaxValue);
						array[1].time = 0.5f;
						array[2].color = new Color32(95, 201, 206, byte.MaxValue);
						array[2].time = 1f;
						ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
						colorChanger.colors = new Gradient
						{
							colorKeys = array
						};
						colorChanger.Start();
					}
					bool rgbtheme = Settings.RGBTheme;
					if (rgbtheme)
					{
						GradientColorKey[] array2 = new GradientColorKey[5];
						array2[0].color = new Color32(193, 39, 39, byte.MaxValue);
						array2[0].time = 0f;
						array2[1].color = new Color32(111, 193, 39, byte.MaxValue);
						array2[1].time = 0.25f;
						array2[2].color = new Color32(39, 98, 193, byte.MaxValue);
						array2[2].time = 0.5f;
						array2[3].color = new Color32(111, 193, 39, byte.MaxValue);
						array2[3].time = 0.75f;
						array2[4].color = new Color32(193, 39, 39, byte.MaxValue);
						array2[4].time = 1f;
						ColorChanger colorChanger2 = gameObject.AddComponent<ColorChanger>();
						colorChanger2.colors = new Gradient
						{
							colorKeys = array2
						};
						colorChanger2.cycleDuration = 7f;
					}
					Menu.OutlineObj(gameObject, false);
					bool flag2 = Menu.selectedPlayerCate == null;
					if (flag2)
					{
						Settings.CycleBackgrounds(true);
					}
					Menu.CanvasObject = new GameObject();
					Menu.CanvasObject.transform.parent = Menu.MenuObj.transform;
					Canvas canvas = Menu.CanvasObject.AddComponent<Canvas>();
					CanvasScaler canvasScaler = Menu.CanvasObject.AddComponent<CanvasScaler>();
					Menu.CanvasObject.AddComponent<GraphicRaycaster>();
					canvas.renderMode = 2;
					canvasScaler.dynamicPixelsPerUnit = 2500f;
					bool tooltips = Settings.Tooltips;
					if (tooltips)
					{
						Text text = new GameObject
						{
							transform = 
							{
								parent = Menu.CanvasObject.transform
							}
						}.AddComponent<Text>();
						text.font = Settings.currentFont;
						text.fontStyle = 2;
						text.fontSize = 1;
						text.alignment = 4;
						text.resizeTextForBestFit = true;
						text.resizeTextMinSize = 0;
						bool flag3 = Menu.recentButton != null && Menu.recentButton.Enabled;
						if (flag3)
						{
							text.text = Menu.recentButton.ToolTip;
						}
						RectTransform component = text.GetComponent<RectTransform>();
						component.sizeDelta = new Vector2(0.2f, 0.04f);
						component.position = new Vector3(0.06f, 0f, -0.208f);
						component.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					}
					Text text2 = new GameObject
					{
						transform = 
						{
							parent = Menu.CanvasObject.transform
						}
					}.AddComponent<Text>();
					text2.font = Settings.currentFont;
					text2.fontSize = 1;
					text2.alignment = 4;
					text2.resizeTextForBestFit = true;
					text2.resizeTextMinSize = 0;
					text2.text = "Untitled Menu";
					RectTransform component2 = text2.GetComponent<RectTransform>();
					component2.sizeDelta = new Vector2(0.2f, 0.04f);
					component2.position = new Vector3(0.06f, 0f, 0.1695f);
					component2.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					bool counter = Settings.Counter;
					if (counter)
					{
						Menu.fps = new GameObject
						{
							transform = 
							{
								parent = Menu.CanvasObject.transform
							}
						}.AddComponent<Text>();
						Menu.fps.font = Settings.currentFont;
						Menu.fps.fontSize = 1;
						Menu.fps.alignment = 7;
						Menu.fps.resizeTextForBestFit = true;
						Menu.fps.resizeTextMinSize = 0;
						Menu.fps.text = string.Format("FPS : {0}", Mathf.RoundToInt(1f / Time.deltaTime));
						RectTransform component3 = Menu.fps.GetComponent<RectTransform>();
						component3.sizeDelta = new Vector2(0.2f, Menu.FPSSize);
						component3.position = Menu.FPSPos;
						component3.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					}
					else
					{
						Menu.fps = null;
					}
					bool arrayList = Settings.ArrayList;
					if (arrayList)
					{
						Text text3 = new GameObject
						{
							transform = 
							{
								parent = Menu.CanvasObject.transform
							}
						}.AddComponent<Text>();
						text3.font = Settings.currentFont;
						text3.fontSize = 1;
						text3.alignment = 0;
						text3.verticalOverflow = 1;
						text3.horizontalOverflow = 1;
						text3.resizeTextForBestFit = true;
						text3.resizeTextMinSize = 0;
						string text4 = "";
						List<string> list = new List<string>();
						foreach (List<Button> list2 in Buttons.List)
						{
							foreach (Button button in list2)
							{
								bool enabled = button.Enabled;
								if (enabled)
								{
									list.Add(button.Name.ToUpper());
								}
							}
						}
						list.Sort((string a, string b) => b.Length.CompareTo(a.Length));
						foreach (string str in list)
						{
							text4 = text4 + "• " + str + "\n";
						}
						text3.text = text4.TrimEnd(new char[]
						{
							'\n'
						});
						text3.color = Menu.TextColor;
						RectTransform component4 = text3.GetComponent<RectTransform>();
						component4.sizeDelta = new Vector2(2f, 2f);
						component4.localPosition = new Vector3(0.075f, -0.257f, 0.0895f);
						component4.localRotation = Quaternion.Euler(25.9f, 270f, 270f);
						component4.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					}
					bool disconnectButton = Settings.DisconnectButton;
					if (disconnectButton)
					{
						Menu.AddDisconnectButton();
					}
					bool pageButtons = Settings.PageButtons;
					if (pageButtons)
					{
						Menu.AddPageButtons();
					}
					Button[] array3 = Menu.GetButtonInfoByPage(Menu.CurrentCategory).Skip(Menu.CurrentPage * Menu.ButtonsPerPage).Take(Menu.ButtonsPerPage).ToArray<Button>();
					for (int i = 0; i < array3.Length; i++)
					{
						Menu.CreateButton(array3[i], (float)i * 0.09f + 0.17f);
					}
					bool stretchy = Settings.stretchy;
					if (stretchy)
					{
						Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.35f, 0.37f);
					}
					else
					{
						Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.35f, 0.45f);
					}
					bool flag4 = Menu.selectedPlayerCate != null;
					if (flag4)
					{
						bool flag5 = Menu.CamView != null;
						if (flag5)
						{
							Object.Destroy(Menu.CamView.gameObject);
						}
						else
						{
							GameObject gameObject2 = new GameObject("cam");
							Menu.CamView = gameObject2.AddComponent<Camera>();
							RenderTexture renderTexture = new RenderTexture(1024, 1024, 16);
							renderTexture.Create();
							renderTexture.filterMode = 1;
							Menu.CamView.targetTexture = renderTexture;
							GameObject gameObject3 = GameObject.CreatePrimitive(3);
							gameObject3.name = "targDisplay";
							gameObject3.transform.parent = Menu.MenuObj.transform;
							gameObject3.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
							gameObject3.transform.localScale = new Vector3(0.01f, 0.6f, 0.6f);
							bool flag6 = Settings.PageSIndex == 1;
							if (flag6)
							{
								gameObject3.transform.localPosition = new Vector3(0.05f, 1.1373f, 0f);
							}
							else
							{
								gameObject3.transform.localPosition = new Vector3(0.05f, 0.83f, 0f);
							}
							Renderer component5 = gameObject3.GetComponent<Renderer>();
							component5.material = new Material(Shader.Find("Unlit/Texture"));
							component5.material.mainTexture = renderTexture;
							Menu.OutlineObj(gameObject3, true);
							Object.Destroy(gameObject3.GetComponent<Collider>());
							Menu.TitleView = new GameObject();
							GameObject titleView = Menu.TitleView;
							titleView.transform.parent = Menu.CanvasObject.transform;
							Text text5 = titleView.AddComponent<Text>();
							text5.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
							text5.fontSize = 1;
							text5.fontStyle = 2;
							text5.alignment = 0;
							text5.resizeTextForBestFit = true;
							text5.resizeTextMinSize = 0;
							text5.text = "TARGET VIEW";
							RectTransform component6 = text5.GetComponent<RectTransform>();
							component6.sizeDelta = new Vector2(0.2f, 0.04f);
							bool flag7 = Settings.PageSIndex != 1;
							if (flag7)
							{
								component6.localPosition = new Vector3(0f, 0.29f, 0.158f);
							}
							else
							{
								component6.localPosition = new Vector3(0f, 0.4f, 0.158f);
							}
							component6.localRotation = Quaternion.Euler(0f, 270f, 270f);
						}
					}
				}
				else
				{
					Menu.ButtonsPerPage = 4;
					Menu.MenuObj = GameObject.CreatePrimitive(3);
					Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
					Object.Destroy(Menu.MenuObj.GetComponent<Collider>());
					Object.Destroy(Menu.MenuObj.GetComponent<Renderer>());
					Object.Destroy(Menu.MenuObj.GetComponent<Rigidbody>());
					GameObject gameObject4 = GameObject.CreatePrimitive(3);
					gameObject4.transform.parent = Menu.MenuObj.transform;
					gameObject4.transform.rotation = Quaternion.identity;
					gameObject4.transform.localScale = new Vector3(0.1f, 0.92f, 0.84f);
					gameObject4.transform.position = new Vector3(0.05f, 0f, 0f);
					Object.Destroy(gameObject4.GetComponent<BoxCollider>());
					Object.Destroy(gameObject4.GetComponent<Rigidbody>());
					Menu.RoundObj(gameObject4);
					gameObject4.GetComponent<Renderer>().material.color = Menu.BgColor1;
					bool beachTheme2 = Settings.BeachTheme;
					if (beachTheme2)
					{
						GradientColorKey[] array4 = new GradientColorKey[3];
						array4[0].color = new Color32(95, 201, 206, byte.MaxValue);
						array4[0].time = 0f;
						array4[1].color = new Color32(201, 123, 223, byte.MaxValue);
						array4[1].time = 0.5f;
						array4[2].color = new Color32(95, 201, 206, byte.MaxValue);
						array4[2].time = 1f;
						ColorChanger colorChanger3 = gameObject4.AddComponent<ColorChanger>();
						colorChanger3.colors = new Gradient
						{
							colorKeys = array4
						};
						colorChanger3.Start();
					}
					bool rgbtheme2 = Settings.RGBTheme;
					if (rgbtheme2)
					{
						GradientColorKey[] array5 = new GradientColorKey[5];
						array5[0].color = new Color32(193, 39, 39, byte.MaxValue);
						array5[0].time = 0f;
						array5[1].color = new Color32(111, 193, 39, byte.MaxValue);
						array5[1].time = 0.25f;
						array5[2].color = new Color32(39, 98, 193, byte.MaxValue);
						array5[2].time = 0.5f;
						array5[3].color = new Color32(111, 193, 39, byte.MaxValue);
						array5[3].time = 0.75f;
						array5[4].color = new Color32(193, 39, 39, byte.MaxValue);
						array5[4].time = 1f;
						ColorChanger colorChanger4 = gameObject4.AddComponent<ColorChanger>();
						colorChanger4.colors = new Gradient
						{
							colorKeys = array5
						};
						colorChanger4.cycleDuration = 7f;
					}
					Menu.OutlineObj(gameObject4, false);
					Menu.CanvasObject = new GameObject();
					Menu.CanvasObject.transform.parent = Menu.MenuObj.transform;
					Canvas canvas2 = Menu.CanvasObject.AddComponent<Canvas>();
					CanvasScaler canvasScaler2 = Menu.CanvasObject.AddComponent<CanvasScaler>();
					Menu.CanvasObject.AddComponent<GraphicRaycaster>();
					canvas2.renderMode = 2;
					canvasScaler2.dynamicPixelsPerUnit = 2500f;
					Text text6 = new GameObject
					{
						transform = 
						{
							parent = Menu.CanvasObject.transform
						}
					}.AddComponent<Text>();
					text6.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
					text6.fontSize = 1;
					text6.alignment = 4;
					text6.resizeTextForBestFit = true;
					text6.resizeTextMinSize = 0;
					text6.text = "Untitled Menu";
					RectTransform component7 = text6.GetComponent<RectTransform>();
					component7.sizeDelta = new Vector2(0.2f, 0.04f);
					component7.position = new Vector3(0.06f, 0f, 0.1461f);
					component7.localRotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
					Menu.AddPageButtons();
					bool disconnectButton2 = Settings.DisconnectButton;
					if (disconnectButton2)
					{
						Menu.AddDisconnectButton();
					}
					Button[] array6 = Menu.GetButtonInfoByPage(Menu.CurrentCategory).Skip(Menu.CurrentPage * Menu.ButtonsPerPage).Take(Menu.ButtonsPerPage).ToArray<Button>();
					for (int j = 0; j < array6.Length; j++)
					{
						Menu.CreateButton(array6[j], (float)j * 0.11f + 0.24f);
					}
					bool stretchy2 = Settings.stretchy;
					if (stretchy2)
					{
						Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.35f, 0.35f);
					}
					else
					{
						Menu.MenuObj.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			try
			{
				Menu.PositionMenu(Menu.PCMenuOpen ? Menu.PositionType.Computer : Menu.PositionType.Hand);
			}
			catch
			{
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000B9FC
		public static void ToggleButton(Button button)
		{
			int num = (Menu.GetButtonInfoByPage(Menu.CurrentCategory).Count + Menu.ButtonsPerPage + 1) / Menu.ButtonsPerPage;
			string name = button.Name;
			string a = name;
			if (!(a == "NextPage"))
			{
				if (!(a == "PreviousPage"))
				{
					if (!(a == "HomeButton"))
					{
						if (!(a == "DisconnectButton"))
						{
							Menu.OnClick(button);
						}
						else
						{
							PhotonNetwork.Disconnect();
						}
					}
					else
					{
						Menu.ReturnBackHome();
					}
				}
				else
				{
					Menu.NavigatePage(true);
				}
			}
			else
			{
				Menu.NavigatePage(false);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BA98
		public static void OnClick(Button button)
		{
			try
			{
				Menu.recentButton = button;
				bool flag = !button.IsToggle;
				if (flag)
				{
					button.OnEnable();
					Menu.RefreshMenu();
				}
				else
				{
					button.Enabled = !button.Enabled;
					bool enabled = button.Enabled;
					if (enabled)
					{
						button.OnEnable();
						button.DisableBool = false;
					}
					else
					{
						bool flag2 = button.OnDisable != null && !button.DisableBool;
						if (flag2)
						{
							button.OnDisable();
							button.DisableBool = true;
						}
					}
					Menu.RefreshMenu();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x04000034 RID: 52
		public static int[] bones = new int[]
		{
			4,
			3,
			5,
			4,
			19,
			18,
			20,
			19,
			3,
			18,
			21,
			20,
			22,
			21,
			25,
			21,
			29,
			21,
			31,
			29,
			27,
			25,
			24,
			22,
			6,
			5,
			7,
			6,
			10,
			6,
			14,
			6,
			16,
			14,
			12,
			10,
			9,
			7
		};

		// Token: 0x04000036 RID: 54
		public static Player selectedPlayerCate = null;

		// Token: 0x04000037 RID: 55
		public static float btnDelay;

		// Token: 0x04000038 RID: 56
		public static Button recentButton;

		// Token: 0x04000039 RID: 57
		public static Camera CamView;

		// Token: 0x0400003A RID: 58
		public static GameObject MenuObj;

		// Token: 0x0400003B RID: 59
		public static GameObject MenuBackground;

		// Token: 0x0400003C RID: 60
		public static GameObject ButtonObject;

		// Token: 0x0400003D RID: 61
		public static GameObject CanvasObject;

		// Token: 0x0400003E RID: 62
		public static GameObject PointerObject;

		// Token: 0x0400003F RID: 63
		public static GameObject DisconnectButton;

		// Token: 0x04000040 RID: 64
		public static GameObject TitleView;

		// Token: 0x04000041 RID: 65
		public static Rigidbody currentMenuRigidbody;

		// Token: 0x04000042 RID: 66
		public static int ButtonsPerPage = 7;

		// Token: 0x04000043 RID: 67
		public static int CurrentPage = 0;

		// Token: 0x04000044 RID: 68
		public static int ButtonCooldown = 0;

		// Token: 0x04000045 RID: 69
		public static Menu.Category CurrentCategory = Menu.Category.Base;

		// Token: 0x04000046 RID: 70
		public static Vector3 previousVelocity = Vector3.zero;

		// Token: 0x04000047 RID: 71
		public const float velocityThreshold = 0.05f;

		// Token: 0x04000048 RID: 72
		public static int PageIndex;

		// Token: 0x04000049 RID: 73
		public static bool InMenuCondition;

		// Token: 0x0400004A RID: 74
		public static bool InPcCondition;

		// Token: 0x0400004B RID: 75
		public static bool viewingTarget;

		// Token: 0x0400004C RID: 76
		public static bool PCMenuOpen = false;

		// Token: 0x0400004D RID: 77
		public static KeyCode PCMenuKey = 308;

		// Token: 0x0400004E RID: 78
		public static bool openMenu;

		// Token: 0x0400004F RID: 79
		public static bool menuOpen = false;

		// Token: 0x04000050 RID: 80
		public static bool firstopen = false;

		// Token: 0x04000051 RID: 81
		public static bool backgroundinit = false;

		// Token: 0x04000052 RID: 82
		public static GameObject StaticCameraObject;

		// Token: 0x04000053 RID: 83
		public static GameObject StaticVCam;

		// Token: 0x04000054 RID: 84
		public static Vector3 pagePos = new Vector3(0.56f, -0.541f, -0.0489f);

		// Token: 0x04000055 RID: 85
		public static Vector3 pageNextPos = new Vector3(0.56f, 0.541f, -0.0489f);

		// Token: 0x04000056 RID: 86
		public static Vector3 pageSize = new Vector3(0.09f, 0.1742534f, 0.7638814f);

		// Token: 0x04000057 RID: 87
		public static Vector3 pageTextpos = new Vector3(0.064f, -0.19f, 0f);

		// Token: 0x04000058 RID: 88
		public static Vector3 pageTextNPos = new Vector3(0.064f, 0.19f, 0f);

		// Token: 0x04000059 RID: 89
		public static Text fps;

		// Token: 0x0400005A RID: 90
		public static Vector3 FPSPos = new Vector3(0.06f, 0f, 0.1456f);

		// Token: 0x0400005B RID: 91
		public static float FPSSize = 0.029f;

		// Token: 0x0400005C RID: 92
		public static Color32 TextColor = Color.white;

		// Token: 0x0400005D RID: 93
		public static float flySpeed = 5f;

		// Token: 0x0400005E RID: 94
		public static Color32 OutlineColor = new Color32(120, 120, 120, byte.MaxValue);

		// Token: 0x0400005F RID: 95
		public static Color32 BgColor1 = new Color32(6, 6, 6, byte.MaxValue);

		// Token: 0x04000060 RID: 96
		public static Color32 BgColor2 = new Color32(14, 14, 14, byte.MaxValue);

		// Token: 0x04000061 RID: 97
		public static Color32 ToggleColor = new Color32(6, 6, 6, byte.MaxValue);

		// Token: 0x04000062 RID: 98
		public static Color32 DefaultColor1 = Color.black;

		// Token: 0x04000063 RID: 99
		public static Color32 DefaultColor2 = Color.black;

		// Token: 0x04000064 RID: 100
		public static Color32 EnabledColor = Color.magenta;

		// Token: 0x04000065 RID: 101
		public static Color32 DisabledColor = Menu.DefaultColor1;

		// Token: 0x04000066 RID: 102
		public static float fpsDelay;

		// Token: 0x04000067 RID: 103
		public static Settings.SettingData cachedData;

		// Token: 0x04000068 RID: 104
		public static GTPlayer __instance;

		// Token: 0x04000069 RID: 105
		public static VRRig __self;

		// Token: 0x0400006A RID: 106
		private static Vector3 menuOffset = new Vector3(1.2f, 0f, 2f);

		// Token: 0x0400006B RID: 107
		private const string playerNetworkStr = "Player Network";

		// Token: 0x0400006C RID: 108
		private const string lckStr = "LCK";

		// Token: 0x0400006D RID: 109
		private Camera cachedCamera;

		// Token: 0x0400006E RID: 110
		private int photonCheckIntervalFrames = 60;

		// Token: 0x0400006F RID: 111
		public static Vector3 backgroundPos = new Vector3(-65.8373f, 21.6568f, -80.9763f);

		// Token: 0x04000070 RID: 112
		private bool lastRightTrigger;

		// Token: 0x04000071 RID: 113
		private bool lastLeftTrigger;

		// Token: 0x02000044 RID: 68
		public enum Category
		{
			// Token: 0x0400015D RID: 349
			Base,
			// Token: 0x0400015E RID: 350
			Settings,
			// Token: 0x0400015F RID: 351
			Players,
			// Token: 0x04000160 RID: 352
			Vrrig,
			// Token: 0x04000161 RID: 353
			Movement,
			// Token: 0x04000162 RID: 354
			Visuals,
			// Token: 0x04000163 RID: 355
			OP,
			// Token: 0x04000164 RID: 356
			Master,
			// Token: 0x04000165 RID: 357
			Global,
			// Token: 0x04000166 RID: 358
			Mode,
			// Token: 0x04000167 RID: 359
			MenuSettings,
			// Token: 0x04000168 RID: 360
			VRRigSettings,
			// Token: 0x04000169 RID: 361
			MovementSettings,
			// Token: 0x0400016A RID: 362
			VisualSettings,
			// Token: 0x0400016B RID: 363
			Plugin
		}

		// Token: 0x02000045 RID: 69
		public enum PositionType
		{
			// Token: 0x0400016D RID: 365
			Hand,
			// Token: 0x0400016E RID: 366
			Computer
		}
	}
}
