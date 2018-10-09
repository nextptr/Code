#include "dbmanger.h"
#include <QMutex>
#include <QSqlQuery>
#include <QDir>
#include <QFile>
#include <QVariant>
#include <QDebug>

static QMutex s_MutexConstr;
DBManager *DBManager::m_pInstance = NULL;

bool DBManager::getuserlist(QStringList* userlist)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlSearch;
	userlist->clear();

	//设置搜索语句
	sqlSearch += QString("SELECT * FROM usermanagerecord WHERE id > -1 ORDER BY id desc");
	//查询用户管理表
	sqlQuery.prepare(sqlSearch);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	else
	{
		while (sqlQuery.next())
		{
			userlist->append(sqlQuery.value(1).toString());
		}
	}
	return true;
}

bool DBManager::getapplist(QList<appitem>*applist, QString username)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlSearch;
	(*applist).clear();

	//设置搜索语句
	sqlSearch += QString("SELECT * FROM passrecord WHERE username = '%1' ORDER BY id desc").arg(username);
	//查询用户管理表
	sqlQuery.prepare(sqlSearch);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	else
	{
		while (sqlQuery.next())
		{
			appitem tmp;
			tmp.user = sqlQuery.value(1).toString();
			tmp.app = sqlQuery.value(2).toString();
			tmp.pass = sqlQuery.value(3).toString();
			tmp.remark = sqlQuery.value(4).toString();
			(*applist).append(tmp);
		}
	}
	return true;
}

bool DBManager::adduser(QString user)
{
	QSqlQuery sqlQuery(m_db);
		QString sqlCmd = QString(
			"INSERT INTO usermanagerecord(username)VALUES('%1');")
		.arg(user);
	sqlQuery.prepare(sqlCmd);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	return true;
}

bool DBManager::addapp(appitem*tmp)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlCmd = QString(
		"INSERT INTO passrecord(username, app ,password ,remark)VALUES('%1','%2','%3','%4');")
		.arg(tmp->user).arg(tmp->app).arg(tmp->pass).arg(tmp->remark);
	sqlQuery.prepare(sqlCmd);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	return true;
}

bool DBManager::isexituser(QString user)
{
	QSqlQuery query;
	QString strSql = QString("SELECT 1 FROM usermanagerecord where username = '%1'").arg(user);
	query.exec(strSql);
	if (query.next())
	{
		int nResult = query.value(0).toInt();//有表时返回1，无表时返回null  
		if (nResult)
		{
			return true;
		}
	}
	return false;
}

bool DBManager::isexitapp(QString user,QString app)
{
	QSqlQuery query;
	QString strSql = QString("SELECT 1 FROM passrecord where username = '%1' and app = '%2'  ").arg(user).arg(app);
	query.exec(strSql);
	if (query.next())
	{
		int nResult = query.value(0).toInt();//有表时返回1，无表时返回null  
		if (nResult)
		{
			return true;
		}
	}
	return false;
}

bool DBManager::getappitem(QString user, QString app, appitem *tmp)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlSearch;
	//设置搜索语句
	sqlSearch += QString("SELECT * FROM passrecord where username = '%1' and app = '%2'  ").arg(user).arg(app);
	//查询用户管理表
	sqlQuery.prepare(sqlSearch);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	else
	{
		while (sqlQuery.next())
		{
			tmp->user = sqlQuery.value(1).toString();
			tmp->app = sqlQuery.value(2).toString();
			tmp->pass = sqlQuery.value(3).toString();
			tmp->remark = sqlQuery.value(4).toString();
			return true;
		}
	}
	return false;
}

bool DBManager::modifyappremark(appitem * item, QString mark)
{ 
	QSqlQuery sqlQuery(m_db);
	QString sqlUpdateRecord = QString(
		"UPDATE passrecord SET remark = '%1' WHERE username = '%2' and app = '%3';")
		.arg(mark)
		.arg(item->user)
		.arg(item->app);
	sqlQuery.prepare(sqlUpdateRecord);
	if (!sqlQuery.exec())
	{
		return false;
	}
	return true;
}

bool DBManager::ereaseuser(QString user)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlCmd;
	//设置搜索语句
	sqlCmd += QString("DELETE FROM usermanagerecord WHERE username = '%1';").arg(user);
	sqlQuery.prepare(sqlCmd);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	return ereaseapp(user);
}

