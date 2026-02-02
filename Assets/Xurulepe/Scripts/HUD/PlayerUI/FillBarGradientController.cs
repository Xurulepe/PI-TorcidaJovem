using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FillBarGradientController : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private Color[] _colors;
    [SerializeField] private float _changeColorTime;

    private void Update()
    {
        UpdadeBarGradient();
    }

    private void UpdadeBarGradient()
    {
        //int colorCount = _colors.Length;

        _fillBar.DOColor(_colors[GetColorIndex()], _changeColorTime);
    }

    private int GetColorIndex()
    {
        if (_fillBar.fillAmount > 0.7f)
        {
            return 0;
        }
        else if (_fillBar.fillAmount > 0.3f)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
