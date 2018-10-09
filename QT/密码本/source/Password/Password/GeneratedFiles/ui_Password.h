/********************************************************************************
** Form generated from reading UI file 'Password.ui'
**
** Created by: Qt User Interface Compiler version 5.7.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_PASSWORD_H
#define UI_PASSWORD_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_PasswordClass
{
public:

    void setupUi(QWidget *PasswordClass)
    {
        if (PasswordClass->objectName().isEmpty())
            PasswordClass->setObjectName(QStringLiteral("PasswordClass"));
        PasswordClass->resize(600, 400);

        retranslateUi(PasswordClass);

        QMetaObject::connectSlotsByName(PasswordClass);
    } // setupUi

    void retranslateUi(QWidget *PasswordClass)
    {
        PasswordClass->setWindowTitle(QApplication::translate("PasswordClass", "Password", 0));
    } // retranslateUi

};

namespace Ui {
    class PasswordClass: public Ui_PasswordClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_PASSWORD_H
