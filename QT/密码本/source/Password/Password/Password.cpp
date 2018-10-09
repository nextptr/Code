#include "Password.h"
#include "dbmanger.h"

Password::Password(QWidget *parent)
	: QWidget(parent)
{
	ui.setupUi(this);

	InitWidget();
	Inituserlist();
	Initapplist();
	InitConnect();
}
Password::~Password()
{

}

void Password::InitWidget()
{
	{
		this->setFixedSize(345, 120);
		this->setWindowTitle("密码本");
		this->setWindowIcon(QIcon("./pass.ico"));
	}
	{   //用户管理
		userBox = new QComboBox(this);
		userBox->setFixedSize(100, 25);
		userBox->move(5, 5);
		username = new QLineEdit(this);
		username->setFixedSize(100, 25);
		username->move(110, 5);
		btn_adduser = new QPushButton(this);
		btn_adduser->setFixedSize(70, 25);
		btn_adduser->move(215, 5);
		btn_adduser->setText("添加用户");
		btn_eraseuser = new QPushButton(this);
		btn_eraseuser->setFixedSize(50, 25);
		btn_eraseuser->move(290, 5);
		btn_eraseuser->setText("删除");
		//密码管理
		itemBox = new QComboBox(this);
		itemBox->setFixedSize(100, 25);
		itemBox->move(5, 35);
		appname = new QLineEdit(this);
		appname->setFixedSize(100, 25);
		appname->move(110, 35);
		btn_addapp = new QPushButton(this);
		btn_addapp->setFixedSize(70, 25);
		btn_addapp->move(215, 35);
		btn_addapp->setText("确认转换");
		btn_eraseapp = new QPushButton(this);
		btn_eraseapp->setFixedSize(50, 25);
		btn_eraseapp->move(290, 35);
		btn_eraseapp->setText("删除");
		//备注
		QLabel *lab_mark = new QLabel(this);
		lab_mark->setFixedSize(70,25);
		lab_mark->setText("《《《备注");
		lab_mark->move(215, 65);
		btn_markmodif = new QPushButton(this);
		btn_markmodif->setFixedSize(50, 25);
		btn_markmodif->move(290, 65);
		btn_markmodif->setText("修改");
		appRemark= new QLineEdit(this);
		appRemark->setFixedSize(205, 25);
		appRemark->move(5, 65);
		//显示
		labord = new QLabel(this);
		labord->setFixedWidth(250);
		labord->move(5, 100);
	}
	{
		userlist = new QStringList();
		applist = new QList<appitem>();
		App = new QString();
		Pass = new QString();
	}
}

void Password::InitConnect()
{
	connect(btn_adduser, &QPushButton::clicked, this, &Password::ON_btnAddClick);
	connect(btn_addapp, &QPushButton::clicked, this, &Password::ON_btnPassClick);
	connect(btn_eraseuser, &QPushButton::clicked, this, &Password::ON_btnEreaseUser);
	connect(btn_eraseapp, &QPushButton::clicked, this, &Password::ON_btnEreaseApp);
	connect(userBox, SIGNAL(currentIndexChanged(int)), this, SLOT(ON_comboxAddChange()));
	connect(itemBox, SIGNAL(currentIndexChanged(int)), this, SLOT(ON_comboxPassChange()));
	connect(btn_markmodif, &QPushButton::clicked, this, &Password::ON_labRemarkChange);
	//connect(appRemark, &QLineEdit::textChanged, this, &Password::ON_btnEreaseApp);

}


void Password::Inituserlist()//初始化用户容器
{
	DBManager::getInstance()->getuserlist(userlist);//从数据库获取数据
	if (!userlist->isEmpty())//不为空
	{
		userBox->clear();
		userBox->addItems(*userlist);//设置下拉框
		username->setText((*userlist)[0]);//设置行编辑器
		labord->setText("当前用户:" + (*userlist)[0]);
	}
	else
	{
		labord->setText("请添加用户！");
		userBox->clear();
		username->clear();
	}
}

