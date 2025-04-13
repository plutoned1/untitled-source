using System;
using System.IO;
using System.Media;
using System.Reflection;
using UnityEngine;

namespace untitled.Assets
{
	// Token: 0x02000007 RID: 7
	public class AssetLoader
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002C6C
		public static void LoadCube()
		{
			bool flag = AssetLoader.gradientCube == null;
			if (flag)
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UntitledMenu.Assets.gradient");
				AssetBundle assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
				AssetLoader.gradientCube = (assetBundle.LoadAsset("Cube") as GameObject);
				Object.Destroy(AssetLoader.gradientCube.GetComponent<Collider>());
				Object.Destroy(AssetLoader.gradientCube.GetComponent<Rigidbody>());
				Object.Destroy(AssetLoader.gradientCube.GetComponent<BoxCollider>());
				AssetLoader.gradientCube.transform.position = Vector3.zero;
				AssetLoader.gradientCube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002D24
		public static byte[] GetEmbeddedResourceBytes(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			byte[] result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName))
			{
				bool flag = manifestResourceStream == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						manifestResourceStream.CopyTo(memoryStream);
						result = memoryStream.ToArray();
					}
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002DA0
		public static void PlayClick(string name)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Untitled.Assets." + name))
			{
				bool flag = manifestResourceStream != null;
				if (flag)
				{
					AssetLoader.soundPlayer = new SoundPlayer(manifestResourceStream);
					AssetLoader.soundPlayer.Play();
				}
				else
				{
					Console.WriteLine("either you didnt embedd it as a resource or its not a .wav OR you didnt put the file path correctly");
				}
			}
		}

		// Token: 0x0400001E RID: 30
		public static GameObject gradientCube;

		// Token: 0x0400001F RID: 31
		public static SoundPlayer soundPlayer;
	}
}
