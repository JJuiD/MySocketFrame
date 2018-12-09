#include "SocketUdp.h"


namespace socketframe
{
	int UDP::StartServer(int port, WORD mode)
	{
		WSADATA wsaData;
		WORD sockVersion = MAKEWORD(2, 2);
		if (WSAStartup(sockVersion, &wsaData) != 0)
		{
			return 0;
		}

		if (m_SocketUdp != NULL)
		{
			printf("socket is Connect............ ");
			return 0;
		}

		m_SocketUdp = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
		if (m_SocketUdp == INVALID_SOCKET)
		{
			printf("socket error !");
			return 0;
		}


		sockaddr_in serAddr;
		serAddr.sin_family = AF_INET;
		serAddr.sin_port = htons(port);
		serAddr.sin_addr.S_un.S_addr = INADDR_ANY;
		if (bind(m_SocketUdp, (sockaddr *)&serAddr, sizeof(serAddr)) == SOCKET_ERROR)
		{
			printf("bind error !");
			closesocket(m_SocketUdp);
			return 0;
		}
		Client_Num = 0;
		m_Server_Kcp = ikcp_create(KCP_SESSION, (void*)0);

		ikcp_wndsize(m_Server_Kcp, 128, 128);
		m_ClientMap = new UDP_MAP_CLIENT();
		if (mode == 0)
		{
			ikcp_nodelay(m_Server_Kcp, 0, 10, 0, 0);
		}


		printf("StartServer Success...\n");
		return 0;
	}

	void UDP::Close()
	{
		closesocket(m_SocketUdp);
		WSACleanup();

		m_SocketUdp = NULL;
		m_ClientMap->clear();
		Client_Num = 0;
	}

	void UDP::Send(char * buffer, UINT clientAddrId)
	{
		if (m_SocketUdp == NULL)
		{
			printf("m_SocketUdp is NULL ...... Send Error");
			return;
		}
		//ikcp_send(m_Server_Kcp, buffer, RECV_KCP_BUFFER_LEN);
		UDP_USERINFO _userInfo = GetUerSocketInfo(clientAddrId);
		sendto(m_SocketUdp, buffer, RECV_BUFFER_LEN, 0, (sockaddr *)&_userInfo.addr, _userInfo.len);
	}

	void UDP::Recv(NET_CLIENT netClient, char* buffer)
	{
		if (m_SocketUdp == NULL)
		{
			printf("m_SocketUdp is NULL ...... Recv Error");
			return;
		}
		//SOCKET_TOLOGIC_PACKET data;
		//char* ptrPack = data.Get(buffer,struct_addrIn.id);
		//struct_addrIn.logic->DoMainlogic(buffer, RECV_BUFFER_LEN);
		//ikcp_input(m_Server_Kcp, ptrPack, RECV_KCP_BUFFER_LEN);
	}

	UDP_USERINFO UDP::GetUerSocketInfo(sockaddr_in _addr) {
		UDP_MAP_CLIENT::iterator itr = m_ClientMap->begin();
		while (itr != m_ClientMap->end())
		{
			if (itr->second == _addr)
			{
				return itr->second;
			}
		}
		return UDP_USERINFO();
	}

	UDP_USERINFO UDP::GetUerSocketInfo(UINT id)
	{
		if ((*m_ClientMap)[id] == USERID_NULL) return UDP_USERINFO();

		return (*m_ClientMap)[id];
	}

	bool UDP::AddUserSocketInfo(UINT id, UDP_USERINFO user)
	{
		if (GetUerSocketInfo(id).id != USERID_NULL) return false;

		m_ClientMap->emplace(pair<UINT, UDP_USERINFO>(id, user));
		return true;

	}

	void UDP::SetKCPOutPutListener(int output(const char * buf, int len, ikcpcb *kcp, void *user))
	{
		m_Server_Kcp->output = output;
	}

}







