using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public enum AnimationMode
    {
        OneAtATime,
        Everyone
    }

    public void AnimateMenu(Menu menu, AnimationMode animationMode)
    {
        switch (animationMode)
        {
            case AnimationMode.OneAtATime:
                StartCoroutine(AnimateMenuElementsOneAtTime(menu));

                break;
            case AnimationMode.Everyone:

                break;
        }
    }

    private IEnumerator AnimateMenuElementsOneAtTime(Menu menu)
    {
        HideMenuElements(menu);

        foreach (var element in menu.animatedElements)
        {
            element.DOScaleY(1f, 0.15f);
            yield return new WaitForSeconds(0.10f);
        }
    }

    private void AnimateMenuElementsTogheter(Menu menu)
    {
        HideMenuElements(menu);

        foreach (var element in menu.animatedElements)
        {
            element.DOScaleY(1f, 0.25f);
        }
    }

    /// <summary>
    /// Oculta os elementos do menu que serão animados.
    /// </summary>
    /// <param name="menu"> O menu que contém a lista de elementos que serão ocultados.</param>
    private void HideMenuElements(Menu menu)
    {
        foreach (var element in menu.animatedElements)
        {
            element.localScale = new Vector3(element.localScale.x, 0f, element.localScale.z);
        }
    }

    public void AnimateSingleElement(GameObject gameObj)
    {
        gameObj.transform.localScale = Vector3.zero;
        gameObj.transform.DOScale(1f, 0.25f).SetEase(Ease.InOutBack);
    }
}
