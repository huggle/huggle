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
    this->setWindowTitle("Huggle 3 QT");
    this->Reset();
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
    delete ui;
}

void Login::Reset()
{
    ui->label_6->setText("Please enter your wikipedia username and pick a project. The authentication will be processed using OAuth.");
}

void Login::CancelLogin()
{
    ui->progressBar->setValue(0);
    this->Enable();
    this->_Status = Nothing;
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
    Configuration::Project = Configuration::ProjectList.at(ui->Project->currentIndex());
    Configuration::UserName = ui->lineEdit->text();
    this->_Status = LoggingIn;
    this->Disable();
    ui->ButtonOK->setText("Cancel");
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
    QApplication::quit();
}

void Login::on_Login_destroyed()
{
    QApplication::quit();
}
