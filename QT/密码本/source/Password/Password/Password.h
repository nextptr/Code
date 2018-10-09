#ifndef PASSWORD_H
#define PASSWORD_H
#pragma execution_character_set("utf-8")

#include <QtWidgets/QWidget>
#include <QComboBox>
#include <QLineEdit>
#include <QPushButton>
#include <QLabel>
#include <QCryptographicHash>
#include <QSettings>
#include <QClipboard>
#include <QMessageBox>
#include "ui_Password.h"
#include "publiclass.h"

class Password : public QWidget
{
	Q_OBJECT

public:
	Password(QWidget *parent = 0);
	~Password();

private Q_SLOTS:
	void ON_btnAddClick();
	void ON_btnPassClick();
	void ON_comboxAddChange();
	void ON_comboxPassChange();
	void ON_btnEreaseUser();
	void ON_btnEreaseApp();
	void ON_labRemarkChange();

private:
	void InitWidget();
	void InitConnect();
	void Inituserlist();
	void Initapplist();

private:
	Ui::PasswordClass ui;
	QComboBox *userBox;
	QLineEdit *username;
	QPushButton *btn_adduser;
	QStringList *userlist;
	QPushButton *btn_eraseuser;

	QComboBox *itemBox;
	QLineEdit *appname;
	QPushButton *btn_addapp;
	QPushButton *btn_markmodif;
	QList<appitem>* applist;
	QPushButton *btn_eraseapp;
	QLineEdit *appRemark;

	QLabel *labord;
	QString *App;
	QString *Pass;

	QClipboard *clipboard;
};

#endif // PASSWORD_H
