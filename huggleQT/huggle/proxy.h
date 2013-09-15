#ifndef PROXY_H
#define PROXY_H

#include <QDialog>

namespace Ui {
class Proxy;
}

class Proxy : public QDialog
{
    Q_OBJECT
    
public:
    explicit Proxy(QWidget *parent = 0);
    ~Proxy();
    
private:
    Ui::Proxy *ui;
};

#endif // PROXY_H
