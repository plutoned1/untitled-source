using System;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.XR;
using untitled.Cheat;

namespace untitled.Libs
{
	// Token: 0x02000009 RID: 9
	internal class GunLib
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000336C
		public static void GunCleanUp()
		{
			bool flag = GunLib.pointer == null || GunLib.lr == null;
			if (!flag)
			{
				Object.Destroy(GunLib.pointer);
				GunLib.pointer = null;
				Object.Destroy(GunLib.lr.gameObject);
				GunLib.lr = null;
				GunLib.data = new GunLib.GunLibData(false, false, false, null, default(Vector3), default(RaycastHit));
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000033E4
		public static GunLib.GunLibData ShootLock()
		{
			GunLib.GunLibData result;
			try
			{
				bool isDeviceActive = XRSettings.isDeviceActive;
				if (isDeviceActive)
				{
					bool flag = false;
					bool flag2 = !flag;
					Transform transform;
					if (flag2)
					{
						transform = GTPlayer.Instance.rightControllerTransform;
						GunLib.data.isShooting = ControllerInputPoller.instance.rightGrab;
						GunLib.data.isTriggered = (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f);
					}
					else
					{
						transform = GTPlayer.Instance.leftControllerTransform;
						GunLib.data.isShooting = ControllerInputPoller.instance.leftGrab;
						GunLib.data.isTriggered = (ControllerInputPoller.instance.leftControllerIndexFloat > 0.1f);
					}
					bool isShooting = GunLib.data.isShooting;
					if (isShooting)
					{
						Renderer renderer = (GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null;
						bool flag3 = GunLib.data.lockedPlayer == null && !GunLib.data.isLocked;
						if (flag3)
						{
							RaycastHit raycastHit;
							bool flag4 = Physics.Raycast(transform.position - transform.up, -transform.up, ref raycastHit) && GunLib.pointer == null;
							if (flag4)
							{
								GunLib.pointer = GameObject.CreatePrimitive(0);
								Object.Destroy(GunLib.pointer.GetComponent<Rigidbody>());
								Object.Destroy(GunLib.pointer.GetComponent<SphereCollider>());
								GunLib.pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
								renderer = ((GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null);
								renderer.material.color = Color.red;
								renderer.material.shader = Shader.Find("GUI/Text Shader");
							}
							bool gunLine = Settings.GunLine;
							if (gunLine)
							{
								bool flag5 = GunLib.lr == null;
								if (flag5)
								{
									GameObject gameObject = new GameObject("line");
									GunLib.lr = gameObject.AddComponent<LineRenderer>();
									GunLib.lr.endWidth = 0.01f;
									GunLib.lr.startWidth = 0.01f;
									GunLib.lr.material.shader = Shader.Find("GUI/Text Shader");
								}
								GunLib.lr.SetPosition(0, transform.position);
								GunLib.lr.SetPosition(1, raycastHit.point);
							}
							GunLib.data.hitPosition = raycastHit.point;
							GunLib.pointer.transform.position = raycastHit.point;
							VRRig componentInParent = raycastHit.collider.GetComponentInParent<VRRig>();
							bool flag6 = componentInParent != null;
							if (flag6)
							{
								bool isTriggered = GunLib.data.isTriggered;
								if (isTriggered)
								{
									GunLib.data.lockedPlayer = componentInParent;
									GunLib.data.isLocked = true;
									bool gunLine2 = Settings.GunLine;
									if (gunLine2)
									{
										GunLib.lr.startColor = Color.blue;
										GunLib.lr.endColor = Color.blue;
									}
									renderer.material.color = Color.blue;
								}
								else
								{
									GunLib.data.isLocked = false;
									bool gunLine3 = Settings.GunLine;
									if (gunLine3)
									{
										GunLib.lr.startColor = Color.green;
										GunLib.lr.endColor = Color.green;
									}
									renderer.material.color = Color.green;
									GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
								}
							}
							else
							{
								GunLib.data.isLocked = false;
								bool gunLine4 = Settings.GunLine;
								if (gunLine4)
								{
									GunLib.lr.startColor = Color.red;
									GunLib.lr.endColor = Color.red;
								}
								renderer.material.color = Color.red;
							}
						}
						bool flag7 = GunLib.data.isTriggered && GunLib.data.lockedPlayer != null;
						if (flag7)
						{
							GunLib.data.isLocked = true;
							bool gunLine5 = Settings.GunLine;
							if (gunLine5)
							{
								GunLib.lr.SetPosition(0, transform.position);
								GunLib.lr.SetPosition(1, GunLib.data.lockedPlayer.transform.position);
							}
							GunLib.data.hitPosition = GunLib.data.lockedPlayer.transform.position;
							GunLib.pointer.transform.position = GunLib.data.lockedPlayer.transform.position;
							bool gunLine6 = Settings.GunLine;
							if (gunLine6)
							{
								GunLib.lr.startColor = Color.blue;
								GunLib.lr.endColor = Color.blue;
							}
							renderer.material.color = Color.blue;
						}
						else
						{
							bool flag8 = GunLib.data.lockedPlayer != null;
							if (flag8)
							{
								GunLib.data.isLocked = false;
								GunLib.data.lockedPlayer = null;
								bool gunLine7 = Settings.GunLine;
								if (gunLine7)
								{
									GunLib.lr.startColor = Color.red;
									GunLib.lr.endColor = Color.red;
								}
								renderer.material.color = Color.red;
							}
						}
					}
					else
					{
						GunLib.GunCleanUp();
					}
					result = GunLib.data;
				}
				else
				{
					GunLib.data.isShooting = UnityInput.Current.GetMouseButton(1);
					GunLib.data.isTriggered = UnityInput.Current.GetMouseButton(0);
					bool isShooting2 = GunLib.data.isShooting;
					if (isShooting2)
					{
						Renderer renderer2 = (GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null;
						bool flag9 = GunLib.data.lockedPlayer == null && !GunLib.data.isLocked;
						if (flag9)
						{
							Ray ray = (GameObject.Find("Shoulder Camera").GetComponent<Camera>() != null) ? GameObject.Find("Shoulder Camera").GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition) : GorillaTagger.Instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition);
							RaycastHit raycastHit;
							bool flag10 = Physics.Raycast(ray.origin, ray.direction, ref raycastHit) && GunLib.pointer == null;
							if (flag10)
							{
								GunLib.pointer = GameObject.CreatePrimitive(0);
								Object.Destroy(GunLib.pointer.GetComponent<Rigidbody>());
								Object.Destroy(GunLib.pointer.GetComponent<SphereCollider>());
								GunLib.pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
								renderer2 = ((GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null);
								renderer2.material.color = Color.red;
								renderer2.material.shader = Shader.Find("GUI/Text Shader");
							}
							bool gunLine8 = Settings.GunLine;
							if (gunLine8)
							{
								bool flag11 = GunLib.lr == null;
								if (flag11)
								{
									GameObject gameObject2 = new GameObject("line");
									GunLib.lr = gameObject2.AddComponent<LineRenderer>();
									GunLib.lr.endWidth = 0.01f;
									GunLib.lr.startWidth = 0.01f;
									GunLib.lr.material.shader = Shader.Find("GUI/Text Shader");
								}
								GunLib.lr.SetPosition(0, GTPlayer.Instance.headCollider.transform.position);
								GunLib.lr.SetPosition(1, raycastHit.point);
							}
							GunLib.data.hitPosition = raycastHit.point;
							GunLib.pointer.transform.position = raycastHit.point;
							VRRig componentInParent2 = raycastHit.collider.GetComponentInParent<VRRig>();
							bool flag12 = componentInParent2 != null && GunLib.data.lockedPlayer == null;
							if (flag12)
							{
								bool isTriggered2 = GunLib.data.isTriggered;
								if (isTriggered2)
								{
									GunLib.data.lockedPlayer = componentInParent2;
									GunLib.data.isLocked = true;
								}
								else
								{
									GunLib.data.isLocked = false;
									bool gunLine9 = Settings.GunLine;
									if (gunLine9)
									{
										GunLib.lr.startColor = Color.green;
										GunLib.lr.endColor = Color.green;
									}
									renderer2.material.color = Color.green;
								}
							}
							else
							{
								GunLib.data.isLocked = false;
								bool gunLine10 = Settings.GunLine;
								if (gunLine10)
								{
									GunLib.lr.startColor = Color.red;
									GunLib.lr.endColor = Color.red;
								}
								renderer2.material.color = Color.red;
							}
						}
						bool flag13 = renderer2 != null;
						if (flag13)
						{
							bool flag14 = GunLib.data.isTriggered && GunLib.data.lockedPlayer != null;
							if (flag14)
							{
								bool gunLine11 = Settings.GunLine;
								if (gunLine11)
								{
									GunLib.lr.SetPosition(0, GTPlayer.Instance.rightControllerTransform.position);
									GunLib.lr.SetPosition(1, GunLib.data.lockedPlayer.transform.position);
								}
								GunLib.data.hitPosition = GunLib.data.lockedPlayer.transform.position;
								GunLib.pointer.transform.position = GunLib.data.lockedPlayer.transform.position;
								GunLib.data.isLocked = true;
								bool gunLine12 = Settings.GunLine;
								if (gunLine12)
								{
									GunLib.lr.startColor = Color.blue;
									GunLib.lr.endColor = Color.blue;
								}
								renderer2.material.color = Color.blue;
							}
							else
							{
								bool flag15 = GunLib.data.lockedPlayer != null;
								if (flag15)
								{
									GunLib.data.isLocked = false;
									GunLib.data.lockedPlayer = null;
									bool gunLine13 = Settings.GunLine;
									if (gunLine13)
									{
										GunLib.lr.startColor = Color.red;
										GunLib.lr.endColor = Color.red;
									}
									renderer2.material.color = Color.red;
								}
							}
						}
					}
					else
					{
						GunLib.GunCleanUp();
					}
					result = GunLib.data;
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00003E98
		public static GunLib.GunLibData Shoot()
		{
			GunLib.GunLibData result;
			try
			{
				bool isDeviceActive = XRSettings.isDeviceActive;
				if (isDeviceActive)
				{
					bool flag = false;
					bool flag2 = !flag;
					Transform transform;
					if (flag2)
					{
						transform = GTPlayer.Instance.rightControllerTransform;
						GunLib.data.isShooting = ControllerInputPoller.instance.rightGrab;
						GunLib.data.isTriggered = (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f);
					}
					else
					{
						transform = GTPlayer.Instance.leftControllerTransform;
						GunLib.data.isShooting = ControllerInputPoller.instance.leftGrab;
						GunLib.data.isTriggered = (ControllerInputPoller.instance.leftControllerIndexFloat > 0.1f);
					}
					bool isShooting = GunLib.data.isShooting;
					if (isShooting)
					{
						Renderer renderer = (GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null;
						RaycastHit raycastHit;
						bool flag3 = Physics.Raycast(transform.position - transform.up, -transform.up, ref raycastHit) && GunLib.pointer == null;
						if (flag3)
						{
							GunLib.pointer = GameObject.CreatePrimitive(0);
							Object.Destroy(GunLib.pointer.GetComponent<Rigidbody>());
							Object.Destroy(GunLib.pointer.GetComponent<SphereCollider>());
							GunLib.pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
							renderer = ((GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null);
							renderer.material.color = Color.red;
							renderer.material.shader = Shader.Find("GUI/Text Shader");
						}
						bool gunLine = Settings.GunLine;
						if (gunLine)
						{
							bool flag4 = GunLib.lr == null;
							if (flag4)
							{
								GameObject gameObject = new GameObject("line");
								GunLib.lr = gameObject.AddComponent<LineRenderer>();
								GunLib.lr.endWidth = 0.01f;
								GunLib.lr.startWidth = 0.01f;
								GunLib.lr.material.shader = Shader.Find("GUI/Text Shader");
							}
							GunLib.lr.SetPosition(0, transform.position);
							GunLib.lr.SetPosition(1, raycastHit.point);
						}
						GunLib.data.hitPosition = raycastHit.point;
						GunLib.data.RaycastHit = raycastHit;
						GunLib.pointer.transform.position = raycastHit.point;
						VRRig componentInParent = raycastHit.collider.GetComponentInParent<VRRig>();
						bool flag5 = componentInParent != null;
						if (flag5)
						{
							bool isTriggered = GunLib.data.isTriggered;
							if (isTriggered)
							{
								GunLib.data.lockedPlayer = componentInParent;
								GunLib.data.isLocked = true;
								renderer.material.color = Color.blue;
								bool gunLine2 = Settings.GunLine;
								if (gunLine2)
								{
									GunLib.lr.startColor = Color.blue;
									GunLib.lr.endColor = Color.blue;
								}
							}
							else
							{
								bool gunLine3 = Settings.GunLine;
								if (gunLine3)
								{
									GunLib.lr.startColor = Color.green;
									GunLib.lr.endColor = Color.green;
								}
								renderer.material.color = Color.green;
								GunLib.data.isLocked = false;
								GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 3f, GorillaTagger.Instance.tagHapticDuration / 2f);
							}
						}
						else
						{
							bool gunLine4 = Settings.GunLine;
							if (gunLine4)
							{
								GunLib.lr.startColor = Color.red;
								GunLib.lr.endColor = Color.red;
							}
							renderer.material.color = Color.red;
							GunLib.data.isLocked = false;
						}
					}
					else
					{
						GunLib.GunCleanUp();
					}
					result = GunLib.data;
				}
				else
				{
					GunLib.data.isShooting = true;
					GunLib.data.isTriggered = UnityInput.Current.GetMouseButton(0);
					Renderer renderer2 = (GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null;
					Ray ray = (GameObject.Find("Shoulder Camera").GetComponent<Camera>() != null) ? GameObject.Find("Shoulder Camera").GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition) : GorillaTagger.Instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition);
					RaycastHit raycastHit2;
					bool flag6 = Physics.Raycast(ray.origin, ray.direction, ref raycastHit2) && GunLib.pointer == null;
					if (flag6)
					{
						GunLib.pointer = GameObject.CreatePrimitive(0);
						Object.Destroy(GunLib.pointer.GetComponent<Rigidbody>());
						Object.Destroy(GunLib.pointer.GetComponent<SphereCollider>());
						GunLib.pointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
						renderer2 = ((GunLib.pointer != null) ? GunLib.pointer.GetComponent<Renderer>() : null);
						renderer2.material.color = Color.red;
						renderer2.material.shader = Shader.Find("GUI/Text Shader");
					}
					bool gunLine5 = Settings.GunLine;
					if (gunLine5)
					{
						bool flag7 = GunLib.lr == null;
						if (flag7)
						{
							GameObject gameObject2 = new GameObject("line");
							GunLib.lr = gameObject2.AddComponent<LineRenderer>();
							GunLib.lr.endWidth = 0.01f;
							GunLib.lr.startWidth = 0.01f;
							GunLib.lr.material.shader = Shader.Find("GUI/Text Shader");
						}
						GunLib.lr.SetPosition(0, GTPlayer.Instance.headCollider.transform.position);
						GunLib.lr.SetPosition(1, raycastHit2.point);
					}
					GunLib.data.hitPosition = raycastHit2.point;
					GunLib.pointer.transform.position = raycastHit2.point;
					VRRig componentInParent2 = raycastHit2.collider.GetComponentInParent<VRRig>();
					bool flag8 = componentInParent2 != null;
					if (flag8)
					{
						bool isTriggered2 = GunLib.data.isTriggered;
						if (isTriggered2)
						{
							GunLib.data.isLocked = true;
							bool gunLine6 = Settings.GunLine;
							if (gunLine6)
							{
								GunLib.lr.startColor = Color.blue;
								GunLib.lr.endColor = Color.blue;
							}
							renderer2.material.color = Color.blue;
						}
						else
						{
							GunLib.data.isLocked = false;
							bool gunLine7 = Settings.GunLine;
							if (gunLine7)
							{
								GunLib.lr.startColor = Color.green;
								GunLib.lr.endColor = Color.green;
							}
							renderer2.material.color = Color.green;
						}
					}
					else
					{
						GunLib.data.isLocked = false;
						bool gunLine8 = Settings.GunLine;
						if (gunLine8)
						{
							GunLib.lr.startColor = Color.red;
							GunLib.lr.endColor = Color.red;
						}
						renderer2.material.color = Color.red;
					}
					result = GunLib.data;
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x04000022 RID: 34
		private static GameObject pointer;

		// Token: 0x04000023 RID: 35
		private static LineRenderer lr;

		// Token: 0x04000024 RID: 36
		private static GunLib.GunLibData data = new GunLib.GunLibData(false, false, false, null, default(Vector3), default(RaycastHit));

		// Token: 0x02000042 RID: 66
		public class GunLibData
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600020F RID: 527 RVA: 0x0001A535 File Offset: 0x00018735
			// (set) Token: 0x06000210 RID: 528 RVA: 0x0001A53D File Offset: 0x0001873D
			public VRRig lockedPlayer { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000211 RID: 529 RVA: 0x0001A546 File Offset: 0x00018746
			// (set) Token: 0x06000212 RID: 530 RVA: 0x0001A54E File Offset: 0x0001874E
			public bool isShooting { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000213 RID: 531 RVA: 0x0001A557 File Offset: 0x00018757
			// (set) Token: 0x06000214 RID: 532 RVA: 0x0001A55F File Offset: 0x0001875F
			public bool isLocked { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000215 RID: 533 RVA: 0x0001A568 File Offset: 0x00018768
			// (set) Token: 0x06000216 RID: 534 RVA: 0x0001A570 File Offset: 0x00018770
			public Vector3 hitPosition { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000217 RID: 535 RVA: 0x0001A579 File Offset: 0x00018779
			// (set) Token: 0x06000218 RID: 536 RVA: 0x0001A581 File Offset: 0x00018781
			public GameObject hitPointer { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000219 RID: 537 RVA: 0x0001A58A File Offset: 0x0001878A
			// (set) Token: 0x0600021A RID: 538 RVA: 0x0001A592 File Offset: 0x00018792
			public RaycastHit RaycastHit { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600021B RID: 539 RVA: 0x0001A59B File Offset: 0x0001879B
			// (set) Token: 0x0600021C RID: 540 RVA: 0x0001A5A3 File Offset: 0x000187A3
			public bool isTriggered { get; set; }

			// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A5AC
			public GunLibData(bool stateTriggered, bool triggy, bool foundPlayer, VRRig player = null, Vector3 hitpos = default(Vector3), RaycastHit raycastHit = default(RaycastHit))
			{
				this.lockedPlayer = player;
				this.isShooting = stateTriggered;
				this.isLocked = foundPlayer;
				this.hitPosition = hitpos;
				this.isTriggered = triggy;
				this.RaycastHit = raycastHit;
			}
		}
	}
}
