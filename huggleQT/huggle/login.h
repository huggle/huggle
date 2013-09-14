//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#ifndef LOGIN_H
#define LOGIN_H

#include <QDialog>
#include <QMessageBox>
#include <QThread>
#include <QUrl>
#include <QTimer>
#include "oauthloginquery.h"
#include "core.h"
#include "apiquery.h"
#include "configuration.h"

namespace Ui {
class Login;
}

enum Status
{
    LoggingIn,
    WaitingForLoginQuery,
    LoggedIn,
    Nothing,
    Cancelling,
    LoginFailed,
    LoginDone
};

class ApiQuery;

class Login : public QDialog
{
    Q_OBJECT

public:
    explicit Login(QWidget *parent = 0);
    ~Login();
    Status _Status;
    void Progress(int progress);

private slots:
    void on_ButtonOK_clicked();
    void on_ButtonExit_clicked();
    void on_Login_destroyed();
    void on_Time();

private:
    //LoginThread *Thread;
    Ui::Login *ui;
    QTimer *timer;
    ApiQuery *LoginQuery;
    void Reset();
    void Enable();
    void CancelLogin();
    void Disable();
    void PressOK();
    void PerformLogin();
    void FinishLogin();
};

#endif // LOGIN_H
