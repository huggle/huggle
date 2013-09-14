#-------------------------------------------------
#
# Project created by QtCreator 2013-09-11T13:41:34
#
#-------------------------------------------------

QT       += webkit core gui network

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = huggle
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    login.cpp \
    core.cpp \
    configuration.cpp \
    preferences.cpp \
    oauth.cpp \
    query.cpp \
    apiquery.cpp \
    queryresult.cpp \
    exception.cpp \
    wikisite.cpp \
    user.cpp \
    oauthlogin.cpp \
    oauthloginquery.cpp \
    aboutform.cpp \
    hugglequeue.cpp \
    hugglelog.cpp \
    huggleweb.cpp

HEADERS  += mainwindow.h \
    login.h \
    core.h \
    configuration.h \
    preferences.h \
    oauth.h \
    query.h \
    apiquery.h \
    queryresult.h \
    exception.h \
    wikisite.h \
    user.h \
    oauthlogin.h \
    oauthloginquery.h \
    aboutform.h \
    hugglequeue.h \
    hugglelog.h \
    huggleweb.h

FORMS    += mainwindow.ui \
    login.ui \
    preferences.ui \
    oauthlogin.ui \
    aboutform.ui \
    hugglequeue.ui \
    hugglelog.ui \
    huggleweb.ui

RESOURCES += \
    pictures.qrc
