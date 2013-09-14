//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "hugglelog.h"
#include "ui_hugglelog.h"

HuggleLog::HuggleLog(QWidget *parent) :
    QDockWidget(parent),
    ui(new Ui::HuggleLog)
{
    ui->setupUi(this);
}

HuggleLog::~HuggleLog()
{
    delete ui;
}
