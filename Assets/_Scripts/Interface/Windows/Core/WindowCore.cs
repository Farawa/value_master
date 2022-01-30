using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WindowCore : MonoBehaviour
{
    #region Methods
    public virtual void Show(float time=1)
    {
        gameObject.SetActive(true);
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        ParticleSystem[] effects = GetComponentsInChildren<ParticleSystem>();
        for(int i=0; i<effects.Length; i++)
		{
            effects[i].Stop(true);
        }
        canvas.alpha = 0;
        canvas.DOFade(1, 0.3f).SetDelay(time).OnStart(() =>
            {
                canvas.interactable = true;
            })
            .OnComplete(() =>
            {
                for (int i = 0; i < effects.Length; i++)
                {
                    effects[i].Play(true);
                }
            });
    }

    public virtual void Hide()
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.interactable = false;
        canvas.DOFade(0, 0.2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    #endregion
}
