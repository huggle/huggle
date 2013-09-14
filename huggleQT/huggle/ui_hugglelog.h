/********************************************************************************
** Form generated from reading UI file 'hugglelog.ui'
**
** Created: Sat Sep 14 20:29:31 2013
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
    QVBoxLayout *verticalLayout;
    QTextEdit *textEdit;

    void setupUi(QDockWidget *HuggleLog)
    {
        if (HuggleLog->objectName().isEmpty())
            HuggleLog->setObjectName(QString::fromUtf8("HuggleLog"));
        HuggleLog->resize(637, 116);
        HuggleLog->setFeatures(QDockWidget::DockWidgetFloatable|QDockWidget::DockWidgetMovable);
        dockWidgetContents = new QWidget();
        dockWidgetContents->setObjectName(QString::fromUtf8("dockWidgetContents"));
        verticalLayout = new QVBoxLayout(dockWidgetContents);
        verticalLayout->setSpacing(0);
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        verticalLayout->setObjectName(QString::fromUtf8("verticalLayout"));
        textEdit = new QTextEdit(dockWidgetContents);
        textEdit->setObjectName(QString::fromUtf8("textEdit"));
        textEdit->setReadOnly(true);

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
