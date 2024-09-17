using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Smuzi.Scripts.Utilities
{
    public class RectItemSlot : MonoBehaviour, IDropHandler
    {
        public event Action<DragRectItem> Drop;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null
                && eventData.pointerDrag.TryGetComponent(out DragRectItem item))
            {
                Drop?.Invoke(item);
            }
        }
    }
}