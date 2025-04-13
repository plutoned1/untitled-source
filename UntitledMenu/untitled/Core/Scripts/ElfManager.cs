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
	// Token: 0x0200003B RID: 59
	public class ElfManager : MonoBehaviour
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00019D72 File Offset: 0x00017F72
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00019D79 File Offset: 0x00017F79
		public static ElfManager Instance { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00019D81 File Offset: 0x00017F81
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00019D89 File Offset: 0x00017F89
		public bool ReadyForActions { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00019D92 File Offset: 0x00017F92
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00019D9A File Offset: 0x00017F9A
		public float WonderTiming { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00019DA3 File Offset: 0x00017FA3
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00019DAB File Offset: 0x00017FAB
		public GameObject Elf { get; set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019DB4
		public static IEnumerator DownloadPathing(string fileName)
		{
			string url = "https://raw.githubusercontent.com/gorillauntitled/untitled_elf/refs/heads/main/" + fileName + ".txt";
			UnityWebRequest request = new UnityWebRequest(url, "GET");
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();
			File.WriteAllText(fileName + ".txt", request.downloadHandler.text);
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019DC3
		public static IEnumerator WaitAndRenderElfs()
		{
			while (ElfManager.Instance == null || !UpdateBasedElfsManager.Instance.Initialized)
			{
				yield return new WaitForSeconds(0.1f);
			}
			ElfManager.Instance.StartCoroutine(ElfManager.DownloadPathing("Peaking"));
			ElfManager.Instance.StartCoroutine(ElfManager.DownloadPathing("Fun"));
			ElfManager.Instance.StartCoroutine(ElfManager.DownloadPathing("Wandering_Start"));
			while (!File.Exists("Peaking.txt") || !File.Exists("Fun.txt") || !File.Exists("Wandering_Start.txt"))
			{
				yield return new WaitForSeconds(0.1f);
			}
			ElfManager.Instance.RenderElf(ElfType.Welcome);
			ElfManager.Instance.RenderElf(ElfType.Peaking);
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019DCB
		public IEnumerator InstantiateElf()
		{
			Type vrrigCacheType = typeof(VRRig).Assembly.GetType("VRRigCache");
			PropertyInfo unsafeInstance = vrrigCacheType.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
			while (unsafeInstance.GetValue(null) == null)
			{
				yield return new WaitForSeconds(0.01f);
			}
			this.SetupTemplateRig();
			bool flag = this.Elf != null;
			if (flag)
			{
				int num;
				for (int i = 0; i < 10; i = num + 1)
				{
					VRRig clonedRig = Object.Instantiate<GameObject>(this.Elf).GetComponent<VRRig>();
					clonedRig.gameObject.SetActive(false);
					Object.Destroy(clonedRig.gameObject.transform.Find("VR Constraints"));
					this.ReadyToRenderElfs.Add(clonedRig);
					Debug.Log(i);
					clonedRig = null;
					num = i;
				}
				this.ReadyForActions = true;
			}
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019DDA
		public IEnumerator InitializeElf(VRRig vrrig, string path, bool doSound = false, int soundTick = -1, bool disableAfter = true)
		{
			bool flag = !vrrig.gameObject.activeSelf;
			if (flag)
			{
				vrrig.gameObject.SetActive(true);
			}
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

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019E10
		public void RenderElf(ElfType elf)
		{
			switch (elf)
			{
			case ElfType.Peaking:
				this.RenderSinglerElf("Peaking", false, -1, true);
				break;
			case ElfType.Welcome:
				this.RenderSinglerElf("Fun", true, 40, true);
				break;
			case ElfType.Wandering:
				this.RenderSinglerElf("Wandering_Start", false, -1, true);
				break;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019E6C
		public void RenderSinglerElf(string path, bool doSound = false, int soundTick = -1, bool disableAfter = true)
		{
			bool flag = this.ReadyToRenderElfs.Count == 0;
			if (flag)
			{
				Debug.Log("Unable to render elf for action " + path + ", ran out of rendering elfs!");
			}
			else
			{
				VRRig vrrig = this.ReadyToRenderElfs[Random.Range(0, this.ReadyToRenderElfs.Count - 1)];
				this.ReadyToRenderElfs.Remove(vrrig);
				this.CurrentRenderedElfs.Add(vrrig);
				base.StartCoroutine(this.InitializeElf(vrrig, path, doSound, soundTick, disableAfter));
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019EF4
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
			this.Elf = vrrig.gameObject;
			Object.Destroy(this.Elf.transform.Find("VR Constraints"));
			this.Elf.SetActive(false);
			this.Elf.name = "Untitled Elf";
		}

		// Token: 0x0400013B RID: 315
		public List<VRRig> ReadyToRenderElfs = new List<VRRig>();

		// Token: 0x0400013C RID: 316
		public List<VRRig> CurrentRenderedElfs = new List<VRRig>();
	}
}
