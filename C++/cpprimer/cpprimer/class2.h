#pragma once
#include "ClassBase.h"

int reused = 42; //全局变量

class MyClass2:ClassBase
{
public:
	MyClass2();
	~MyClass2();
	void display()
	{
		//基本数据类型
		{
			cout << endl << endl << "基本数据类型" << endl;
			cout << "bool        " << sizeof(bool) << endl;
			cout << "char        " << sizeof(char) << endl;
			cout << "wchar_t     " << sizeof(wchar_t) << endl;
			cout << "char16_t    " << sizeof(char16_t) << endl;
			cout << "char32_t    " << sizeof(char32_t) << endl;
			cout << "short       " << sizeof(short) << endl;
			cout << "int         " << sizeof(int) << endl;
			cout << "long        " << sizeof(long) << endl;
			cout << "long long   " << sizeof(long long) << endl;
			cout << "float       " << sizeof(float) << endl;
			cout << "double      " << sizeof(double) << endl;
			cout << "long double " << sizeof(long double) << endl;

			//类型转换
			bool b1 = 42;
			cout << "bool b1 = 42 " << b1 << endl;

			int i = b1;
			cout << "int i = b1 " << i << endl;

			i = 3.14;
			cout << "i = 3.14 " << i << endl;

			double pi = i;
			cout << "double pi = i " << pi << endl;

			unsigned char c = -1;
			cout << "unsigned char c = -1 " << c << endl;

			signed char c2 = 256;
			cout << "signed char c2 = 256 " << c2 << endl;


			i = 20;   //10进制
			cout << "i = 20 " << i << endl;
			i = 020;  //8进制
			cout << "i = 020 " << i << endl;
			i = 0x20; //16进制
			cout << "i = 0x20 " << i << endl;
		}

		//初始化和赋值
		{
			cout << endl << endl << "初始化和赋值" << endl;
			//初始化：创建变量时赋予其一个初始值
			//赋  值：把对象的当前值擦除，而以一个新值来代替
			long double ld = 3.1415926;
			//int a{ ld }, b = { ld }; //使用{}来列表初始化，对于内置类型的赋值会进行精度检查，并报错
			int c(ld), d = ld;
		}

		//变量作用域
		{
			cout << endl << endl << "变量作用域" << endl;
			int sum = 0;
			for (int val = 1; val <= 10; val++)
			{
				sum += val;
			}
			cout << "sum:" << sum << endl;
		}
		//嵌套的作用域
		{
			cout << endl << endl << "嵌套的作用域" << endl;
			int unique = 0;
			cout << reused << " " << unique << endl;
			int reused = 0;
			cout << reused << " " << unique << endl;   //在不同作用域定义同名变量之后，使用新的变量
			cout << ::reused << " " << unique << endl; //显式使用全局变量
		}

		//引用
		{
			cout << endl << endl << "引用" << endl;
			int ival = 1024;
			int &refval = ival;
			//int &refval2;   //引用必须初始化 

			int i, &ri = i;
			i = 5; ri = 10;
			cout << i << " " << ri << endl;
		}

		//指针
		{
			cout << endl << endl << "指针" << endl;
			int i = 42;
			int *p;         //int型指针
			int *&r = p;    //r是一个对指针p的引用

			r = &i;         //p指向i
			*r = 0;         //i的值改为0
		}
		//const限定符
		{
			cout << endl << endl << "const限定符" << endl;

			double dval = 3.14;
			const int &ri = dval;
			//类型不匹配，等于引用一个临时变量
			const int tmp = dval;
			const int &tmpri = tmp;

			int i = 1;
			int j = 2;
			int*const p1 = &i;         //p1只能指向i
			const int ci = 42;         //ci的值不能改变
			const int *p2 = &ci;       //*p2的值不能改变，p2的值可以改变

			//p1 = &j;  //报错,p1只能指向i
			*p1 = 3;    //p1只能指向i

			//*p2 = 4;  //报错,*p2的值不能改变
			p2 = &j;    //p2的值可以改变
		}

		//auto类型和decltype类型推断
		{
			cout << endl << endl << "auto类型和decltype类型推断" << endl;

			const auto ci = 0, &ck = ci;
			decltype(ci)x = 0;    //x的类型是const int
			decltype(ck)y = x;    //y的类型是const int&
			//decltype(ck)z;       //z是未初始化的引用，报错
		}
	}
private:

};

MyClass2::MyClass2()
{
}

MyClass2::~MyClass2()
{
}