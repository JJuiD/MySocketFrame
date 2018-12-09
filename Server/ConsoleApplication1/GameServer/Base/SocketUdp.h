#ifndef SOCKET_UDP_H
#define SOCKET_UDP_H

#include "SocketBase.h"

namespace socketframe
{
	struct UDP_USERINFO
	{
		sockaddr_in* addr;
		UINT id;
		UINT len;
		//BaseLogic* logic;
		//ikcpcb* kcp;
		UDP_USERINFO()
		{
			addr = new sockaddr_in();
			id = USERID_NULL;
			//logic = new BaseLogic();
		}

		~UDP_USERINFO()
		{
			//delete logic;
			delete addr;
		}

		bool operator == (sockaddr_in _addr)
		{
			return (_addr.sin_port == addr->sin_port
				&& _addr.sin_addr.S_un.S_addr == addr->sin_addr.S_un.S_addr
				);
		}
		bool operator == (UINT _id)
		{
			return (id == _id);
		}
	};

	typedef map<UINT, UDP_USERINFO> UDP_MAP_CLIENT;

	class UDP :public SocketBase
	{
	private:
		SOCKET m_SocketUdp;
		UDP_MAP_CLIENT *m_ClientMap;
		BYTE Client_Num;
		ikcpcb* m_Server_Kcp;
		void* MainLoopFunc;
	public:
		int StartServer(int, WORD);
		void Close();
		void Send(char *, UINT);
		void Recv(NET_CLIENT, char*);
	public:
		SOCKET GetSocket() { return m_SocketUdp; }
		BYTE GetConnectNum() { return Client_Num; }
		ikcpcb* GetKcp() { return m_Server_Kcp; }
	public:
		UDP_USERINFO GetUerSocketInfo(sockaddr_in);
		UDP_USERINFO GetUerSocketInfo(UINT);
		bool AddUserSocketInfo(UINT, UDP_USERINFO);
		void SetKCPOutPutListener(int output(const char *buf, int len, ikcpcb *kcp, void *user));

		/*static bool AddClient(UDP_USERINFO data)
		{
			if(GetAddrByRecvNetAddr_IN(data.addr) == NULL)
		}*/
		//int Kcp_Output(const char*, int, ikcpcb*, void*);


	};
}

#endif
