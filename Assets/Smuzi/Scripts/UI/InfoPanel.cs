using System;
using UnityEngine;
using UnityEngine.UI;

namespace Smuzi.Scripts.UI
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        private void OnEnable() => 
            closeButton.onClick.AddListener(OnCloseButtonPressed);

        private void OnDisable() => 
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);

        private void OnCloseButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}
