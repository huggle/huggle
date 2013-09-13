//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef QUERY_H
#define QUERY_H

#include <QObject>
#include <QString>
#include "queryresult.h"

enum _Status
{
    Null,
    Done,
    Processing,
    InError
};

//! Every request to website is processed as a query, this is a base object that all
//! other queries are derived from
class Query : public QObject
{
public:
    Query();
    virtual ~Query();
    //! If true the request will be processed in a separate thread. In that case
    //! the process function will not immediately result in query being
    //! completed. You will need to wait for status to change to Done
    bool ProcessInSeparateThread;
    //! Result
    QueryResult *Result;
    //! Current status
    enum _Status Status;
    virtual void Process() {}
};

#endif // QUERY_H
