using System;
using System.Collections.Generic;
using System.IO;
using GorillaLocomotion;
using Newtonsoft.Json;
using UnityEngine;
using untitled.Core;
using untitled.Core.Scripts;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x02000035 RID: 53
	public class Settings
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001555C
		public static void SetNotificationState(bool state)
		{
			Notifications.IsEnabled = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015564
		public static void SetRigidbodyState(bool state)
		{
			Settings.DropMenu = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001556C
		public static void FreezePlayerInMenu()
		{
			bool flag = !Settings.isrunningfirst;
			if (flag)
			{
				Settings.FreezePlayerInMenuEnabled();
				Settings.isrunningfirst = true;
			}
			bool flag2 = Menu.MenuObj != null;
			if (flag2)
			{
				bool flag3 = Settings.closePosition == Vector3.zero;
				if (flag3)
				{
					Settings.closePosition = GorillaTagger.Instance.rigidbody.transform.position;
				}
				else
				{
					GorillaTagger.Instance.rigidbody.transform.position = Settings.closePosition;
				}
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			}
			else
			{
				Settings.closePosition = Vector3.zero;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001561F
		public static void FreezePlayerInMenuEnabled()
		{
			Settings.closePosition = GorillaTagger.Instance.rigidbody.transform.position;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001563B
		public static void DisableFreezePlayerInMenu()
		{
			Settings.isrunningfirst = false;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015644
		public static void SetDisconnectButtonState(bool state)
		{
			Settings.DisconnectButton = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001564C
		public static void SetAutosaveButtonState(bool state)
		{
			Settings.autoSave = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015654
		public static void SetRounding(bool state)
		{
			Settings.Rounding = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001565C
		public static void SetOutline(bool state)
		{
			Settings.Outline = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015664
		public static void SetPad(bool state)
		{
			Settings.Pad = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001566C
		public static void SetStretchy(bool state)
		{
			Settings.stretchy = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015674
		public static void SetAnimations(bool state)
		{
			Settings.Animations = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001567C
		public static void SetTooltips(bool state)
		{
			Settings.Tooltips = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015684
		public static void SetFPS(bool state)
		{
			Settings.Counter = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001568C
		public static void SetHome(bool state)
		{
			Settings.HomeButtons = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015694
		public static void SetArraylist(bool state)
		{
			Settings.ArrayList = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001569C
		public static void SetGunLine(bool state)
		{
			Settings.GunLine = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000156A4
		public static void AlwaysGUI(bool state)
		{
			Settings.AlwaysGUIB = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000156AC
		public static void SetGhostview(bool state)
		{
			Settings.Ghostview = state;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000156B4
		public static void CycleFlySpeed(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.FlyIndex++;
				bool flag2 = Settings.FlyIndex > 4;
				if (flag2)
				{
					Settings.FlyIndex = 0;
				}
			}
			switch (Settings.FlyIndex)
			{
			case 0:
				Settings.FlyString = "Super Slow";
				Menu.flySpeed = 2f;
				break;
			case 1:
				Settings.FlyString = "Slow";
				Menu.flySpeed = 6f;
				break;
			case 2:
				Settings.FlyString = "Normal";
				Menu.flySpeed = 10f;
				break;
			case 3:
				Settings.FlyString = "Fast";
				Menu.flySpeed = 20f;
				break;
			case 4:
				Settings.FlyString = "Super Fast";
				Menu.flySpeed = 35f;
				break;
			}
			Buttons.List[11][1].Name = "Fly Speed : " + Settings.FlyString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000157B0
		public static void CycleThemes(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.ThemeIndex++;
				bool flag2 = Settings.ThemeIndex > 16;
				if (flag2)
				{
					Settings.ThemeIndex = 0;
				}
			}
			switch (Settings.ThemeIndex)
			{
			case 0:
				Settings.ThemeString = "Purple";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(75, 25, 195, byte.MaxValue);
				Menu.BgColor2 = new Color32(128, 94, 208, byte.MaxValue);
				break;
			case 1:
				Settings.ThemeString = "Red";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(180, 18, 18, byte.MaxValue);
				Menu.BgColor2 = new Color32(105, 0, 0, byte.MaxValue);
				break;
			case 2:
				Settings.ThemeString = "Yellow";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(240, 190, 1, byte.MaxValue);
				Menu.BgColor2 = new Color32(210, 180, 74, byte.MaxValue);
				break;
			case 3:
				Settings.ThemeString = "Forest";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(132, 184, 132, byte.MaxValue);
				Menu.BgColor2 = new Color32(105, 145, 105, byte.MaxValue);
				break;
			case 4:
				Settings.ThemeString = "Sky";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(45, 115, 175, byte.MaxValue);
				Menu.BgColor2 = new Color32(75, 140, 200, byte.MaxValue);
				break;
			case 5:
				Settings.ThemeString = "Perrywinkle";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(210, 150, 225, byte.MaxValue);
				Menu.BgColor2 = new Color32(180, 130, 195, byte.MaxValue);
				break;
			case 6:
				Settings.ThemeString = "Blush";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(205, 108, 231, byte.MaxValue);
				Menu.BgColor2 = new Color32(149, 76, 170, byte.MaxValue);
				break;
			case 7:
				Settings.ThemeString = "Blood";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(60, 13, 21, byte.MaxValue);
				Menu.BgColor2 = new Color32(114, 38, 50, byte.MaxValue);
				break;
			case 8:
				Settings.ThemeString = "Sand";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(125, 115, 85, byte.MaxValue);
				Menu.BgColor2 = new Color32(150, 139, 105, byte.MaxValue);
				break;
			case 9:
				Settings.ThemeString = "Apricot";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(200, 125, 7, byte.MaxValue);
				Menu.BgColor2 = new Color32(168, 107, 13, byte.MaxValue);
				break;
			case 10:
				Settings.ThemeString = "Apricot";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(200, 125, 7, byte.MaxValue);
				Menu.BgColor2 = new Color32(168, 107, 13, byte.MaxValue);
				break;
			case 11:
				Settings.ThemeString = "Ink";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(31, 28, 29, byte.MaxValue);
				Menu.BgColor2 = new Color32(1, 98, byte.MaxValue, byte.MaxValue);
				break;
			case 12:
				Settings.ThemeString = "Dusk";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(56, 56, 56, byte.MaxValue);
				Menu.BgColor2 = new Color32(79, 79, 79, byte.MaxValue);
				break;
			case 13:
				Settings.ThemeString = "Beach";
				Settings.BeachTheme = true;
				Settings.RGBTheme = false;
				break;
			case 14:
				Settings.ThemeString = "RGB";
				Settings.BeachTheme = false;
				Settings.RGBTheme = true;
				break;
			case 15:
				Settings.ThemeString = "Midnight";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(20, 20, 35, byte.MaxValue);
				Menu.BgColor2 = new Color32(39, 39, 60, byte.MaxValue);
				break;
			case 16:
				Settings.ThemeString = "Night";
				Settings.BeachTheme = false;
				Settings.RGBTheme = false;
				Menu.BgColor1 = new Color32(6, 6, 6, byte.MaxValue);
				Menu.BgColor2 = new Color32(14, 14, 14, byte.MaxValue);
				break;
			}
			Buttons.List[9][9].Name = "Menu Theme : " + Settings.ThemeString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015D10
		public static void CycleOutlines(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.OutlineIndex++;
				bool flag2 = Settings.OutlineIndex > 10;
				if (flag2)
				{
					Settings.OutlineIndex = 0;
				}
			}
			switch (Settings.OutlineIndex)
			{
			case 0:
				Settings.OutlineString = "Midnight";
				Menu.OutlineColor = new Color32(31, 2, 102, byte.MaxValue);
				break;
			case 1:
				Settings.OutlineString = "Black";
				Menu.OutlineColor = Color.black;
				break;
			case 2:
				Settings.OutlineString = "Purple";
				Menu.OutlineColor = Color.magenta;
				break;
			case 3:
				Settings.OutlineString = "Red";
				Menu.OutlineColor = Color.red;
				break;
			case 4:
				Settings.OutlineString = "Yellow";
				Menu.OutlineColor = Color.yellow;
				break;
			case 5:
				Settings.OutlineString = "Green";
				Menu.OutlineColor = Color.green;
				break;
			case 6:
				Settings.OutlineString = "Blue";
				Menu.OutlineColor = Color.blue;
				break;
			case 7:
				Settings.OutlineString = "Pink";
				Menu.OutlineColor = Color.magenta;
				break;
			case 8:
				Settings.OutlineString = "Blush";
				Menu.OutlineColor = new Color32(230, 178, 243, byte.MaxValue);
				break;
			case 9:
				Settings.OutlineString = "Sand";
				Menu.OutlineColor = new Color32(163, 144, 93, byte.MaxValue);
				break;
			case 10:
				Settings.OutlineString = "White";
				Menu.OutlineColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				break;
			}
			Buttons.List[9][10].Name = "Outline Theme : " + Settings.OutlineString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00015F2C
		public static void CycleClick(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.ClickIndex++;
				bool flag2 = Settings.ClickIndex > 7;
				if (flag2)
				{
					Settings.ClickIndex = 0;
				}
			}
			switch (Settings.ClickIndex)
			{
			case 0:
				Settings.ClickString = "Button";
				Settings.Audio = false;
				Settings.AudioID = 67;
				break;
			case 1:
				Settings.ClickString = "Keyboard";
				Settings.Audio = false;
				Settings.AudioID = 66;
				break;
			case 2:
				Settings.ClickString = "Pop";
				Settings.Audio = false;
				Settings.AudioID = 84;
				break;
			case 3:
				Settings.ClickString = "Minecraft";
				Settings.Audio = true;
				Settings.AudioName = "minecraftclick.wav";
				break;
			case 4:
				Settings.ClickString = "Custom 1";
				Settings.Audio = true;
				Settings.AudioName = "customclick1.wav";
				break;
			case 5:
				Settings.ClickString = "Custom 2";
				Settings.Audio = true;
				Settings.AudioName = "customclick2.wav";
				break;
			case 6:
				Settings.ClickString = "Custom 3";
				Settings.Audio = true;
				Settings.AudioName = "customclick3.wav";
				break;
			case 7:
				Settings.ClickString = "Steal";
				Settings.Audio = true;
				Settings.AudioName = "steal.wav";
				break;
			}
			Buttons.List[9][12].Name = "Click Sound : " + Settings.ClickString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000160AC
		public static void CyclePageButtons(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.PageSIndex++;
				bool flag2 = Settings.PageSIndex > 3;
				if (flag2)
				{
					Settings.PageSIndex = 0;
				}
			}
			switch (Settings.PageSIndex)
			{
			case 0:
				Settings.PageString = "Triggers";
				Settings.PageButtons = false;
				Menu.pagePos = new Vector3(0.56f, -0.541f, -0.0489f);
				Menu.pageNextPos = new Vector3(0.56f, 0.541f, -0.0489f);
				Menu.pageSize = new Vector3(0.09f, 0.1742534f, 0.7638814f);
				Menu.pageTextpos = new Vector3(0.064f, -0.19f, 0f);
				Menu.pageTextNPos = new Vector3(0.064f, 0.19f, 0f);
				break;
			case 1:
				Settings.PageString = "Side Buttons";
				Settings.PageButtons = true;
				Menu.pagePos = new Vector3(0.56f, -0.541f, -0.0489f);
				Menu.pageNextPos = new Vector3(0.56f, 0.541f, -0.0489f);
				Menu.pageSize = new Vector3(0.09f, 0.1742534f, 0.7638814f);
				Menu.pageTextpos = new Vector3(0.064f, -0.19f, 0f);
				Menu.pageTextNPos = new Vector3(0.064f, 0.19f, 0f);
				break;
			case 2:
				Settings.PageString = "Bottom Buttons";
				Settings.PageButtons = true;
				Menu.pagePos = new Vector3(0.56f, -0.314f, -0.6065f);
				Menu.pageNextPos = new Vector3(0.56f, 0.314f, -0.6065f);
				Menu.pageSize = new Vector3(0.09f, 0.2278f, 0.1026f);
				Menu.pageTextpos = new Vector3(0.064f, -0.1105f, -0.273f);
				Menu.pageTextNPos = new Vector3(0.064f, 0.1105f, -0.273f);
				break;
			case 3:
				Settings.PageString = "Corner Buttons";
				Settings.PageButtons = true;
				Menu.pagePos = new Vector3(0.56f, -0.343f, -0.5067f);
				Menu.pageNextPos = new Vector3(0.56f, 0.343f, -0.5067f);
				Menu.pageSize = new Vector3(0.09f, 0.1796735f, 0.07010112f);
				Menu.pageTextpos = new Vector3(0.064f, -0.122f, -0.23f);
				Menu.pageTextNPos = new Vector3(0.064f, 0.122f, -0.23f);
				break;
			}
			Buttons.List[9][13].Name = "Page Buttons : " + Settings.PageString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001637C
		public static void CycleBackgrounds(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.BackgroundIndex++;
				bool flag2 = Settings.BackgroundIndex > 4;
				if (flag2)
				{
					Settings.BackgroundIndex = 0;
				}
			}
			switch (Settings.BackgroundIndex)
			{
			case 0:
				Settings.BackgroundString = "Above Stump";
				Menu.backgroundPos = new Vector3(-65.8373f, 21.6568f, -80.9763f);
				break;
			case 1:
				Settings.BackgroundString = "Sky";
				Menu.backgroundPos = new Vector3(-60.2801f, 43.404f, -13.5172f);
				break;
			case 2:
				Settings.BackgroundString = "Clouds";
				Menu.backgroundPos = new Vector3(-73.9161f, 162.7094f, -90.1657f);
				break;
			case 3:
				Settings.BackgroundString = "Stump";
				Menu.backgroundPos = new Vector3(-67.0842f, 12.582f, -83.5003f);
				break;
			case 4:
				Settings.BackgroundString = "Treehouse";
				Menu.backgroundPos = new Vector3(-62.8854f, 15.693f, -50.747f);
				break;
			}
			Buttons.List[9][14].Name = "PC Menu Background : " + Settings.BackgroundString;
			bool flag3 = !isConfig;
			if (flag3)
			{
				Menu.RefreshMenu();
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000164D4
		public static void CycleCounterPos(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.CounterIndex++;
				bool flag2 = Settings.CounterIndex > 2;
				if (flag2)
				{
					Settings.CounterIndex = 0;
				}
			}
			switch (Settings.CounterIndex)
			{
			case 0:
				Settings.CounterString = "Below Title";
				Menu.FPSPos = new Vector3(0.06f, 0f, 0.1456f);
				Menu.FPSSize = 0.029f;
				break;
			case 1:
				Settings.CounterString = "Bottom Left";
				Menu.FPSPos = new Vector3(0.06f, 0.105f, -0.215f);
				Menu.FPSSize = 0.02f;
				break;
			case 2:
				Settings.CounterString = "Bottom Right";
				Menu.FPSPos = new Vector3(0.06f, -0.105f, -0.215f);
				Menu.FPSSize = 0.02f;
				break;
			}
			Buttons.List[9][18].Name = "FPS Counter Pos : " + Settings.CounterString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000165E8
		public static void CycleCam(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.CamIndex++;
				bool flag2 = Settings.CamIndex > 1;
				if (flag2)
				{
					Settings.CamIndex = 0;
				}
			}
			int camIndex = Settings.CamIndex;
			int num = camIndex;
			if (num != 0)
			{
				if (num == 1)
				{
					Settings.CamString = "First Person";
				}
			}
			else
			{
				Settings.CamString = "Third Person";
			}
			Buttons.List[9][20].Name = "Players Tab Cam : " + Settings.CamString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001667C
		public static void CycleText(bool isConfig = false)
		{
			bool flag = !isConfig;
			if (flag)
			{
				Settings.TextIndex++;
				bool flag2 = Settings.TextIndex > 7;
				if (flag2)
				{
					Settings.TextIndex = 0;
				}
			}
			switch (Settings.TextIndex)
			{
			case 0:
				Settings.TextString = "Black";
				Menu.TextColor = Color.black;
				break;
			case 1:
				Settings.TextString = "Magenta";
				Menu.TextColor = Color.magenta;
				break;
			case 2:
				Settings.TextString = "Cyan";
				Menu.TextColor = Color.cyan;
				break;
			case 3:
				Settings.TextString = "Red";
				Menu.TextColor = Color.red;
				break;
			case 4:
				Settings.TextString = "Yellow";
				Menu.TextColor = Color.yellow;
				break;
			case 5:
				Settings.TextString = "Blue";
				Menu.TextColor = Color.blue;
				break;
			case 6:
				Settings.TextString = "Green";
				Menu.TextColor = Color.green;
				break;
			case 7:
				Settings.TextString = "White";
				Menu.TextColor = Color.white;
				break;
			}
			Buttons.List[9][11].Name = "Text Theme : " + Settings.TextString;
			Menu.RefreshMenu();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000167FC
		public static void TestGun()
		{
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData != null;
			if (flag)
			{
				bool isTriggered = gunLibData.isTriggered;
				if (isTriggered)
				{
					Debug.Log("Shooting!");
				}
				else
				{
					Debug.Log("Not Shooting!");
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00016840
		private static void WriteJson(Settings.SettingData data, string path)
		{
			string contents = JsonConvert.SerializeObject(data, 1);
			File.WriteAllText(path, contents);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00016860
		public static Settings.SettingData ReadJson(string path)
		{
			string text = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<Settings.SettingData>(text);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00016884
		public static void SaveConfig()
		{
			List<Settings.ModData> list = new List<Settings.ModData>();
			foreach (List<Button> list2 in Buttons.List)
			{
				foreach (Button button in list2)
				{
					bool flag = button != null;
					if (flag)
					{
						list.Add(new Settings.ModData
						{
							Name = button.Name,
							Enabled = button.Enabled
						});
					}
				}
			}
			Settings.SettingData data = new Settings.SettingData
			{
				ThemeIndex = Settings.ThemeIndex,
				OutlineIndex = Settings.OutlineIndex,
				ClickIndex = Settings.ClickIndex,
				PageSIndex = Settings.PageSIndex,
				TextIndex = Settings.TextIndex,
				FlyIndex = Settings.FlyIndex,
				BackgroundIndex = Settings.BackgroundIndex,
				CounterIndex = Settings.CounterIndex,
				CamIndex = Settings.CamIndex,
				EnabledMods = list
			};
			Directory.CreateDirectory("Untitled");
			Settings.WriteJson(data, "Untitled\\config.json");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000169DC
		public static void LoadConfig()
		{
			bool flag = File.Exists("Untitled\\config.json");
			if (flag)
			{
				Settings.SettingData settingData = Settings.ReadJson("Untitled\\config.json");
				foreach (Settings.ModData modData in settingData.EnabledMods)
				{
					bool flag2 = modData != null;
					if (flag2)
					{
						foreach (List<Button> list in Buttons.List)
						{
							foreach (Button button in list)
							{
								bool flag3 = button != null;
								if (flag3)
								{
									bool flag4 = button.Name == modData.Name && modData.Enabled;
									if (flag4)
									{
										button.Enabled = true;
									}
									bool flag5 = button.Name == modData.Name && !modData.Enabled;
									if (flag5)
									{
										button.Enabled = false;
									}
								}
							}
						}
					}
				}
				Settings.ThemeIndex = settingData.ThemeIndex;
				Settings.CycleThemes(true);
				Settings.OutlineIndex = settingData.OutlineIndex;
				Settings.CycleOutlines(true);
				Settings.ClickIndex = settingData.ClickIndex;
				Settings.CycleClick(true);
				Settings.PageSIndex = settingData.PageSIndex;
				Settings.CyclePageButtons(true);
				Settings.TextIndex = settingData.TextIndex;
				Settings.CycleText(true);
				Settings.FlyIndex = settingData.FlyIndex;
				Settings.CycleFlySpeed(true);
				Settings.BackgroundIndex = settingData.BackgroundIndex;
				Settings.CycleBackgrounds(true);
				Settings.CounterIndex = settingData.CounterIndex;
				Settings.CycleCounterPos(true);
				Settings.CamIndex = settingData.CamIndex;
				Settings.CycleCam(true);
				Menu.RefreshMenu();
			}
			else
			{
				Debug.Log(string.Format("No Config Found!\n\nconfig: {0}\noutline: {1}\ntheme: {2}", File.Exists("Untitled\\config.json"), File.Exists("outline.txt"), File.Exists("theme.txt")));
			}
		}

		// Token: 0x040000F2 RID: 242
		public static int OutlineIndex = 0;

		// Token: 0x040000F3 RID: 243
		public static int TextIndex = 0;

		// Token: 0x040000F4 RID: 244
		public static int LayoutIndex = 0;

		// Token: 0x040000F5 RID: 245
		public static int ThemeIndex = 0;

		// Token: 0x040000F6 RID: 246
		public static int ClickIndex = 0;

		// Token: 0x040000F7 RID: 247
		public static int PageSIndex = 0;

		// Token: 0x040000F8 RID: 248
		public static int BackgroundIndex = 0;

		// Token: 0x040000F9 RID: 249
		public static int CounterIndex = 0;

		// Token: 0x040000FA RID: 250
		public static int AntiTagIndex = 0;

		// Token: 0x040000FB RID: 251
		public static int CamIndex = 0;

		// Token: 0x040000FC RID: 252
		public static int FlyIndex = 0;

		// Token: 0x040000FD RID: 253
		public static string OutlineString;

		// Token: 0x040000FE RID: 254
		public static string LayoutString;

		// Token: 0x040000FF RID: 255
		public static string FlyString;

		// Token: 0x04000100 RID: 256
		public static string CamString;

		// Token: 0x04000101 RID: 257
		public static string ThemeString = "Midnight";

		// Token: 0x04000102 RID: 258
		public static string TextString;

		// Token: 0x04000103 RID: 259
		public static string ClickString;

		// Token: 0x04000104 RID: 260
		public static string PageString;

		// Token: 0x04000105 RID: 261
		public static string BackgroundString;

		// Token: 0x04000106 RID: 262
		public static string CounterString;

		// Token: 0x04000107 RID: 263
		public static string AntiString = "Invis Monkey";

		// Token: 0x04000108 RID: 264
		public static bool DisconnectButton = false;

		// Token: 0x04000109 RID: 265
		public static bool stretchy = false;

		// Token: 0x0400010A RID: 266
		public static bool Rounding = false;

		// Token: 0x0400010B RID: 267
		public static bool Outline = false;

		// Token: 0x0400010C RID: 268
		public static bool Pad = false;

		// Token: 0x0400010D RID: 269
		public static bool DropMenu = false;

		// Token: 0x0400010E RID: 270
		public static bool Tooltips = false;

		// Token: 0x0400010F RID: 271
		public static bool Counter = false;

		// Token: 0x04000110 RID: 272
		public static bool Ghostview = false;

		// Token: 0x04000111 RID: 273
		public static bool Audio = false;

		// Token: 0x04000112 RID: 274
		public static bool RGBTheme = false;

		// Token: 0x04000113 RID: 275
		public static bool RGBOutline = false;

		// Token: 0x04000114 RID: 276
		public static bool BeachTheme = false;

		// Token: 0x04000115 RID: 277
		public static bool BeachOutline = false;

		// Token: 0x04000116 RID: 278
		public static bool PageButtons = true;

		// Token: 0x04000117 RID: 279
		public static bool HomeButtons = false;

		// Token: 0x04000118 RID: 280
		public static bool ArrayList = false;

		// Token: 0x04000119 RID: 281
		public static bool GunLine = false;

		// Token: 0x0400011A RID: 282
		public static bool AlwaysGUIB = false;

		// Token: 0x0400011B RID: 283
		public static bool Animations = false;

		// Token: 0x0400011C RID: 284
		public static bool autoSave;

		// Token: 0x0400011D RID: 285
		public static int AudioID = 67;

		// Token: 0x0400011E RID: 286
		public static string AudioName;

		// Token: 0x0400011F RID: 287
		public static float pullPower = 0.05f;

		// Token: 0x04000120 RID: 288
		public static float speedStrength = 7.5f;

		// Token: 0x04000121 RID: 289
		public static float jumpStrenth = 1.25f;

		// Token: 0x04000122 RID: 290
		public static Font currentFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

		// Token: 0x04000123 RID: 291
		private static Vector3 closePosition;

		// Token: 0x04000124 RID: 292
		private static bool isrunningfirst;

		// Token: 0x02000053 RID: 83
		public class ModData
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000313 RID: 787 RVA: 0x0001B830 File Offset: 0x00019A30
			// (set) Token: 0x06000314 RID: 788 RVA: 0x0001B838 File Offset: 0x00019A38
			public string Name { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000315 RID: 789 RVA: 0x0001B841 File Offset: 0x00019A41
			// (set) Token: 0x06000316 RID: 790 RVA: 0x0001B849 File Offset: 0x00019A49
			public bool Enabled { get; set; }
		}

		// Token: 0x02000054 RID: 84
		public class SettingData
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000318 RID: 792 RVA: 0x0001B85B File Offset: 0x00019A5B
			// (set) Token: 0x06000319 RID: 793 RVA: 0x0001B863 File Offset: 0x00019A63
			public int ThemeIndex { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0001B86C File Offset: 0x00019A6C
			// (set) Token: 0x0600031B RID: 795 RVA: 0x0001B874 File Offset: 0x00019A74
			public int OutlineIndex { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0001B87D File Offset: 0x00019A7D
			// (set) Token: 0x0600031D RID: 797 RVA: 0x0001B885 File Offset: 0x00019A85
			public int PageSIndex { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0001B88E File Offset: 0x00019A8E
			// (set) Token: 0x0600031F RID: 799 RVA: 0x0001B896 File Offset: 0x00019A96
			public int ClickIndex { get; set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0001B89F File Offset: 0x00019A9F
			// (set) Token: 0x06000321 RID: 801 RVA: 0x0001B8A7 File Offset: 0x00019AA7
			public int PageIndex { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000322 RID: 802 RVA: 0x0001B8B0 File Offset: 0x00019AB0
			// (set) Token: 0x06000323 RID: 803 RVA: 0x0001B8B8 File Offset: 0x00019AB8
			public int TextIndex { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0001B8C1 File Offset: 0x00019AC1
			// (set) Token: 0x06000325 RID: 805 RVA: 0x0001B8C9 File Offset: 0x00019AC9
			public int FlyIndex { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0001B8D2 File Offset: 0x00019AD2
			// (set) Token: 0x06000327 RID: 807 RVA: 0x0001B8DA File Offset: 0x00019ADA
			public int BackgroundIndex { get; set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0001B8E3 File Offset: 0x00019AE3
			// (set) Token: 0x06000329 RID: 809 RVA: 0x0001B8EB File Offset: 0x00019AEB
			public int CounterIndex { get; set; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0001B8F4 File Offset: 0x00019AF4
			// (set) Token: 0x0600032B RID: 811 RVA: 0x0001B8FC File Offset: 0x00019AFC
			public int CamIndex { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0001B905 File Offset: 0x00019B05
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0001B90D File Offset: 0x00019B0D
			public List<Settings.ModData> EnabledMods { get; set; }
		}
	}
}
