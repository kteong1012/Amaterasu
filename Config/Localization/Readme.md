多语言工具使用说明
## 准备工作：
 - 安装好 net8.0 sdk。下载地址： https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/sdk-8.0.303-windows-x64-installer
 - 打开Unity工程
## 步骤
 1. 更新Unity目录。
 2. 以下步骤可以不用按顺序做
    - 在Unity菜单栏上点击命令 Tools/多语言/导出预制体和代码文本，会在Source目录下 code.txt 和 prefab.txt 文件。这是提取预制体和代码调用的步骤。
    - 运行 "0.收集配置文本.bat"文件，会在Source目录生成 config.txt 文件。这是提取配置表里类型为 text 的文本的步骤。
 3. 运行 "1. 收集待翻译文本.bat"。如果有新增的翻译项，Main.xlsx 会更新新的key和text，生成时默认text会和key一样，如果需要修改text实际文本的话就需要手动修改。
 4. 运行 "2. 更新多语言Patch表.bat"。会更新 Translate 目录下的表格和 Patch 目录下的表格。策划只需要关注 Patch 目录，如果有需要翻译的文本， Patch/Tasks 在此步骤完成后就会有 xlsx，如果没有文本就不会有表格。
 5. 如果 Patch/Tasks 目录下有表格。就翻译它，翻译完成之后就放到 Patch/Results 的对应语言目录下。
 6. 运行 "3. 采用已翻译表.bat" 。 此步骤会采用 Patch/Results 目录下的翻译文本，然后更新 Translate 目录下的表格。
 7. 运行 "4. 导出多语言数据.bat" 。 
    - 在 Json 目录下生成各语言的 .json 文件，也会在客户端输出实际运行要用到的 .bytes 文件。这用于SVN对比，所以上传时尽量不要忽略它们。
    - 在客户端的资源目录下生成二进制文件，这个策划不需要关心。
 8. 提交Unity目录的改动。