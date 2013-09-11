#ifndef QUERYRESULT_H
#define QUERYRESULT_H

#include <QString>

class QueryResult
{
public:
    QueryResult();
    QString Data;
    QString ErrorMessage;
    bool Failed;
};

#endif // QUERYRESULT_H
