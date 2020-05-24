using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.NetworkRoomCanvas
{
    [RequireComponent(typeof(Button))]
    public class ServerStartButton : MonoBehaviour
    {
        NetworkRoomPlayerExample localPlayer;
        private Button button;
        private NetworkRoomManagerExample manager;

        void OnEnable()
        {
            button = GetComponent<Button>();
            if (NetworkServer.active)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(OnClick);

                manager = NetworkManager.singleton as NetworkRoomManagerExample;
                manager.onServerAllPlayersReady += Manager_onServerAllPlayersReady;
            }
            else
            {
                // button only needs to be active for server
                button.gameObject.SetActive(false);
            }
        }

        void OnDisable()
        {
            manager.onServerAllPlayersReady -= Manager_onServerAllPlayersReady;
        }

        private void Manager_onServerAllPlayersReady(bool allReady)
        {
            button.interactable = allReady;
        }

        private void OnClick()
        {
            manager.ServerChangeScene(manager.GameplayScene);
        }
    }
}