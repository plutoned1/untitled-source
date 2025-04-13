using System;
using System.Globalization;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using untitled.Assets;
using untitled.Cheat;
using untitled.Core.Scripts;

namespace untitled.Core
{
	// Token: 0x0200000C RID: 12
	public class PCGUI : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00006164
		public void OnGUI()
		{
			bool flag = Menu.InPcCondition || Settings.AlwaysGUIB;
			if (flag)
			{
				GUI.backgroundColor = Menu.BgColor1;
				PCGUI.windowRect = GUI.Window(0, PCGUI.windowRect, new GUI.WindowFunction(this.DrawLegacyWindow), "untitled gui");
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000061B8
		private void DrawLegacyWindow(int windowID)
		{
			GUI.backgroundColor = Menu.BgColor1;
			bool flag = !this.testingints;
			if (flag)
			{
				PCGUI.tab = GUILayout.Toolbar(PCGUI.tab, new string[]
				{
					"pc",
					"emulators"
				}, Array.Empty<GUILayoutOption>());
			}
			else
			{
				PCGUI.tab = GUILayout.Toolbar(PCGUI.tab, new string[]
				{
					"pc",
					"emulators",
					"testing"
				}, Array.Empty<GUILayoutOption>());
			}
			bool flag2 = PCGUI.tab == 0;
			if (flag2)
			{
				PCGUI.windowRect.height = 122f;
				this.textField = GUILayout.TextField(this.textField, Array.Empty<GUILayoutOption>());
				bool flag3 = GUILayout.Button("Join Room", Array.Empty<GUILayoutOption>());
				if (flag3)
				{
					PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(this.textField, 0);
					this.ClickSound();
				}
				bool flag4 = GUILayout.Button("Set Name", Array.Empty<GUILayoutOption>());
				if (flag4)
				{
					PhotonNetwork.LocalPlayer.NickName = this.textField;
					PhotonNetwork.NickName = this.textField;
					PlayerPrefs.SetString("playerName", this.textField);
					GorillaComputer.instance.currentName = this.textField;
					PlayerPrefs.Save();
					this.ClickSound();
				}
			}
			bool flag5 = PCGUI.tab == 1;
			if (flag5)
			{
				PCGUI.windowRect.height = 233f;
				PCGUI.lt = GUILayout.Toggle(PCGUI.lt, "Left Trigger", Array.Empty<GUILayoutOption>());
				PCGUI.rt = GUILayout.Toggle(PCGUI.rt, "Right Trigger", Array.Empty<GUILayoutOption>());
				PCGUI.lg = GUILayout.Toggle(PCGUI.lg, "Left Grip", Array.Empty<GUILayoutOption>());
				PCGUI.rt = GUILayout.Toggle(PCGUI.rt, "Right Grip", Array.Empty<GUILayoutOption>());
				PCGUI.a = GUILayout.Toggle(PCGUI.a, "A Button", Array.Empty<GUILayoutOption>());
				PCGUI.b = GUILayout.Toggle(PCGUI.b, "B Button", Array.Empty<GUILayoutOption>());
				PCGUI.x = GUILayout.Toggle(PCGUI.x, "X Button", Array.Empty<GUILayoutOption>());
				PCGUI.y = GUILayout.Toggle(PCGUI.y, "Y Button", Array.Empty<GUILayoutOption>());
				Input.LeftTrigger = PCGUI.lt;
				Input.RightTrigger = PCGUI.rt;
				Input.LeftGrip = PCGUI.lg;
				Input.RightGrip = PCGUI.rg;
				Input.A_Button = PCGUI.a;
				Input.B_Button = PCGUI.b;
				Input.X_Button = PCGUI.x;
				Input.Y_Button = PCGUI.y;
			}
			bool flag6 = PCGUI.tab == 2;
			if (flag6)
			{
				PCGUI.windowRect.height = 122f;
				PCGUI.Delay = float.Parse(GUILayout.TextField(PCGUI.Delay.ToString(), Array.Empty<GUILayoutOption>()), NumberStyles.Any);
				PCGUI.forr = int.Parse(GUILayout.TextField(PCGUI.forr.ToString(), Array.Empty<GUILayoutOption>()));
			}
			GUI.DragWindow();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000064AC
		public void ClickSound()
		{
			bool audio = Settings.Audio;
			if (audio)
			{
				AssetLoader.PlayClick(Settings.AudioName);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Settings.AudioID, false, 0.8f);
			}
		}

		// Token: 0x04000026 RID: 38
		public static Rect windowRect = new Rect((float)(Screen.width / 2 - 111), (float)(Screen.height - 222), 222f, 111f);

		// Token: 0x04000027 RID: 39
		private string textField = "untitled";

		// Token: 0x04000028 RID: 40
		public static int tab = 0;

		// Token: 0x04000029 RID: 41
		private static bool lt;

		// Token: 0x0400002A RID: 42
		private static bool rt;

		// Token: 0x0400002B RID: 43
		private static bool lg;

		// Token: 0x0400002C RID: 44
		private static bool rg;

		// Token: 0x0400002D RID: 45
		private static bool a;

		// Token: 0x0400002E RID: 46
		private static bool b;

		// Token: 0x0400002F RID: 47
		private static bool x;

		// Token: 0x04000030 RID: 48
		private static bool y;

		// Token: 0x04000031 RID: 49
		public static float Delay;

		// Token: 0x04000032 RID: 50
		public static int forr;

		// Token: 0x04000033 RID: 51
		private bool testingints = true;
	}
}
