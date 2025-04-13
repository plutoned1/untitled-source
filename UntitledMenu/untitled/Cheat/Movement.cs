using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.InputSystem;
using untitled.Core;
using untitled.Core.Scripts;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x02000033 RID: 51
	public class Movement
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012CE0
		public static void WASDd()
		{
			float num = Menu.flySpeed;
			Transform transform = Camera.main.transform;
			GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
			bool key = UnityInput.Current.GetKey(304);
			if (key)
			{
				num *= 2.5f;
			}
			bool flag = UnityInput.Current.GetKey(119) || UnityInput.Current.GetKey(273);
			if (flag)
			{
				transform.position += transform.forward * num * Time.deltaTime;
			}
			bool flag2 = UnityInput.Current.GetKey(97) || UnityInput.Current.GetKey(276);
			if (flag2)
			{
				transform.position += -transform.right * num * Time.deltaTime;
			}
			bool flag3 = UnityInput.Current.GetKey(115) || UnityInput.Current.GetKey(274);
			if (flag3)
			{
				transform.position += -transform.forward * num * Time.deltaTime;
			}
			bool flag4 = UnityInput.Current.GetKey(100) || UnityInput.Current.GetKey(275);
			if (flag4)
			{
				transform.position += transform.right * num * Time.deltaTime;
			}
			bool key2 = UnityInput.Current.GetKey(32);
			if (key2)
			{
				transform.position += transform.up * num * Time.deltaTime;
			}
			bool key3 = UnityInput.Current.GetKey(306);
			if (key3)
			{
				transform.position += -transform.up * num * Time.deltaTime;
			}
			bool mouseButton = UnityInput.Current.GetMouseButton(1);
			if (mouseButton)
			{
				Vector3 vector = UnityInput.Current.mousePosition - Movement.oldMousePos;
				float num2 = transform.localEulerAngles.x - vector.y * 0.3f;
				float num3 = transform.localEulerAngles.y + vector.x * 0.3f;
				transform.localEulerAngles = new Vector3(num2, num3, 0f);
			}
			Movement.oldMousePos = UnityInput.Current.mousePosition;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012F7C
		public static void UpdateClipColliders(bool enabledd)
		{
			foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
			{
				meshCollider.enabled = enabledd;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00012FB0
		public static void WallWalk()
		{
			bool flag = GTPlayer.Instance.wasLeftHandColliding || GTPlayer.Instance.wasRightHandColliding;
			if (flag)
			{
				FieldInfo field = typeof(GTPlayer).GetField("lastHitInfoHand", BindingFlags.Instance | BindingFlags.NonPublic);
				RaycastHit raycastHit = (RaycastHit)field.GetValue(GTPlayer.Instance);
				Movement.pos = raycastHit.point;
				Movement.strength = raycastHit.normal;
			}
			bool flag2 = Movement.pos != Vector3.zero && Input.RightGrip;
			if (flag2)
			{
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Movement.strength * -5f, 5);
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), 5);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013094
		public static void NoClip()
		{
			bool rightTrigger = Input.RightTrigger;
			if (rightTrigger)
			{
				bool flag = !Movement.noClip;
				if (flag)
				{
					Movement.noClip = true;
					Movement.UpdateClipColliders(false);
				}
			}
			else
			{
				bool flag2 = Movement.noClip;
				if (flag2)
				{
					Movement.noClip = false;
					Movement.UpdateClipColliders(true);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000130E4
		public static void SteamLongArms()
		{
			GTPlayer.Instance.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001310B
		public static void ReallyLongArms()
		{
			GTPlayer.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013132
		public static void NormalArms()
		{
			GTPlayer.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001315C
		public static void JoystickFly()
		{
			float num = 1f;
			bool flag = Input.LeftStickClick || Input.RightStickClick;
			if (flag)
			{
				num = 3f;
			}
			bool flag2 = (double)Mathf.Abs(Input.LeftJoystick.x) > 0.3 || (double)Mathf.Abs(Input.LeftJoystick.y) > 0.3 || (double)Mathf.Abs(Input.RightJoystick.y) > 0.3;
			if (flag2)
			{
				Vector3 vector = GorillaTagger.Instance.headCollider.transform.forward * (Input.LeftJoystick.y * 8f) + GorillaTagger.Instance.headCollider.transform.right * (Input.LeftJoystick.x * 8f) + Vector3.up * (Input.RightJoystick.y * 8f);
				vector *= num;
				GTPlayer.Instance.transform.position += vector * Time.deltaTime;
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000132A4
		public static void HandFly()
		{
			bool a_Button = Input.A_Button;
			if (a_Button)
			{
				GTPlayer.Instance.transform.position += GTPlayer.Instance.rightControllerTransform.transform.forward * Time.deltaTime * Menu.flySpeed;
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013318
		public static void BodyFly()
		{
			bool a_Button = Input.A_Button;
			if (a_Button)
			{
				GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * Menu.flySpeed;
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001338C
		public static void SlingshotFly()
		{
			bool a_Button = Input.A_Button;
			if (a_Button)
			{
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity += GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * (Menu.flySpeed * 2f);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000133F0
		public static void NoClipFly()
		{
			bool a_Button = Input.A_Button;
			if (a_Button)
			{
				GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * Menu.flySpeed;
				GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
				bool flag = !Movement.noClip;
				if (flag)
				{
					Movement.noClip = true;
					Movement.UpdateClipColliders(false);
				}
			}
			else
			{
				bool flag2 = Movement.noClip;
				if (flag2)
				{
					Movement.noClip = false;
					Movement.UpdateClipColliders(true);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013498
		public static void IronMonkey()
		{
			bool a_Button = Input.A_Button;
			if (a_Button)
			{
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Menu.flySpeed * -GorillaTagger.Instance.leftHandTransform.right, 5);
				GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
			}
			bool x_Button = Input.X_Button;
			if (x_Button)
			{
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Menu.flySpeed * GorillaTagger.Instance.rightHandTransform.right, 5);
				GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 50f * GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000135A8
		public static void PullMod()
		{
			bool flag = (!GTPlayer.Instance.IsHandTouching(true) && Movement.LastTouchL) || (!GTPlayer.Instance.IsHandTouching(false) && Movement.LastTouchR && Input.RightGrip);
			if (flag)
			{
				Vector3 velocity = GTPlayer.Instance.GetComponent<Rigidbody>().velocity;
				GTPlayer.Instance.transform.position += new Vector3(velocity.x * Settings.pullPower, 0f, velocity.z * Settings.pullPower);
			}
			Movement.LastTouchL = GTPlayer.Instance.IsHandTouching(true);
			Movement.LastTouchR = GTPlayer.Instance.IsHandTouching(false);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001365C
		public static void SpiderWalk()
		{
			bool flag = GTPlayer.Instance.IsHandTouching(true) || GTPlayer.Instance.IsHandTouching(false);
			if (flag)
			{
				FieldInfo field = typeof(GTPlayer).GetField("lastHitInfoHand", BindingFlags.Instance | BindingFlags.NonPublic);
				RaycastHit raycastHit = (RaycastHit)field.GetValue(GTPlayer.Instance);
				Movement.walkpos = raycastHit.point;
				Movement.walknormal = raycastHit.normal;
			}
			bool flag2 = Movement.walkpos != Vector3.zero;
			if (flag2)
			{
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Movement.walknormal * -9.81f, 5);
				GTPlayer.Instance.rightControllerTransform.parent.rotation = Quaternion.Lerp(GTPlayer.Instance.rightControllerTransform.parent.rotation, Quaternion.LookRotation(Movement.walknormal) * Quaternion.Euler(90f, 0f, 0f), Time.deltaTime);
				GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.unscaledDeltaTime * (9.81f / Time.unscaledDeltaTime)), 5);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013790
		public static void Freecam()
		{
			bool flag = Movement.UCam == null;
			if (flag)
			{
				Debug.Log("1");
				Movement.UCam = new GameObject("Untitled Freecam");
				Camera camera = Movement.UCam.AddComponent<Camera>();
				camera.fieldOfView = 120f;
				Movement.UCam.transform.position = GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position;
				Object.DontDestroyOnLoad(Movement.UCam);
			}
			Movement.UCam.transform.rotation = GTPlayer.Instance.headCollider.transform.rotation;
			bool flag2 = Input.RightTrigger || Mouse.current.leftButton.isPressed;
			if (flag2)
			{
				float num = 1f;
				bool flag3 = Input.LeftStickClick || Input.RightStickClick;
				if (flag3)
				{
					num = 3f;
				}
				bool flag4 = (double)Mathf.Abs(Input.LeftJoystick.x) > 0.3 || (double)Mathf.Abs(Input.LeftJoystick.y) > 0.3 || (double)Mathf.Abs(Input.RightJoystick.y) > 0.3;
				if (flag4)
				{
					Vector3 vector = Movement.UCam.transform.forward * (Input.LeftJoystick.y * 8f) + Movement.UCam.transform.right * (Input.LeftJoystick.x * 8f) + Vector3.up * (Input.RightJoystick.y * 8f);
					vector *= num;
					Movement.UCam.transform.position += vector * Time.deltaTime;
				}
			}
			else
			{
				Object.Destroy(Movement.UCam);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001398C
		public static void Rewind()
		{
			bool rightTrigger = Input.RightTrigger;
			if (rightTrigger)
			{
				float num = Input.LeftTrigger ? 2.5f : 1f;
				Movement.pba += num;
				while (Movement.pba >= 1f && Movement.playerPositions.Count > 0)
				{
					Movement.pba -= 1f;
					GorillaTagger.Instance.offlineVRRig.enabled = false;
					int index = Movement.playerPositions.Count - 1;
					object[] array = Movement.playerPositions[index];
					Vector3 position = (Vector3)array[0];
					Quaternion rotation = (Quaternion)array[1];
					Vector3 position2 = (Vector3)array[2];
					Quaternion rotation2 = (Quaternion)array[3];
					Vector3 position3 = (Vector3)array[4];
					Quaternion rotation3 = (Quaternion)array[5];
					Vector3 position4 = (Vector3)array[6];
					Quaternion rotation4 = (Quaternion)array[7];
					GorillaTagger.Instance.offlineVRRig.transform.position = position;
					GorillaTagger.Instance.offlineVRRig.transform.rotation = rotation;
					GorillaTagger.Instance.offlineVRRig.rightHandPlayer.transform.position = position2;
					GorillaTagger.Instance.offlineVRRig.rightHandPlayer.transform.rotation = rotation2;
					GorillaTagger.Instance.offlineVRRig.leftHandPlayer.transform.position = position3;
					GorillaTagger.Instance.offlineVRRig.leftHandPlayer.transform.rotation = rotation3;
					GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position = position4;
					GorillaTagger.Instance.offlineVRRig.headConstraint.transform.rotation = rotation4;
					Movement.TeleportPlayer(GorillaTagger.Instance.offlineVRRig.transform.position);
					Movement.playerPositions.RemoveAt(index);
				}
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				bool flag = !Movement.recording;
				if (flag)
				{
					Movement.playerPositions.Clear();
					Movement.recording = true;
				}
				VRRig offlineVRRig = GorillaTagger.Instance.offlineVRRig;
				object[] item = new object[]
				{
					offlineVRRig.transform.position,
					offlineVRRig.transform.rotation,
					offlineVRRig.rightHandTransform.position,
					offlineVRRig.rightHandTransform.rotation,
					offlineVRRig.leftHandTransform.position,
					offlineVRRig.leftHandTransform.rotation,
					offlineVRRig.headConstraint.transform.position,
					offlineVRRig.headConstraint.transform.rotation
				};
				Movement.playerPositions.Add(item);
				bool flag2 = Movement.playerPositions.Count > 16640;
				if (flag2)
				{
					Movement.playerPositions.RemoveAt(0);
				}
			}
			else
			{
				Movement.recording = false;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00013CBC
		public static void MacroRecorder()
		{
			bool rightTrigger = Input.RightTrigger;
			if (rightTrigger)
			{
				float num = Input.LeftTrigger ? 2.5f : 1f;
				Movement.pba += num;
				while (Movement.pba >= 1f && Movement.playerPositions.Count > 0)
				{
					Movement.pba -= 1f;
					GorillaTagger.Instance.offlineVRRig.enabled = false;
					Vector3 position = (Vector3)Movement.playerPositions[0][0];
					Quaternion rotation = (Quaternion)Movement.playerPositions[0][1];
					Vector3 position2 = (Vector3)Movement.playerPositions[0][2];
					Quaternion rotation2 = (Quaternion)Movement.playerPositions[0][3];
					Vector3 position3 = (Vector3)Movement.playerPositions[0][4];
					Quaternion rotation3 = (Quaternion)Movement.playerPositions[0][5];
					Vector3 position4 = (Vector3)Movement.playerPositions[0][6];
					Quaternion rotation4 = (Quaternion)Movement.playerPositions[0][7];
					GorillaTagger.Instance.offlineVRRig.transform.position = position;
					GorillaTagger.Instance.offlineVRRig.transform.rotation = rotation;
					GorillaTagger.Instance.offlineVRRig.rightHandPlayer.transform.position = position2;
					GorillaTagger.Instance.offlineVRRig.rightHandPlayer.transform.rotation = rotation2;
					GorillaTagger.Instance.offlineVRRig.leftHandPlayer.transform.position = position3;
					GorillaTagger.Instance.offlineVRRig.leftHandPlayer.transform.rotation = rotation3;
					GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position = position4;
					GorillaTagger.Instance.offlineVRRig.headConstraint.transform.rotation = rotation4;
					Movement.TeleportPlayer(GorillaTagger.Instance.offlineVRRig.transform.position);
					Movement.playerPositions.RemoveAt(0);
				}
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				bool flag = !Movement.recording;
				if (flag)
				{
					Movement.playerPositions.Clear();
					Movement.recording = true;
				}
				VRRig offlineVRRig = GorillaTagger.Instance.offlineVRRig;
				object[] item = new object[]
				{
					offlineVRRig.transform.position,
					offlineVRRig.transform.rotation,
					offlineVRRig.rightHandTransform.position,
					offlineVRRig.rightHandTransform.rotation,
					offlineVRRig.leftHandTransform.position,
					offlineVRRig.leftHandTransform.rotation,
					offlineVRRig.headConstraint.transform.position,
					offlineVRRig.headConstraint.transform.rotation
				};
				Movement.playerPositions.Add(item);
				bool flag2 = Movement.playerPositions.Count > 16640;
				if (flag2)
				{
					Movement.playerPositions.RemoveAt(0);
				}
			}
			else
			{
				Movement.recording = false;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001401D
		public static void TeleportPlayer(Vector3 pos)
		{
			GTPlayer.Instance.TeleportTo(pos, GTPlayer.Instance.transform.rotation);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001403B
		public static void SpeedBoost()
		{
			GTPlayer.Instance.maxJumpSpeed = Settings.speedStrength;
			GTPlayer.Instance.jumpMultiplier = Settings.jumpStrenth;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014060
		public static void Checkpoint()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				bool flag = Movement.checkpoint == null;
				if (flag)
				{
					Movement.checkpoint = GameObject.CreatePrimitive(0);
					Object.Destroy(Movement.checkpoint.GetComponent<Collider>());
					Movement.checkpoint.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				}
				else
				{
					Movement.checkpoint.GetComponent<Renderer>().material.color = Menu.BgColor1;
					Movement.checkpoint.transform.position = Global.TrueRightHand().Item1;
				}
			}
			bool flag2 = Input.RightTrigger && Movement.checkpoint;
			if (flag2)
			{
				Movement.checkpoint.GetComponent<Renderer>().material.color = Color.red;
				TeleportLib.TeleportTo(Movement.checkpoint.transform.position);
				GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
			}
			else
			{
				Movement.checkpoint.GetComponent<Renderer>().material.color = Menu.BgColor1;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00014190
		public static void Platforms()
		{
			bool flag = Input.RightGrip && Movement.RPlat == null;
			if (flag)
			{
				Movement.RPlat = GameObject.CreatePrimitive(0);
				Movement.RPlat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
				Movement.RPlat.transform.position = GTPlayer.Instance.rightControllerTransform.transform.position + new Vector3(0f, -0.2f, 0f);
			}
			else
			{
				bool flag2 = !Input.RightGrip && Movement.RPlat != null;
				if (flag2)
				{
					Object.Destroy(Movement.RPlat);
				}
			}
			bool flag3 = Input.LeftGrip && Movement.LPlat == null;
			if (flag3)
			{
				Movement.LPlat = GameObject.CreatePrimitive(0);
				Movement.LPlat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
				Movement.LPlat.transform.position = GTPlayer.Instance.leftControllerTransform.transform.position + new Vector3(0f, -0.2f, 0f);
			}
			else
			{
				bool flag4 = !Input.LeftGrip && Movement.LPlat != null;
				if (flag4)
				{
					Object.Destroy(Movement.LPlat);
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		public static bool LastTouchR;

		// Token: 0x040000D6 RID: 214
		private static List<object[]> playerPositions = new List<object[]>();

		// Token: 0x040000D7 RID: 215
		public static bool LastTouchL;

		// Token: 0x040000D8 RID: 216
		public static bool noClip;

		// Token: 0x040000D9 RID: 217
		public static bool recording;

		// Token: 0x040000DA RID: 218
		public static Vector3 walkpos;

		// Token: 0x040000DB RID: 219
		public static Vector3 walknormal;

		// Token: 0x040000DC RID: 220
		public static Vector3 oldMousePos;

		// Token: 0x040000DD RID: 221
		public static Vector3 strength = Vector3.zero;

		// Token: 0x040000DE RID: 222
		public static Vector3 pos = Vector3.zero;

		// Token: 0x040000DF RID: 223
		public static GameObject LPlat;

		// Token: 0x040000E0 RID: 224
		public static GameObject RPlat;

		// Token: 0x040000E1 RID: 225
		public static GameObject checkpoint;

		// Token: 0x040000E2 RID: 226
		public static GameObject VRKeyboard;

		// Token: 0x040000E3 RID: 227
		public static GameObject UCam;

		// Token: 0x040000E4 RID: 228
		public static GameObject RightHandTest;

		// Token: 0x040000E5 RID: 229
		public static float pba = 0f;
	}
}
