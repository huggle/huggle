/********************************************************************************
** Form generated from reading UI file 'hugglequeue.ui'
**
** Created: Sat Sep 14 20:29:31 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_HUGGLEQUEUE_H
#define UI_HUGGLEQUEUE_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QDockWidget>
#include <QtGui/QHeaderView>
#include <QtGui/QWidget>

QT_BEGIN_NAMESPACE

class Ui_HuggleQueue
{
public:
    QWidget *dockWidgetContents;

    void setupUi(QDockWidget *HuggleQueue)
    {
        if (HuggleQueue->objectName().isEmpty())
            HuggleQueue->setObjectName(QString::fromUtf8("HuggleQueue"));
        HuggleQueue->resize(156, 332);
        dockWidgetContents = new QWidget();
        dockWidgetContents->setObjectName(QString::fromUtf8("dockWidgetContents"));
        HuggleQueue->setWidget(dockWidgetContents);

        retranslateUi(HuggleQueue);

        QMetaObject::connectSlotsByName(HuggleQueue);
    } // setupUi

    void retranslateUi(QDockWidget *HuggleQueue)
    {
        HuggleQueue->setWindowTitle(QApplication::translate("HuggleQueue", "Queue", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class HuggleQueue: public Ui_HuggleQueue {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_HUGGLEQUEUE_H
