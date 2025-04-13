using System;
using UnityEngine;

namespace untitled.Core
{
	// Token: 0x02000012 RID: 18
	public class TimedBehaviour : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BFEF
		public virtual void Start()
		{
			this.startTime = Time.time;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C000
		public virtual void Update()
		{
			bool flag = !this.complete;
			if (flag)
			{
				this.progress = Mathf.Clamp((Time.time - this.startTime) / this.duration, 0f, 1f);
				bool flag2 = Time.time - this.startTime > this.duration;
				if (flag2)
				{
					bool flag3 = this.loop;
					if (flag3)
					{
						this.OnLoop();
					}
					else
					{
						this.complete = true;
					}
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C07D
		public virtual void OnLoop()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000081 RID: 129
		public bool complete = false;

		// Token: 0x04000082 RID: 130
		public bool loop = true;

		// Token: 0x04000083 RID: 131
		public float progress = 0f;

		// Token: 0x04000084 RID: 132
		protected bool paused = false;

		// Token: 0x04000085 RID: 133
		protected float startTime;

		// Token: 0x04000086 RID: 134
		protected float duration = 2f;
	}
}
