#ifndef OAUTHLOGIN_H
#define OAUTHLOGIN_H

#include <QDialog>

namespace Ui {
class OAuthLogin;
}

class OAuthLogin : public QDialog
{
    Q_OBJECT

public:
    explicit OAuthLogin(QWidget *parent = 0);
    ~OAuthLogin();

private:
    Ui::OAuthLogin *ui;
};

#endif // OAUTHLOGIN_H
