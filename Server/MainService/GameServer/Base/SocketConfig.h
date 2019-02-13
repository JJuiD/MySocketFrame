#ifndef SOCKET_CONFIG_H
#define SOCKET_CONFIG_H

#include "../../Config/ConfigBase.h"
//////////////////////////////[KCP BASE]//////////////////////////////
#include "ikcp.h"
//////////////////////////////[SOCKET]//////////////////////////////
#include <winsock2.h>
#include <WS2tcpip.h>
#pragma comment(lib,"ws2_32.lib")  
//////////////////////////////[LOGIC]//////////////////////////////
//#include "../Logic/BaseLogic.h"
//using namespace _logic;
//////////////////////////////[DEFINE]//////////////////////////////
#define KCP_SESSION 0x11223344
#define USERID_LEN 32
#define RECV_KCP_BUFFER_LEN RECV_BUFFER_LEN+USERID_LEN
#define NET_TYPE_UDP 1
#define NET_PORT 5501

//////////////////////////////[STRUCT]//////////////////////////////

struct SOCKET_TOLOGIC_PACKET
{
	char* buffer;
	WORD id;

	char* Get(char* _buffer,WORD _id)
	{
		char pack[RECV_KCP_BUFFER_LEN];
		char* ptr = pack;
		strcpy(ptr, _buffer);
		*((WORD*)(ptr + RECV_BUFFER_LEN)) = _id;
		return ptr;
	}
};
//////////////////////////////[TYPEDEF]//////////////////////////////











#endif // !SOCKET_CONFIG_H
