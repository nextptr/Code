#pragma once
#include"ClassBase.h"
#include "sales_item.h"

class MyClass1:ClassBase
{
public:
	MyClass1();
	~MyClass1();
	void display()
	{
		cycle();
		sales_item();
	}
private:
	void cycle()
	{
		//while循环
		int sum = 0, val = 1;
		while (val <= 10)
		{
			sum += val;
			cout << " val:" << val << "  sum:" << sum << endl;
			++val;
		}
		cout << "while Sum of 1 to 10 inclusive is:" << sum << endl;


		//for循环
		sum = 0;
		for (int i = 0; i <= 10; i++)
		{
			sum += i;
			cout << " i:" << i << "  sum:" << sum << endl;
		}
		cout << "for Sum of 1 to 10 inclusive is:" << sum << endl;

		//if语句
		val = 0;
		int currval = 0;
		if (cin >> currval)
		{
			int cnt = 1;
			while (cin >> val)
			{
				if (val == currval)
				{
					++cnt;
				}
				else
				{
					cout << currval << " occurs " << cnt << " times" << endl;
					currval = val;
					cnt = 1;
				}
			}
			cout << currval << " occurs " << cnt << " times" << endl;
		}
	}
	void sales_item()
	{
		//输入输出重定义
		Sales_item book;
		cin >> book;
		cout << book << endl;
		//0-200 4 49


		//加法重定义
		Sales_item item1, item2;
		cin >> item1 >> item2;
		cout << item1 + item2 << endl;
		//0-201 3 20
		//0-201 2 25

		//书店程序
		Sales_item total;
		if (cin >> total)
		{
			Sales_item trans;
			while (cin >> trans)
			{
				if (total.isbn() == trans.isbn())
				{
					total += trans;
				}
				else
				{
					cout << total << endl;
					total = trans;
				}
			}
			cout << total << endl;
		}
		else
		{
			cerr << "No data?!" << endl;
		}
	}
};

MyClass1::MyClass1()
{
}

MyClass1::~MyClass1()
{
}