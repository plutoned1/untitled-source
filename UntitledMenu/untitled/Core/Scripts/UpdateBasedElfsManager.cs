using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using untitled.Assets;

namespace Untitled.Core.Scripts
{
	// Token: 0x0200003E RID: 62
	public class UpdateBasedElfsManager : MonoBehaviour
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0001A11F File Offset: 0x0001831F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0001A126 File Offset: 0x00018326
		public static UpdateBasedElfsManager Instance { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0001A130 File Offset: 0x00018330
		public bool IsWondering
		{
			get
			{
				return this.CachedRigs != null && this.CachedRigs[2].gameObject.activeSelf;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A163
		public void Start()
		{
			UpdateBasedElfsManager.Instance = this;
			base.StartCoroutine(this.InstantiatePlayerWhenInstanceInitialized());
			base.StartCoroutine(UpdateBasedElfsManager.WaitAndInitializeCallSetup());
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A188
		public void Update()
		{
			bool flag = this.lastWonderTime == 0f;
			if (flag)
			{
				this.RenderElf(UpdateBasedElfsManager.ElfType.Wandering);
				this.lastWonderTime = Time.time + 220f;
			}
			else
			{
				bool flag2 = this.lastWonderTime < Time.time && !this.IsWondering;
				if (flag2)
				{
					this.RenderElf(UpdateBasedElfsManager.ElfType.Wandering);
					this.lastWonderTime = Time.time + 220f;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A1FE
		public static IEnumerator GetElfPathingFromService(string fileName, Action<string> onGot)
		{
			string url = "https://raw.githubusercontent.com/gorillauntitled/untitled_elf/refs/heads/main/" + fileName + ".txt";
			UnityWebRequest request = new UnityWebRequest(url, "GET");
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();
			onGot(request.downloadHandler.text);
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A214
		public void RenderElf(UpdateBasedElfsManager.ElfType elf)
		{
			switch (elf)
			{
			case UpdateBasedElfsManager.ElfType.Peaking:
				base.StartCoroutine(this.DoRigPathing(this.CachedRigs[0], "Peaking", false, -1, true));
				break;
			case UpdateBasedElfsManager.ElfType.Welcome:
				base.StartCoroutine(this.DoRigPathing(this.CachedRigs[1], "Fun", true, 40, true));
				break;
			case UpdateBasedElfsManager.ElfType.Wandering:
				base.StartCoroutine(this.DoRigPathing(this.CachedRigs[2], "Wandering_Start", false, -1, true));
				break;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A2A4
		public void SetupTemplateRig()
		{
			VRRig vrrig = Object.Instantiate<VRRig>(GorillaTagger.Instance.offlineVRRig);
			vrrig.rightHandPlayer.enabled = false;
			vrrig.leftHandPlayer.enabled = false;
			vrrig.enabled = false;
			foreach (AudioSource audioSource in vrrig.gameObject.GetComponentsInChildren<AudioSource>())
			{
				Object.Destroy(audioSource);
			}
			this.TemplateEmptyRig = vrrig.gameObject;
			Object.Destroy(this.TemplateEmptyRig.transform.Find("VR Constraints"));
			this.TemplateEmptyRig.SetActive(false);
			this.TemplateEmptyRig.name = "Untitled Elf";
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A35D
		public IEnumerator InstantiatePlayerWhenInstanceInitialized()
		{
			Type vrrigCacheType = typeof(VRRig).Assembly.GetType("VRRigCache");
			PropertyInfo unsafeInstance = vrrigCacheType.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
			while (unsafeInstance.GetValue(null) == null)
			{
				yield return new WaitForSeconds(0.01f);
			}
			this.SetupTemplateRig();
			bool flag = this.TemplateEmptyRig != null;
			if (flag)
			{
				int num;
				for (int i = 0; i < 10; i = num + 1)
				{
					VRRig nigger = Object.Instantiate<GameObject>(this.TemplateEmptyRig).GetComponent<VRRig>();
					nigger.gameObject.SetActive(false);
					Object.Destroy(nigger.gameObject.transform.Find("VR Constraints"));
					this.CachedRigs.Add(nigger);
					Debug.Log(i);
					this.Initialized = true;
					nigger = null;
					num = i;
				}
			}
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A36C
		public IEnumerator DoRigPathing(VRRig vrrig, string path, bool doSound = false, int soundTick = -1, bool disableAfter = true)
		{
			bool flag = !vrrig.gameObject.activeSelf;
			if (flag)
			{
				vrrig.gameObject.SetActive(true);
			}
			vrrig.myDefaultSkinMaterialInstance.color = new Color(Random.Range(0f, 0.9f), Random.Range(0f, 0.9f), Random.Range(0f, 0.9f));
			vrrig.playerText1.text = "Untitled Elf";
			vrrig.enabled = false;
			vrrig.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			string[] lines = File.ReadAllLines(path + ".txt");
			float previousTime = 0f;
			int tick = 0;
			bool did = false;
			foreach (string line in lines)
			{
				JArray array = JsonConvert.DeserializeObject<JArray>(line);
				float timestamp = array[0].ToObject<float>();
				Vector3 Bpos = array[1].ToObject<Vector3>();
				Quaternion Brot = array[2].ToObject<Quaternion>();
				Vector3 rHandPos = array[3].ToObject<Vector3>();
				Quaternion rHandRot = array[4].ToObject<Quaternion>();
				Vector3 lHandPos = array[5].ToObject<Vector3>();
				Quaternion lHandRot = array[6].ToObject<Quaternion>();
				Vector3 headPos = array[7].ToObject<Vector3>();
				Quaternion headRot = array[8].ToObject<Quaternion>();
				float loudness = array[9].ToObject<float>();
				yield return new WaitForSeconds(timestamp - previousTime);
				int num = tick;
				tick = num + 1;
				previousTime = timestamp;
				vrrig.transform.position = Bpos;
				vrrig.transform.rotation = Brot;
				vrrig.rightHandPlayer.transform.position = rHandPos;
				vrrig.rightHandPlayer.transform.rotation = rHandRot;
				vrrig.leftHandPlayer.transform.position = lHandPos;
				vrrig.leftHandPlayer.transform.rotation = lHandRot;
				vrrig.headConstraint.transform.position = headPos;
				vrrig.headConstraint.transform.rotation = headRot;
				vrrig.IsMicEnabled = true;
				if (doSound)
				{
					bool flag2 = soundTick != -1;
					if (flag2)
					{
						bool flag3 = tick >= soundTick && !did;
						if (flag3)
						{
							AssetLoader.PlayClick("HaveFun.wav");
							did = true;
						}
					}
					else
					{
						bool flag4 = !did;
						if (flag4)
						{
							AssetLoader.PlayClick("HaveFun.wav");
						}
						did = true;
					}
				}
				Traverse.Create(vrrig).Field("speakingLoudness").SetValue(loudness);
				array = null;
				Bpos = default(Vector3);
				Brot = default(Quaternion);
				rHandPos = default(Vector3);
				rHandRot = default(Quaternion);
				lHandPos = default(Vector3);
				lHandRot = default(Quaternion);
				headPos = default(Vector3);
				headRot = default(Quaternion);
				line = null;
			}
			string[] array2 = null;
			if (disableAfter)
			{
				vrrig.gameObject.SetActive(false);
			}
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A3A0
		public static IEnumerator WaitAndInitializeCallSetup()
		{
			while (UpdateBasedElfsManager.Instance == null || !UpdateBasedElfsManager.Instance.Initialized)
			{
				yield return new WaitForSeconds(0.1f);
			}
			UpdateBasedElfsManager.Instance.StartCoroutine(UpdateBasedElfsManager.GetElfPathingFromService("Peaking", delegate(string str)
			{
				File.WriteAllText("Peaking.txt", str);
			}));
			UpdateBasedElfsManager.Instance.StartCoroutine(UpdateBasedElfsManager.GetElfPathingFromService("Fun", delegate(string str)
			{
				File.WriteAllText("Fun.txt", str);
			}));
			UpdateBasedElfsManager.Instance.StartCoroutine(UpdateBasedElfsManager.GetElfPathingFromService("Wandering_Start", delegate(string str)
			{
				File.WriteAllText("Wandering_Start.txt", str);
			}));
			while (!File.Exists("Peaking.txt") || !File.Exists("Fun.txt") || !File.Exists("Wandering_Start.txt"))
			{
				yield return new WaitForSeconds(0.1f);
			}
			Debug.Log("[ELF MANAGER]: Instance & 3 backend elfs enabled!");
			UpdateBasedElfsManager.Instance.RenderElf(UpdateBasedElfsManager.ElfType.Welcome);
			UpdateBasedElfsManager.Instance.RenderElf(UpdateBasedElfsManager.ElfType.Peaking);
			yield break;
		}

		// Token: 0x04000146 RID: 326
		public List<VRRig> CachedRigs = new List<VRRig>();

		// Token: 0x04000148 RID: 328
		public float lastWonderTime;

		// Token: 0x04000149 RID: 329
		public string Peaking;

		// Token: 0x0400014A RID: 330
		public string Welcome;

		// Token: 0x0400014B RID: 331
		public string Wandering;

		// Token: 0x0400014C RID: 332
		public GameObject TemplateEmptyRig;

		// Token: 0x0400014D RID: 333
		public bool Initialized = false;

		// Token: 0x02000069 RID: 105
		public enum ElfType
		{
			// Token: 0x0400022C RID: 556
			Peaking,
			// Token: 0x0400022D RID: 557
			Welcome,
			// Token: 0x0400022E RID: 558
			Wandering
		}
	}
}
