using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Smuzi.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Smuzi.Scripts.Gameplay
{
    [RequireComponent(typeof(RectItemSlot))]
    public class IngredientContainer : MonoBehaviour
    {
        [SerializeField] private RectItemSlot blenderContainerSlot;
        [SerializedDictionary("IngredientType", "OrderedIngredient")]
        [SerializeField] private SerializedDictionary<IngredientType, OrderedIngredient> requiredIngredients;
        
        private int _order;
        private int _activeIngredient;
        private List<IngredientType> _activeIngredientTypes;
        
        public IReadOnlyList<IngredientType> ActiveIngredientTypes => _activeIngredientTypes;
        public bool IsFull => _activeIngredient >= requiredIngredients.Count;
        
        private void OnEnable()
        {
            blenderContainerSlot.Drop += OnDrop;
        }

        private void OnDisable()
        {
            blenderContainerSlot.Drop -= OnDrop;
        }

        private void Awake()
        {
            _order = 0;
            _activeIngredient = 0;
            _activeIngredientTypes = new List<IngredientType>();
        }

        public void HideAllIngredients()
        {
            foreach (var (key, value) in requiredIngredients)
            foreach (GameObject o in value.IngredientGameObjectOrder) 
                    o.gameObject.SetActive(false);
        }
        
        private void OnDrop(DragRectItem dragRectItem)
        {
            if (dragRectItem.TryGetComponent(out Ingredient ingredient)
                && requiredIngredients.ContainsKey(ingredient.IngredientType))
            {
                _activeIngredientTypes.Add(ingredient.IngredientType);
                ingredient.DestroyIngredient();
                dragRectItem.ResetPosition();
                dragRectItem.enabled = false;
                OrderedIngredient orderedIngredient = requiredIngredients[ingredient.IngredientType];
                if (orderedIngredient.IsOrdered)
                {
                    orderedIngredient.IngredientGameObjectOrder[_order].SetActive(true);
                    _order++;
                }
                else
                {
                    orderedIngredient.IngredientGameObjectOrder[0].SetActive(true);
                }
                _activeIngredient++;
            }
        }

        [Serializable]
        public class OrderedIngredient
        {
            [field: SerializeField] public bool IsOrdered { get; private set; }
            [field: SerializeField] public GameObject[] IngredientGameObjectOrder { get; private set; }
        }
    }
}
