#ifndef SOCKET_BASE_H
#define SOCKET_BASE_H

#include "SocketConfig.h"
#include "../DataCenter.h"

namespace socketframe
{
	class SocketBase
	{


	public:
		//开启服务 
		virtual int StartServer(int, WORD) = 0;
		//关闭服务
		virtual void Close() = 0;
		//发送函数
		virtual void Send(char *, UINT) = 0;
		//接收函数
		virtual void Recv(NET_CLIENT, char *) = 0;
	public:
		//获取Socket
		virtual SOCKET GetSocket() = 0;
		//获取连接数
		virtual BYTE GetConnectNum() = 0;
		//获取kcp
		virtual ikcpcb* GetKcp() = 0;

		//----------------------------UDP----------------------------
	public:
		//添加用户
		//virtual bool AddUserSocketInfo(UINT, UDP_USERINFO) = 0;
		//获取用户(sockaddr_in)
		//virtual UDP_USERINFO GetUerSocketInfo(sockaddr_in) = 0;
		//获取用户(UINT ID)
		//virtual UDP_USERINFO GetUerSocketInfo(UINT) = 0;
		//添加kcp输出监听
		virtual void SetKCPOutPutListener(int output(const char *buf, int len, ikcpcb *kcp, void *user)) = 0;
		//------------------------------------------------------------
	};
}

#endif