using System;
using UnityEngine;
using UnityEngine.UI;

namespace GeekBox.TechnologyTree.UI
{
    public abstract class FieldDrawer : MonoBehaviour
    {
        #region Fields

        [Space]
        [SerializeField]
        private InputField inputField;
        [SerializeField]
        private Text label;

        [Space]
        [SerializeField]
        private InputFieldType fieldType;

        #endregion

        #region Propeties

        public InputField InputField {
            get => inputField;
        }

        public Text Label {
            get => label;
        }

        public InputFieldType FieldType {
            get => fieldType;
        }

        public object Value {
            get;
            protected set;
        } = default;

        public Type DrawedType {
            get;
            private set;
        }

        #endregion

        #region Methods

        public abstract void SetValue(string text);

        public void SetLabel(string text)
        {
            Label.text = text;
        }

        public void SetDrawedType(Type type)
        {
            DrawedType = type;
        }

        /// <summary>
        /// Value is returned as object type because Unity in default not supporting serialize generic class.
        /// </summary>
        public virtual object GetValue()
        {
            return Value;
        }

        public virtual void Draw(Type targetType, string label)
        {
            SetLabel(label);
            SetDrawedType(targetType);

            if (InputField != null && targetType.IsValueType == true)
            {
                SetValue(Activator.CreateInstance(targetType).ToString());
                InputField.text = Value.ToString();
            }
        }

        #endregion

        #region Enums

        public enum InputFieldType
        {
            INT,
            FLOAT,
            ENUM
        }

        #endregion
    }
}
