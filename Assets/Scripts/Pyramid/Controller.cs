using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

namespace Pyramid
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> sprites;
        [SerializeField] private ColorController colorController;
        [SerializeField] private StopController stopController;
        [SerializeField] private float rechangeColorDelay;
        private List<Color> _colors;
        private List<SpriteRenderer> _actualSprites;
        private List<Color> _actualColors;
        private Coroutine _selectColorRoutine;
        private bool _isStop;

        private void Awake()
        {
            _actualColors = new List<Color>();
            _actualSprites = new List<SpriteRenderer>();
            
            stopController.OnStopCallback(false);
            
            colorController.ColorsChangedEvent += SetColors;
            stopController.StopEvent += UpdateStop;
        }

        private void OnDestroy()
        {
            colorController.ColorsChangedEvent -= SetColors;
            if (_selectColorRoutine != null) StopCoroutine(_selectColorRoutine);
        }

        private void UpdateStop(bool value)
        {
            _isStop = value;
            if (_isStop)
            {
                StopCoroutine(_selectColorRoutine);
                foreach (var sprite in _actualSprites)
                {
                    sprite.DOColor(Color.white, 0.2f);
                }
            }
            else
            {
                _selectColorRoutine = StartCoroutine(SelectColorSprite());
            }
        }
        private void SetColors(List<Color> colors)
        {
            _colors = colors;
            _selectColorRoutine = StartCoroutine(SelectColorSprite());
        }

        public bool HasColor(Color color) => _actualColors.Contains(color);

        private List<T> GetRandomValue<T>(List<T> list, int count)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                var emtyIndex = Random.Range(0, list.Count);
                var selectedEmpty = list[emtyIndex];
                if (result.Contains(selectedEmpty))
                {
                    i--;
                }
                else
                {
                    result.Add(selectedEmpty);
                }
            }

            return result;
        }

        private IEnumerator SelectColorSprite()
        {
            for (;;)
            {
                _actualColors.Clear();
                _actualSprites.Clear();

                _actualColors = GetRandomValue(_colors, 2);
                _actualSprites = GetRandomValue(sprites, 2);
                for (int i = 0; i < _actualSprites.Count; i++)
                {
                    var sprite = _actualSprites[i];
                    var color = _actualColors[i];
                    sprite.DOColor(color, 0.4f);
                }

                yield return new WaitForSeconds(rechangeColorDelay);
                if(_isStop) continue;
                foreach (var sprite in _actualSprites)
                {
                    sprite.DOColor(Color.white, 0.2f);
                }
            }
        }
    }
}