using System;
using UnityEngine;

namespace GeekBox.Scripts.ValuesSystem
{
    [Serializable]
    public abstract class SplittedValue<T>
    {
        #region Fields

        [SerializeField]
        private T normalValue = default;
        [SerializeField]
        private T percentValue = default;

        #endregion

        #region Propeties

        public T NormalValue { 
            get => normalValue; 
            private set => normalValue = value; 
        }

        /// <summary>
        /// Wartosc przechowywana jako punkty procentowe (PP).
        /// Przyklad: 40% nalezy podac jako wartosc 40.
        /// </summary>
        public T PercentValue { 
            get => percentValue; 
            private set => percentValue = value; 
        }

        #endregion

        #region Methods

        public abstract void AddPercentValue(T valueToAdd);
        public abstract void AddNormalValue(T valueToAdd);

        public void SetNormalValue(T value)
        {
            NormalValue = value;
        }

        public void SetPercentValue(T percentValue)
        {
            PercentValue = percentValue;
        }

        #endregion

        #region Enums



        #endregion
    }
}
