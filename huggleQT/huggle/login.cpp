//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "login.h"
#include "ui_login.h"

Login::Login(QWidget *parent) :   QDialog(parent),   ui(new Ui::Login)
{
    ui->setupUi(this);
    this->_Status = Nothing;
    this->timer = new QTimer(this);
    connect(this->timer, SIGNAL(timeout()), this, SLOT(on_Time()));
    this->setWindowTitle("Huggle 3 QT");
    this->Thread = new LoginThread();
    this->Reset();
    this->StatusText = "hello";
    this->progress = 0;
    // set the language to dummy english
    ui->Language->addItem("English");
    ui->Language->setCurrentIndex(0);
    int current = 0;
    while (current < Configuration::ProjectList.size())
    {
        ui->Project->addItem(Configuration::ProjectList.at(current).Name);
        current++;
    }
    ui->Project->setCurrentIndex(0);
}

Login::~Login()
{
    delete Thread;
    delete ui;
    delete timer;
}

void Login::Progress(int progress)
{
    this->progress = progress;
}

void Login::Reset()
{
    ui->label_6->setText("Please enter your wikipedia username and pick a project. The authentication will be processed using OAuth.");
}

void Login::CancelLogin()
{
    this->timer->stop();
    this->Thread->terminate();
    ui->progressBar->setValue(0);
    this->Enable();
    this->_Status = Nothing;
    this->Reset();
    ui->ButtonOK->setText("Login");
}

void Login::Enable()
{
    ui->lineEdit->setEnabled(true);
    ui->Language->setEnabled(true);
    ui->Project->setEnabled(true);
}

void Login::Disable()
{
    ui->lineEdit->setDisabled(true);
    ui->Language->setDisabled(true);
    ui->Project->setDisabled(true);
}

void Login::PressOK()
{
    if (ui->tab->isVisible())
    {
        QMessageBox mb;
        mb.setText("Function not supported");
        mb.setInformativeText("This function is not available for wmf wikis in this moment");
        mb.exec();
        //mb.setStyle(QStyle::SP_MessageBoxCritical);
        return;
    }
    Configuration::Project = Configuration::ProjectList.at(ui->Project->currentIndex());
    Configuration::UserName = ui->lineEdit_2->text();
    Configuration::Password = ui->lineEdit_3->text();
    this->_Status = LoggingIn;
    this->Disable();
    ui->ButtonOK->setText("Cancel");
    // First of all, we need to login to the site
    this->timer->start(200);
    this->Thread->start();
}

void Login::on_ButtonOK_clicked()
{
    if (this->_Status == Nothing)
    {
        this->PressOK();
        return;
    }

    if (this->_Status == LoggingIn)
    {
        this->CancelLogin();
        return;
    }
}

void Login::on_ButtonExit_clicked()
{
    Core::Shutdown();
}

void Login::on_Login_destroyed()
{
    QApplication::quit();
}

void Login::on_Time()
{
    ui->label_6->setText(this->StatusText);
    ui->progressBar->setValue(this->progress);
    if (this->_Status == LoginFailed)
    {
        this->Enable();
        timer->stop();
        ui->ButtonOK->setText("Login");
        this->_Status = Nothing;
    }
}

void LoginThread::run()
{
    Core::f_Login->StatusText = "Logging in";
    Core::f_Login->Progress(10);
    // we create an api request to login
    ApiQuery *query = new ApiQuery();
    query->SetAction(ActionLogin);
    query->Parameters = "lgname=" + QUrl::toPercentEncoding(Configuration::UserName) + "&lgpassword=" + QUrl::toPercentEncoding(Configuration::Password);
    query->Process();
    if (query->Result->Failed)
    {
        Core::f_Login->StatusText = "Login failed: " + query->Result->ErrorMessage;
        Core::f_Login->Progress(0);
        Core::f_Login->_Status = LoginFailed;
    }
    delete query;
}
