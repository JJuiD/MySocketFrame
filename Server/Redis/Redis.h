#ifndef _REDISDLL_H_
#define _REDISDLL_H_
#ifdef REDISDLL_EXPORTS  
#define REDISDLL_API __declspec(dllexport)   
#else  
#define REDISDLL_API __declspec(dllimport)   
#endif

#include"stdafx.h"
//#include <stdio.h>
//#include <stdlib.h>
#include <hiredis.h>
#define NO_OFORKIMPL 
#include <Win32_Interop\win32fixes.h>
#pragma comment(lib,"hiredis.lib")
#pragma comment(lib,"Win32_Interop.lib")
#include <iostream>
// This class is exported from the testdll.dll  
class redisAPI
{

public:
	//Á´½ÓRedis
	REDISDLL_API bool Connect(const char*, int);
	//template<class... T>
	//REDISDLL_API std::string Excute(std::string, std::string, T..., int);
private:
	template <class T>
	void setCmd(std::string&, T);
private:
	redisContext* _connect;
	redisReply* _reply;
};
#endif

