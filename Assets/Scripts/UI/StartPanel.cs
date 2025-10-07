using UnityEngine;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class StartPanel : MonoBehaviour
    {
        public GameObject startPanel;
        [SerializeField] internal Button startButton;

        public void Show()
        {
            startPanel.SetActive(true);
        }
        public void Hide()
        {
            startPanel.SetActive(false);
        }
    }
}