//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef HUGGLEFEEDPROVIDERIRC_H
#define HUGGLEFEEDPROVIDERIRC_H

#include <QString>
#include <QThread>
#include <QList>
#include <QTcpSocket>
#include "core.h"
#include "exception.h"
#include "configuration.h"
#include "wikiedit.h"
#include "hugglefeed.h"

class HuggleFeedProviderIRC;

class HuggleFeedProviderIRC_t : public QThread
{
    Q_OBJECT
public:
    HuggleFeedProviderIRC_t(QTcpSocket *socket);
    ~HuggleFeedProviderIRC_t();
    bool Running;
    HuggleFeedProviderIRC *p;
protected:
    void run();
private:
    QTcpSocket *s;
};

class HuggleFeedProviderIRC : public HuggleFeed
{
public:
    HuggleFeedProviderIRC();
    ~HuggleFeedProviderIRC();
    void Start();
    bool IsWorking();
    void Stop();
    void Restart() { this->Stop(); this->Start(); }
    void InsertEdit(WikiEdit edit);
    void ParseEdit(QString line);
private:
    bool Connected;
    QList<WikiEdit> Buffer;
    HuggleFeedProviderIRC_t *thread;
    QTcpSocket *TcpSocket;
};

#endif // HUGGLEFEEDPROVIDERIRC_H
