using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Lose
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private StopController stopController;
        [SerializeField] private Button restart;

        private void Start()
        {
            restart.onClick.AddListener(() => stopController.OnStopCallback(false));
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, 0.2f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            canvasGroup.DOFade(1, 0.2f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}