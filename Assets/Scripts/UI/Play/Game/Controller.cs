using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI.Play.Game
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private ColorController colorController;
        [SerializeField] private List<ButtonLinker> buttons;
        [SerializeField] private CanvasGroup canvasGroup;
        private List<Color> _colors;
        public event Func<Color, bool> CheckColorEvent;
        public event Action<bool> UpdatePlayEvent;

        private void Awake()
        {
            colorController.ColorsChangedEvent += SetColors;
        }

        private void OnDestroy()
        {
            colorController.ColorsChangedEvent -= SetColors;
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

        private void SetColors(List<Color> colors)
        {
            _colors = colors;

            for (int i = 0; i < buttons.Count; i++)
            {
                var color = _colors[i];
                var linker = buttons[i];
                linker.Image.color = color;
                linker.Button.onClick.AddListener(() => CheckColor(color));
            }
        }

        private void CheckColor(Color color)
        {
            if (CheckColorEvent != null) UpdatePlayEvent?.Invoke(CheckColorEvent.Invoke(color));
        }

        private void Print(string text) => Debug.Log($"PlayController: {text}");
    }
}