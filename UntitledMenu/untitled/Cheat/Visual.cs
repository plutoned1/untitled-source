using System;
using System.Collections.Generic;
using System.Linq;
using GorillaExtensions;
using HarmonyLib;
using TMPro;
using UnityEngine;
using untitled.Core;
using untitled.Core.Scripts;

namespace untitled.Cheat
{
	// Token: 0x02000036 RID: 54
	public class Visual
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00016D88
		public static void BoxESP2D()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig != GorillaTagger.Instance.offlineVRRig;
				if (flag)
				{
					Color color = Menu.BgColor1;
					GameObject gameObject = GameObject.CreatePrimitive(3);
					gameObject.transform.position = vrrig.transform.position;
					Object.Destroy(gameObject.GetComponent<BoxCollider>());
					gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
					gameObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
					gameObject.GetComponent<Renderer>().enabled = false;
					GameObject gameObject2 = GameObject.CreatePrimitive(3);
					gameObject2.transform.position = vrrig.transform.position + gameObject.transform.up * 0.25f;
					Object.Destroy(gameObject2.GetComponent<BoxCollider>());
					gameObject2.transform.localScale = new Vector3(0.5f, 0.05f, 0f);
					gameObject2.transform.rotation = gameObject.transform.rotation;
					gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
					gameObject2.GetComponent<Renderer>().material.color = color;
					Object.Destroy(gameObject2, Time.deltaTime);
					gameObject2 = GameObject.CreatePrimitive(3);
					gameObject2.transform.position = vrrig.transform.position + gameObject.transform.up * -0.25f;
					Object.Destroy(gameObject2.GetComponent<BoxCollider>());
					gameObject2.transform.localScale = new Vector3(0.55f, 0.05f, 0f);
					gameObject2.transform.rotation = gameObject.transform.rotation;
					gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
					gameObject2.GetComponent<Renderer>().material.color = color;
					Object.Destroy(gameObject2, Time.deltaTime);
					gameObject2 = GameObject.CreatePrimitive(3);
					gameObject2.transform.position = vrrig.transform.position + gameObject.transform.right * 0.25f;
					Object.Destroy(gameObject2.GetComponent<BoxCollider>());
					gameObject2.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
					gameObject2.transform.rotation = gameObject.transform.rotation;
					gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
					gameObject2.GetComponent<Renderer>().material.color = color;
					Object.Destroy(gameObject2, Time.deltaTime);
					gameObject2 = GameObject.CreatePrimitive(3);
					gameObject2.transform.position = vrrig.transform.position + gameObject.transform.right * -0.25f;
					Object.Destroy(gameObject2.GetComponent<BoxCollider>());
					gameObject2.transform.localScale = new Vector3(0.05f, 0.55f, 0f);
					gameObject2.transform.rotation = gameObject.transform.rotation;
					gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
					gameObject2.GetComponent<Renderer>().material.color = color;
					Object.Destroy(gameObject2, Time.deltaTime);
					Object.Destroy(gameObject);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017194
		public static void BoxEspOn3D()
		{
			Visual.ESPManager.Execute();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001719D
		public static void BoxEspOff3D()
		{
			Visual.ESPManager.Disable();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000171A8
		public static void FullBoxESP()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					GameObject gameObject = GameObject.CreatePrimitive(3);
					Object.Destroy(gameObject.GetComponent<Collider>());
					gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
					Ghost.Hologram(gameObject.GetComponent<Renderer>().material);
					gameObject.GetComponent<Renderer>().material.color = new Color32(Menu.BgColor1.r, Menu.BgColor1.g, Menu.BgColor1.b, 100);
					gameObject.transform.position = vrrig.transform.position;
					gameObject.transform.localScale = new Vector3(0.4f, 0.7f, 0.4f);
					Object.Destroy(gameObject, Time.deltaTime);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000172D4
		public static void BoneESP()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					LineRenderer orAddComponent = GTExt.GetOrAddComponent<LineRenderer>(vrrig.head.rigTarget.gameObject);
					orAddComponent.startWidth = 0.025f;
					orAddComponent.endWidth = 0.025f;
					orAddComponent.startColor = Menu.BgColor1;
					orAddComponent.endColor = Menu.BgColor2;
					orAddComponent.material.shader = Shader.Find("GUI/Text Shader");
					orAddComponent.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
					orAddComponent.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
					for (int i = 0; i < Menu.bones.Count<int>(); i += 2)
					{
						LineRenderer orAddComponent2 = GTExt.GetOrAddComponent<LineRenderer>(vrrig.mainSkin.bones[Menu.bones[i]].gameObject);
						orAddComponent2.startWidth = 0.025f;
						orAddComponent2.endWidth = 0.025f;
						orAddComponent2.startColor = Menu.BgColor1;
						orAddComponent2.endColor = Menu.BgColor2;
						orAddComponent2.material.shader = Shader.Find("GUI/Text Shader");
						orAddComponent2.SetPosition(0, vrrig.mainSkin.bones[Menu.bones[i]].position);
						orAddComponent2.SetPosition(1, vrrig.mainSkin.bones[Menu.bones[i + 1]].position);
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017504
		public static void BoneESPOff()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					LineRenderer component = vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>();
					bool flag2 = component != null;
					if (flag2)
					{
						Object.Destroy(component);
					}
					for (int i = 0; i < Menu.bones.Count<int>(); i += 2)
					{
						LineRenderer component2 = vrrig.mainSkin.bones[Menu.bones[i]].gameObject.GetComponent<LineRenderer>();
						bool flag3 = component2 != null;
						if (flag3)
						{
							Object.Destroy(component2);
						}
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000175F8
		public static void TracerESP()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					LineRenderer orAddComponent = GTExt.GetOrAddComponent<LineRenderer>(vrrig.head.rigTarget.gameObject);
					orAddComponent.startWidth = 0.0075f;
					orAddComponent.endWidth = 0.0075f;
					orAddComponent.startColor = Menu.BgColor2;
					orAddComponent.endColor = Menu.BgColor1;
					orAddComponent.material.shader = Shader.Find("GUI/Text Shader");
					orAddComponent.SetPosition(0, Global.TrueRightHand().Item1);
					orAddComponent.SetPosition(1, vrrig.head.rigTarget.transform.position);
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000176FC
		public static void TracerESPOff()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					LineRenderer component = vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>();
					bool flag2 = component != null;
					if (flag2)
					{
						Object.Destroy(component);
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017790
		public static void ChamESPOn()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
					vrrig.mainSkin.material.color = Menu.BgColor2;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001782C
		public static void ChamESPOff()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = !vrrig.isOfflineVRRig;
				if (flag)
				{
					vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000178B0
		public static void BreadCrumbs()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig == GorillaTagger.Instance.offlineVRRig;
				if (!flag)
				{
					Material material = new Material(Shader.Find("GUI/Text Shader"));
					bool flag2 = !vrrig.gameObject.GetComponent<TrailRenderer>();
					if (flag2)
					{
						vrrig.gameObject.AddComponent<TrailRenderer>();
					}
					vrrig.gameObject.GetComponent<TrailRenderer>();
					vrrig.GetComponent<TrailRenderer>().startWidth = 0.1f;
					vrrig.GetComponent<TrailRenderer>().endWidth = 0f;
					vrrig.GetComponent<TrailRenderer>().material = material;
					vrrig.GetComponent<TrailRenderer>().startColor = Menu.BgColor1;
					vrrig.GetComponent<TrailRenderer>().endColor = Menu.BgColor2;
					vrrig.GetComponent<TrailRenderer>().time = 1f;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000179D4
		public static void DisableBreadCrumbs()
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag = vrrig.gameObject.GetComponent<TrailRenderer>() != null;
				if (flag)
				{
					Object.Destroy(vrrig.gameObject.GetComponent<TrailRenderer>());
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017A54
		public static void DistanceESP()
		{
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017A58
		public static void InfoTag()
		{
			bool flag = Visual.tag.Count > 1;
			if (flag)
			{
				foreach (KeyValuePair<VRRig, GameObject> keyValuePair in Visual.tag)
				{
					bool flag2 = !GorillaParent.instance.vrrigs.Contains(keyValuePair.Key);
					if (flag2)
					{
						Object.Destroy(keyValuePair.Value);
						Visual.tag.Remove(keyValuePair.Key);
					}
				}
			}
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				bool flag3 = !vrrig.isOfflineVRRig && vrrig != null;
				if (flag3)
				{
					bool flag4 = !Visual.tag.ContainsKey(vrrig);
					if (flag4)
					{
						GameObject gameObject = new GameObject("info Tags");
						gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
						Visual.texttag = gameObject.AddComponent<TextMesh>();
						Visual.texttag.fontSize = 40;
						Visual.texttag.characterSize = 0.08f;
						Visual.texttag.alignment = 0;
						Visual.texttag.font = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").GetComponent<TextMeshPro>().font.sourceFontFile;
						Visual.tag.Add(vrrig, gameObject);
					}
					else
					{
						GameObject gameObject2 = Visual.tag[vrrig];
						TextMesh component = gameObject2.GetComponent<TextMesh>();
						string text = vrrig.concatStringOfCosmeticsAllowed.Contains("FIRST LOGIN") ? "<color=blue>STEAM</color>" : "<color=gray>OCULUS</color>";
						int value = Traverse.Create(vrrig).Field("fps").GetValue<int>();
						string text2 = value.ToString();
						bool flag5 = value < 30;
						if (flag5)
						{
							text2 = "<color=red>" + value.ToString() + "</color>";
						}
						bool flag6 = value > 30 && value < 60;
						if (flag6)
						{
							text2 = "<color=yellow>" + value.ToString() + "</color>";
						}
						bool flag7 = value > 60;
						if (flag7)
						{
							text2 = "<color=green>" + value.ToString() + "</color>";
						}
						bool flag8 = value > 100;
						if (flag8)
						{
							text2 = "<color=cyan>" + value.ToString() + "</color>";
						}
						component.text = string.Concat(new string[]
						{
							"USERNAME : ",
							vrrig.Creator.NickName,
							"\n ID : ",
							vrrig.Creator.UserId,
							"\n PLATFORM : ",
							text,
							"\n FPS : ",
							text2
						});
						gameObject2.transform.position = vrrig.headMesh.transform.position + vrrig.headMesh.transform.up * 0.4f;
						gameObject2.transform.LookAt(Camera.main.transform.position);
						gameObject2.transform.Rotate(0f, 180f, 0f);
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017E00
		public static void DisableInfoTags()
		{
			foreach (KeyValuePair<VRRig, GameObject> keyValuePair in Visual.tag)
			{
				Object.Destroy(keyValuePair.Value);
			}
			Visual.tag.Clear();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017E68
		public static void LowQuality()
		{
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017E6B
		public static void OldGraphics()
		{
		}

		// Token: 0x04000125 RID: 293
		public static GameObject taggo3;

		// Token: 0x04000126 RID: 294
		public static List<GameObject> taggot = new List<GameObject>();

		// Token: 0x04000127 RID: 295
		public static TextMesh texttag;

		// Token: 0x04000128 RID: 296
		public static Dictionary<VRRig, GameObject> tag = new Dictionary<VRRig, GameObject>();

		// Token: 0x02000055 RID: 85
		[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
		public class WFCFing : MonoBehaviour
		{
			// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001B920
			public void Start()
			{
				this.mesh = new Mesh();
				base.GetComponent<MeshFilter>().mesh = this.mesh;
				this.cwfb();
				base.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
				base.GetComponent<Renderer>().material.color = Menu.BgColor2;
				Object.Destroy(base.GetComponent<BoxCollider>());
				base.transform.localScale = new Vector3(0.35f, 0.705f, 0.35f);
			}

			// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001B9B4
			private void cwfb()
			{
				Vector3[] vertices = new Vector3[]
				{
					new Vector3(-0.5f, -0.5f, -0.5f),
					new Vector3(0.5f, -0.5f, -0.5f),
					new Vector3(0.5f, 0.5f, -0.5f),
					new Vector3(-0.5f, 0.5f, -0.5f),
					new Vector3(-0.5f, -0.5f, 0.5f),
					new Vector3(0.5f, -0.5f, 0.5f),
					new Vector3(0.5f, 0.5f, 0.5f),
					new Vector3(-0.5f, 0.5f, 0.5f)
				};
				int[] array = new int[]
				{
					0,
					1,
					1,
					2,
					2,
					3,
					3,
					0,
					4,
					5,
					5,
					6,
					6,
					7,
					7,
					4,
					0,
					4,
					1,
					5,
					2,
					6,
					3,
					7
				};
				this.mesh.vertices = vertices;
				this.mesh.SetIndices(array, 3, 0);
			}

			// Token: 0x040001C3 RID: 451
			public Mesh mesh;

			// Token: 0x040001C4 RID: 452
			public static Material wireframeMaterial;

			// Token: 0x040001C5 RID: 453
			public static float time;

			// Token: 0x040001C6 RID: 454
			public GameObject frameParent;
		}

		// Token: 0x02000056 RID: 86
		public class ESPManager : MonoBehaviour
		{
			// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001BADC
			public static void Disable()
			{
				foreach (KeyValuePair<VRRig, Visual.WFCFing> keyValuePair in Visual.ESPManager.pairs.ToArray<KeyValuePair<VRRig, Visual.WFCFing>>())
				{
					bool flag = keyValuePair.Value != null;
					if (flag)
					{
						bool flag2 = keyValuePair.Value.GetComponent<MeshFilter>();
						if (flag2)
						{
							keyValuePair.Value.GetComponent<MeshFilter>().mesh = null;
						}
						bool flag3 = keyValuePair.Value.mesh;
						if (flag3)
						{
							Object.Destroy(keyValuePair.Value.mesh);
						}
						bool flag4 = keyValuePair.Value.frameParent;
						if (flag4)
						{
							Object.Destroy(keyValuePair.Value.frameParent);
						}
						Visual.ESPManager.pairs.Remove(keyValuePair.Key);
					}
				}
			}

			// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001BBC0
			public static void Execute()
			{
				bool flag = Camera.main;
				if (flag)
				{
					Camera.main.cullingMask = -1;
				}
				foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
				{
					bool flag2 = vrrig != null && !vrrig.isOfflineVRRig;
					if (flag2)
					{
						bool flag3 = Visual.ESPManager.pairs.ContainsKey(vrrig);
						if (flag3)
						{
							bool flag4 = Visual.ESPManager.pairs[vrrig].frameParent;
							if (flag4)
							{
								bool flag5 = Visual.ESPManager.pairs[vrrig].frameParent.transform.parent != vrrig.transform;
								if (flag5)
								{
									Visual.ESPManager.pairs[vrrig].frameParent.transform.SetParent(vrrig.transform);
								}
							}
						}
						else
						{
							GameObject gameObject = GameObject.CreatePrimitive(3);
							Visual.WFCFing wfcfing = gameObject.AddComponent<Visual.WFCFing>();
							Visual.ESPManager.pairs.Add(vrrig, wfcfing);
							wfcfing.frameParent = gameObject;
							wfcfing.frameParent.transform.SetParent(vrrig.transform);
							wfcfing.frameParent.transform.localPosition = Vector3.zero;
						}
					}
					else
					{
						bool flag6 = Visual.ESPManager.pairs.ContainsKey(vrrig);
						if (flag6)
						{
							Visual.ESPManager.pairs[vrrig].GetComponent<MeshFilter>().mesh = null;
							Object.Destroy(Visual.ESPManager.pairs[vrrig].mesh);
							Object.Destroy(Visual.ESPManager.pairs[vrrig].frameParent);
							Visual.ESPManager.pairs.Remove(vrrig);
						}
					}
				}
			}

			// Token: 0x040001C7 RID: 455
			private Dictionary<VRRig, GameObject> rigToWireframeMap = new Dictionary<VRRig, GameObject>();

			// Token: 0x040001C8 RID: 456
			public static GameObject ESPGo;

			// Token: 0x040001C9 RID: 457
			public static Dictionary<VRRig, Visual.WFCFing> pairs = new Dictionary<VRRig, Visual.WFCFing>();
		}
	}
}
