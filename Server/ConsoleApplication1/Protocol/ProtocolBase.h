#ifndef PROTOCOL_BASE_H
#define PROTOCOL_BASE_H

//#include <../../GameServer/Base/SocketConfig.h>
#include "_protocol/ProtoBase.pb.h"

namespace protocol
{
//////////////////////////////////////////////////////////////////////////////////
#define CMD_PROTO_PERSON            1
//////////////////////////////////////////////////////////////////////////////////
#define CMD_FIRST                   10000             //协议起始编号
#define CMD_MESSAGE_TEST            CMD_FIRST + 1     //测试协议编号

//////////////////////////////////////////////////////////////////////////////////

	
	class ProtocolBase 
	{
	private:
		//map< WORD, class> cmdTomessageTable;
	public:

		


	};






 



























}
#endif // PROTOCOL_BASE_H
