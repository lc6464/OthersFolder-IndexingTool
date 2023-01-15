using System.Reflection;

namespace 其他人文件夹索引工具;

/// <summary>
/// 储存此应用程序相关信息的静态类。
/// </summary>
internal static class OthersIndexer {
	public static readonly Assembly ApplicationAssembly = typeof(OthersIndexer).Assembly;
	public static readonly string ApplicationVersion = ApplicationAssembly.GetName().Version!.ToString();
	public static readonly string ApplicationCopyright = ApplicationAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>()!.Copyright;
	public const string ApplicationName = "其他人文件夹索引工具";
	public static readonly DateTime ApplicationUpdateDate = new(2023, 1, 6);
	public static readonly string ApplicationFullName = $"{ ApplicationName } { ApplicationVersion } { ApplicationUpdateDate }";
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