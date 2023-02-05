using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GeekBox.TechnologyTree.UI
{
    public class EnumFieldDrawer : FieldDrawer
    {
        #region Fields

        [SerializeField]
        private Dropdown enumDropdown;

        #endregion

        #region Propeties

        public Dropdown EnumDropdown { 
            get => enumDropdown;
        }

        #endregion

        #region Methods

        public override void Draw(Type targetType, string label)
        {
            if (targetType.IsEnum == true)
            {
                SetDropdownOptions(targetType.GetEnumNames());
            }

            base.Draw(targetType, label);
        }

        public override void SetValue(string text)
        {
            int optionIndex = DrawedType.GetEnumNames().IndexOf(text);
            if(optionIndex >= 0)
            {
                EnumDropdown.value = optionIndex;
                Value = Enum.Parse(DrawedType, EnumDropdown.options[optionIndex].text);
            }
        }

        public void OnDropdownChanged(int index)
        {
            SetValue(EnumDropdown.options[index].text);
        }

        private void SetDropdownOptions(string[] options)
        {
            EnumDropdown.options.Clear();
            EnumDropdown.AddOptions(options.ToList());
        }

        #endregion

        #region Enums



        #endregion
    }
}
