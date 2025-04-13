using System;
using UnityEngine;

namespace untitled.Core
{
	// Token: 0x0200000E RID: 14
	public class ClampColor : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BD4A
		public void Start()
		{
			this.gameObjectRenderer = base.GetComponent<Renderer>();
			this.Update();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BD60
		public void Update()
		{
			this.gameObjectRenderer.material.shader = this.targetRenderer.material.shader;
			this.gameObjectRenderer.material.renderQueue = this.targetRenderer.material.renderQueue;
			this.gameObjectRenderer.material.color = this.targetRenderer.material.color;
		}

		// Token: 0x04000072 RID: 114
		public Renderer gameObjectRenderer;

		// Token: 0x04000073 RID: 115
		public Renderer targetRenderer;
	}
}
