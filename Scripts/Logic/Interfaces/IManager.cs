using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IManager
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods
    bool HasContentOnScene(SceneLabel sceneLabel);

    SceneLabel GetSceneLifeLabel();

    string GetLocalizedKey();

    void AttachEvents();

    #endregion

    #region Handlers



    #endregion
}
