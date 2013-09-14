/********************************************************************************
** Form generated from reading UI file 'login.ui'
**
** Created: Sat Sep 14 20:29:31 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_LOGIN_H
#define UI_LOGIN_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QCheckBox>
#include <QtGui/QComboBox>
#include <QtGui/QDialog>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QProgressBar>
#include <QtGui/QPushButton>
#include <QtGui/QTabWidget>
#include <QtGui/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Login
{
public:
    QLabel *label;
    QLabel *label_5;
    QProgressBar *progressBar;
    QLabel *label_6;
    QPushButton *ButtonOK;
    QPushButton *ButtonExit;
    QComboBox *Project;
    QComboBox *Language;
    QTabWidget *tabWidget;
    QWidget *tab;
    QLabel *label_2;
    QLineEdit *lineEdit;
    QLabel *label_8;
    QWidget *tab_2;
    QLineEdit *lineEdit_2;
    QLineEdit *lineEdit_3;
    QLabel *label_3;
    QLabel *label_7;
    QLabel *label_4;
    QCheckBox *checkBox;

    void setupUi(QDialog *Login)
    {
        if (Login->objectName().isEmpty())
            Login->setObjectName(QString::fromUtf8("Login"));
        Login->resize(362, 475);
        QSizePolicy sizePolicy(QSizePolicy::Fixed, QSizePolicy::Fixed);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(Login->sizePolicy().hasHeightForWidth());
        Login->setSizePolicy(sizePolicy);
        QIcon icon;
        icon.addFile(QString::fromUtf8(":/huggle/pictures/Resources/huggle3_newlogo.ico"), QSize(), QIcon::Normal, QIcon::Off);
        Login->setWindowIcon(icon);
        label = new QLabel(Login);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(120, 0, 121, 131));
        label->setPixmap(QPixmap(QString::fromUtf8(":/huggle/pictures/Resources/huggle3_newlogo.png")));
        label_5 = new QLabel(Login);
        label_5->setObjectName(QString::fromUtf8("label_5"));
        label_5->setGeometry(QRect(10, 320, 91, 16));
        progressBar = new QProgressBar(Login);
        progressBar->setObjectName(QString::fromUtf8("progressBar"));
        progressBar->setGeometry(QRect(20, 350, 341, 23));
        progressBar->setValue(0);
        label_6 = new QLabel(Login);
        label_6->setObjectName(QString::fromUtf8("label_6"));
        label_6->setGeometry(QRect(10, 380, 351, 41));
        label_6->setWordWrap(true);
        ButtonOK = new QPushButton(Login);
        ButtonOK->setObjectName(QString::fromUtf8("ButtonOK"));
        ButtonOK->setGeometry(QRect(140, 430, 91, 31));
        ButtonExit = new QPushButton(Login);
        ButtonExit->setObjectName(QString::fromUtf8("ButtonExit"));
        ButtonExit->setGeometry(QRect(250, 430, 101, 31));
        Project = new QComboBox(Login);
        Project->setObjectName(QString::fromUtf8("Project"));
        Project->setGeometry(QRect(110, 290, 241, 22));
        Language = new QComboBox(Login);
        Language->setObjectName(QString::fromUtf8("Language"));
        Language->setGeometry(QRect(110, 320, 241, 22));
        tabWidget = new QTabWidget(Login);
        tabWidget->setObjectName(QString::fromUtf8("tabWidget"));
        tabWidget->setGeometry(QRect(10, 130, 341, 121));
        tab = new QWidget();
        tab->setObjectName(QString::fromUtf8("tab"));
        label_2 = new QLabel(tab);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(10, 20, 91, 16));
        lineEdit = new QLineEdit(tab);
        lineEdit->setObjectName(QString::fromUtf8("lineEdit"));
        lineEdit->setGeometry(QRect(120, 20, 201, 20));
        label_8 = new QLabel(tab);
        label_8->setObjectName(QString::fromUtf8("label_8"));
        label_8->setGeometry(QRect(10, 70, 311, 16));
        tabWidget->addTab(tab, QString());
        tab_2 = new QWidget();
        tab_2->setObjectName(QString::fromUtf8("tab_2"));
        lineEdit_2 = new QLineEdit(tab_2);
        lineEdit_2->setObjectName(QString::fromUtf8("lineEdit_2"));
        lineEdit_2->setGeometry(QRect(110, 10, 221, 20));
        lineEdit_3 = new QLineEdit(tab_2);
        lineEdit_3->setObjectName(QString::fromUtf8("lineEdit_3"));
        lineEdit_3->setGeometry(QRect(110, 40, 221, 20));
        lineEdit_3->setEchoMode(QLineEdit::Password);
        label_3 = new QLabel(tab_2);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(10, 10, 91, 16));
        label_7 = new QLabel(tab_2);
        label_7->setObjectName(QString::fromUtf8("label_7"));
        label_7->setGeometry(QRect(10, 40, 91, 16));
        tabWidget->addTab(tab_2, QString());
        label_4 = new QLabel(Login);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(10, 290, 91, 16));
        checkBox = new QCheckBox(Login);
        checkBox->setObjectName(QString::fromUtf8("checkBox"));
        checkBox->setGeometry(QRect(20, 260, 321, 17));

        retranslateUi(Login);

        tabWidget->setCurrentIndex(0);


        QMetaObject::connectSlotsByName(Login);
    } // setupUi

    void retranslateUi(QDialog *Login)
    {
        Login->setWindowTitle(QApplication::translate("Login", "Huggle", 0, QApplication::UnicodeUTF8));
        label->setText(QString());
        label_5->setText(QApplication::translate("Login", "Language:", 0, QApplication::UnicodeUTF8));
        label_6->setText(QString());
        ButtonOK->setText(QApplication::translate("Login", "Login", 0, QApplication::UnicodeUTF8));
        ButtonExit->setText(QApplication::translate("Login", "Exit", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("Login", "Username:", 0, QApplication::UnicodeUTF8));
        label_8->setText(QApplication::translate("Login", "This method is not supported in this moment", 0, QApplication::UnicodeUTF8));
        tabWidget->setTabText(tabWidget->indexOf(tab), QApplication::translate("Login", "OAuth", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("Login", "Username", 0, QApplication::UnicodeUTF8));
        label_7->setText(QApplication::translate("Login", "Password", 0, QApplication::UnicodeUTF8));
        tabWidget->setTabText(tabWidget->indexOf(tab_2), QApplication::translate("Login", "Login", 0, QApplication::UnicodeUTF8));
        label_4->setText(QApplication::translate("Login", "Project:", 0, QApplication::UnicodeUTF8));
        checkBox->setText(QApplication::translate("Login", "Enforce SSL", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class Login: public Ui_Login {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_LOGIN_H
