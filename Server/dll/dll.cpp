// dll.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "dll.h"

void RedisThread()
{

}

DLL_API bool API::Connect(const char *ip, int port)
{
	
	/*isOpenRedisThread = true;
	std::thread t(&API::RedisThread, this);
	t.detach();*/
	/*int a = 1;
	int b = 2;
	return a==b;*/
	return true;
}

void API::RedisThread()
{
	redisContext* _connect;
	while (true)
	{
		if (isOpenRedisThread)
		{
			_connect = redisConnect("127.0.0.1", 6379);
			/*redisContext* _connect = redisConnect("127.0.0.1", 6379);
			if (_connect != NULL && _connect->err)
			{
				printf("connect error: %s\n", _connect->errstr);
				break;
			}*/
		}
		else
		{
			break;
		}

	}
}
