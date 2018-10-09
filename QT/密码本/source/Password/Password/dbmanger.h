#ifndef DBMANGER_H
#define DBMANGER_H

#include <QThread>
#include <QSqlDatabase>
#include <QSqlError>
#include <QMutex>
#include "publiclass.h"


class DBManager : public QThread
{
	Q_OBJECT

public:
	static DBManager*getInstance(void);

	bool getuserlist(QStringList*userlist);
	bool getapplist(QList<appitem>*applist,QString username);
	bool adduser(QString user);
	bool addapp(appitem*tmp);
	bool isexituser(QString user);
	bool isexitapp(QString user, QString app);
	bool getappitem(QString user, QString app, appitem* tmp);
	bool modifyappremark(appitem*item, QString mark);
	bool ereaseuser(QString user);
	bool ereaseapp(QString user, QString app=NULL);


private:
	static DBManager*m_pInstance;
	QSqlDatabase m_db;
	bool m_isQuit;

	explicit DBManager(QObject *parent = 0);
	~DBManager(void);
	bool init(void);
	bool openDataBase(const QString&strFileName);
	bool isexittable(const QString&strtablename);

	void closedatabase();

	class CGarbo
	{
	public:
		~CGarbo()
		{
			if (DBManager::m_pInstance)
			{
				delete DBManager::m_pInstance;
			}
		}
	};
	static CGarbo m_CGarbo; //定义一个静态成员，程序结束时，系统会自动调用它的析构函数
protected:
	void run();
};


#endif // DBMANGER_H
