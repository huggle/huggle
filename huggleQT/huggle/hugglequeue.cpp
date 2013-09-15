//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "hugglequeue.h"
#include "ui_hugglequeue.h"

HuggleQueue::HuggleQueue(QWidget *parent) :
    QDockWidget(parent),
    ui(new Ui::HuggleQueue)
{
    ui->setupUi(this);
    CurrentFilter = new HuggleQueueFilter(this);
    this->layout = new QVBoxLayout(ui->scrollArea);
    this->layout->setMargin(0);
    this->layout->setSpacing(0);
    this->xx = new QWidget();
    ui->scrollArea->setWidget(this->xx);
    xx->setLayout(this->layout);
}

HuggleQueue::~HuggleQueue()
{
    delete layout;
    delete CurrentFilter;
    delete ui;
}

void HuggleQueue::AddItem(WikiPage *page)
{
    HuggleQueueItemLabel *label = new HuggleQueueItemLabel(this);
    label->SetName(page->PageName);
    this->layout->addWidget(label);
    //this->Items.append(label);
}
