/****************************************************************************
** Meta object code from reading C++ file 'apiquery.h'
**
** Created: Sat Sep 14 20:32:05 2013
**      by: The Qt Meta Object Compiler version 63 (Qt 4.8.1)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "apiquery.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'apiquery.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.1. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_ApiQuery[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
       2,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      10,    9,    9,    9, 0x08,
      21,    9,    9,    9, 0x08,

       0        // eod
};

static const char qt_meta_stringdata_ApiQuery[] = {
    "ApiQuery\0\0ReadData()\0Finished()\0"
};

void ApiQuery::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        ApiQuery *_t = static_cast<ApiQuery *>(_o);
        switch (_id) {
        case 0: _t->ReadData(); break;
        case 1: _t->Finished(); break;
        default: ;
        }
    }
    Q_UNUSED(_a);
}

const QMetaObjectExtraData ApiQuery::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject ApiQuery::staticMetaObject = {
    { &Query::staticMetaObject, qt_meta_stringdata_ApiQuery,
      qt_meta_data_ApiQuery, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &ApiQuery::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *ApiQuery::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *ApiQuery::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_ApiQuery))
        return static_cast<void*>(const_cast< ApiQuery*>(this));
    return Query::qt_metacast(_clname);
}

int ApiQuery::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = Query::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 2)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 2;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
