using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace KeyAuth
{
	// Token: 0x02000039 RID: 57
	public class json_wrapper
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019CA0
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019CC0
		public json_wrapper(object obj_to_work_with)
		{
			this.current_object = obj_to_work_with;
			Type type = this.current_object.GetType();
			this.serializer = new DataContractJsonSerializer(type);
			bool flag = !json_wrapper.is_serializable(type);
			if (flag)
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.current_object));
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019D18
		public object string_to_object(string json)
		{
			byte[] bytes = Encoding.Default.GetBytes(json);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = this.serializer.ReadObject(memoryStream);
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019D64
		public T string_to_generic<T>(string json)
		{
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x04000134 RID: 308
		private DataContractJsonSerializer serializer;

		// Token: 0x04000135 RID: 309
		private object current_object;
	}
}
