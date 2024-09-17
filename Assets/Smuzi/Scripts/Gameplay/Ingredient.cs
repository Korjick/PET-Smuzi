using UnityEngine;

namespace Smuzi.Scripts.Gameplay
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private GameObject ingredientGameObject;
        
        [field: SerializeField] public IngredientType IngredientType { get; private set; }
        
        public void DestroyIngredient() => 
            Destroy(ingredientGameObject);
    }
}