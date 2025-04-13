using System;
using GorillaLocomotion;
using UnityEngine;
using untitled.Cheat;

namespace untitled.Core.Scripts
{
	// Token: 0x0200002B RID: 43
	public class Ghost : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C9D8
		public static void Hologram(Material material)
		{
			material.SetFloat("_Mode", 3f);
			material.SetInt("_SrcBlend", 5);
			material.SetInt("_DstBlend", 10);
			material.SetInt("_ZWrite", 0);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			material.renderQueue = 3000;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000CA4F File Offset: 0x0000AC4F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000CA56 File Offset: 0x0000AC56
		public static Ghost Instance { get; private set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CA5E
		private void Awake()
		{
			Ghost.Instance = this;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CA68
		private void CreateCubes()
		{
			this.handR = GameObject.CreatePrimitive(3);
			Object.Destroy(this.handR.GetComponent<Collider>());
			Object.Destroy(this.handR.GetComponent<Rigidbody>());
			this.handR.transform.localScale = Vector3.one * 0.063f;
			this.handR.transform.position = GTPlayer.Instance.rightControllerTransform.position;
			this.handR.transform.rotation = GTPlayer.Instance.rightControllerTransform.rotation;
			this.handL = GameObject.CreatePrimitive(3);
			Object.Destroy(this.handL.GetComponent<Collider>());
			Object.Destroy(this.handL.GetComponent<Rigidbody>());
			this.handL.transform.localScale = Vector3.one * 0.063f;
			this.handL.transform.position = GTPlayer.Instance.leftControllerTransform.position;
			this.handL.transform.rotation = GTPlayer.Instance.leftControllerTransform.rotation;
			this.handR.GetComponent<Renderer>().material.color = Menu.BgColor1;
			this.handL.GetComponent<Renderer>().material.color = Menu.BgColor1;
			Ghost.Hologram(this.handR.GetComponent<Renderer>().material);
			Ghost.Hologram(this.handL.GetComponent<Renderer>().material);
			Object.Destroy(this.handR, Time.deltaTime);
			Object.Destroy(this.handL, Time.deltaTime);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CC20
		private void LateUpdate()
		{
			bool ghostview = Settings.Ghostview;
			if (ghostview)
			{
				bool flag = !GorillaTagger.Instance.offlineVRRig.enabled;
				if (flag)
				{
					this.CreateCubes();
				}
			}
		}

		// Token: 0x04000096 RID: 150
		private GameObject handL;

		// Token: 0x04000097 RID: 151
		private GameObject handR;

		// Token: 0x04000098 RID: 152
		private Material InstantiatedPlayerMaterial;
	}
}
