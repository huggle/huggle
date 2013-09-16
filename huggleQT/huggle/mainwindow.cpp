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
    this->tb = new HuggleTool();
    this->SystemLog = new HuggleLog(this);
    this->Browser = new HuggleWeb(this);
    this->Queue1 = new HuggleQueue(this);
    this->addDockWidget(Qt::LeftDockWidgetArea, this->Queue1);
    this->addDockWidget(Qt::BottomDockWidgetArea, this->SystemLog);
    this->addDockWidget(Qt::TopDockWidgetArea, this->tb);
    this->preferencesForm = new Preferences(this);
    this->aboutForm = new AboutForm(this);
    this->SystemLog->resize(100, 80);
    SystemLog->InsertText(Core::RingLogToText());
    this->CurrentEdit = NULL;
    this->setWindowTitle("Huggle 3 QT-LX");
    ui->verticalLayout->addWidget(this->Browser);
    DisplayWelcomeMessage();
    // initialise queues
    if (Configuration::UsingIRC)
    {
        Core::PrimaryFeedProvider = new HuggleFeedProviderIRC();
        if (!Core::PrimaryFeedProvider->Start())
        {
            Core::Log("ERROR: primary feed provider has failed, fallback to wiki provider");
            delete Core::PrimaryFeedProvider;
            Core::PrimaryFeedProvider = new HuggleFeedProviderWiki();
            Core::PrimaryFeedProvider->Start();
        }
    } else
    {
        Core::PrimaryFeedProvider = new HuggleFeedProviderWiki();
        Core::PrimaryFeedProvider->Start();
    }
    this->timer1 = new QTimer(this);
    connect(this->timer1, SIGNAL(timeout()), this, SLOT(on_Tick()));
    this->timer1->start(200);
}

MainWindow::~MainWindow()
{
    delete this->preferencesForm;
    delete this->aboutForm;
    delete this->Queue1;
    delete this->SystemLog;
    delete this->Browser;
    delete ui;
    delete this->tb;
}

void MainWindow::ProcessEdit(WikiEdit *e)
{
    this->CurrentEdit = e;
    this->Browser->DisplayDiff(e);
    this->Render();
}

void MainWindow::Render()
{
    if (this->CurrentEdit != NULL)
    {
        if (this->CurrentEdit->Page == NULL)
        {
            throw new Exception("Page of CurrentEdit can't be NULL at MainWindow::Render()");
        }
        this->tb->SetTitle(this->CurrentEdit->Page->PageName);
        this->tb->SetUser(this->CurrentEdit->User->Username);
        this->tb->SetInfo("Diff of page: " + this->CurrentEdit->Page->PageName);
        return;
    }
    this->tb->SetTitle(this->Browser->CurrentPageName());
}

void MainWindow::on_actionExit_triggered()
{
    Core::Shutdown();
}

void MainWindow::DisplayWelcomeMessage()
{
    WikiPage *welcome = new WikiPage(Configuration::WelcomeMP);
    this->Browser->DisplayPreFormattedPage(welcome);
    this->Render();
}

void MainWindow::on_actionPreferences_triggered()
{
    preferencesForm->show();
}

void MainWindow::on_actionContents_triggered()
{

}

void MainWindow::on_actionAbout_triggered()
{
    aboutForm->show();
}

void MainWindow::on_MainWindow_destroyed()
{
    Core::Shutdown();
}

void MainWindow::on_Tick()
{
    if (Core::PrimaryFeedProvider->ContainsEdit())
    {
        this->Queue1->AddItem(Core::PrimaryFeedProvider->RetrieveEdit());
    }
}

void MainWindow::on_actionNext_triggered()
{
    this->Queue1->Next();
}

void MainWindow::on_actionNext_2_triggered()
{
    this->Queue1->Next();
}

void MainWindow::on_actionWarn_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}

void MainWindow::on_actionRevert_currently_displayed_edit_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}

void MainWindow::on_actionWarn_the_user_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}

void MainWindow::on_actionRevert_currently_displayed_edit_and_warn_the_user_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}

void MainWindow::on_actionRevert_and_warn_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}

void MainWindow::on_actionRevert_triggered()
{
    if (Configuration::Restricted)
    {
        Core::DeveloperError();
        return;
    }
}
