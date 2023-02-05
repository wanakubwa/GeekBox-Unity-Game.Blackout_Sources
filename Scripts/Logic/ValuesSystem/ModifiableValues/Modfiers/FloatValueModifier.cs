using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class FloatValueModifier : ValueModifier<float>
    {
        #region Fields

        public const float DEFAULT_VALUE = 0f;

        #endregion

        #region Propeties



        #endregion

        #region Methods

        public FloatValueModifier(float modifierValue, int sourceId, ModifierType modType) : base(modifierValue, sourceId, modType)
        {

        }

        public override float GetModifierValue(float baseValue)
        {
            if(ModType == ModifierType.NORMAL)
            {
                return ModifierValue;
            }
            else if (ModType == ModifierType.PERCENT)
            {
                return baseValue * (ModifierValue * 0.01f);
            }

            return DEFAULT_VALUE;
        }

        #endregion

        #region Enums



        #endregion
    }
}
