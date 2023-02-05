using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class ModifiableFloatValue : ModifiableValueBase<float, FloatValueModifier>
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public ModifiableFloatValue() { }

        public ModifiableFloatValue(float baseValue) : base(baseValue) { }

        public override void RecalculateFinalValue()
        {
            FinalValue = BaseValue + GetModsModifiersValue() + GetPerksModifiersValue();
        }

        private float GetModsModifiersValue()
        {
            float sum = 0;

            foreach (FloatValueModifier modificator in ModsModifiers)
            {
                sum += modificator.GetModifierValue(BaseValue);
            }

            return sum;
        }

        private float GetPerksModifiersValue()
        {
            float sum = 0;

            foreach (FloatValueModifier modificator in PerksModifiers)
            {
                sum += modificator.GetModifierValue(BaseValue);
            }

            return sum;
        }

        #endregion

        #region Enums



        #endregion
    }
}

