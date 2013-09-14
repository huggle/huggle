//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "apiquery.h"

void ApiQuery::ConstructUrl()
{
    if (this->ActionPart == "")
    {
        throw new Exception("No action provided for api request");
    }
    URL = Core::GetProjectScriptURL(Configuration::Project) + "api.php?action=" + this->ActionPart;
    if (this->Parameters != "")
    {
        URL = URL + "&" + this->Parameters;
    }

    if (!this->FormatIsCurrentlySupported())
    {
        Core::Log("WARNING: you requested to process api request using JSON format, which isn't supported yet");
    }

    switch (this->RequestFormat)
    {
    case XML:
        URL += "&format=xml";
        break;
    }
}

bool ApiQuery::FormatIsCurrentlySupported()
{
    // other formats will be supported later
    return (this->RequestFormat == XML);
}

ApiQuery::ApiQuery()
{
    RequestFormat = XML;
    URL = "";
    this->ActionPart = "";
    this->Parameters = "";
    this->UsingPOST = false;
}

void ApiQuery::Finished()
{
    this->Result->Data += QString(this->reply->readAll());
    if (this->reply->error())
    {
        this->Result->ErrorMessage = reply->errorString();
        this->Result->Failed = true;
        this->reply->deleteLater();
        this->reply = NULL;
        return;
    }
    // now we need to check if request was successful or not
    if (!this->FormatIsCurrentlySupported())
    {
        this->Result->Failed = true;
        this->Result->ErrorMessage = "The requested format isn't supported";
    } else
    {
        if (this->RequestFormat == XML)
        {
            // we support XML


        }
    }
    this->reply->deleteLater();
    this->reply = NULL;
    Core::DebugLog("Finished request " + URL);
    this->Status = Done;
}

void ApiQuery::Process()
{
    if (this->URL == "")
    {
        this->ConstructUrl();
    }
    this->Status = Processing;
    this->Result = new QueryResult();
    QNetworkRequest request(QUrl(this->URL));
    this->reply = this->NetworkManager.get(request);
    QObject::connect(this->reply, SIGNAL(finished()), this, SLOT(Finished()));
    QObject::connect(this->reply, SIGNAL(readyRead()), this, SLOT(ReadData()));
    Core::DebugLog("Processing api request " + this->URL);
}

void ApiQuery::ReadData()
{
    this->Result->Data += QString(this->reply->readAll());
}

void ApiQuery::SetAction(Action action)
{
    switch (action)
    {
    case ActionQuery:
        this->ActionPart = "query";
        return;
    case ActionLogin:
        this->ActionPart = "login";
        return;
    case ActionLogout:
        this->ActionPart = "logout";
        return;
    case ActionTokens:
        this->ActionPart = "tokens";
        return;
    case ActionPurge:
        this->ActionPart = "purge";
        return;
    case ActionRollback:
        this->ActionPart = "rollback";
        return;
    case ActionDelete:
        this->ActionPart = "delete";
        return;
    case ActionUndelete:
        this->ActionPart = "undelete";
        return;
    }
}

void ApiQuery::SetAction(QString action)
{
    this->ActionPart = action;
}

void ApiQuery::Kill()
{
    reply->abort();
}
