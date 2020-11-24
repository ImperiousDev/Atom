#pragma once
#include <windows.h>
#include "structs.h"
#include "_spoofer_stub.h"
#include "object.h"
#include "Offsets.h"
#include "imports.h"
#include "Settings.h"
#include <iostream>
#include <fstream>

PVOID(*UEEvent)(PVOID, PVOID, PVOID, PVOID) = nullptr;
FVector AimTo;
PVOID Enemy;

bool AtomSearch(LPCSTR str, ATOM id)
{
	id = spoof_call(jmp, GlobalFindAtomA, str);
	if (id != 0)
		return true;
	else return false;
}

PVOID UEEventHook(UObject* object, UObject* func, PVOID params, PVOID result) 
{
	auto objectname = gobj::getobjectname(object);
	auto funcName = gobj::getobjectname(func);

	if (objectname == "" || funcName == "")
		return spoof_call(jmp, UEEvent, (PVOID)object, (PVOID)func, params, result);

	ATOM ida = 0;

	if (AtomSearch(E("Memory"), ida))
		settings::aimtype = 2;
	else
		settings::aimtype = 3;
	
	if (AtomSearch(E("Box"), ida))
		settings::boxesp = true;
	else
		settings::boxesp = false;

	if (AtomSearch(E("Lines"), ida))

		settings::snaplines = true;

	else
		settings::snaplines = false;

	if (AtomSearch(E("FOV"), ida))
	{
		std::string line;
		std::ifstream myfile("C:\\Maven\\settings.bak");
		if (myfile.is_open())
		{
			while (getline(myfile, line))
			{
				settings::fov = atoi(line.c_str());
			}
			myfile.close();
		}
		else exit(-1);
	}

	if (AtomSearch(E("E"), ida))
	{

	}

	if(Enemy && settings::sniperbullettp)                      
	if (strstr(objectname.c_str(), E("B_Prj_Bullet_Sniper")) && funcName == E("OnRep_FireStart")) //check if function is called with bullet
	{
		// x = 0, y = 0, z = 0
		FVector nullvector = { 0 };

		//rootcomponent
		PVOID rootcomp = read(object, offsets::RootComponent);

		//changing firelocation to players head
		*reinterpret_cast<FVector*>((PBYTE)object + offsets::P_FireStartLocation) = AimTo;

		//changing bullet location to players head
		*reinterpret_cast<FVector*>((PBYTE)rootcomp + offsets::RelativeLocation) = AimTo;

		//setting velocity of bullet to 0
		*reinterpret_cast<FVector*>((PBYTE)rootcomp + offsets::ComponentVelocity) = nullvector;
	}

	return spoof_call(jmp, UEEvent, (PVOID)object, (PVOID)func, params, result);
}
