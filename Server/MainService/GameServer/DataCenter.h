#ifndef DATA_CENTER_H
#define DATA_CENTER_H

#include "../Protocol/ProtocolBase.h"
#include "../Config/ConfigBase.h"

struct NET_CLIENT
{
	sockaddr_in addr;
	UINT id;
	time_t heartTime;

	NET_CLIENT()
	{
		id = USERID_NULL;
		heartTime = time(NULL);
	}

	~NET_CLIENT()
	{
		
	}

	bool operator == (sockaddr_in _addr)
	{
		return (_addr.sin_port == addr.sin_port
			&& _addr.sin_addr.S_un.S_addr == addr.sin_addr.S_un.S_addr
			);
	}
	bool operator == (UINT _id)
	{
		return (id == _id);
	}
	bool IsOverTime(time_t nowTime)
	{
		return (nowTime - heartTime) >= HEART_INTERVAL_TIME;
	}
};

struct PACKET
{
	Proto::ProtoCommand CMD_HEAD;
	UINT32 CMD_BODY;
	//WORD PACKET_SIZE;
	UINT ID;
	string PACKET_BUFFER;
};

typedef map<UINT, NET_CLIENT> MAP_CLIENT;

class DataCenter
{
private:
	MAP_CLIENT ClientMap;
	WORD Connect_Client_Num = 1;
	list<PACKET*> RecvBufferList;
	time_t HeartStartTime = NULL;
public:
	~DataCenter() {}
	static DataCenter & getInstance()
	{
		static DataCenter m_instance;
		return m_instance;
	}
public:
	void AddBuffer(char* buffer,int size,UINT id)
	{
		/*string str_id = to_string(id);
		for (WORD i = 0; i < str_id.length(); ++i)
		{
			buffer[size + i + 1] = str_id[i];
		}*/
		if (HeartStartTime == NULL)
		{
			HeartStartTime = time(NULL);
		}

		Proto::ProtoBaseCmd CmdData;
		CmdData.ParseFromArray(buffer, size);
		PACKET* packet = new PACKET() ;
		packet->CMD_HEAD = CmdData.cmdhead();
		packet->CMD_BODY = CmdData.cmdinfo();
		packet->PACKET_BUFFER = CmdData.buffer();
		packet->ID = id;
		RecvBufferList.push_back(packet);
	}
	PACKET* GetBuffer()
	{
		if (RecvBufferList.empty()) return NULL;
		PACKET* packet = RecvBufferList.front();
		RecvBufferList.pop_front();
		return packet;
	}
public:
	NET_CLIENT GetClient(UINT id)
	{
		if (ClientMap[id] == USERID_NULL) return NET_CLIENT();
		return ClientMap[id];
	}
	NET_CLIENT GetClient(sockaddr_in _addr) {
		MAP_CLIENT::iterator itr = ClientMap.begin();
		while (itr != ClientMap.end())
		{
			if (itr->second == _addr)
			{
				return itr->second;
			}
		}
		return NET_CLIENT();
	}
	bool AddClient(sockaddr_in _addr)
	{
		NET_CLIENT client = GetClient(_addr);
		if (client.id != USERID_NULL) return false;
		client.id = Connect_Client_Num;
		client.addr = _addr;
		ClientMap.emplace(pair<UINT, NET_CLIENT>(Connect_Client_Num, client));
		++Connect_Client_Num;
		return true;
	}
	bool UpdateClientHeart(UINT id)
	{
		if (id == CHECK_ALL_HEART_TAG)
		{
			if (ClientMap.size() == 0 || HeartStartTime == NULL) return false;
			time_t now_time = time(NULL);
			if ((now_time - HeartStartTime) < HEART_CHECK_INTERVAL_TIME) return false;
			MAP_CLIENT::iterator itr = ClientMap.begin();
			HeartStartTime = now_time;
			list<UINT> deleteId;
			while (itr != ClientMap.end())
			{
				if (itr->second.IsOverTime(HeartStartTime))
				{
					deleteId.push_back(itr->second.id);
				}
			}
			while (deleteId.size() != 0)
			{
				RemoveClient(deleteId.front());
				deleteId.pop_front();
			}
		}
		else
		{
			NET_CLIENT client = GetClient(id);
			if (client.id != USERID_NULL) return false;
			time_t now_time = time(NULL);
			if (client.IsOverTime(now_time))
			{
				RemoveClient(id);
				return false;
			}
			else
			{
				client.heartTime = now_time;
			}
		}

		return true;
	}
	//bool AddClient(NET_CLIENT client)
	//{
	//	NET_CLIENT _client = GetClient(*client.addr);
	//	if (_client.id != USERID_NULL) return false;
	//	ClientMap.emplace(pair<UINT, NET_CLIENT>(Connect_Client_Num, client));
	//	++Connect_Client_Num;
	//	return true;
	//}

	bool RemoveClient(UINT id)
	{
		if (ClientMap[id] == USERID_NULL) return false;
		cout << "RemoveClient Id:" << id << " Time:" << ClientMap[id].heartTime << endl;
		ClientMap.erase(id);
		if (ClientMap.size() == 0)
		{
			HeartStartTime = NULL;
		}
		--Connect_Client_Num;
		return true;
	}
	bool RemoveClient(sockaddr_in _addr)
	{
		NET_CLIENT client = GetClient(_addr);
		if (client.id != USERID_NULL) return false;

		return RemoveClient(client.id);
	}

	public:

};




#endif