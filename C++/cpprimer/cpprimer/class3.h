#pragma once
#include"ClassBase.h"
#include <string>
#include <thread>
#include <cctype>
#include <Windows.h>
#include <vector>


class MyClass3:ClassBase
{
public:
	MyClass3();
	~MyClass3();

	bool binarySearch(vector<int> v, int val)//使用迭代器的二分查找
	{
		auto beg = v.begin(), end = v.end();
		auto mid = v.begin() + (end - beg) / 2;

		while (mid != end && *mid != val)
		{
			if (val < *mid)
			{
				end = mid;
			}
			else
			{
				beg = mid + 1;
			}
			mid = beg + (end - beg) / 2;
		}

		return *mid == val;
	}

	void Display1(int *arr, int count)
	{
		for (int i = 0; i < count; i++)
		{
			cout << arr[i] << " ";
		}
		cout << endl;
	}
	void Display2(int arr[3][4])
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				cout << arr[i][j] << " ";
			}
		}
		cout << endl;
	}
	void Display3(int arr[][4], int row)
	{
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				cout << arr[i][j] << " ";
			}
		}
		cout << endl;
	}
	void Display4(int(&arr)[3][4])
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				cout << arr[i][j] << " ";
			}
		}
		cout << endl;
	}
	void Display5(int(*arr)[4], int row)
	{
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				cout << arr[i][j] << " ";
			}
			cout << endl;
		}
	}


	void display()
	{
		//string赋值
		{
			goto fo;
			cout << endl << endl << "string " << endl;
			string s1;
			string s2 = s1;
			string s3 = "hiya";
			string s4("hello");
			string s5(10, 'x');

			cin >> s1;          //" string "忽略前面的空白，以字符后的第一个空白为间隔
			cout << s1 << endl;
			getline(cin, s1);   //读取一行
			cout << "s1 " + s1 << endl;

			string s6;
			string s7;
			while (cin >> s6)
			{
				s7 += s6;
			}
			cout << s7;
		}
	fo:

		//cctype中包含大量字符操作函数
		{
			char ch = 1;

			cout << "isalnum " << isalnum(ch) << endl;
			cout << "isalpha " << isalpha(ch) << endl;
			cout << "iscntrl " << iscntrl(ch) << endl;
			cout << "isdigit " << isdigit(ch) << endl;
			cout << "isgraph " << isgraph(ch) << endl;

			ch = 'b';
			cout << "islower " << islower(ch) << endl;
			cout << "isprint " << isprint(ch) << endl;
			cout << "ispunct " << ispunct(ch) << endl;
			cout << "isspace " << isspace(ch) << endl;
			cout << "isupper " << isupper(ch) << endl;
			cout << "tolower " << (char)tolower(ch) << endl;
			cout << "toupper " << (char)toupper(ch) << endl;
		}


		//范围for,等价于foreach(var c in s1)
		{
			string s1 = "abcdefg";
			for (auto a : s1)
			{
				cout << a << endl;
				Sleep(300);
			}

			// auto&a,则可以在循环内修改a变量
			for (auto &a : s1)
			{
				a = toupper(a);
			}
			cout << s1 << endl;
		}

		//使用下标随机访问
		{
			const string hexdigits = "0123456789ABCDEF";
			cout << "输入一系列使用空格间隔的数字" << endl;
			string result;
			string::size_type n;
			while (cin >> n)
			{
				n = n % 16;
				result += hexdigits[n];
			}
			cout << "16进制数：" << result << endl;
		}

		//vector定义和和初始化
		{
			cout << endl << "vector定义和和初始化" << endl;
			vector<int> v1;
			vector<int> v2(v1);
			vector<int> v3 = v2;
			vector<int> v4(4, 1);
			vector<int> v5(5);
			vector<int> v6{ 1, 2, 3, 4, 5, 6 };
			vector<char>v7 = { 'a', 'b', 'c', 'd' };

			cout << "v1 ";
			for (auto a : v1)
			{
				cout << " " << a;
			}

			cout << endl << "v2 ";
			for (auto a : v2)
			{
				cout << " " << a;
			}

			cout << endl << "v3 ";
			for (auto a : v3)
			{
				cout << " " << a;
			}

			cout << endl << "v4 ";
			for (auto a : v4)
			{
				cout << " " << a;
			}

			cout << endl << "v5 ";
			for (auto a : v5)
			{
				cout << " " << a;
			}

			cout << endl << "v6 ";
			for (auto a : v6)
			{
				cout << " " << a;
			}

			cout << endl << "v7 ";
			for (auto a : v7)
			{
				cout << " " << a;
			}
		}

		//vector操作
		{
			cout << endl << "vector操作" << endl;
			vector<int> v1;
			for (int i = 0; i < 10; i++)
			{
				v1.push_back(i);
			}
			cout << endl;
			for (auto a : v1)
			{
				cout << " " << a;
			}
			cout << endl;

			cout << "" << v1.empty() << endl;
			cout << "" << v1.size() << endl;
		}

		//迭代器
		{
			cout << endl << endl << "迭代器" << endl;
			vector<char> ch;
			for (char c = 'a'; c <= 'z'; c++)
			{
				ch.push_back(c);
			}


			if (ch.begin() != ch.end())
			{
				auto it = ch.begin();
				*it = toupper(*it);
			}
			for (auto it = ch.begin(); it != ch.end(); it++)
			{
				cout << *it << " ";
			}
			cout << endl << ch.cend() - ch.cbegin() << endl;
		}

		//数组
		{
			cout << endl << endl << "数组" << endl;
			const unsigned sz = 3;
			int arr1[sz] = { 0, 1, 2 };
			int arr2[] = { 4, 5, 6 };
			int arr3[10] = { 4, 5, 6 };  //后面7个默认为0
			string arr4[3] = { "hi", "bye" };  //后面一个默认为""

			cout << "arr1  " << arr1 << " count " << sizeof(arr1) / sizeof(arr1[0]) << endl;
			cout << "arr2  " << arr2 << " count " << sizeof(arr2) / sizeof(arr2[0]) << endl;
			cout << "arr3  " << arr3 << " count " << sizeof(arr3) / sizeof(arr3[0]) << endl;
			cout << "arr4  " << arr4 << " count " << sizeof(arr4) / sizeof(arr4[0]) << endl;

			char arr5[] = { 'c', '+', '+' };          //打印时越界了
			char arr6[] = { 'c', '+', '+', '\0' };   //打印正常
			char arr7[] = "c++";                     //自动加\0

			cout << "arr5  " << arr5 << " count " << sizeof(arr5) / sizeof(arr5[0]) << endl;
			cout << "arr6  " << arr6 << " count " << sizeof(arr6) / sizeof(arr6[0]) << endl;
			cout << "arr7  " << arr7 << " count " << sizeof(arr7) / sizeof(arr7[0]) << endl;
		}

		//数组和指针
		{
			cout << endl << endl << "数组和指针" << endl;
			string nums[] = { "one", "two", "three" };
			string *p1 = &nums[0];   //首元素地址
			string *p2 = nums;       //数组名

			//三个值相同
			cout << nums << endl; //nums等于nums[0],指向首元素地址的指针
			cout << p1 << endl;   //
			cout << p2 << endl;   //


			int arr[] = { 0, 1, 2, 3 };
			int *p = arr;
			auto a1(arr);   //a1 是int*
			a1[0] = 1;
			cout << arr << endl;
			cout << p << endl;
			cout << a1 << endl;

			decltype(arr)a2 = { 3, 2, 1, 0 };

			char str[] = "abcdefg";
			char*ch = str;
			while (*ch)
			{
				printf("%c", *ch);
				ch++;
			}

		}

		//C风格字符串
		{
			cout << endl << endl << "C风格字符串" << endl;
			char str1[] = "c style string";
			char str2[] = "another string";
			char str3[1];
			char str4[100];
			//strcpy_s(str3, str1);   //报错，strcpy()，必须确保dst中有足够的空间存储src
			strcpy_s(str4, str1);   //strcat()，必须确保dst的空间足够存储连接后的字符串
			strcat_s(str4, str2);

			cout << strlen(str1) << endl;
			cout << strlen(str2) << endl;
			cout << strcmp(str1, str2) << endl;
			cout << str4 << endl;
		}

		//多维数组
		{
			cout << endl << endl << "多维数组" << endl;

			int arr1[3][4] = {
				{ 0, 1, 2, 3 },
			{ 4, 5, 6, 7 },
			{ 8, 9, 10, 11 }
			};
			int arr2[3][4] = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }; //内存中的实际存储
			int arr3[3][4] = { { 0 }, { 4 }, { 8 } }; //初始化每行首元素
			int arr4[3][4] = { 0, 1, 2, 3 };    //初始化第一行元素

			int(&row)[4] = arr2[1];

			cout << arr1 << endl;
		}

		//多维数组传参
		{
			cout << endl << endl << "多维数组传参" << endl;
			int arr[3][4] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			Display1((int*)arr, 12);
			Display2(arr);
			Display3(arr, 3);
			Display4(arr);
		}

		//多维数组的遍历
		{
			cout << endl << endl << "多维数组传参" << endl;
			char arr[4][4] = { "abc", "def", "ghi", "jkl" };


			//范围for
			for (auto &row : arr)  //row,必须是引用，否则不能对一个char*进行内层的范围for
			{
				for (auto col : row)
				{
					cout << col;
				}
			}
			cout << endl;

			//使用迭代器遍历数组
			for (auto p = begin(arr); p != end(arr); p++)
			{
				for (auto q = begin(*p); q != end(*p); q++)
				{
					cout << *q;
				}
			}
			cout << endl;


		}
	}

private:

};

MyClass3::MyClass3()
{
}

MyClass3::~MyClass3()
{
}
