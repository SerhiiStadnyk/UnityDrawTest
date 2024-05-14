using UnityEngine;

namespace Core.Runtime
{
    public class ScreenDefault : MonoBehaviour
    {
        public virtual void OpenScreen()
        {
            gameObject.SetActive(true);
        }


        public virtual void CloseScreen()
        {
            gameObject.SetActive(false);
        }
    }
}
