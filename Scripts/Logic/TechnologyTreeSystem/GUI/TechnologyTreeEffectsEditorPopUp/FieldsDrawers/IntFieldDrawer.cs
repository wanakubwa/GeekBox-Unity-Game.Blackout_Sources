using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.TechnologyTree.UI
{
    public class IntFieldDrawer : FieldDrawer
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        public override void SetValue(string text)
        {
            Value = text.ParseToInt();
            InputField.text = Value.ToString();
        }

        #endregion

        #region Enums



        #endregion
    }
}
