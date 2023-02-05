using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParentsFactory
{
    #region Fields

    private static ParentsContentSettings parentsSettings = ParentsContentSettings.Instance;

    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static NodeParent GetPlayerParent()
    {
        NodeParent playerParent = new NodeParent(Constants.NODE_PLAYER_PARENT_ID);
        Color parentColor = parentsSettings.GetPlayerParentColor().ParentColor;
        Color parentFontColor = parentsSettings.GetPlayerParentColor().FontColor;
        Color parentShieldsColor = parentsSettings.GetPlayerParentColor().ShieldsColor;
        playerParent.Settings.SetMainColor(parentColor);
        playerParent.Settings.SetFontColor(parentFontColor);
        playerParent.Settings.SetShieldsColor(parentShieldsColor);
        return playerParent;
    }

    public static NodeParent GetNeutralParent()
    {
        NodeParent parent = new NodeParent(Constants.NODE_NEUTRAL_PARENT_ID);
        Color parentColor = parentsSettings.GetNeutralParentColor().ParentColor;
        Color parentFontColor = parentsSettings.GetNeutralParentColor().FontColor;
        Color parentShieldsColor = parentsSettings.GetNeutralParentColor().ShieldsColor;
        parent.Settings.SetMainColor(parentColor);
        parent.Settings.SetFontColor(parentFontColor);
        parent.Settings.SetShieldsColor(parentShieldsColor);
        return parent;
    }

    public static NodeParent GetNewParent(int id)
    {
        NodeParent parent = new NodeParent(id);
        return parent;
    }

    public static List<NodeParent> GetDefaultParentsCollection()
    {
        List<NodeParent> parents = new List<NodeParent>();
        parents.Add(GetNeutralParent());
        parents.Add(GetPlayerParent());
        return parents;
    }

    //Brzydkie ale na potrzeby edytora, nie uzywane w grze.
    public static NodeParent GetNewParent(List<NodeParent> currentParents)
    {
        int maxId = currentParents.FirstOrDefault().ID;

        for (int i = 0; i < currentParents.Count; i++)
        {
            if(currentParents[i].ID > maxId)
            {
                maxId = currentParents[i].ID;
            }
        }

        return GetNewParent(maxId + 1);
    }

    #endregion

    #region Enums



    #endregion
}
