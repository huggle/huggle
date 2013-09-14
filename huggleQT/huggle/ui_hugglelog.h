/********************************************************************************
** Form generated from reading UI file 'hugglelog.ui'
**
** Created: Sat Sep 14 18:35:03 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_HUGGLELOG_H
#define UI_HUGGLELOG_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDockWidget>
#include <QtGui/QHeaderView>
#include <QtGui/QTextEdit>
#include <QtGui/QVBoxLayout>
#include <QtGui/QWidget>

QT_BEGIN_NAMESPACE

class Ui_HuggleLog
{
public:
    QWidget *dockWidgetContents;
    QWidget *verticalLayoutWidget;
    QVBoxLayout *verticalLayout;
    QTextEdit *textEdit;

    void setupUi(QDockWidget *HuggleLog)
    {
        if (HuggleLog->objectName().isEmpty())
            HuggleLog->setObjectName(QString::fromUtf8("HuggleLog"));
        HuggleLog->resize(637, 163);
        dockWidgetContents = new QWidget();
        dockWidgetContents->setObjectName(QString::fromUtf8("dockWidgetContents"));
        verticalLayoutWidget = new QWidget(dockWidgetContents);
        verticalLayoutWidget->setObjectName(QString::fromUtf8("verticalLayoutWidget"));
        verticalLayoutWidget->setGeometry(QRect(30, 10, 531, 101));
        verticalLayout = new QVBoxLayout(verticalLayoutWidget);
        verticalLayout->setObjectName(QString::fromUtf8("verticalLayout"));
        verticalLayout->setSizeConstraint(QLayout::SetMaximumSize);
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        textEdit = new QTextEdit(verticalLayoutWidget);
        textEdit->setObjectName(QString::fromUtf8("textEdit"));

        verticalLayout->addWidget(textEdit);

        HuggleLog->setWidget(dockWidgetContents);

        retranslateUi(HuggleLog);

        QMetaObject::connectSlotsByName(HuggleLog);
    } // setupUi

    void retranslateUi(QDockWidget *HuggleLog)
    {
        HuggleLog->setWindowTitle(QApplication::translate("HuggleLog", "System log", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class HuggleLog: public Ui_HuggleLog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_HUGGLELOG_H
