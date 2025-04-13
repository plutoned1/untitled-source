using System;
using GorillaLocomotion;
using UnityEngine;
using untitled.Core.Scripts;
using untitled.Libs;

namespace untitled.Cheat
{
	// Token: 0x0200002F RID: 47
	public class Client
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D9AC
		public static void GhostMonkey()
		{
			bool isGhostMonkey = Client.IsGhostMonkey;
			if (isGhostMonkey)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
			bool flag = Input.A_Button && Time.time > Client.VRRigTimedAction;
			if (flag)
			{
				Client.VRRigTimedAction = Time.time + 0.2f;
				Client.IsGhostMonkey = !Client.IsGhostMonkey;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DA24
		public static void InvisMonkey()
		{
			bool isInvisMonkey = Client.IsInvisMonkey;
			if (isInvisMonkey)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(100000f, 10000f, 10000f);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
			bool flag = Input.RightTrigger && Time.time > Client.VRRigTimedAction;
			if (flag)
			{
				Client.VRRigTimedAction = Time.time + 0.2f;
				Client.IsInvisMonkey = !Client.IsInvisMonkey;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DAC8
		public static void RGBMonkey()
		{
			float num = Time.time * 0.7f;
			float num2 = Mathf.Sin(num) * 0.5f + 0.5f;
			float num3 = Mathf.Sin(num + 2.0943952f) * 0.5f + 0.5f;
			float num4 = Mathf.Sin(num + 4.1887903f) * 0.5f + 0.5f;
			GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", 0, new object[]
			{
				num2,
				num3,
				num4
			});
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DB60
		public static void MoveRigGun()
		{
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position = gunLibData.hitPosition;
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DBCC
		public static void ScareGun()
		{
			GunLib.GunLibData gunLibData = GunLib.Shoot();
			bool flag = gunLibData.isShooting && gunLibData.isTriggered;
			if (flag)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position = gunLibData.hitPosition;
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DC38
		public static void SpectateGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData == null;
			if (!flag)
			{
				bool flag2 = gunLibData.isShooting && gunLibData.isTriggered;
				if (flag2)
				{
					bool flag3 = gunLibData.lockedPlayer != null;
					if (flag3)
					{
						bool flag4 = Client.UCam == null;
						if (flag4)
						{
							Debug.Log("1");
							Client.UCam = new GameObject("Untitled Freecam");
							Camera camera = Client.UCam.AddComponent<Camera>();
							camera.fieldOfView = 120f;
							Client.UCam.transform.position = GorillaTagger.Instance.offlineVRRig.headConstraint.transform.position;
							Object.DontDestroyOnLoad(Client.UCam);
						}
						Client.UCam.transform.rotation = gunLibData.lockedPlayer.head.rigTarget.rotation;
						Client.UCam.transform.position = gunLibData.lockedPlayer.head.rigTarget.position;
					}
				}
				else
				{
					Object.Destroy(Client.UCam);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000DD5C
		public static void SexGun()
		{
			GunLib.GunLibData gunLibData = GunLib.ShootLock();
			bool flag = gunLibData == null;
			if (!flag)
			{
				bool flag2 = gunLibData.isShooting && gunLibData.isTriggered;
				if (flag2)
				{
					bool flag3 = gunLibData.lockedPlayer != null;
					if (flag3)
					{
						bool flag4 = gunLibData.lockedPlayer != null;
						if (flag4)
						{
							bool flag5 = !gunLibData.lockedPlayer.isOfflineVRRig;
							if (flag5)
							{
								VRRig lockedPlayer = gunLibData.lockedPlayer;
								GorillaTagger.Instance.offlineVRRig.enabled = false;
								GorillaTagger.Instance.offlineVRRig.transform.position = lockedPlayer.transform.position + lockedPlayer.transform.forward * -(0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f);
								GorillaTagger.Instance.myVRRig.transform.position = lockedPlayer.transform.position + lockedPlayer.transform.forward * -(0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f);
								GorillaTagger.Instance.offlineVRRig.transform.rotation = lockedPlayer.transform.rotation;
								GorillaTagger.Instance.myVRRig.transform.rotation = lockedPlayer.transform.rotation;
								GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = lockedPlayer.transform.position + lockedPlayer.transform.right * -0.2f + lockedPlayer.transform.up * -0.4f;
								GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = lockedPlayer.transform.position + lockedPlayer.transform.right * 0.2f + lockedPlayer.transform.up * -0.4f;
								GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = lockedPlayer.transform.rotation;
								GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = lockedPlayer.transform.rotation;
								GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = lockedPlayer.transform.rotation;
								bool flag6 = Time.frameCount % 45 == 0;
								if (flag6)
								{
									GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", 0, new object[]
									{
										64,
										false,
										999999f
									});
									Vector3 vector = lockedPlayer.transform.position - new Vector3(0f, 0.5f, 0f);
									GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", 0, new object[]
									{
										vector,
										Quaternion.identity,
										1.5f,
										1.5f,
										false,
										true
									});
								}
							}
						}
						else
						{
							GorillaTagger.Instance.offlineVRRig.enabled = true;
						}
					}
					else
					{
						GorillaTagger.Instance.offlineVRRig.enabled = true;
						GorillaTagger.Instance.offlineVRRig.headConstraint.rotation = GTPlayer.Instance.headCollider.transform.rotation;
					}
				}
				else
				{
					bool flag7 = !gunLibData.isShooting;
					if (flag7)
					{
						GorillaTagger.Instance.offlineVRRig.enabled = true;
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E174
		public static void HoldRig()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position = GTPlayer.Instance.rightControllerTransform.position;
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E1D8
		public static void SpazHead()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				VRMap head = GorillaTagger.Instance.offlineVRRig.head;
				head.trackingRotationOffset.x = head.trackingRotationOffset.x + Random.Range(1f, 360f);
				VRMap head2 = GorillaTagger.Instance.offlineVRRig.head;
				head2.trackingRotationOffset.y = head2.trackingRotationOffset.y + Random.Range(1f, 360f);
				VRMap head3 = GorillaTagger.Instance.offlineVRRig.head;
				head3.trackingRotationOffset.z = head3.trackingRotationOffset.z + Random.Range(1f, 360f);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E2D8
		public static void SpinHeadX()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				VRMap head = GorillaTagger.Instance.offlineVRRig.head;
				head.trackingRotationOffset.x = head.trackingRotationOffset.x + Random.Range(1f, 360f);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E340
		public static void SpinHeadY()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				VRMap head = GorillaTagger.Instance.offlineVRRig.head;
				head.trackingRotationOffset.y = head.trackingRotationOffset.y + Random.Range(1f, 360f);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E3A8
		public static void SpinHeadZ()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				VRMap head = GorillaTagger.Instance.offlineVRRig.head;
				head.trackingRotationOffset.z = head.trackingRotationOffset.z + Random.Range(1f, 360f);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000E410
		public static void HelicopterRig()
		{
			bool rightGrip = Input.RightGrip;
			if (rightGrip)
			{
				GorillaTagger.Instance.offlineVRRig.enabled = false;
				GorillaTagger.Instance.offlineVRRig.transform.position += new Vector3(0f, 0.05f, 0f);
				GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10f, 0f));
				GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
				GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
				GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
				GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
				GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
				GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation *= Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset);
				GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation *= Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset);
			}
			else
			{
				GorillaTagger.Instance.offlineVRRig.enabled = true;
			}
		}

		// Token: 0x040000B4 RID: 180
		public static float RelayDelay;

		// Token: 0x040000B5 RID: 181
		public static bool Relay;

		// Token: 0x040000B6 RID: 182
		public static bool IsGhostMonkey;

		// Token: 0x040000B7 RID: 183
		public static bool IsInvisMonkey;

		// Token: 0x040000B8 RID: 184
		public static float VRRigTimedAction;

		// Token: 0x040000B9 RID: 185
		public static GameObject UCam;
	}
}
