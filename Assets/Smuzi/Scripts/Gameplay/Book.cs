using System;
using Smuzi.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Smuzi.Scripts.Gameplay
{
    [RequireComponent(typeof(Button))]
    public class Book : MonoBehaviour
    {
        [SerializeField] private Button infoPanelOpenButton;
        [SerializeField] private InfoPanel infoPanel;
        
        private void OnEnable() => 
            infoPanelOpenButton.onClick.AddListener(OnInfoPanelButtonPressed);

        private void OnDisable() => 
            infoPanelOpenButton.onClick.RemoveListener(OnInfoPanelButtonPressed);

        private void Awake()
        {
            infoPanel.gameObject.SetActive(false);
        }

        private void OnInfoPanelButtonPressed() => 
            infoPanel.gameObject.SetActive(true);
    }
}