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

#include <iostream>
#include <QString>
#include "configuration.h"
#include "mainwindow.h"
#include "login.h"

using namespace std;

class Core
{
public:
    // Global variables
    static MainWindow Main;
    Core();
    static void Log(QString Message);
    static void DebugLog(QString Message, unsigned int Verbosity = 1);
    //! Helper function that will return URL of project in question
    static QString GetProjectURL(QString Project);
};

#endif // CORE_H
