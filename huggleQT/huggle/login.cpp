#include "login.h"
#include "ui_login.h"

Login::Login(QWidget *parent) :   QDialog(parent),   ui(new Ui::Login)
{
    ui->setupUi(this);
    this->setWindowTitle("Huggle 3 QT");
}

Login::~Login()
{
    delete ui;
}