bool DBManager::ereaseapp(QString user, QString app)
{
	QSqlQuery sqlQuery(m_db);
	QString sqlCmd;
	//设置搜索语句
	sqlCmd += QString("DELETE FROM passrecord WHERE username = '");
	if (user != NULL)
	{
		sqlCmd += user;
	}
	else
	{
		sqlCmd += "null";
	}

	if (app != NULL)
	{
		sqlCmd += QString("' and app = '");
		sqlCmd += app;
		sqlCmd += QString("';");
	}
	else
	{
		sqlCmd += QString("';");
	}
	sqlQuery.prepare(sqlCmd);
	if (!sqlQuery.exec())
	{
		qDebug() << sqlQuery.lastError();
		return false;
	}
	return true;
}



DBManager::DBManager(QObject *parent)
	: QThread(parent)
	, m_isQuit(false)
{
	if (!init())
	{
		;
	}
}
DBManager::~DBManager()
{

}
bool DBManager::init(void)
{
	if (!openDataBase("password.db"))
	{
		qDebug() << "openDatabase failed.";
		return false;
	}

	if (!isexittable("passrecord"))
	{
		//创建数据密码表  
		QSqlQuery sqlQuery;
		QString sql_create_passrecord = "CREATE TABLE passrecord(\
			id INTEGER PRIMARY KEY AUTOINCREMENT,\
            username varchar,\
            app varchar,\
            password varchar,\
            remark varchar)";
		sqlQuery.prepare(sql_create_passrecord); //创建表  
		if (!sqlQuery.exec()) //查看创建表是否成功  
		{
			qDebug() << QObject::tr("Table Create failed");
			qDebug() << sqlQuery.lastError();
			return false;
		}
		else
		{
			qDebug() << "Table Created";
		}
	}
	if (!isexittable("usermanagerecord"))
	{
		//创建用户管理表
		QSqlQuery sqlQuery;
		QString sql_create_usermanagerecord = "CREATE TABLE usermanagerecord(\
			id INTEGER PRIMARY KEY AUTOINCREMENT,\
            username varchar)";
		sqlQuery.prepare(sql_create_usermanagerecord);
		if (!sqlQuery.exec())
		{
			qDebug() << QObject::tr("Table Create failed");
			qDebug() << sqlQuery.lastError();
			return false;
		}
		else
		{
			qDebug() << "Table Created";
		}
	}
	return true;
}
void DBManager::closedatabase()
{
	if (!m_db.open())
	{
		m_db.close();
	}
}
void DBManager::run()
{
	QString threadText = QStringLiteral("@0x%1").arg(quintptr(QThread::currentThreadId()), 16, 16, QLatin1Char('0'));
	QString logStr = QString("run function, thread id = %1").arg(threadText);
	qDebug() << "DBManager:" << logStr;

	while (!m_isQuit)
	{
		static bool isFistConnect = true;
		static bool isConnected = true;
		if (!m_db.isOpen())
		{
			if (!m_db.open())
			{
				if (isConnected != false)
				{
					//emitError();
					;
				}

				isConnected = false;
			}
			else
			{
				qDebug() << "Connect to database";
				if (isConnected != true || isFistConnect)
				{
					//emit signalError("Connect to database successsful");
					;
				}
				isConnected = true;
				isFistConnect = false;
			}
		}
		msleep(500);
	}
	closedatabase();
}
DBManager*DBManager::getInstance(void)
{
	if (m_pInstance == NULL)
	{
		s_MutexConstr.lock();
		if (m_pInstance == NULL)
		{
			m_pInstance = new DBManager();
		}
		s_MutexConstr.unlock();
	}
	return m_pInstance;
}
bool DBManager::openDataBase(const QString & strFileName)
{
	if (!QFile::exists(strFileName))//文件不存在，则创建  
	{
		QDir fileDir = QFileInfo(strFileName).absoluteDir();
		QString strFileDir = QFileInfo(strFileName).absolutePath();
		if (!fileDir.exists()) //路径不存在，创建路径  
		{
			fileDir.mkpath(strFileDir);
		}
		QFile dbFile(strFileName);
		if (!dbFile.open(QIODevice::WriteOnly))//未成功打开  
		{
			dbFile.close();
			return false;
		}
		dbFile.close();
	}

	m_db = QSqlDatabase::addDatabase("QSQLITE");
	m_db.setDatabaseName(strFileName);
	if (m_db.open())
	{
		return true;
	}
	return false;
}
bool DBManager::isexittable(const QString & strtablename)
{
	QSqlQuery query;
	QString strSql = QString("SELECT 1 FROM sqlite_master where type = 'table' and  name = '%1'").arg(strtablename);
	query.exec(strSql);
	if (query.next())
	{
		int nResult = query.value(0).toInt();//有表时返回1，无表时返回null  
		if (nResult)
		{
			return true;
		}
	}
	return false;
}


