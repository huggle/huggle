//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) : QMainWindow(parent), ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    this->showMaximized();
    this->SystemLog = new HuggleLog(this);
    this->Browser = new HuggleWeb(this);
    this->Queue1 = new HuggleQueue(this);
    this->addDockWidget(Qt::LeftDockWidgetArea, this->Queue1);
    this->addDockWidget(Qt::BottomDockWidgetArea, this->SystemLog);
    this->setWindowTitle("Huggle 3 QT-LX");
    ui->verticalLayout->addWidget(this->Browser);
    setLayout(ui->verticalLayout);
}

MainWindow::~MainWindow()
{
    delete this->Queue1;
    delete this->SystemLog;
    delete this->Browser;
    delete ui;
}

void MainWindow::on_actionExit_triggered()
{
    Core::Shutdown();
}
