/********************************************************************************
** Form generated from reading UI file 'aboutform.ui'
**
** Created: Sat Sep 14 18:32:14 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_ABOUTFORM_H
#define UI_ABOUTFORM_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDialog>
#include <QtGui/QDialogButtonBox>
#include <QtGui/QHeaderView>

QT_BEGIN_NAMESPACE

class Ui_AboutForm
{
public:
    QDialogButtonBox *buttonBox;

    void setupUi(QDialog *AboutForm)
    {
        if (AboutForm->objectName().isEmpty())
            AboutForm->setObjectName(QString::fromUtf8("AboutForm"));
        AboutForm->resize(400, 300);
        buttonBox = new QDialogButtonBox(AboutForm);
        buttonBox->setObjectName(QString::fromUtf8("buttonBox"));
        buttonBox->setGeometry(QRect(30, 240, 341, 32));
        buttonBox->setOrientation(Qt::Horizontal);
        buttonBox->setStandardButtons(QDialogButtonBox::Cancel|QDialogButtonBox::Ok);

        retranslateUi(AboutForm);
        QObject::connect(buttonBox, SIGNAL(accepted()), AboutForm, SLOT(accept()));
        QObject::connect(buttonBox, SIGNAL(rejected()), AboutForm, SLOT(reject()));

        QMetaObject::connectSlotsByName(AboutForm);
    } // setupUi

    void retranslateUi(QDialog *AboutForm)
    {
        AboutForm->setWindowTitle(QApplication::translate("AboutForm", "Dialog", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class AboutForm: public Ui_AboutForm {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_ABOUTFORM_H
