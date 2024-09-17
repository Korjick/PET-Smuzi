using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Smuzi.Scripts.Utilities
{
    [RequireComponent(typeof(Canvas))]
    public class DragRectItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] protected Canvas canvas;
        [field: SerializeField] public RectTransform RectTransform { get; private set; }

        private Vector3 _initialPosition;

        public event Action BeginDrag;
        public event Action Drag;
        public event Action EndDrag;
        
        public virtual void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            Drag?.Invoke();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            ResetPosition();
            EndDrag?.Invoke();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _initialPosition = RectTransform.anchoredPosition;
            BeginDrag?.Invoke();
        }

        public void ResetPosition() => 
            RectTransform.anchoredPosition = _initialPosition;
    }
}
