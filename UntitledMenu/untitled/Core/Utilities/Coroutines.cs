using System;
using System.Collections;
using UnityEngine;

namespace untitled.Core.Utilities
{
	// Token: 0x02000014 RID: 20
	public class Coroutines : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C474
		private void Awake()
		{
			Coroutines.instance = this;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C480
		public static Coroutine RunCoroutine(IEnumerator enumerator)
		{
			return Coroutines.instance.StartCoroutine(enumerator);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000C49D
		public static void EndCoroutine(Coroutine enumerator)
		{
			Coroutines.instance.StopCoroutine(enumerator);
		}

		// Token: 0x0400008A RID: 138
		public static Coroutines instance;
	}
}
