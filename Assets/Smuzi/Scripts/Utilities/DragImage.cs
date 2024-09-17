using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Smuzi.Scripts.Utilities
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(Image))]
    public class DragImage : DragRectItem
    {
        [SerializeField] private Image current;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private int beginDragOrder = 2;
        [SerializeField] private int endDragOrder = 1;
        [Space]
        [SerializeField] private Image[] disableDragImage;
        [Space] 
        [SerializeField] private Sprite swapWhenDragImage;

        private Sprite _startImage;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            canvas.sortingOrder = beginDragOrder;
            SetImagesVisible(disableDragImage, false);
            if (swapWhenDragImage != null)
            {
                _startImage = current.sprite;
                current.sprite = swapWhenDragImage;
            }
            base.OnBeginDrag(eventData);
        }
        
        public override void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvas.sortingOrder = endDragOrder;
            SetImagesVisible(disableDragImage, true);
            if (swapWhenDragImage != null)
            {
                current.sprite = _startImage;
            }
            base.OnEndDrag(eventData);
        }

        private void SetImagesVisible(Image[] images, bool visible)
        {
            foreach (Image image in images) 
                image.color = visible ? Color.white : Color.clear;
        }
    }
}
