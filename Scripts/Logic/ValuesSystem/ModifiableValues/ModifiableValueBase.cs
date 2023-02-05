using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public abstract class ModifiableValueBase<T, U> where U : ValueModifier<T>
    {
        #region Fields

        [SerializeField]
        private List<U> perksModifiers = new List<U>();
        [SerializeField]
        private List<U> modsModifiers = new List<U>();
        [SerializeField]
        private T baseValue;

        #endregion

        #region Propeties

        public List<U> PerksModifiers
        {
            get => perksModifiers;
            private set => perksModifiers = value;
        }

        public List<U> ModsModifiers
        {
            get => modsModifiers;
            private set => modsModifiers = value;
        }

        public T BaseValue
        {
            get => baseValue;
            private set => baseValue = value;
        }

        public T FinalValue
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        public ModifiableValueBase() { }

        public ModifiableValueBase(T baseValue)
        {
            SetBaseValue(baseValue);
        }

        public abstract void RecalculateFinalValue();

        public void AddPerksModifier(U modifier)
        {
            PerksModifiers.Add(modifier);
            RecalculateFinalValue();
        }

        public void AddModsModifier(U modifier)
        {
            ModsModifiers.Add(modifier);
            RecalculateFinalValue();
        }

        public void RemovePerksModifier(int sourceId)
        {
            PerksModifiers.RemoveElementByID(sourceId);
            RecalculateFinalValue();
        }

        public void RemoveModsModifier(int sourceId)
        {
            ModsModifiers.RemoveElementByID(sourceId);
            RecalculateFinalValue();
        }

        public void RemoveAllPerksModifiers(int sourceId)
        {
            PerksModifiers.RemoveElementsByID(sourceId);
            RecalculateFinalValue();
        }

        public void RemoveAllModsModifiers(int sourceId)
        {
            ModsModifiers.RemoveElementsByID(sourceId);
            RecalculateFinalValue();
        }

        public void SetBaseValue(T value)
        {
            BaseValue = value;
            RecalculateFinalValue();
        }

        public virtual void Reset()
        {
            PerksModifiers.Clear();
            ModsModifiers.Clear();
            RecalculateFinalValue();
        }

        public bool HasModifiers()
        {
            return PerksModifiers.Count > 0 || ModsModifiers.Count > 0;
        }

        public override string ToString()
        {
            string info = string.Format("[Base: {0}; Final: {1}] \n", BaseValue, FinalValue);

            info += "MODS \n";
            for (int i =0; i < ModsModifiers.Count; i++)
            {
                info += string.Format("[Mod: {0}] \n", ModsModifiers[i].ToString());
            }

            info += "PERKS \n";
            for (int i = 0; i < PerksModifiers.Count; i++)
            {
                info += string.Format("[Perk: {0}] \n", PerksModifiers[i].ToString());
            }

            return info;
        }

        #endregion

        #region Enums



        #endregion
    }
}

