#-------------------------------------------------
#
# Project created by QtCreator 2013-09-11T13:41:34
#
#-------------------------------------------------

QT       += core gui network

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
    exception.cpp

HEADERS  += mainwindow.h \
    login.h \
    core.h \
    configuration.h \
    preferences.h \
    oauth.h \
    query.h \
    apiquery.h \
    queryresult.h \
    exception.h

FORMS    += mainwindow.ui \
    login.ui \
    preferences.ui

RESOURCES += \
    pictures.qrc