void Password::Initapplist()//初始化应用容器
{
	QStringList liststring;
	if ((*userlist).isEmpty())
	{
		labord->setText("请添加用户!");
		itemBox->clear();
		appname->clear();
		appRemark->clear();
		return;
	}
	DBManager::getInstance()->getapplist(applist, username->text());//从数据库获取数据
	if ((*applist).count() <= 0)
	{
		labord->setText("密码本为空!");
		itemBox->clear();
		appname->clear();
		appRemark->clear();
		return;
	}
	else
	{
		//设置下拉选项
		for (int i = 0; i < (*applist).count(); i++)
		{
			liststring.append((*applist)[i].app);
		}
		itemBox->clear();
		itemBox->addItems(liststring);
		//显示应用及密码，复制到剪切板
		appname->setText((*applist)[0].app);
		appRemark->setText((*applist)[0].remark);
		clipboard = QApplication::clipboard();
		clipboard->setText((*applist)[0].pass);
		labord->setText((*applist)[0].app + " 密码为：" + (*applist)[0].pass + " 已复制到剪切板！");
	}
}

void Password::ON_btnPassClick()//添加应用
{
	QString use = username->text();
	QString app = appname->text();
	QString rec = use + app;
	//判空
	if (use == NULL)
	{
		labord->setText("用户为空，请输入用户名!");
		return;
	}
	if (app == NULL)
	{
		labord->setText("请输入应用名称!");
		return;
	}
	if (DBManager::getInstance()->isexitapp(use, app))//已经存在，查询
	{
		appitem *tmp = new appitem();
		DBManager::getInstance()->getappitem(use, app, tmp);
		if (appRemark->text() != tmp->remark)
		{
			appRemark->setText(tmp->remark);
		}
		clipboard = QApplication::clipboard();
		clipboard->setText(tmp->pass);
		labord->setText(app + " 密码为：" + tmp->pass + " 已复制到剪切板！");
		delete tmp;
	}
	else//不存在，生成并存储
	{
		//数据选择下标
		int index = 0;
		for (int i = 0; i < rec.count(); i++)
		{
			index += rec[i].unicode();
		}
		index %= 25;
		//MD5加密
		QString pas;
		pas = (QCryptographicHash::hash(rec.toUtf8(), QCryptographicHash::Md5)).toHex();
		pas = pas.mid(index, 6);
		DBManager::getInstance()->addapp(new appitem(use, app, pas, appRemark->text()));//存储到数据库
		Initapplist();//刷新
	}
}

void Password::ON_btnAddClick()//添加用户
{
	if (username->text() == NULL)//输入为空
	{
		labord->setText("用户为空，请输入用户名!");
		return;
	}
	if (DBManager::getInstance()->isexituser(username->text()))//已经存在用户
	{
		Initapplist();//刷新
		return;
	}
	else//新增用户
	{
		DBManager::getInstance()->adduser(username->text());//存储到数据库
		Inituserlist();//刷新
		Initapplist();
	}
}


void Password::ON_comboxAddChange()
{
	username->setText(userBox->currentText());
	Initapplist();//刷新
}

void Password::ON_comboxPassChange()
{
	appname->setText(itemBox->currentText());
	appitem *tmp = new appitem();
	if (DBManager::getInstance()->getappitem(username->text(), appname->text(), tmp))
	{
		clipboard = QApplication::clipboard();
		clipboard->setText(tmp->pass);
		appRemark->setText(tmp->remark);
		labord->setText(appname->text() + " 密码为：" + tmp->pass + " 已复制到剪切板！");
	}
	else
	{
		labord->setText("应用列表为空！");
	}
	delete tmp;
}

void Password::ON_btnEreaseUser()
{
	int ret = QMessageBox::warning(this, tr("警告"), tr("确定删除此用户吗？"), QMessageBox::Ok, QMessageBox::Abort);
	if (ret == QMessageBox::Ok)
	{
		if (DBManager::getInstance()->ereaseuser(username->text()))
		{
			Inituserlist();
			Initapplist();
		}
	}
}

void Password::ON_btnEreaseApp()
{
	int ret = QMessageBox::warning(this, tr("警告"), tr("确定删除此记录吗？"), QMessageBox::Ok, QMessageBox::Abort);
	if (ret == QMessageBox::Ok)
	{
		if (DBManager::getInstance()->ereaseapp(username->text(),appname->text()))
		{
			Initapplist();
		}
	}
}

void Password::ON_labRemarkChange()
{
	//app是否为空
	if (appname->text() == NULL)
	{
		return;
	}
	else
	{
		appitem *tmp = new appitem();
		if (DBManager::getInstance()->getappitem(username->text(), appname->text(), tmp))
		{
			//修改备注
		}
		else
		{
			return;
		}
		DBManager::getInstance()->modifyappremark(tmp, appRemark->text());
		delete tmp;
	}
}
