/********************************************************************************
** Form generated from reading UI file 'huggletool.ui'
**
** Created: Sat Sep 14 20:29:31 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_HUGGLETOOL_H
#define UI_HUGGLETOOL_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QComboBox>
#include <QtGui/QDockWidget>
#include <QtGui/QHBoxLayout>
#include <QtGui/QHeaderView>
#include <QtGui/QLabel>
#include <QtGui/QLineEdit>
#include <QtGui/QWidget>

QT_BEGIN_NAMESPACE

class Ui_HuggleTool
{
public:
    QWidget *dockWidgetContents;
    QHBoxLayout *horizontalLayout;
    QLabel *label;
    QComboBox *comboBox;
    QLabel *label_2;
    QComboBox *comboBox_2;
    QLabel *label_3;
    QLineEdit *lineEdit;

    void setupUi(QDockWidget *HuggleTool)
    {
        if (HuggleTool->objectName().isEmpty())
            HuggleTool->setObjectName(QString::fromUtf8("HuggleTool"));
        HuggleTool->resize(715, 66);
        HuggleTool->setFeatures(QDockWidget::DockWidgetFloatable|QDockWidget::DockWidgetMovable);
        dockWidgetContents = new QWidget();
        dockWidgetContents->setObjectName(QString::fromUtf8("dockWidgetContents"));
        horizontalLayout = new QHBoxLayout(dockWidgetContents);
        horizontalLayout->setObjectName(QString::fromUtf8("horizontalLayout"));
        label = new QLabel(dockWidgetContents);
        label->setObjectName(QString::fromUtf8("label"));

        horizontalLayout->addWidget(label);

        comboBox = new QComboBox(dockWidgetContents);
        comboBox->setObjectName(QString::fromUtf8("comboBox"));
        comboBox->setEditable(true);

        horizontalLayout->addWidget(comboBox);

        label_2 = new QLabel(dockWidgetContents);
        label_2->setObjectName(QString::fromUtf8("label_2"));

        horizontalLayout->addWidget(label_2);

        comboBox_2 = new QComboBox(dockWidgetContents);
        comboBox_2->setObjectName(QString::fromUtf8("comboBox_2"));
        comboBox_2->setEditable(true);

        horizontalLayout->addWidget(comboBox_2);

        label_3 = new QLabel(dockWidgetContents);
        label_3->setObjectName(QString::fromUtf8("label_3"));

        horizontalLayout->addWidget(label_3);

        lineEdit = new QLineEdit(dockWidgetContents);
        lineEdit->setObjectName(QString::fromUtf8("lineEdit"));

        horizontalLayout->addWidget(lineEdit);

        HuggleTool->setWidget(dockWidgetContents);

        retranslateUi(HuggleTool);

        QMetaObject::connectSlotsByName(HuggleTool);
    } // setupUi

    void retranslateUi(QDockWidget *HuggleTool)
    {
        HuggleTool->setWindowTitle(QApplication::translate("HuggleTool", "Tools", 0, QApplication::UnicodeUTF8));
        label->setText(QApplication::translate("HuggleTool", "User", 0, QApplication::UnicodeUTF8));
        label_2->setText(QApplication::translate("HuggleTool", "Page", 0, QApplication::UnicodeUTF8));
        label_3->setText(QApplication::translate("HuggleTool", "Currently displayed", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class HuggleTool: public Ui_HuggleTool {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_HUGGLETOOL_H
