//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "configuration.h"

unsigned int Configuration::Verbosity = 1;
WikiSite Configuration::Project("enwiki", "en.wikipedia.org/");
bool Configuration::UsingSSL = true;
QString Configuration::UserName = "User";
QString Configuration::Password = "";
QString Configuration::WelcomeMP = "Project:Huggle/Message";
QList<WikiSite> Configuration::ProjectList;
//! This is a consumer key for "huggle" on wmf wikis
QString Configuration::WmfOAuthConsumerKey = "56a6d6de895e3b859faa57b677f6cd21";
int Configuration::Cache_InfoSize = 200;
#ifdef PYTHONENGINE
bool Configuration::PythonEngine = true;
#else
bool Configuration::PythonEngine = false;
#endif

QString Configuration::GetURLProtocolPrefix()
{
    if (!Configuration::UsingSSL)
    {
        return "http://";
    }
    return "https://";
}

Configuration::Configuration()
{
}
