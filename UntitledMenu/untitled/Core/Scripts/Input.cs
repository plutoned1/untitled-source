using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace untitled.Core.Scripts
{
	// Token: 0x0200002D RID: 45
	public class Input : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D1B3
		public void Awake()
		{
			this.type = Input.HeadsetType();
			this.leftController = InputDevices.GetDeviceAtXRNode(4);
			this.rightController = InputDevices.GetDeviceAtXRNode(5);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D1DC
		public static Input.VrType HeadsetType()
		{
			bool isDeviceActive = XRSettings.isDeviceActive;
			Input.VrType result;
			if (isDeviceActive)
			{
				bool flag = XRSettings.loadedDeviceName.Contains("Oculus");
				if (flag)
				{
					result = Input.VrType.Oculus;
				}
				else
				{
					bool flag2 = XRSettings.loadedDeviceName.Contains("Windows");
					if (flag2)
					{
						result = Input.VrType.WindowsMR;
					}
					else
					{
						bool flag3 = XRSettings.loadedDeviceName.Contains("Open");
						if (flag3)
						{
							result = Input.VrType.OpenVR;
						}
						else
						{
							result = Input.VrType.MockHMD;
						}
					}
				}
			}
			else
			{
				Debug.Log("No VR device detected.");
				result = Input.VrType.none;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D254
		private static bool CalculateGripState(float grabValue, float grabThreshold)
		{
			return grabValue >= grabThreshold;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D270
		private void Update()
		{
			bool flag = PCGUI.tab != 1;
			if (flag)
			{
				bool isDeviceActive = XRSettings.isDeviceActive;
				if (isDeviceActive)
				{
					ControllerInputPoller controllerInputPoller = ControllerInputPoller.instance;
					bool flag2 = this.type == Input.VrType.OpenVR;
					if (flag2)
					{
						Input.A_Button = controllerInputPoller.rightControllerPrimaryButton;
						Input.B_Button = controllerInputPoller.rightControllerSecondaryButton;
						Input.RightTrigger = Input.CalculateGripState(controllerInputPoller.rightControllerIndexFloat, Input.sensitivity);
						Input.RightGrip = Input.CalculateGripState(controllerInputPoller.rightControllerGripFloat, Input.sensitivity);
						Input.RightJoystick = controllerInputPoller.rightControllerPrimary2DAxis;
						Input.RightStickClick = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(2);
						Input.X_Button = controllerInputPoller.leftControllerPrimaryButton;
						Input.Y_Button = controllerInputPoller.leftControllerSecondaryButton;
						Input.LeftTrigger = Input.CalculateGripState(controllerInputPoller.leftControllerIndexFloat, Input.sensitivity);
						Input.LeftGrip = Input.CalculateGripState(controllerInputPoller.leftControllerGripFloat, Input.sensitivity);
						Input.LeftJoystick = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.GetAxis(1);
						Input.LeftStickClick = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(1);
					}
					else
					{
						this.rightController.TryGetFeatureValue(CommonUsages.primaryButton, ref Input.A_Button);
						this.rightController.TryGetFeatureValue(CommonUsages.secondaryButton, ref Input.B_Button);
						this.rightController.TryGetFeatureValue(CommonUsages.triggerButton, ref Input.RightTrigger);
						this.rightController.TryGetFeatureValue(CommonUsages.gripButton, ref Input.RightGrip);
						this.rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, ref Input.RightStickClick);
						this.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, ref Input.RightJoystick);
						this.leftController.TryGetFeatureValue(CommonUsages.primaryButton, ref Input.X_Button);
						this.leftController.TryGetFeatureValue(CommonUsages.secondaryButton, ref Input.Y_Button);
						this.leftController.TryGetFeatureValue(CommonUsages.triggerButton, ref Input.LeftTrigger);
						this.leftController.TryGetFeatureValue(CommonUsages.gripButton, ref Input.LeftGrip);
						this.leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, ref Input.LeftStickClick);
						this.leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, ref Input.LeftJoystick);
					}
				}
			}
		}

		// Token: 0x040000A3 RID: 163
		public static Input instance;

		// Token: 0x040000A4 RID: 164
		public static float sensitivity = 0.5f;

		// Token: 0x040000A5 RID: 165
		public static bool A_Button;

		// Token: 0x040000A6 RID: 166
		public static bool B_Button;

		// Token: 0x040000A7 RID: 167
		public static bool RightTrigger;

		// Token: 0x040000A8 RID: 168
		public static bool RightGrip;

		// Token: 0x040000A9 RID: 169
		public static Vector2 RightJoystick;

		// Token: 0x040000AA RID: 170
		public static bool RightStickClick;

		// Token: 0x040000AB RID: 171
		public static bool X_Button;

		// Token: 0x040000AC RID: 172
		public static bool Y_Button;

		// Token: 0x040000AD RID: 173
		public static bool LeftGrip;

		// Token: 0x040000AE RID: 174
		public static bool LeftTrigger;

		// Token: 0x040000AF RID: 175
		public static Vector2 LeftJoystick;

		// Token: 0x040000B0 RID: 176
		public static bool LeftStickClick;

		// Token: 0x040000B1 RID: 177
		private Input.VrType type;

		// Token: 0x040000B2 RID: 178
		public InputDevice leftController;

		// Token: 0x040000B3 RID: 179
		public InputDevice rightController;

		// Token: 0x0200004D RID: 77
		public enum VrType
		{
			// Token: 0x04000192 RID: 402
			OpenVR,
			// Token: 0x04000193 RID: 403
			Oculus,
			// Token: 0x04000194 RID: 404
			WindowsMR,
			// Token: 0x04000195 RID: 405
			MockHMD,
			// Token: 0x04000196 RID: 406
			none
		}
	}
}
