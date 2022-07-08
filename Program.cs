// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

if (File.Exists("index.json")) { // 判断索引文件是否存在
	try {
		string index = File.ReadAllText("index.json").Trim(); // 读取索引文件
		try {
			OthersIndexes? indexRoot = JsonSerializer.Deserialize<OthersIndexes>(index); // 解析索引文件
			if (indexRoot != null) {
				if (indexRoot.Index != null) { // 获取索引列表
					OneOtherIndex[] indexes = indexRoot.Index;
					Console.WriteLine($"解析索引文件成功！共有{ indexes.Length }项索引！");
					if (indexes.Length > 0) { // 索引不为空，开始查询逻辑
						Console.Write("请输入要查找的姓名或 GUID：");
						string? query = Console.ReadLine();
						OneOtherIndex? result = null;
						if (Guid.TryParse(query, out var id)) { // 尝试解析为 GUID，若成功则进入 GUID 搜索模式，否则进入姓名搜索模式
							Console.WriteLine($"正在根据 GUID { id } 查找项目中……");
							foreach (var item in indexes) {
								if (item.Guid == id) {
									result = item;
									Console.WriteLine($"已找到 GUID 为 { id } 的项目！\r\n姓名：{ item.Name }");
									break;
								}
							}
						} else {
							Console.WriteLine($"正在根据 姓名 { query } 查找项目中……");
							foreach (var item in indexes) {
								if (item.Name == query) {
									result = item;
									Console.WriteLine($"已找到 姓名 为 { query } 的项目！\r\nGUID：{ item.Guid }");
									break;
								}
							}
						}
						if (result != null) {
							string guid = result.Guid.ToString();
							if (Directory.Exists(guid)) {
								if (OperatingSystem.IsWindows()) {
									Console.Write("是否需要为您打开此文件夹？(Y/n)");
									string? isOpen = Console.ReadLine();
									if (string.IsNullOrWhiteSpace(isOpen) || (isOpen.ToLower() != "no" && isOpen.ToLower() != "n")) {
										System.Diagnostics.Process.Start("explorer.exe", guid);
								    }
								} else {
									Console.WriteLine("您正在使用的系统不是 Windows，暂不支持快速打开对应文件夹。");
									Console.WriteLine($"文件夹：{ Path.Combine(Environment.CurrentDirectory, guid) }");
                                }
                            } else {
								Console.WriteLine("警告：索引中存在此条数据，但未找到对应的文件夹！");
								Console.Write("是否需要为您创建此文件夹？(y/N)");
								string? isCreate = Console.ReadLine();
								if (!(string.IsNullOrWhiteSpace(isCreate) || (isCreate.ToLower() != "yes" && isCreate.ToLower() != "y"))) {
									try {
										DirectoryInfo directoryInfo = Directory.CreateDirectory(guid);
										Console.WriteLine($"创建文件夹 { directoryInfo.FullName } 成功！");
										if (OperatingSystem.IsWindows()) {
											Console.Write("是否需要为您打开此文件夹？(Y/n)");
											string? isOpen = Console.ReadLine();
											if (string.IsNullOrWhiteSpace(isOpen) || (isOpen.ToLower() != "no" && isOpen.ToLower() != "n")) {
												System.Diagnostics.Process.Start("explorer.exe", directoryInfo.FullName);
											}
										} else {
											Console.WriteLine("您正在使用的系统不是 Windows，暂不支持快速打开对应文件夹。");
											Console.WriteLine($"文件夹：{ directoryInfo.FullName }");
										}
									} catch (Exception e) {
										Console.Error.WriteLine("创建文件夹时发生异常！\r\n详细信息：");
										Console.Error.Write(e);
									}
								}
							}
                        } else {
							Console.WriteLine($"未找到项目 { query }！");
                        }
					} else {
						Console.WriteLine("索引列表为空！");
                    }
                } else {
					Console.Error.WriteLine("获取索引项失败！结果为空！");
				}
			} else {
				Console.Error.WriteLine("解析索引文件失败！结果为空！");
			}
		} catch (Exception e) {
			Console.Error.WriteLine("解析索引文件时发生异常！\r\n详细信息：");
			Console.Error.Write(e);
		}
	} catch (Exception e) {
		Console.Error.WriteLine("读取索引文件时发生异常！\r\n详细信息：");
		Console.Error.Write(e);
	}
} else {
	Console.Error.WriteLine("索引文件不存在！");
}