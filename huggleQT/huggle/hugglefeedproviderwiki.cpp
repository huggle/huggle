//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "hugglefeedproviderwiki.h"

HuggleFeedProviderWiki::HuggleFeedProviderWiki()
{
    Buffer = new QList<WikiEdit>();
    Refreshing = false;
    q = NULL;
    // we set the latest time to yesterday so that we don't get in troubles with time offset
    LatestTime = QDateTime::currentDateTime();
    LatestTime = LatestTime.addDays(-1);
}

HuggleFeedProviderWiki::~HuggleFeedProviderWiki()
{
    delete q;
    delete Buffer;
}

bool HuggleFeedProviderWiki::Start()
{
    Refresh();
    return true;
}

bool HuggleFeedProviderWiki::IsWorking()
{
    return true;
}

void HuggleFeedProviderWiki::Stop()
{

}

bool HuggleFeedProviderWiki::ContainsEdit()
{
    if (this->Buffer->size() == 0)
    {
        Refresh();
        return false;
    }
    return true;
}

void HuggleFeedProviderWiki::Refresh()
{
    if (Refreshing)
    {
        // the query is still in progress now
        if (!q->Processed())
        {
            return;
        }
        if (q->Result->Failed)
        {
            // failed to obtain the data
            Core::Log("Unable to retrieve data from wiki feed, last error: " + q->Result->ErrorMessage);
            delete this->q;
            this->q = NULL;
            Refreshing = false;
            return;
        }
        this->Process(q->Result->Data);
        delete this->q;
        this->q = NULL;
        Refreshing = false;
        return;
    }

    Refreshing = true;
    q = new ApiQuery();
    q->SetAction(ActionQuery);
    q->Parameters = "list=recentchanges&rcprop=user%7Cuserid%7Ccomment%7Cflags%7Ctimestamp%7Ctitle%7Cids%7Csizes&rcshow=!bot&rclimit=10";
    q->Process();
}

WikiEdit *HuggleFeedProviderWiki::RetrieveEdit()
{
    if (this->Buffer->size() == 0)
    {
        return NULL;
    }
    WikiEdit *edit = new WikiEdit(this->Buffer->at(0));
    this->Buffer->removeAt(0);
    return edit;
}

void HuggleFeedProviderWiki::Process(QString data)
{
    //QStringList lines = data.split("\n");
    QDomDocument d;
    d.setContent(data);
    QDomNodeList l = d.elementsByTagName("rc");
    int CurrentNode = 0;
    // recursively scan all RC changes
    while (CurrentNode < l.size())
    {
        // get a time of rc change
        QDomElement item = l.at(CurrentNode).toElement();

        if (!item.attributes().contains("timestamp"))
        {
            Core::DebugLog("RC Feed: Item was missing timestamp attribute: " + item.text());
            CurrentNode++;
            continue;
        }

        QDateTime time = QDateTime::fromString(item.attribute("timestamp"), "yyyy-MM-ddThh:mi:ssZ");

        if (time < this->LatestTime)
        {
            // this record is older than latest parsed record, so we don't want to parse it
            CurrentNode++;
            continue;
        }

        if (!item.attributes().contains("type"))
        {
            Core::DebugLog("RC Feed: Item was missing type attribute: " + item.text());
            CurrentNode++;
            continue;
        }

        QString type = item.attribute("type");

        if (type != "edit" && type != "new")
        {
            CurrentNode++;
            continue;
        }

        WikiEdit edit;
        edit.Page = new WikiPage(item.attribute("title"));

        if (type == "new")
        {
            edit.NewPage = true;
        }

        if (item.attributes().contains("newlen") && item.attributes().contains("oldlen"))
        {
            edit.Size = item.attribute("newlen").toInt() - item.attribute("oldlen").toInt();
        }

        if (item.attributes().contains("user"))
        {
            edit.User = new WikiUser(item.attribute("user"));
        }

        if (item.attributes().contains("comment"))
        {
            edit.Summary = item.attribute("comment");
        }

        this->InsertEdit(edit);
        CurrentNode++;
    }
}

void HuggleFeedProviderWiki::InsertEdit(WikiEdit edit)
{
    while (this->Buffer->size() > Configuration::ProviderCache)
    {
        this->Buffer->removeAt(0);
    }
    this->Buffer->append(edit);
}
