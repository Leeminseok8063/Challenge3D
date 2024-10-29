using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class UIEnviroments : MonoBehaviour
    {
        public GameObject EnvPanel;

        private void Start()
        {
            CharacterManager.Instance.Player.controller.enviroments += ToggleEnv;
            EnvPanel.SetActive(false);
        }

        public void ToggleEnv()
        {
            EnvPanel.SetActive(!EnvPanel.activeSelf);
        }
    }
}
