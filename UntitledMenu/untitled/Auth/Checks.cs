using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Untitled.Auth
{
	// Token: 0x02000040 RID: 64
	public class Checks : MonoBehaviour
	{
		// Token: 0x0600020C RID: 524
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A424
		public static void BlockCheck()
		{
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (Assembly assembly in assemblies)
				{
					bool flag = assembly.GetName().Name == "BloopBonk18";
					if (flag)
					{
						Type type = assembly.GetType("AuthFlag");
						FieldInfo field = type.GetField("Token");
						string a = (string)field.GetValue(null);
						bool flag2 = a != "XJ-91$Zk";
						if (flag2)
						{
							Checks.MessageBox(IntPtr.Zero, "Violation detected #0001: Invalid Token Value", "Error", 16U);
							Environment.Exit(0);
						}
						return;
					}
				}
				Checks.MessageBox(IntPtr.Zero, "Violation Detected #0001: ANF", "Error", 16U);
				Environment.Exit(0);
			}
			catch
			{
				Checks.MessageBox(IntPtr.Zero, "Violation Detected #0002: AE0", "Error", 16U);
				Environment.Exit(0);
			}
		}

		// Token: 0x04000150 RID: 336
		public const uint MB_OK = 0U;

		// Token: 0x04000151 RID: 337
		public const uint MB_ICONERROR = 16U;
	}
}
