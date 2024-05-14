using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Runtime
{
    public class ColorSelector : MonoBehaviour
    {
        [SerializeField]
        private Image _preview;

        [SerializeField]
        private Slider _redSlider;
        
        [SerializeField]
        private Slider _greenSlider;
        
        [SerializeField]
        private Slider _blueSlider;
        
        [SerializeField]
        private Slider _alphaSlider;

        [SerializeField]
        private TMP_InputField _redInputField;
        
        [SerializeField]
        private TMP_InputField _greenInputField;
        
        [SerializeField]
        private TMP_InputField _blueInputField;

        [SerializeField]
        private TMP_InputField _alphaInputField;

        [SerializeField]
        private UnityEvent<Color> _unityEvent;
        
        private Color32 _color = new Color32(255, 255, 255, 255);


        private void Start()
        {
            _redInputField.onValidateInput += ValidateInputFields;
            _greenInputField.onValidateInput += ValidateInputFields;
            _blueInputField.onValidateInput += ValidateInputFields;
            _alphaInputField.onValidateInput += ValidateInputFields;

            UpdateSelector();
        }


        public void SetColor(Color32 color)
        {
            _color = color;
            UpdateSelector();
            _unityEvent?.Invoke(_color);
        }


        public void UpdateSelector()
        {
            UpdateSliders();
            UpdateInputFields();
            _preview.color = _color;
        }


        public void UpdateSliders()
        {
            _redSlider.value = _color.r;
            _greenSlider.value = _color.g;
            _blueSlider.value = _color.b;
            _alphaSlider.value = _color.a;
        }


        public void UpdateInputFields()
        {
            _redInputField.text = _color.r.ToString();
            _greenInputField.text = _color.g.ToString();
            _blueInputField.text = _color.b.ToString();
            _alphaInputField.text = _color.a.ToString();
        }


        public void SetRedValue(float value)
        {
            Color32 color = new Color32((byte)value, _color.g, _color.b, _color.a);
            SetColor(color);
        }
        
        
        public void SetGreenValue(float value)
        {
            Color32 color = new Color32(_color.r, (byte)value, _color.b, _color.a);
            SetColor(color);
        }
        
        
        public void SetBlueValue(float value)
        {
            Color32 color = new Color32(_color.r, _color.g, (byte)value, _color.a);
            SetColor(color);
        }
        
        
        public void SetAlphaValue(float value)
        {
            Color32 color = new Color32(_color.r, _color.g, _color.b, (byte)value);
            SetColor(color);
        }
        
        
        public void SetRedValue(string value)
        {
            byte.TryParse(value, out byte intValue);
            Color32 color = new Color32(intValue, _color.g, _color.b, _color.a);
            SetColor(color);
        }
        
        
        public void SetGreenValue(string value)
        {
            byte.TryParse(value, out byte intValue);
            Color32 color = new Color32(_color.r, intValue, _color.b, _color.a);
            SetColor(color);
        }
        
        
        public void SetBlueValue(string value)
        {
            byte.TryParse(value, out byte intValue);
            Color32 color = new Color32(_color.r, _color.g, intValue, _color.a);
            SetColor(color);
        }
        
        
        public void SetAlphaValue(string value)
        {
            byte.TryParse(value, out byte intValue);
            Color32 color = new Color32(_color.r, _color.g, _color.b, intValue);
            SetColor(color);
        }


        private char ValidateInputFields(string text, int charIndex, char addedChar)
        {
            if (addedChar == '-' || charIndex >= 3)
            {
                addedChar = '\0';
            }

            TMP_InputField[] inputs = { _redInputField, _blueInputField, _greenInputField, _alphaInputField };

            foreach (var inputField in inputs)
            {
                int.TryParse(inputField.text, out int value);
                value = value switch
                {
                    > 255 => 255,
                    < 0 => 0,
                    _ => value
                };

                inputField.text = value.ToString();
            }

            return addedChar;
        }
    }
}
