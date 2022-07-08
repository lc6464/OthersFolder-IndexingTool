namespace 其他人文件夹索引工具 {
	/// <summary>
	/// 储存此应用程序相关信息的静态类。
	/// </summary>
	internal static class OthersIndexer {
		public const string ApplicationVersion = "v0.1.0.0";
		public const string ApplicationName = "其他人文件夹索引工具";
		public const string ApplicationUpdateDate = "2022-03-25";
		public const string ApplicationFullName = $"{ ApplicationName } { ApplicationVersion } { ApplicationUpdateDate }";
	}

	/// <summary>
	/// 一个索引项。
	/// </summary>
	public class OneOtherIndex {
		[JsonPropertyName("GUID")]
		public Guid Guid { get; set; }
		public string? Name { get; set; }
	}

	/// <summary>
	/// 索引项列表。
	/// </summary>
	public class OthersIndexes {
		public OneOtherIndex[]? Index { get; set; }
	}
}