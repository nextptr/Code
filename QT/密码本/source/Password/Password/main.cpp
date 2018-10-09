#include "Password.h"
#include "dbmanger.h"

#include <QtWidgets/QApplication>

DBManager *g_pDBManage;


int main(int argc, char *argv[])
{
	QApplication a(argc, argv);

	g_pDBManage = DBManager::getInstance();
	g_pDBManage->start();

	Password w;
	w.show();
	return a.exec();
}
