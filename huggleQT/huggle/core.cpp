//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "core.h"

void Core::Init()
{
    Configuration::Project = new WikiSite("enwiki", "en.wikipedia.org/");
    Configuration::ProjectList << *Configuration::Project;
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

QString Core::GetProjectURL(WikiSite Project)
{
    return Configuration::GetURLProtocolPrefix() + Project.URL;
}

QString Core::GetProjectWikiURL(WikiSite Project)
{
    return Core::GetProjectURL(Project) + Project.LongPath;
}

QString Core::GetProjectScriptURL(WikiSite Project)
{
    return Core::GetProjectURL(Project) + Project.ScriptPath;
}

