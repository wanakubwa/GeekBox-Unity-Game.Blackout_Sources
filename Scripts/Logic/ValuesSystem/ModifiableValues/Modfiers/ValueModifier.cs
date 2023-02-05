using System;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public abstract class ValueModifier<T> : IIDEquatable
    {
        #region Fields

        private T modifierValue;
        private int sourceId;
        private ModifierType modType;

        #endregion

        #region Propeties

        public T ModifierValue { 
            get => modifierValue; 
            private set => modifierValue = value; 
        }

        public int SourceId { 
            get => sourceId; 
            private set => sourceId = value; 
        }

        public ModifierType ModType { 
            get => modType; 
            private set => modType = value; 
        }

        public int ID => SourceId;

        #endregion

        #region Methods

        protected ValueModifier(T modifierValue, int sourceId, ModifierType modType)
        {
            ModifierValue = modifierValue;
            SourceId = sourceId;
            ModType = modType;
        }

        public abstract T GetModifierValue(T baseValue);

        public bool IDEqual(int otherId)
        {
            return SourceId == otherId;
        }

        public override string ToString()
        {
            return string.Format("[Modifier - source: {0}, type: {1}, value: {2}]", SourceId, ModType, ModifierValue);
        }

        #endregion

        #region Enums

        public enum ModifierType
        {
            NORMAL,
            PERCENT
        }

        #endregion
    }
}
