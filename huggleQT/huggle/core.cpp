//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "core.h"

#ifdef PYTHONENGINE
PythonEngine *Core::Python = NULL;
#endif

MainWindow *Core::Main = NULL;
Login *Core::f_Login = NULL;
HuggleFeed *Core::SecondaryFeedProvider = NULL;
HuggleFeed *Core::PrimaryFeedProvider = NULL;
QList<QString> *Core::RingLog = new QList<QString>();

void Core::Init()
{
    Core::Log("Huggle 3 QT-LX, version " + Configuration::HuggleVersion);
    Core::Log("Loading configuration");
    Core::LoadConfig();
#ifdef PYTHONENGINE
    Core::Log("Loading python engine");
    Core::Python = new PythonEngine();
#endif
    Core::DebugLog("Loading wikis");
    Configuration::ProjectList << Configuration::Project;
    // this is a temporary only for oauth testing
    Configuration::ProjectList << WikiSite("mediawiki","www.mediawiki.org/");
}

void Core::Log(QString Message)
{
    std::cout << Message.toStdString() << std::endl;
    Core::InsertToRingLog(Message);
    if (Core::Main != NULL)
    {
        if (Core::Main->SystemLog != NULL)
        {
            Core::Main->SystemLog->InsertText(Message);
        }
    }
}

void Core::DebugLog(QString Message, unsigned int Verbosity)
{
    if (Configuration::Verbosity >= Verbosity)
    {
        Core::Log("DEBUG[" + QString::number(Verbosity) + "]: " + Message);
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

QString Core::GetProjectURL()
{
    return Configuration::GetURLProtocolPrefix() + Configuration::Project.URL;
}

QString Core::GetProjectWikiURL()
{
    return Core::GetProjectURL(Configuration::Project) + Configuration::Project.LongPath;
}

QString Core::GetProjectScriptURL()
{
    return Core::GetProjectURL(Configuration::Project) + Configuration::Project.ScriptPath;
}

void Core::ProcessEdit(WikiEdit *e)
{
    Core::Main->ProcessEdit(e);
}

void Core::Shutdown()
{
#ifdef PYTHONENGINE
    Core::Log("Unloading python");
    delete Core::Python;
#endif
    delete Core::f_Login;
    QApplication::quit();
}

QString Core::RingLogToText()
{
    int i = 0;
    QString text = "";
    while (i<Core::RingLog->size())
    {
        text = Core::RingLog->at(i) + "\n" + text;
        i++;
    }
    return text;
}

void Core::InsertToRingLog(QString text)
{
    if (Core::RingLog->size()+1 > Configuration::RingLogMaxSize)
    {
        Core::RingLog->removeAt(0);
    }
    Core::RingLog->append(text);
}

void Core::DeveloperError()
{
    QMessageBox *mb = new QMessageBox();
    mb->setWindowTitle("Function is restricted now");
    mb->setText("You can't perform this action in developer mode, because you aren't logged into the wiki");
    mb->exec();
    delete mb;
}

void Core::LoadConfig()
{

}

void Core::SaveConfig()
{

}

