using System;
using TMPro;
using UnityEngine;

namespace untitled.Core.Scripts
{
	// Token: 0x0200002A RID: 42
	public class Boards : MonoBehaviour
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000C803 File Offset: 0x0000AA03
		public static Boards Instance { get; private set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C80C
		private void Awake()
		{
			bool flag = Boards.Instance != null && Boards.Instance != this;
			if (flag)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				Boards.Instance = this;
				this.cocText = GameObject.Find("COC Text").GetComponent<TextMeshPro>();
				this.monitorRenderer = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen").GetComponent<Renderer>();
				this.mat = new Material(Shader.Find("GorillaTag/UberShader"));
				this.currentThemeColor = Menu.BgColor1;
				this.mat.color = this.currentThemeColor;
				this.cocText.text = "Banned People:\n";
				GameObject.Find("CodeOfConduct").GetComponent<TextMeshPro>().text = "Untitled";
				Transform transform = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					GameObject gameObject = transform.GetChild(i).gameObject;
					bool flag2 = gameObject.name.Contains("forestatlas");
					if (flag2)
					{
						gameObject.GetComponent<Renderer>().material = this.mat;
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C940
		private void LateUpdate()
		{
			bool flag = this.currentThemeColor != Menu.BgColor1;
			if (flag)
			{
				this.currentThemeColor = Menu.BgColor1;
				this.mat.color = this.currentThemeColor;
				this.monitorRenderer.material = this.mat;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C9A0
		private void OnDestroy()
		{
			bool flag = this.mat != null;
			if (flag)
			{
				Object.Destroy(this.mat);
			}
		}

		// Token: 0x0400008F RID: 143
		private TextMeshPro cocText;

		// Token: 0x04000090 RID: 144
		private Renderer monitorRenderer;

		// Token: 0x04000091 RID: 145
		private Material mat;

		// Token: 0x04000092 RID: 146
		private Color currentThemeColor;

		// Token: 0x04000093 RID: 147
		private string CurrentStatus;

		// Token: 0x04000094 RID: 148
		private bool needsUpdate;
	}
}
