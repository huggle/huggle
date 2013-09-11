//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "core.h"

Core::Core()
{
}

void Core::Log(QString Message)
{
    cout << Message.toStdString() << endl;
}

void Core::DebugLog(QString Message, unsigned int Verbosity)
{
    if (Configuration::Verbosity >= Verbosity)
    {
        Core::DebugLog("DEBUG: " + Message);
    }
}

QString Core::GetProjectURL(QString Project)
{
    if (Project == "enwiki")
    {
        return Configuration::GetURLProtocolPrefix() + "en.wikipedia.org/";
    }
    return NULL;
}
