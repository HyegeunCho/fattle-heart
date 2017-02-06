using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using UniRx;



namespace fattleheart.battle
{
    public class BattleNetworkManager : MonoBehaviour
    {
        [SerializeField]
        private InputField txtIPAddress;

        enum Mode
        {
            SelectHost = 0, 
            Connection, 
            Game, 
            Disconnection, 
            Error
        }

        public enum HostType
        {
            None = 0, 
            Server, 
            Client
        }

        private Mode m_mode;
        private string serverAddress;
        private HostType hostType;
        public HostType GetHostType
        {
            get
            {
                return hostType;
            }
        }

        private const int m_port = 50765;
        private TransportTCP m_transport = null;
        public TransportTCP TCP
        {
            get
            {
                return m_transport;
            }
        }

        private int m_counter = 0;

        void Awake()
        {
            m_mode = Mode.SelectHost;
            hostType = HostType.None;

            GameObject obj = GameObject.Find("Network");
            if (obj == null)
            {
                obj = new GameObject("Network");
                m_transport = obj.AddComponent<TransportTCP>();
                DontDestroyOnLoad(obj);
            }
            else
            {
                OnUpdateDisconnection();
            }

            
            

            // 호스트명 획득
            string hostname = Dns.GetHostName();
            // 호스트명에서 IP 획득
            IPAddress[] adrList = Dns.GetHostAddresses(hostname);
            serverAddress = adrList[0].ToString();

            if (txtIPAddress != null)
            {
                txtIPAddress.text = serverAddress;
            }
        }

        void Start()
        {
            Observable.EveryUpdate().Select(_ => m_mode).DistinctUntilChanged().Subscribe(_ => Debug.Log(string.Format("[BattleNetworkManager] m_mode = {0}", m_mode.ToString())));
            Observable.EveryUpdate().Select(_ => m_transport.IsConnected()).DistinctUntilChanged().Subscribe(_ => Debug.Log(string.Format("[BattleNetworkManager] isConnected - {0}", m_transport.IsConnected())));
        }


        // Update is called once per frame
        void Update()
        {
            switch (m_mode)
            {
                case Mode.SelectHost:
                    OnUpdateSelectHost();
                    break;
                case Mode.Connection:
                    OnUpdateConnection();
                    break;
                case Mode.Game:
                    OnUpdateGame();
                    break;
                case Mode.Disconnection:
                    OnUpdateDisconnection();
                    break;
            }

            ++m_counter;
        }

        public void OnSelectServer()
        {
            hostType = HostType.Server;
            Debug.Log("[BattleNetworkManager] (OnSelectServer) - Change HostType to Server");
        }

        public void OnSelectClient()
        {
            hostType = HostType.Client;
            Debug.Log("[BattleNetworkManager] (OnSelectServer) - Change HostType to Client");
        }

        public void OnUpdateConnection()
        {
            if (m_transport.IsConnected() == true)
            {
                Debug.Log("[BattleNetworkManager] (OnUpdateConnection) - Change Mode to Mode.Game");
                m_mode = Mode.Game;
            }
        }

        public void OnUpdateGame()
        {
            if (m_transport.IsConnected() == true)
            {
                Debug.Log("[BattleNetworkManager] (OnUpdateGame) - Load Battle Scene");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
            }
        }

        public void OnUpdateSelectHost()
        {
            switch (hostType)
            {
                case HostType.Server:
                    {
                        bool ret = m_transport.StartServer(m_port, 1);
                        m_mode = ret ? Mode.Connection : Mode.Error;
                    }
                    break;
                case HostType.Client:
                    {
                        string targetAddress = serverAddress;

                        if (txtIPAddress != null)
                        {
                            targetAddress = txtIPAddress.text;
                        }

                        bool ret = m_transport.Connect(targetAddress, m_port);
                        m_mode = ret ? Mode.Connection : Mode.Error;
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnUpdateDisconnection()
        {
            switch (hostType)
            {
                case HostType.Server:
                    m_transport.StopServer();
                    break;
                case HostType.Client:
                    m_transport.Disconnect();
                    break;
                default:
                    break;
            }

            m_mode = Mode.SelectHost;
            hostType = HostType.None;
            string hostname = Dns.GetHostName();
            IPAddress[] adrList = Dns.GetHostAddresses(hostname);
            serverAddress = adrList[0].ToString();
        }
    }
}
