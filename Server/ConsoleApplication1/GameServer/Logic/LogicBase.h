#ifndef LOGIC_BASE_H
#define LOGIC_BASE_H

#include "../Base/SocketManager.h"
using namespace protocol;

namespace logic
{
	class LogicBase
	{
	protected:
		UINT LogicUserID;
	protected:
		virtual void SendData(UINT, char*) = 0;

	public:
		virtual void onLogicPacket(UINT , string) = 0;

	};
}



#endif