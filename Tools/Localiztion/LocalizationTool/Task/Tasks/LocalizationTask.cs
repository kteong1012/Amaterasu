using System.Reflection;

namespace LocalizationTool
{
    internal abstract class LocalizationTask
    {
        public void ParseParams(params string[] args)
        {
            var parameters = new Dictionary<string, object>();

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("-"))
                {
                    var name = arg.Substring(1);
                    var field = GetParamField(name);
                    if (field == null)
                    {
                        throw new Exception($"未知参数：{name}");
                    }
                    if (parameters.ContainsKey(name))
                    {
                        throw new Exception($"重复的参数：{name}");
                    }

                    if (field.FieldType == typeof(string))
                    {
                        i++;
                        var value = args[i];
                        parameters.Add(name, value);
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        i++;
                        if (int.TryParse(args[i], out var value))
                        {
                            parameters.Add(name, value);
                        }
                        else
                        {
                            throw new ArgumentException($"参数{field.Name}的值不是整数：{args[i]}");
                        }
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        parameters.Add(name, true);
                    }
                    else
                    {
                        throw new ArgumentException($"不支持的参数类型：{field.FieldType}");
                    }
                }
            }

            foreach (var field in GetType().GetFields())
            {
                if (field.GetCustomAttribute<LocalizationTaskParameterAttribute>() != null)
                {
                    if (!parameters.ContainsKey(field.Name))
                    {
                        var attr = field.GetCustomAttribute<LocalizationTaskParameterAttribute>();
                        if (attr.required)
                        {
                            throw new ArgumentException($"缺少必要参数：{field.Name}");
                        }
                    }
                    else
                    {
                        field.SetValue(this, parameters[field.Name]);
                    }
                }
            }
        }
        private FieldInfo GetParamField(string name)
        {
            var fields = GetType().GetFields();
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<LocalizationTaskParameterAttribute>();
                if (attr != null && attr.name == name)
                {
                    return field;
                }
            }

            return null;
        }
        public abstract void Execute();
    }
}