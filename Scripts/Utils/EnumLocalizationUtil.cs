

public class EnumLocalizationUtil
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static string LocalizeNodeMode(NodeModeType nodeMode)
    {
        string output = Constants.LOC_DEFAULT_KEY;
        switch (nodeMode)
        {
            case NodeModeType.DEFEND:
                output = Constants.LOC_EN_NODE_MODE_DEF;
                break;
            case NodeModeType.ATTACK:
                output = Constants.LOC_EN_NODE_MODE_ATTACK;
                break;
            case NodeModeType.SLOWNING:
                output = Constants.LOC_EN_NODE_MODE_SLOW;
                break;
            case NodeModeType.SPEED:
                output = Constants.LOC_EN_NODE_MODE_SPEED;
                break;
            case NodeModeType.SUICIDE:
                output = Constants.LOC_EN_NODE_MODE_SUICIDE;
                break;
            case NodeModeType.REPRODUCTION:
                output = Constants.LOC_EN_NODE_MODE_REPRO;
                break;
            case NodeModeType.PARASITE:
                output = Constants.LOC_EN_NODE_MODE_PARASITE;
                break;
            case NodeModeType.SECOND_CHANCE:
                output = Constants.LOC_EN_NODE_MODE_SCHANCE;
                break;
        }

        return output.Localize();
    }

    #endregion

    #region Enums



    #endregion
}
