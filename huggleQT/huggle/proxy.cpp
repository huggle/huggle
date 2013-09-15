#include "proxy.h"
#include "ui_proxy.h"

Proxy::Proxy(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::Proxy)
{
    ui->setupUi(this);
}

Proxy::~Proxy()
{
    delete ui;
}
