//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include <QApplication>
#include "login.h"
#include "core.h"
#include "exception.h"

int main(int argc, char *argv[])
{
    try
    {
        Core::Init();
        QApplication a(argc, argv);
        Login LoginForm;
        LoginForm.show();

        // basically we should never reach this part of source code
        //delete LoginForm;
        return a.exec();
    } catch (Exception fail)
    {
        Core::Log("FATAL: Unhandled exception occured, description: " + fail.Message);
        return fail.ErrorCode;
    }
}
