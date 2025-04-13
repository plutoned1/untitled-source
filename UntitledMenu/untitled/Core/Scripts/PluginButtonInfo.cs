using System;

namespace Untitled.Core.Scripts
{
	// Token: 0x0200003C RID: 60
	public class PluginButtonInfo
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00019FC1 File Offset: 0x000181C1
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00019FC9 File Offset: 0x000181C9
		public string Name { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00019FD2 File Offset: 0x000181D2
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00019FDA File Offset: 0x000181DA
		public bool IsToggle { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00019FE3 File Offset: 0x000181E3
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00019FEB File Offset: 0x000181EB
		public bool IsToggled { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00019FF4 File Offset: 0x000181F4
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00019FFC File Offset: 0x000181FC
		public object ModInstance { get; set; }
	}
}
