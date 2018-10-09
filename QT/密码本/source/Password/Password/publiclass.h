#pragma once
#include<QString>

struct appitem
{
public:
	QString user;
	QString app;
	QString pass;
	QString remark;
	appitem(QString use = NULL, QString ap = NULL, QString pa = NULL,QString str=NULL)
	{
		user = use;
		app = ap;
		pass = pa;
		remark = str;
	}
};
