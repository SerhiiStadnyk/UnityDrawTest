using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Runtime
{
    public class SaveFilePresenter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _saveFileNameText;

        [SerializeField]
        private Image _backgroundImage;

        [SerializeField]
        private Color _normalColor;
        
        [SerializeField]
        private Color _selectedColor;

        private string _saveFileName;
        private Action<SaveFilePresenter> _callback;

        public string SaveFileName => _saveFileName;


        public void Initialize(string saveFileName, Action<SaveFilePresenter> callback)
        {
            _saveFileName = saveFileName;
            _callback = callback;
            
            _saveFileNameText.text = _saveFileName;
        }


        public void Clicked()
        {
            _callback?.Invoke(this);
        }


        public void MarkAsSelected(bool value)
        {
            _backgroundImage.color = value ? _selectedColor : _normalColor;
        }
    }
}
