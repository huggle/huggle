//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef CORE_H
#define CORE_H

#include "configuration.h"
#include "mainwindow.h"
#include "login.h"
#include "wikisite.h"
#include "user.h"
#include <iostream>
#include <QApplication>
#include <QNetworkAccessManager>
#include <QList>
#include <QString>

class Login;

class Core
{
public:
    // Global variables
    static void Init();
    static MainWindow *Main;
    static Login *f_Login;
    static void Log(QString Message);
    static void DebugLog(QString Message, unsigned int Verbosity = 1);
    //! Helper function that will return URL of project in question
    static QString GetProjectURL(WikiSite Project);
    //! Return a full url like http://en.wikipedia.org/wiki/
    static QString GetProjectWikiURL(WikiSite Project);
    //! Return a script url like http://en.wikipedia.org/w/
    static QString GetProjectScriptURL(WikiSite Project);
    static void Shutdown();
};

#endif // CORE_H
