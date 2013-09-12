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
    this->setWindowTitle("Huggle 3 QT");
    this->Reset();
    // set the language to dummy english
    ui->Language->addItem("English");
    ui->Language->setCurrentText("English");
}

Login::~Login()
{
    delete ui;
}

void Login::Reset()
{
    ui->label_6->setText("Please enter your wikipedia username and pick a project. The authentication will be processed using OAuth.");
}

void Login::on_ButtonOK_clicked()
{

}

void Login::on_ButtonExit_clicked()
{
    QApplication::quit();
}

void Login::on_Login_destroyed()
{
    QApplication::quit();
}
