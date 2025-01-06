using Luban.DataExporter.Builtin.Json;
using Luban.Datas;
using Luban.Types;
using Luban.TypeVisitors;

namespace Luban.Typescript.TypeVisitors
{
    public abstract class BinUnderingDeserializeVisitorBase : ITypeFuncVisitor<string, string, int, string>
    {
        public string Accept(TBool type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadBool()";
        }

        public string Accept(TByte type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadByte()";
        }

        public string Accept(TShort type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadShort()";
        }

        public string Accept(TInt type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadInt()";
        }

        public string Accept(TLong type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.{(type.IsBigInt ? "ReadLong" : "ReadLongAsNumber")}()";
        }

        public string Accept(TFloat type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadFloat()";
        }

        public string Accept(TDouble type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadDouble()";
        }

        public string Accept(TEnum type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadInt()";
        }

        public string Accept(TString type, string bufName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufName}.ReadString()";
        }

        public abstract string Accept(TBean type, string bufVarName, string fieldName, int depth);

        public string Accept(TArray type, string bufVarName, string fieldName, int depth)
        {
            return $"{{ let n = Math.min({bufVarName}.ReadSize(), {bufVarName}.Size); {fieldName} = []; for(let i = 0 ; i < n ; i++) {{ let _e{depth} ;{type.ElementType.Apply(this, bufVarName, $"_e{depth}", depth + 1)}; {fieldName}.push(_e{depth});}}}}";
        }

        public virtual string Accept(TList type, string bufVarName, string fieldName, int depth)
        {
            return $"{{ let n = Math.min({bufVarName}.ReadSize(), {bufVarName}.Size); {fieldName} = []; for(let i = 0 ; i < n ; i++) {{ let _e{depth}; {type.ElementType.Apply(this, bufVarName, $"_e{depth}", depth + 1)}; {fieldName}.push(_e{depth});}}}}";
        }

        public virtual string Accept(TSet type, string bufVarName, string fieldName, int depth)
        {
            return $"{{ let n = Math.min({bufVarName}.ReadSize(), {bufVarName}.Size); {fieldName} = new {type.Apply(DeclaringTypeNameVisitor.Ins)}(); for(let i = 0 ; i < n ; i++) {{ let _e{depth}; {type.ElementType.Apply(this, bufVarName, $"_e{depth}", depth + 1)}; {fieldName}.add(_e{depth});}}}}";
        }

        public virtual string Accept(TMap type, string bufVarName, string fieldName, int depth)
        {
            return $"{{ let n = Math.min({bufVarName}.ReadSize(), {bufVarName}.Size); {fieldName} = new {type.Apply(DeclaringTypeNameVisitor.Ins)}(); for(let i = 0 ; i < n ; i++) {{ let _k{depth}; {type.KeyType.Apply(this, bufVarName, $"_k{depth}", depth + 1)};  let _v{depth};  {type.ValueType.Apply(this, bufVarName, $"_v{depth}", depth + 1)}; {fieldName}.set(_k{depth}, _v{depth});  }} }}";
        }

        public string Accept(TDateTime type, string bufVarName, string fieldName, int depth)
        {
            return $"{fieldName} = {bufVarName}.ReadLongAsNumber()";
        }
    }
}
