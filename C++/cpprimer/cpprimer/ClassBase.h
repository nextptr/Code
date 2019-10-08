#pragma once

#include <iostream>
using namespace std;

class  ClassBase
{
public:
	 ClassBase();
	~ ClassBase();

private:
	virtual void display()
	{}
};

 ClassBase:: ClassBase()
{
}

 ClassBase::~ ClassBase()
{
}
