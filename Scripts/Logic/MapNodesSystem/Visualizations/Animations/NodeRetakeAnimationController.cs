using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class NodeRetakeAnimationController : MonoBehaviour
{
    #region Fields


    #endregion

    #region Propeties

    private SpriteRenderer Renderer { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void ShowRetake(Color targetColor)
    {
        if(Renderer != null)
        {
            Renderer.color = targetColor;
            gameObject.SetActive(true);
        }
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Enums



    #endregion
}
