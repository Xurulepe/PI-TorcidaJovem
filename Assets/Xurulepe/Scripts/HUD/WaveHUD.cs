using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    int waveID = 1;

    private void OnEnable()
    {
       StartCoroutine(StartWaveHUD());
    }

    private void OnDisable()
    {
        waveID++;
    }

    private IEnumerator StartWaveHUD()
    {
        waveText.alpha = 1.0f;
        waveText.text = waveText.text.Replace("{0}", waveID.ToString());

        yield return new WaitForSeconds(1);

        waveText.DOFade(0f, 2f);

        yield return new WaitForSeconds(2);

        DeactiveHUD();
    }

    private void DeactiveHUD()
    {
        waveText.text = waveText.text.Replace(waveID.ToString(), "{0}");

        gameObject.SetActive(false);
    }
}
