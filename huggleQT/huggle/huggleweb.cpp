//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

#include "huggleweb.h"
#include "ui_huggleweb.h"

HuggleWeb::HuggleWeb(QWidget *parent) :
    QFrame(parent),
    ui(new Ui::HuggleWeb)
{
    ui->setupUi(this);
}

HuggleWeb::~HuggleWeb()
{
    delete ui;
}

void HuggleWeb::DisplayPreFormattedPage(WikiPage *page)
{
    ui->webView->load(Core::GetProjectScriptURL() + "index.php?title=" + page->PageName + "&action=render");
}

void HuggleWeb::DisplayPreFormattedPage(QString url)
{
    ui->webView->load(url + "&action=render");
}

void HuggleWeb::DisplayPage(QString url)
{
}

void HuggleWeb::RenderHtml(QString html)
{
}
