#ifndef REDIS_MODEL_H
#define REDIS_MODEL_H
#include "../Config/RedisConfig.h"

#include <hiredis.h>
#define NO_OFORKIMPL 
#include <Win32_Interop\win32fixes.h>
#pragma comment(lib,"hiredis.lib")
#pragma comment(lib,"Win32_Interop.lib")

class RedisModel
{

public:
	~RedisModel() {}
	static RedisModel & geInstance()
	{
		static RedisModel m_instance;
		return m_instance;
	}
public:
	bool Connect(string ip = "127.0.0.1", int port= 6379)
	{
		_connect = redisConnect("127.0.0.1", 6379);
		/*redisContext* _connect = redisConnect("127.0.0.1", 6379);
		if (_connect != NULL && _connect->err)
		{
			printf("connect error: %s\n", _connect->errstr);
			break;
		}*/
	}
private:
	redisContext* _connect;
};


#endif
