
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace Game.Cfg
{
public sealed partial class BattleSettings : Luban.BeanBase
{
    public BattleSettings(ByteBuf _buf) 
    {
        Test = _buf.ReadInt();

        TranslateText();
    }

    public static BattleSettings DeserializeBattleSettings(ByteBuf _buf)
    {
        return new BattleSettings(_buf);
    }

    public int Test { get; private set; }
   
    public const int __ID__ = 1984693531;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(ConfigService tables)
    {
    }

    public  void TranslateText()
    {
    }

    public override string ToString()
    {
        return "{ "
        + "test:" + Test + ","
        + "}";
    }
}

}
