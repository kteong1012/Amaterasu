using System.Reflection;

namespace LocalizationTool
{
    internal class LocalizationTaskManager
    {
        public static LocalizationTask GetTask(string name)
        {
            var asm = typeof(LocalizationTaskManager).Assembly;
            foreach (var type in asm.GetTypes())
            {
                // 判断是不是 LocalizationTask 的子类
                if (type.IsSubclassOf(typeof(LocalizationTask)))
                {
                    // 判断是否有 LocalizationTaskAttribute 特性
                    var attr = type.GetCustomAttribute<LocalizationTaskAttribute>();
                    if (attr != null && attr.taskName == name)
                    {
                        return (LocalizationTask)asm.CreateInstance(type.FullName);
                    }
                }
            }
            return null;
        }
    }
}