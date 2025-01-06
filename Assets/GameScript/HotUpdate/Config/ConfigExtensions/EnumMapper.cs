namespace Game.Cfg
{
    public static class EnumMapper
    {
        public static Game.NumericValueType ToNumericValueType(this Game.Cfg.NumericValueType value)
        {
            return (Game.NumericValueType)value;
        }
        
        public static NumberX1000 GetBase(this INumericGetter reader, NumericId id)
        {
            return reader.GetBase((int)id);
        }
        
        public static NumberX1000 GetBaseAdd(this INumericGetter reader, NumericId id)
        {
            return reader.GetBaseAdd((int)id);
        }
        
        public static NumberX1000 GetBaseMul(this INumericGetter reader, NumericId id)
        {
            return reader.GetBaseMul((int)id);
        }
        
        public static NumberX1000 GetFinalAdd(this INumericGetter reader, NumericId id)
        {
            return reader.GetFinalAdd((int)id);
        }
        
        public static NumberX1000 GetFinalMul(this INumericGetter reader, NumericId id)
        {
            return reader.GetFinalMul((int)id);
        }
        
        public static NumberX1000 GetValue(this INumericGetter reader, NumericId id)
        {
            return reader.GetValue((int)id);
        }
        
        public static void SetBase(this INumericSetter writer, NumericId id, NumberX1000 value)
        {
            writer.SetBase((int)id, value);
        }
        
        public static void SetBaseAdd(this INumericSetter writer, NumericId id, NumberX1000 value)
        {
            writer.SetBaseAdd((int)id, value);
        }
        
        public static void SetBaseMul(this INumericSetter writer, NumericId id, NumberX1000 value)
        {
            writer.SetBaseMul((int)id, value);
        }
        
        public static void SetFinalAdd(this INumericSetter writer, NumericId id, NumberX1000 value)
        {
            writer.SetFinalAdd((int)id, value);
        }
        
        public static void SetFinalMul(this INumericSetter writer, NumericId id, NumberX1000 value)
        {
            writer.SetFinalMul((int)id, value);
        }
    }
}