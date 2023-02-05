using System;
using System.Collections.Generic;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public class TargetValue<T, U> where U : new()
    {
        #region Fields

        private Dictionary<T, U> targetsValues = new Dictionary<T, U>();

        #endregion

        #region Propeties

        public Dictionary<T, U> TargetsValues { 
            get => targetsValues; 
            private set => targetsValues = value; 
        }

        #endregion

        #region Methods

        public void RemoveTarget(T targetKey)
        {
            TargetsValues.Remove(targetKey);
        }

        public U AddTarget(T targetKey)
        {
            // Jezeli dany target jest juz w kolekcji.
            if(TargetsValues.ContainsKey(targetKey) == true)
            {
                return GetValueForTarget(targetKey);
            }

            U newValue = new U();
            TargetsValues.Add(targetKey, newValue);

            return newValue;
        }

        public U GetValueForTarget(T targetKey)
        {
            U valueForTarget;
            TargetsValues.TryGetValue(targetKey, out valueForTarget);

            return valueForTarget;
        }

        public override string ToString()
        {
            string info = string.Empty;

            foreach (var target in TargetsValues)
            {
                info += string.Format("(Target: {0} \n {1})", target.Key, target.Value.ToString());
            }

            return info;
        }

        #endregion

        #region Enums



        #endregion
    }
}

