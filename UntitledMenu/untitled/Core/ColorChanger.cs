using System;
using UnityEngine;

namespace untitled.Core
{
	// Token: 0x02000011 RID: 17
	public class ColorChanger : TimedBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BF6F
		public override void Start()
		{
			base.Start();
			this.goRenderer = base.GetComponent<Renderer>();
			this.Update();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BF8C
		public override void Update()
		{
			base.Update();
			float num = Time.time % this.cycleDuration / this.cycleDuration;
			Color color = this.colors.Evaluate(num);
			base.GetComponent<Renderer>().material.color = color;
		}

		// Token: 0x0400007C RID: 124
		public Gradient colors;

		// Token: 0x0400007D RID: 125
		public float cycleDuration = 10f;

		// Token: 0x0400007E RID: 126
		public Renderer goRenderer;

		// Token: 0x0400007F RID: 127
		public Color32 color;

		// Token: 0x04000080 RID: 128
		public bool timeBased = true;
	}
}
