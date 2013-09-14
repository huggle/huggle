/********************************************************************************
** Form generated from reading UI file 'oauthlogin.ui'
**
** Created: Sat Sep 14 20:29:31 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_OAUTHLOGIN_H
#define UI_OAUTHLOGIN_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>

QT_BEGIN_NAMESPACE

class Ui_OAuthLogin
{
public:
    QDialogButtonBox *buttonBox;
    QLabel *label;
    QLabel *label_2;
    QLineEdit *lineEdit;
    QLineEdit *lineEdit_2;
    QLabel *label_3;

    void setupUi(QDialog *OAuthLogin)
    {
        if (OAuthLogin->objectName().isEmpty())
            OAuthLogin->setObjectName(QString::fromUtf8("OAuthLogin"));
        OAuthLogin->resize(400, 170);
        QSizePolicy sizePolicy(QSizePolicy::Fixed, QSizePolicy::Fixed);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(OAuthLogin->sizePolicy().hasHeightForWidth());
        OAuthLogin->setSizePolicy(sizePolicy);
        buttonBox = new QDialogButtonBox(OAuthLogin);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(30, 130, 341, 32));
        buttonBox->setOrientation(Qt::Horizontal);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);
        label = new QLabel(OAuthLogin);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(10, 0, 381, 41));
        label->setWordWrap(true);
        label_2 = new QLabel(OAuthLogin);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(10, 50, 46, 13));
        lineEdit = new QLineEdit(OAuthLogin);
        lineEdit->setObjectName(QString::fromUtf8("lineEdit"));
        lineEdit->setGeometry(QRect(90, 50, 291, 20));
        lineEdit_2 = new QLineEdit(OAuthLogin);
        lineEdit_2->setObjectName(QString::fromUtf8("lineEdit_2"));
        lineEdit_2->setGeometry(QRect(90, 80, 291, 20));
        label_3 = new QLabel(OAuthLogin);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(10, 80, 46, 13));

        retranslateUi(OAuthLogin);
        QObject::connect(buttonBox, SIGNAL(accepted()), OAuthLogin, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), OAuthLogin, SLOT(reject()));

        QMetaObject::connectSlotsByName(OAuthLogin);
    } // setupUi

    void retranslateUi(QDialog *OAuthLogin)
    {
        OAuthLogin->setWindowTitle(QApplication::translate("OAuthLogin", "OAuth", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("OAuthLogin", "OAuth provider demans a verifier and token information. However you have not provided any so far. Please fill in verifier and token in this form:", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("OAuthLogin", "Verifier", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("OAuthLogin", "Token", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class OAuthLogin: public Ui_OAuthLogin {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_OAUTHLOGIN_H
