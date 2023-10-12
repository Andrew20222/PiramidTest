using System;
using DefaultNamespace;
using UnityEngine;

namespace UI.Play
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Game.Controller gameScreen;
        [SerializeField] private Lose.Controller loseScreen;
        [SerializeField] private Pyramid.Controller pyramid;
        [SerializeField] private StopController stopController;
        private RechangePanels _rechangePanels;

        private void Awake()
        {
            _rechangePanels = new RechangePanels();
            gameScreen.CheckColorEvent += pyramid.HasColor;
            gameScreen.UpdatePlayEvent += UpdatePlay;
            stopController.Subscribe(UpdateStop);
            SetGameScreen();
        }

        private void UpdatePlay(bool value)
        {
            if (value == false) SetLoseScreen();
        }

        private void UpdateStop(bool value)
        {
            if(value == false) SetGameScreen();
        }

        private void SetGameScreen()
        {
            _rechangePanels.SetNewPanel(gameScreen);
        }

        private void SetLoseScreen()
        {
            _rechangePanels.SetNewPanel(loseScreen);
            stopController.OnStopCallback(true);
        }
    }
}