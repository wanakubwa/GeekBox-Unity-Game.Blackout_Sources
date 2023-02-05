using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISystem
{
    public interface IAIActor<T> : IIDEquatable
    {
        #region Fields



        #endregion

        #region Propeties

        T AIActor { get; }

        #endregion

        #region Methods

        void RegisterAIActor();
        void UnregisterAIActor();

        #endregion

        #region Enums



        #endregion
    }
}
