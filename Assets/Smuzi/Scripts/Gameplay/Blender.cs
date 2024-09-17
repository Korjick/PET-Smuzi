using System;
using System.Collections;
using DG.Tweening;
using Smuzi.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Smuzi.Scripts.Gameplay
{
    public class Blender : MonoBehaviour
    {
        [SerializeField] private RectItemSlot blenderContainerSlot;
        [SerializeField] private Vector2 newCoverPosition;
        [Space]
        [SerializeField] private IngredientContainer ingredientContainer;
        [SerializeField] private DragRectItem blenderContainerDrag;
        [SerializeField] private float shakeDuration;
        [Space]
        [SerializeField] private Button mixButton;
        [SerializeField] private Image mixButtonImage;
        [SerializeField] private Sprite mixButtonActiveSprite;
        [Space]
        [SerializeField] private Image mixImage;
        [SerializeField] private Sprite[] mixPhases;
        
        private Transform _blenderCoverInitialParent;
        private DragRectItem _cover;
        private Sprite _mixButtonStartSprite;
        
        private void OnEnable()
        {
            blenderContainerSlot.Drop += OnDrop;
            mixButton.onClick.AddListener(OnButtonPressed);
        }

        private void OnDisable()
        {
            blenderContainerSlot.Drop -= OnDrop;
            mixButton.onClick.RemoveListener(OnButtonPressed);
        }

        private void Awake()
        {
            blenderContainerDrag.enabled = false;
        }

        private void OnDrop(DragRectItem dragRectItem)
        {
            if (dragRectItem.TryGetComponent(out BlenderCover cover)
                && ingredientContainer.IsFull)
            {
                _cover = dragRectItem;
                _blenderCoverInitialParent = _cover.transform.parent;

                _cover.enabled = false;
                _cover.RectTransform.anchoredPosition = newCoverPosition;
                _cover.transform.SetParent(ingredientContainer.transform, true);
            }
        }

        private void OnButtonPressed()
        {
            if (_cover == null)
                return;

            StartCoroutine(Cook());
        }

        private IEnumerator Cook()
        {
            mixButton.interactable = false;
            _mixButtonStartSprite = mixButtonImage.sprite;
            mixButtonImage.sprite = mixButtonActiveSprite;
            ingredientContainer.HideAllIngredients();
            int phase = 0;
            mixImage.gameObject.SetActive(true);
            while (phase < mixPhases.Length)
            {
                mixImage.sprite = mixPhases[phase];
                var sequence = DOTween.Sequence();
                sequence.Append(ingredientContainer.transform.DOShakePosition(shakeDuration, strength: 10));
                yield return sequence.WaitForCompletion();
                phase++;
            }
            _cover.transform.SetParent(_blenderCoverInitialParent, true);
            _cover.ResetPosition();
            mixButtonImage.sprite = _mixButtonStartSprite;
            blenderContainerDrag.enabled = true;
        }
    }
}