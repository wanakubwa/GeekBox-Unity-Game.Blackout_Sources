using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.TechnologyTree.UI
{
    public class FloatFieldDrawer : FieldDrawer
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetValue(string text)
        {
            Value = text.ParseToFloat();
            InputField.text = Value.ToString();
        }

        #endregion

        #region Enums



        #endregion
    }
}
