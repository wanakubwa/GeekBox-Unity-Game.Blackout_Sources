using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class ModifiableIntValue : ModifiableValueBase<int, IntValueModifier>
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public ModifiableIntValue() { }

        public ModifiableIntValue(int baseValue) : base(baseValue) { }

        public override void RecalculateFinalValue()
        {
            FinalValue = BaseValue + GetModsModifiersValue() + GetPerksModifiersValue();
        }

        private int GetModsModifiersValue()
        {
            int sum = 0;

            foreach (IntValueModifier modificator in ModsModifiers)
            {
                sum += modificator.GetModifierValue(BaseValue);
            }

            return sum;
        }

        private int GetPerksModifiersValue()
        {
            int sum = 0;

            foreach (IntValueModifier modificator in PerksModifiers)
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
