//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "hugglefeedproviderirc.h"

HuggleFeedProviderIRC::HuggleFeedProviderIRC()
{
    this->Connected = false;
    TcpSocket = NULL;
    thread = NULL;
}

HuggleFeedProviderIRC::~HuggleFeedProviderIRC()
{
    Stop();
    delete thread;
    delete TcpSocket;
}

void HuggleFeedProviderIRC::Start()
{
    if (Connected)
    {
        Core::DebugLog("Attempted to start connection which was already started");
        return;
    }
    TcpSocket = new QTcpSocket(Core::Main);
    TcpSocket->connectToHost(Configuration::IRCServer, Configuration::IRCPort);
    if (!TcpSocket->waitForConnected())
    {
        Core::Log("IRC: Connection timeout");
        TcpSocket->close();
        delete TcpSocket;
        TcpSocket = NULL;
        return;
    }
    Core::Log("IRC: Successfuly connected to irc rc feed");
    this->TcpSocket->write(QString("USER " + Configuration::IRCNick + " 8 * :" + Configuration::IRCIdent + "\n").toAscii());
    this->TcpSocket->write(QString("NICK " + Configuration::IRCNick + Configuration::UserName + "\n").toAscii());
    this->TcpSocket->write(QString("JOIN " + Configuration::Project.IRCChannel + "\n").toAscii());
    this->thread = new HuggleFeedProviderIRC_t(TcpSocket);
    this->thread->p = this;
    this->thread->start();
    Connected = true;
}

bool HuggleFeedProviderIRC::IsWorking()
{
    return Connected;
}

void HuggleFeedProviderIRC::Stop()
{
    if (!Connected)
    {
        return;
    }
    if (this->TcpSocket == NULL)
    {
        throw new Exception("The pointer to TcpSocket was NULL during Stop() of irc provider");
    }
    this->TcpSocket->close();
    delete this->TcpSocket;
    this->TcpSocket = NULL;
    if (this->thread == NULL)
    {
        throw new Exception("The pointer to thread was NULL during Stop() of irc provider");
    }
    this->thread->exit();
    delete this->thread;
    this->thread = NULL;
}

void HuggleFeedProviderIRC::InsertEdit(WikiEdit edit)
{
    while (this->Buffer.size() > Configuration::ProviderCache)
    {
        this->Buffer.removeAt(0);
    }
    this->Buffer.append(edit);
}

void HuggleFeedProviderIRC::ParseEdit(QString line)
{
    if (!line.startsWith(":PRIVMSG "))
    {
        return;
    }
}

void HuggleFeedProviderIRC_t::run()
{
    if (this->p == NULL)
    {
        throw new Exception("Pointer to parent IRC feed is NULL");
    }
    int ping = 2000;
    while (this->Running && this->s->isOpen())
    {
        if (ping < 0)
        {
            this->s->write(QString("PING :" + Configuration::IRCServer).toAscii());
            ping = 2000;
        }
        QString text(this->s->readLine());
        if (text == "")
        {
            QThread::usleep(2000000);
            ping -= 100;
            continue;
        }
        Core::DebugLog("IRC Input: " + text, 6);
        p->ParseEdit(text);
        QThread::usleep(200000);
        ping--;
    }
    Core::Log("IRC: Closed connection to irc feed");
}

HuggleFeedProviderIRC_t::HuggleFeedProviderIRC_t(QTcpSocket *socket)
{
    this->s = socket;
    Running = true;
    this->p = NULL;
}

HuggleFeedProviderIRC_t::~HuggleFeedProviderIRC_t()
{
    // we must not delete the socket here, that's a job of parent object
}
