#ifndef SOCKET_BASE_H
#define SOCKET_BASE_H

#include "SocketConfig.h"
#include "../DataCenter.h"

namespace socketframe
{
	class SocketBase
	{


	public:
		//�������� 
		virtual int StartServer(int, WORD) = 0;
		//�رշ���
		virtual void Close() = 0;
		//���ͺ���
		virtual void Send(char *, UINT) = 0;
		//���պ���
		virtual void Recv(NET_CLIENT, char *) = 0;
	public:
		//��ȡSocket
		virtual SOCKET GetSocket() = 0;
		//��ȡ������
		virtual BYTE GetConnectNum() = 0;
		//��ȡkcp
		virtual ikcpcb* GetKcp() = 0;

		//----------------------------UDP----------------------------
	public:
		//����û�
		//virtual bool AddUserSocketInfo(UINT, UDP_USERINFO) = 0;
		//��ȡ�û�(sockaddr_in)
		//virtual UDP_USERINFO GetUerSocketInfo(sockaddr_in) = 0;
		//��ȡ�û�(UINT ID)
		//virtual UDP_USERINFO GetUerSocketInfo(UINT) = 0;
		//���kcp�������
		virtual void SetKCPOutPutListener(int output(const char *buf, int len, ikcpcb *kcp, void *user)) = 0;
		//------------------------------------------------------------
	};
}

#endif