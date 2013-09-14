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
    this->Reset();
    ui->checkBox->setChecked(Configuration::UsingSSL);
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
    delete LoginQuery;
    delete ui;
    delete timer;
}

void Login::Progress(int progress)
{
    ui->progressBar->setValue(progress);
}

void Login::Reset()
{
    ui->label_6->setText("Please enter your wikipedia username and pick a project. The authentication will be processed using OAuth.");
}

void Login::CancelLogin()
{
    this->timer->stop();
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
    ui->checkBox->setEnabled(true);
}

void Login::Disable()
{
    ui->lineEdit->setDisabled(true);
    ui->Language->setDisabled(true);
    ui->Project->setDisabled(true);
    ui->checkBox->setDisabled(true);
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
    Configuration::UsingSSL = ui->checkBox->isChecked();
    this->Disable();
    ui->ButtonOK->setText("Cancel");
    // First of all, we need to login to the site
    this->timer->start(200);
    //this->Thread->start();
}

void Login::PerformLogin()
{
    ui->label_6->setText("Logging in");
    this->Progress(10);
    // we create an api request to login
    this->LoginQuery = new ApiQuery();
    this->LoginQuery->SetAction(ActionLogin);
    this->LoginQuery->Parameters = "lgname=" + QUrl::toPercentEncoding(Configuration::UserName) + "&lgpassword=" + QUrl::toPercentEncoding(Configuration::Password);
    this->LoginQuery->UsingPOST = true;
    this->LoginQuery->Process();
    this->_Status = WaitingForLoginQuery;
}

void Login::FinishLogin()
{
    if (!this->LoginQuery->Processed())
    {
        return;
    }
    if (this->LoginQuery->Result->Failed)
    {
        ui->label_6->setText("Login failed: " + this->LoginQuery->Result->ErrorMessage);
        this->Progress(0);
        this->_Status = LoginFailed;
        delete this->LoginQuery;
        this->LoginQuery = NULL;
        return;
    }
    this->Progress(60);
    ui->label_6->setText(this->LoginQuery->Result->Data);
    this->_Status = LoggedIn;
    delete this->LoginQuery;
    this->LoginQuery = NULL;
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
    switch (this->_Status)
    {
    case LoggingIn:
        PerformLogin();
        break;
    case WaitingForLoginQuery:
        FinishLogin();
        break;
    }
    if (this->_Status == LoginFailed)
    {
        this->Enable();
        timer->stop();
        ui->ButtonOK->setText("Login");
        this->_Status = Nothing;
    }
}

