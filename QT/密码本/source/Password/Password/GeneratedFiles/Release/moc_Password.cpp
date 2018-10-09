/****************************************************************************
** Meta object code from reading C++ file 'Password.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.7.0)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../Password.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'Password.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.7.0. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
struct qt_meta_stringdata_Password_t {
    QByteArrayData data[9];
    char stringdata0[132];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_Password_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_Password_t qt_meta_stringdata_Password = {
    {
QT_MOC_LITERAL(0, 0, 8), // "Password"
QT_MOC_LITERAL(1, 9, 14), // "ON_btnAddClick"
QT_MOC_LITERAL(2, 24, 0), // ""
QT_MOC_LITERAL(3, 25, 15), // "ON_btnPassClick"
QT_MOC_LITERAL(4, 41, 18), // "ON_comboxAddChange"
QT_MOC_LITERAL(5, 60, 19), // "ON_comboxPassChange"
QT_MOC_LITERAL(6, 80, 16), // "ON_btnEreaseUser"
QT_MOC_LITERAL(7, 97, 15), // "ON_btnEreaseApp"
QT_MOC_LITERAL(8, 113, 18) // "ON_labRemarkChange"

    },
    "Password\0ON_btnAddClick\0\0ON_btnPassClick\0"
    "ON_comboxAddChange\0ON_comboxPassChange\0"
    "ON_btnEreaseUser\0ON_btnEreaseApp\0"
    "ON_labRemarkChange"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_Password[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       7,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    0,   49,    2, 0x08 /* Private */,
       3,    0,   50,    2, 0x08 /* Private */,
       4,    0,   51,    2, 0x08 /* Private */,
       5,    0,   52,    2, 0x08 /* Private */,
       6,    0,   53,    2, 0x08 /* Private */,
       7,    0,   54,    2, 0x08 /* Private */,
       8,    0,   55,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void,

       0        // eod
};

void Password::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Password *_t = static_cast<Password *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->ON_btnAddClick(); break;
        case 1: _t->ON_btnPassClick(); break;
        case 2: _t->ON_comboxAddChange(); break;
        case 3: _t->ON_comboxPassChange(); break;
        case 4: _t->ON_btnEreaseUser(); break;
        case 5: _t->ON_btnEreaseApp(); break;
        case 6: _t->ON_labRemarkChange(); break;
        default: ;
        }
    }
    Q_UNUSED(_a);
}

const QMetaObject Password::staticMetaObject = {
    { &QWidget::staticMetaObject, qt_meta_stringdata_Password.data,
      qt_meta_data_Password,  qt_static_metacall, Q_NULLPTR, Q_NULLPTR}
};


const QMetaObject *Password::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *Password::qt_metacast(const char *_clname)
{
    if (!_clname) return Q_NULLPTR;
    if (!strcmp(_clname, qt_meta_stringdata_Password.stringdata0))
        return static_cast<void*>(const_cast< Password*>(this));
    return QWidget::qt_metacast(_clname);
}

int Password::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QWidget::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 7)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 7;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 7)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 7;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
