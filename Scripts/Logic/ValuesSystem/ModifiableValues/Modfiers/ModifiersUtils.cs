
namespace GeekBox.Scripts.ValuesSystem
{
    public class ModifiersUtils
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public static FloatValueModifier GetFloatPercentModifier(float percentValue, int sourceId)
        {
            return new FloatValueModifier(percentValue, sourceId, ValueModifier<float>.ModifierType.PERCENT);
        }

        public static FloatValueModifier GetFloatNormalModifier(float percentValue, int sourceId)
        {
            return new FloatValueModifier(percentValue, sourceId, ValueModifier<float>.ModifierType.NORMAL);
        }

        public static IntValueModifier GetIntPercentModifier(int percentValue, int sourceId)
        {
            return new IntValueModifier(percentValue, sourceId, ValueModifier<int>.ModifierType.PERCENT);
        }

        public static IntValueModifier GetIntNormalModifier(int percentValue, int sourceId)
        {
            return new IntValueModifier(percentValue, sourceId, ValueModifier<int>.ModifierType.NORMAL);
        }

        #endregion

        #region Enums



        #endregion
    }
}
