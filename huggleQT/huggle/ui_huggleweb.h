/********************************************************************************
** Form generated from reading UI file 'huggleweb.ui'
**
** Created: Sat Sep 14 20:29:31 2013
**      by: Qt User Interface Compiler version 4.8.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_HUGGLEWEB_H
#define UI_HUGGLEWEB_H

#include <QtCore/QVariant>
#include <QtGui/QAction>
#include <QtGui/QApplication>
#include <QtGui/QButtonGroup>
#include <QtGui/QFrame>
#include <QtGui/QHeaderView>
#include <QtGui/QVBoxLayout>
#include <QtWebKit/QWebView>

QT_BEGIN_NAMESPACE

class Ui_HuggleWeb
{
public:
    QVBoxLayout *verticalLayout;
    QWebView *webView;

    void setupUi(QFrame *HuggleWeb)
    {
        if (HuggleWeb->objectName().isEmpty())
            HuggleWeb->setObjectName(QString::fromUtf8("HuggleWeb"));
        HuggleWeb->resize(400, 300);
        HuggleWeb->setFrameShape(QFrame::StyledPanel);
        HuggleWeb->setFrameShadow(QFrame::Raised);
        verticalLayout = new QVBoxLayout(HuggleWeb);
        verticalLayout->setSpacing(0);
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        verticalLayout->setObjectName(QString::fromUtf8("verticalLayout"));
        webView = new QWebView(HuggleWeb);
        webView->setObjectName(QString::fromUtf8("webView"));
        webView->setUrl(QUrl(QString::fromUtf8("about:blank")));

        verticalLayout->addWidget(webView);


        retranslateUi(HuggleWeb);

        QMetaObject::connectSlotsByName(HuggleWeb);
    } // setupUi

    void retranslateUi(QFrame *HuggleWeb)
    {
        HuggleWeb->setWindowTitle(QApplication::translate("HuggleWeb", "Frame", 0, QApplication::UnicodeUTF8));
    } // retranslateUi

};

namespace Ui {
    class HuggleWeb: public Ui_HuggleWeb {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_HUGGLEWEB_H
