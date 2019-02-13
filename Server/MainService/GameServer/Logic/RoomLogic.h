#ifndef LOGIC_ROOM_H
#define LOGIC_ROOM_H

#include "LogicBase.h"

namespace logic
{
	class RoomLogic : LogicBase
	{
	public:
		RoomLogic(UINT);
		
		~RoomLogic();

		
	private:
		void SendData(UINT, char* );

	public:
		void onLogicPacket(UINT, string);
	};
}



#endif
