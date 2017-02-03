using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

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

        enum HostType
        {
            None = 0, 
            Server, 
            Client
        }

        private Mode m_mode;
        private string serverAddress;
        private HostType hostType;
        private const int m_port = 50765;
        private TransportTCP m_transport = null;
        private int m_counter = 0;

        void Awake()
        {
            m_mode = Mode.SelectHost;
            hostType = HostType.None;

            // Network 클래스 컴포넌트 취득
            GameObject obj = new GameObject("Network");
            m_transport = obj.AddComponent<TransportTCP>();
            DontDestroyOnLoad(obj);

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
                    //OnUpdateGame();
                    break;
                case Mode.Disconnection:
                    //OnUpdateDisconnection();
                    break;
                case Mode.Error:
                    OnUpdateError();
                    break;
            }

            ++m_counter;
        }

        public void OnSelectServer()
        {
            hostType = HostType.Server;
        }

        public void OnSelectClient()
        {
            hostType = HostType.Client;
        }

        public void OnUpdateConnection()
        {
            if (m_transport.IsConnected() == true)
            {
                m_mode = Mode.Game;

                // TOOD 배틀 씬 열기
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


        public void OnSelectClient()
        {
            string targetAddress = serverAddress;

            if (txtIPAddress != null)
            {
                targetAddress = txtIPAddress.text;
            }

            bool ret = m_transport.Connect(targetAddress, m_port);
            m_mode = ret ? Mode.Connection : Mode.Error;

            if (m_mode == Mode.Connection)
            {
                OnUpdateConnection();
            }
        }

    }

}
