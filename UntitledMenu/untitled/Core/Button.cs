using System;

namespace untitled.Core
{
	// Token: 0x0200000F RID: 15
	public class Button
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000BDDA File Offset: 0x00009FDA
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000BDE2 File Offset: 0x00009FE2
		public string Name { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000BDEB File Offset: 0x00009FEB
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000BDF3 File Offset: 0x00009FF3
		public string ToolTip { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000BDFC File Offset: 0x00009FFC
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000BE04 File Offset: 0x0000A004
		public bool IsToggle { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000BE0D File Offset: 0x0000A00D
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000BE15 File Offset: 0x0000A015
		public bool Enabled { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000BE1E File Offset: 0x0000A01E
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000BE26 File Offset: 0x0000A026
		public bool DisableBool { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000BE2F File Offset: 0x0000A02F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000BE37 File Offset: 0x0000A037
		public Action OnEnable { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000BE40 File Offset: 0x0000A040
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000BE48 File Offset: 0x0000A048
		public Action OnDisable { get; set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BE51
		public Button(string Name, bool IsToggle, Action OnEnable, Action OnDisable = null, string tip = null, bool Enabled = false)
		{
			this.Name = Name;
			this.IsToggle = IsToggle;
			this.OnDisable = OnDisable;
			this.OnEnable = OnEnable;
			this.ToolTip = tip;
			this.Enabled = Enabled;
		}
	}
}
