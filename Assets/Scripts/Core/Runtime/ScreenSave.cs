using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ScreenSave : ScreenDefault
    {
        [SerializeField]
        private SaveFilePresenter _saveFilePresenterPrefab;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private TMP_InputField _inputField;

        private TexturesSaveHandler _texturesSaveHandler;
        private ObjectPainter _objectPainter;

        private SaveFilePresenter _selectedPresenter;
        private List<SaveFilePresenter> _presenters = new List<SaveFilePresenter>();


        [Inject]
        private void Inject(TexturesSaveHandler texturesSaveHandler, ObjectPainter objectPainter)
        {
            _texturesSaveHandler = texturesSaveHandler;
            _objectPainter = objectPainter;
        }


        public override void OpenScreen()
        {
            gameObject.SetActive(true);
            UpdateScreen();
        }


        public override void CloseScreen()
        {
            gameObject.SetActive(false);
            ClearScreen();
        }


        public void Save()
        {
            if (ValidateSaveName())
            {
                _texturesSaveHandler.SaveTextureToFile(_objectPainter.DecalTexture, _inputField.text);
                UpdateScreen();
            }
        }


        public void Delete()
        {
            if (_selectedPresenter != null)
            {
                _texturesSaveHandler.DeleteSaveFile(_selectedPresenter.SaveFileName);
                _selectedPresenter = null;
                UpdateScreen();
            }
        }


        private void UpdateScreen()
        {
            ClearScreen();
            foreach (string saveFileName in _texturesSaveHandler.GetSaveFilesNames())
            {
                SaveFilePresenter instance = Instantiate(_saveFilePresenterPrefab, _container);
                instance.Initialize(saveFileName, SelectPresenter);
                _presenters.Add(instance);
            }
        }


        private void ClearScreen()
        {
            for (int index = _presenters.Count - 1; index >= 0; index--)
            {
                Destroy(_presenters[index].gameObject);
            }
            _presenters.Clear();
        }


        private bool ValidateSaveName()
        {
            bool result = true;

            if (string.IsNullOrEmpty(_inputField.text))
            {
                result = false;
            }
            else
            {
                foreach (SaveFilePresenter presenter in _presenters)
                {
                    if (presenter.SaveFileName == _inputField.text)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
        
        
        private void SelectPresenter(SaveFilePresenter presenter)
        {
            if (_selectedPresenter != null)
            {
                _selectedPresenter.MarkAsSelected(false);
            }

            _selectedPresenter = presenter;
            _selectedPresenter.MarkAsSelected(true);
        }
    }
}
