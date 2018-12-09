#ifndef SOCKET_MANAGER_H
#define SOCKET_MANAGER_H

#include "SocketUdp.h"

namespace socketframe
{
	/*static HANDLE m_Thread;
	static DWORD dwThreadId;

	static HANDLE m_KcpRecvThread;
	static DWORD dwKcpRecvThread;*/

	

	//DWORD WINAPI DoUdpMainLoop(LPVOID lpParam)
	//{
	//	

	//	return 0;
	//}

	/*DWORD WINAPI DoKcpRecvLoop(LPVOID lpParam)
	{
		while (true)
		{
			char recvData[RECV_KCP_BUFFER_LEN];
			int ret = ikcp_recv(_SocketClass->GetKcp(), recvData, RECV_KCP_BUFFER_LEN);
			if (ret > 0)
			{
				SOCKET_TOLOGIC_PACKET data;
				memcpy(&data, recvData, sizeof(data) + 1);
				printf("解析kcp包 id:%d \r\n",data.id);
			}
		}

		return 0;
	}*/

	//int Kcp_Output(const char *buf, int len, ikcpcb *kcp, void *user)
	//{
	//	if (_SocketClass->GetSocket() == NULL)
	//	{
	//		printf("m_SocketUdp is NULL ...... Send Error");
	//		return 0;
	//	}
	//	string str = "";
	//	for (WORD i = RECV_BUFFER_LEN; i <= RECV_KCP_BUFFER_LEN; ++i)
	//	{
	//		str += *(buf + i + 1);
	//	}
	//	stringstream stream(str);
	//	UINT id;
	//	stream >> id;
	//	//UDP_USERINFO _userInfo = _SocketClass->GetUerSocketInfo(id);
	//	//sendto(_SocketClass->GetSocket(), buf, RECV_BUFFER_LEN, 0, (sockaddr *)&_userInfo.addr, _userInfo.len);
	//	return 0;
	//}
	static SocketBase* _SocketClass;

	class SocketManager
	{
	private:
		
	public:
		~SocketManager() {};
		static SocketManager & getInstance()
		{
			static SocketManager m_instance;
			return m_instance;
		}

		static void StartListenNetPacket(int type)
		{
			switch (type)
			{
			case NET_TYPE_UDP:
				_SocketClass = new UDP();
				_SocketClass->StartServer(NET_PORT,0);
				//_SocketClass->SetRecvLoopListener(DoUdpMainLoop);
			//	_SocketClass->SetKCPOutPutListener(Kcp_Output);

				//创建线程
				//m_Thread = CreateThread(NULL	// 默认安全属性
				//	, NULL		// 默认堆栈大小
				//	, DoUdpMainLoop // 线程入口地址
				//	, NULL	//传递给线程函数的参数
				//	, 0		// 指定线程立即运行
				//	, &(dwThreadId)	//线程ID号
				//);

				//m_KcpRecvThread = CreateThread(NULL	// 默认安全属性
				//	, NULL		// 默认堆栈大小
				//	, DoKcpRecvLoop // 线程入口地址
				//	, NULL	//传递给线程函数的参数
				//	, 0		// 指定线程立即运行
				//	, &(dwKcpRecvThread)	//线程ID号
				//);

			}

			sockaddr_in remoteAddr;
			int nAddrLen = sizeof(remoteAddr);
			
			while (true)
			{
				char recvData[RECV_BUFFER_LEN] = {'\0'};
				printf("is waiting for client msg..........\n");
				int ret = recvfrom(_SocketClass->GetSocket(), recvData, RECV_BUFFER_LEN, 0, (sockaddr*)&remoteAddr, &nAddrLen);
				if (ret > 0)
				{
					char sendBuf[20] = { '\0' };
					printf("接受到一个连接：%s \r\n", inet_ntop(AF_INET, (void*)&remoteAddr.sin_addr, sendBuf, 16));
					NET_CLIENT client = DataCenter::getInstance().GetClient(remoteAddr);
					if (client.id != USERID_NULL)
					{
						//存在客户端
					}
					else
					{
						DataCenter::getInstance().AddClient(remoteAddr);
						client = DataCenter::getInstance().GetClient(remoteAddr);
					}
					DataCenter::getInstance().AddBuffer(recvData, client.id);
					_SocketClass->Recv(DataCenter::getInstance().GetClient(remoteAddr), recvData);
					//UDP_USERINFO tempData = _SocketClass->GetUerSocketInfo(remoteAddr);
					//if (tempData.id != USERID_NULL)
					//{
					//	//存在客户端地址
					//}
					//else
					//{
					//	tempData.id = _SocketClass->GetConnectNum();
					//	tempData.addr = &(remoteAddr);
					//	tempData.len = nAddrLen;
					//	_SocketClass->AddUserSocketInfo(tempData.id, tempData);
					//}
					//_SocketClass->Recv(tempData, recvData);
				}

			}
		}

		static void Realse()
		{
			if (_SocketClass == NULL) return;
			delete _SocketClass;

			_SocketClass = NULL;
			/*if (m_Thread != NULL)
			{
				CloseHandle(m_Thread);
				m_Thread = NULL;
				dwThreadId = NULL;
			}
			if (m_KcpRecvThread != NULL)
			{
				CloseHandle(m_KcpRecvThread);
				m_KcpRecvThread = NULL;
				dwKcpRecvThread = NULL;
			}*/
		}


		static void Send(char* buffer, UINT id)
		{
			_SocketClass->Send(buffer, id);
		}
	};
}
#endif