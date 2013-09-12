#include "oauthlogin.h"
#include "ui_oauthlogin.h"

OAuthLogin::OAuthLogin(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::OAuthLogin)
{
    ui->setupUi(this);
}

OAuthLogin::~OAuthLogin()
{
    delete ui;
}
