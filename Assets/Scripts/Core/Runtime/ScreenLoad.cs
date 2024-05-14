using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ScreenLoad : ScreenDefault
    {
        [SerializeField]
        private SaveFilePresenter _saveFilePresenterPrefab;

        [SerializeField]
        private Transform _container;

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


        public void Load()
        {
            if (_selectedPresenter != null)
            {
                Texture2D texture = _texturesSaveHandler.LoadTextureFromFile(_selectedPresenter.SaveFileName);
                _objectPainter.SetTexture(texture);
                CloseScreen();
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
                instance.Initialize(saveFileName, (presenter) => _selectedPresenter = presenter);
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
    }
}
