//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "apiquery.h"

ApiQuery::ApiQuery()
{
    URL = "";
}

void ApiQuery::Process()
{
    if (URL == "")
    {
        throw new Exception("You must provide an arguments to query");
    }
    this->Status = Processing;
    QUrl url = QUrl::fromEncoded(URL.toUtf8());
    QNetworkRequest request(url);
    this->Result = new QueryResult();
    QNetworkReply *reply = this->NetworkManager.get(request);
    this->Result->Data = QString(reply->readAll());
    this->Status = Done;
}
